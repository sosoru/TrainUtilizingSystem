using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tus.AutoController;
using Tus.AutoController.Deserialized;

namespace AutoController
{
    public partial class PhaseParameterControl : UserControl
    {

        private bool speedScrollBarEntered = false;
        private bool accelScrollBarEntered = false;

        public PhaseParameterControl()
        {
            InitializeComponent();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void ApplyPhaseData(Phase phase, Unten unten)
        {
            var vehi = unten.GetMyVehicle();

            if (!this.speedScrollBarEntered) this.SpeedScrollBar.Value = (int)(phase.Speed * this.SpeedScrollBar.Maximum);
            if (vehi != null) this.CurrentSpeedProgressBar.Value = (int)(vehi.CurrentSpeed * this.CurrentSpeedProgressBar.Maximum);
            if (!this.accelScrollBarEntered) this.AccelationScrollBar.Value = (int)(phase.Accelation * this.AccelationScrollBar.Maximum);
            ChangeSpeedText(phase.Speed);
            if (vehi != null) this.CurrentSpeedValueLabel.Text = Math.Round(vehi.CurrentSpeed * 100, 2).ToString();
            ChangeAccelationText((float)phase.Accelation);

            if (vehi != null) this.DistanceValueLabel.Text = vehi.Distance.ToString();

            this.StayGoSignalCheckBox.Checked = phase.StayGoSignal;
            this.StayDistanceNumericUpDown.Value = phase.StayDistance;

            // initialize triggers for apply states to the controls
            this.BlockTriggerRadioButton.Checked = phase.Trigger is BlockReachedTrigger;
            this.SpeedReachedTriggerRadioButton.Checked = phase.Trigger is SpeedReachedTrigger;
            this.WaitByTimeTriggerRadioButton.Checked = phase.Trigger is WaitByTimeTrigger;

            if (phase.Trigger is BlockReachedTrigger)
            {
                var trigger = phase.Trigger as BlockReachedTrigger;
                this.BlockReachedTriggerVehicleTextBox.Text = trigger.VehicleName;
                this.BlockReachedTriggerBlockTextBox.Text = trigger.BlockName;
                this.BlockReachedTriggerThisVehicleCheckBox.Checked = (trigger.VehicleName == unten.VehicleName);
            }

            if (phase.Trigger is WaitByTimeTrigger)
            {
                var trigger = phase.Trigger as WaitByTimeTrigger;
                this.WaitByTimeTriggerWaitingTextBox.Text = trigger.ScheduledTimeSpan.TotalSeconds.ToString();
            }
        }

        private void ChangeAccelationText(float value)
        {
            this.AccelationValueLabel.Text = Math.Round(value * 100, 2).ToString();
        }

        private void ChangeSpeedText(float value)
        {
            this.SpeedValueLabel.Text = Math.Round(value * 100, 2).ToString();
        }

        public void ExtractPhaseData(ref Phase phase, Unten unten)
        {
            if (!this.speedScrollBarEntered) phase.Speed = (float)this.SpeedScrollBar.Value / this.SpeedScrollBar.Maximum;
            if (!this.accelScrollBarEntered) phase.Accelation = (float)this.AccelationScrollBar.Value / this.AccelationScrollBar.Maximum;
            phase.StayGoSignal = this.StayGoSignalCheckBox.Checked;
            phase.StayDistance = (int)this.StayDistanceNumericUpDown.Value;
            if (this.BlockTriggerRadioButton.Checked)
            {
                if (this.BlockReachedTriggerThisVehicleCheckBox.Checked)
                    this.BlockReachedTriggerVehicleTextBox.Text = unten.VehicleName;

                phase.TriggerInitializer =
                    t =>
                        t.BlockReached(this.BlockReachedTriggerVehicleTextBox.Text.Trim(),
                            this.BlockReachedTriggerBlockTextBox.Text.Trim());

                if (!(phase.Trigger is BlockReachedTrigger))
                    phase.InitializeTrigger();
            }
            else if (this.WaitByTimeTriggerRadioButton.Checked)
            {
                var text = this.WaitByTimeTriggerWaitingTextBox.Text.Trim();
                int value;
                if (!int.TryParse(text, out value)) value = 10;
                phase.TriggerInitializer = t => t.WaitByTime(TimeSpan.FromSeconds(value));

                if (!(phase.Trigger is WaitByTimeTrigger))
                    phase.InitializeTrigger();
            }
            else if (this.SpeedReachedTriggerRadioButton.Checked)
            {
                var speed = phase.Speed; // copy the value for lazy evaluation
                phase.TriggerInitializer = t => t.SpeedReached(speed);

                if (!(phase.Trigger is SpeedReachedTrigger))
                    phase.InitializeTrigger();
            }
            else
            {
                phase.TriggerInitializer = t => new Trigger();
                phase.InitializeTrigger();
            }
        }

        public static PhaseParameterControl CreateFromModels(Phase p, Unten unten)
        {
            var cnt = new PhaseParameterControl();
            cnt.ApplyPhaseData(p, unten);
            return cnt;
        }

        private void BlockReachedTriggerThisVehicleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.BlockReachedTriggerThisVehicleCheckBox.Checked)
            {
                this.BlockReachedTriggerVehicleTextBox.Enabled = false;
            }
            else
            {
                this.BlockReachedTriggerVehicleTextBox.Enabled = true;
            }
        }

        private void SpeedScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            ChangeSpeedText((float)e.NewValue / (float)(sender as ScrollBar).Maximum);
        }

        private void AccelationScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            ChangeAccelationText((float)e.NewValue / (float)(sender as ScrollBar).Maximum);
        }

        private void SpeedScrollBar_MouseEnter(object sender, EventArgs e)
        {

        }
        private void SpeedScrollBar_MouseLeave(object sender, EventArgs e)
        {
            this.speedScrollBarEntered = false;
        }
        private void SpeedScrollBar_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (Control.MouseButtons.HasFlag(MouseButtons.Left))
            {
                this.speedScrollBarEntered = true;
            }
            else
            {
                this.speedScrollBarEntered = false;
            }
        }

        private void AccelationScrollBar_MouseLeave(object sender, EventArgs e)
        {
            this.accelScrollBarEntered = false;
        }

        private void AccelationScrollBar_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (Control.MouseButtons.HasFlag(MouseButtons.Left))
            {
                this.accelScrollBarEntered = true;
            }
            else
            {
                this.accelScrollBarEntered = false;
            }
        }

        private void StayGoSignalCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
