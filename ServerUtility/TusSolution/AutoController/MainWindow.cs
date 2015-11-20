using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoController.Properties;
using DialogConsole.Features;
using Tus.AutoController;
using Tus.AutoController.Deserialized;

namespace AutoController
{
    public partial class MainWindow : Form
    {
        private Unten unten;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.unten = new Unten();
            this.ServerAddressTextBox.Text = Settings.Default.ServerAddress;
            this.RefreshIntervalNumericUpDown.Value = Settings.Default.RefreshInterval;

            this.unten.SetParameterDefault();
        }
        private void RefreshStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Enabled)
            {
                this.ServerAddressTextBox.Enabled = false;
                this.RefreshIntervalNumericUpDown.Enabled = false;

                timer1.Interval = (int)this.RefreshIntervalNumericUpDown.Value * 1000;
                timer1.Start();
            }
            else
            {
                this.ServerAddressTextBox.Enabled = true;
                this.RefreshIntervalNumericUpDown.Enabled = true;

                timer1.Stop();
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.ServerAddress = this.ServerAddressTextBox.Text;
            Settings.Default.RefreshInterval = (int)this.RefreshIntervalNumericUpDown.Value;
            Settings.Default.Save();
            refreshControls();
        }

        // get the current states from vehicle server and refresh associated controls.   
        private void timer1_Tick(object sender, EventArgs e)
        {
            byte[] receiveddata;
            var cnt = new DataContractJsonSerializer(typeof(DeserializedVehicle[]));

            try
            {
                var location = new Uri(this.ServerAddressTextBox.Text);
                var client = new WebClient();
                receiveddata = client.DownloadData(location);
            }
            catch (WebException)
            {
                StatusMessage("Web error");
                return;
            }

            if (receiveddata == null || receiveddata.Length == 0) return; // if empty, return

            using (var ms = new MemoryStream(receiveddata, false))
            {
                var obj = cnt.ReadObject(ms) as DeserializedVehicle[];

                this.unten.VehiclesProvider.VehiclesStatus = () => obj;

                if (this.unten.IsWatching)
                {
                    this.unten.ContinueWatching();
                    StatusMessage(this.unten.PhaseBatch.CurrentPhase.Name);
                }
                else
                {
                    StatusMessage("Controller Mode");
                }

                // sync vehicle names 
                var vehinamecol = this.VehicleNameComboBox.Items;
                foreach (var vehi in obj) // add vehicle name which is not contained in combo box.
                {
                    if (!vehinamecol.Contains(vehi.Name))
                        vehinamecol.Add(vehi.Name);
                }
                foreach (var vehiname in vehinamecol) // delete vehicle name which is not contained in received data
                {
                    if (obj.All(v => v.Name != (string)vehiname))
                        vehinamecol.Remove(vehiname);
                }

                // find the vehicle spicified by VehicleNameColumn
                if (string.IsNullOrWhiteSpace(this.VehicleNameComboBox.Text)) return;  
                var associatedVehi = unten.VehiclesProvider.FindByName(this.VehicleNameComboBox.Text);
                if (associatedVehi == null) return; // if the vehicle is not found in received data, then exit this function.

                // show current position of the vehicle
                this.CurrentBlockStatusLabel1.Text = associatedVehi.CurrentBlockObject.Name;

                // collect current state on control
                Phase vehistateFromControl;
                if (this.unten.IsWatching)
                {
                    SyncronizePhasePages();
                    vehistateFromControl = this.unten.PhaseBatch.CurrentPhase;
                }
                else
                {
                    vehistateFromControl = this.unten.CreateNewPhase();
                    this.phaseParameterControl1.ExtractPhaseData(ref vehistateFromControl, this.unten);
                }

                // if current state on control is not matched with received state, then throw received state away, and send the state on control.
                // matching is evaluated by speed, accelation. 
                if (Math.Abs(vehistateFromControl.Speed - associatedVehi.Speed) > 0.01f
                    || Math.Abs(vehistateFromControl.Accelation - associatedVehi.Accelation) > 0.01f)
                {
                    // send the state on contorl
                    var sendvehi = new VehicleInfoReceived()
                    {
                        Name = this.unten.VehicleName,
                        Speed = vehistateFromControl.Speed.ToString(),
                        Accelation = vehistateFromControl.Accelation.ToString(),
                    };
                    sendVehicle(sendvehi);
                }
                else // matched these states, then apply received state
                {
                    // apply current state 
                    var currentphase = new Phase() // may be 'dummy' phase. almost parameters are refreshed by Unten.VehicleProvider. the phase might not be used.
                    {
                        Speed = associatedVehi.Speed,
                        Accelation = associatedVehi.Accelation,
                    };
                    this.phaseParameterControl1.ApplyPhaseData(currentphase, this.unten);
                }
            }
        }

        private void SyncronizePhasePages()
        {
            for (int i = 0; i < this.unten.PhaseBatch.Phases.Count; i++)
            {
                var phasecontrol = ((TabPage)this.PhaseBatchTabControl.TabPages[i]).Controls[0] as PhaseParameterControl;
                var phase = this.unten.PhaseBatch.Phases[i];
                phasecontrol.ExtractPhaseData(ref phase, this.unten);
            }
        }

        private void StatusMessage(string message)
        {
            this.CurrentStatusLabel.Text = message;
        }

        private void sendVehicle(VehicleInfoReceived vehi)
        {
            var location = new Uri(this.ServerAddressTextBox.Text);
            var client = new WebClient();
            var cnt = new DataContractJsonSerializer(typeof(VehicleInfoReceived[]));
            using (var ms = new MemoryStream())
            {
                cnt.WriteObject(ms, new[] { vehi });
                ms.Seek(0, SeekOrigin.Begin);
                client.UploadString(location, "POST", UTF8Encoding.UTF8.GetString(ms.ToArray()));
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            // Phaseが未設定 == タブコントロールにページがないならreturn
            if (this.PhaseBatchTabControl.TabPages.Count == 0) return;

            if (this.unten.IsWatching) this.unten.AbortWatching();
            this.unten.InitializeWatching();

            // 選択されているPhaseから運転開始
            var selectedindex = this.PhaseBatchTabControl.SelectedIndex;
            if (selectedindex > 0) this.unten.SkipPhases(selectedindex);

            this.StartButton.Enabled = false;
            this.StopButton.Enabled = true;
            this.VehicleNameComboBox.Enabled = false;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (this.unten.IsWatching)
            {
                this.unten.AbortWatching();
            }
            this.StartButton.Enabled = true;
            this.StopButton.Enabled = false;
            this.VehicleNameComboBox.Enabled = true;
        }

        private void 編集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var phaseeditwnd = new PhaseEditWindow()
            {
                Unten = this.unten,
            };
            phaseeditwnd.ShowDialog(this);

            refreshControls();
        }

        private void 設定のセーブToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != this.saveFileDialog1.ShowDialog(this)) return;

            SyncronizePhasePages();

            var ser = unten.Serializer;
            var info = new FileInfo(this.saveFileDialog1.FileName);
            if (info.Exists) info.Delete();
            using (var fs = new FileStream(this.saveFileDialog1.FileName, FileMode.OpenOrCreate))
            {
                ser.WriteObject(fs, this.unten);
            }
        }

        private void 設定のロードToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != this.openFileDialog1.ShowDialog(this)) return;

            var ser = unten.Serializer;
            this.RefreshStartCheckBox.Checked = false;
            using (var fs = new FileStream(this.openFileDialog1.FileName, FileMode.Open))
            {
                var unten_ = ser.ReadObject(fs) as Unten;
                if (unten_ == null) return;

                this.unten.AbortWatching();
                this.unten.PhaseBatch = unten_.PhaseBatch;
                this.unten.VehicleName = unten_.VehicleName;
                this.unten.InitializeWatching();
                this.unten.AbortWatching();

                refreshControls();
            }
        }

        private void refreshControls()
        {
            var index = 0;
            // get selected tab page
            if (this.PhaseBatchTabControl.TabPages.Count == 0) index = 0; // if empty tabpages, select first
            else
            {
                var pagename = this.PhaseBatchTabControl.SelectedTab.Text;
                index =
                    this.unten.PhaseBatch.Phases.IndexOf(
                        this.unten.PhaseBatch.Phases.FirstOrDefault(p => p.Name == pagename));
                if (index < 0) index = 0; // if not phases contain before page, index selects first
            }

            // clear phase pages
            this.PhaseBatchTabControl.Controls.Clear();

            // if phasebatch is empty, add nothing and exit
            if (this.unten.PhaseBatch.Phases.Count == 0) return;

            // add phase pages
            foreach (var p in this.unten.PhaseBatch.Phases)
            {
                var cnt = new PhaseParameterControl();
                var p1 = p;
                cnt.ApplyPhaseData(p, this.unten);
                cnt.ExtractPhaseData(ref p1, this.unten);

                cnt.Dock = DockStyle.Fill;

                var page = new TabPage(p.Name);
                page.Controls.Add(cnt);
                this.PhaseBatchTabControl.TabPages.Add(page);
            }

            // select tab page before selected
            this.PhaseBatchTabControl.SelectTab(index);

            // refresh controlling vehicle name
            this.VehicleNameComboBox.Text = this.unten.VehicleName;
        }

        private void VehicleNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.unten.VehicleName = (string)this.VehicleNameComboBox.SelectedItem;
        }

        private void toolStripStatusLabel1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
