using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tus.AutoController;
using Tus.AutoController.Deserialized;

namespace AutoController
{
    public partial class PhaseEditWindow : Form
    {
        public Unten Unten { get; set; }
        public PhaseEditWindow()
        {
            InitializeComponent();
        }

        private void stackPhasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var prov = new VehiclesProvider()
            {
                VehiclesStatus = () => new[]
                                       {
                                          new DeserializedVehicle()
                                          {
                                              Name = "pero",
                                              Speed = 0.5f,
                                              CurrentSpeed = 0.1f,
                                              Accelation = 0.5f,
                                              Distance = 2,
                                              CurrentBlock = new DeserializedBlock() {Name = "AT1"},
                                          },
                                      },
            };
            var unte = new Unten()
            {
                PhaseBatch = new LocalOperationPhaseBatchFactory().Create(),
                VehicleName = "pero",
                VehiclesProvider = prov,
            };

            var controls = unte.PhaseBatch.Phases.Select(p => PhaseCommandControl.CreateFromPhase(p, unte)).ToList();
            this.PhaseLayoutPanel.Controls.AddRange(controls.ToArray());
            controls.ForEach(c =>
                             {
                                 c.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                                 this.PhaseLayoutPanel.SetFlowBreak(c, true);
                             });
        }

        private void PhaseEditWindow_Load(object sender, EventArgs e)
        {
            refreshControls();
        }

        private void refreshControls()
        {
            // add phases to AddToolStrips
            this.AddToolStripMenuItem.DropDownItems.Clear();
            this.AddToolStripMenuItem.DropDownItems.AddRange(
                this.Unten.PhaseBatch.Phases.Select(p => new ToolStripLabel(p.Name, null, false, (s, e2) => AddPhase(p))).ToArray());

            // add phases to DelToolStrips
            this.DeleteToolStripMenuItem.DropDownItems.Clear();
            this.DeleteToolStripMenuItem.DropDownItems.AddRange(
                this.Unten.PhaseBatch.Phases.Select(p => new ToolStripLabel(p.Name, null, false, (s, e2) => DeletePhase(p))).ToArray());
            var controls =
                this.Unten.PhaseBatch.Phases.Select(p => PhaseCommandControl.CreateFromPhase(p, this.Unten)).ToList();
            this.PhaseLayoutPanel.Controls.Clear();
            this.PhaseLayoutPanel.Controls.AddRange(controls.ToArray());
            controls.ForEach(c =>
                             {
                                 c.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                                 this.PhaseLayoutPanel.SetFlowBreak(c, true);
                             });
        }

        private void WritebackPhases()
        {
            for (int i = 0; i < this.Unten.PhaseBatch.Phases.Count; i++)
            {
                var cnt = this.PhaseLayoutPanel.Controls[i] as PhaseCommandControl;
                if (cnt == null) continue;

                var phase = this.Unten.PhaseBatch.Phases[i];
                cnt.ExtractPhaseData(ref phase, this.Unten);
            }
        }

        private void DeletePhase(Phase p)
        {
            WritebackPhases();
            this.Unten.PhaseBatch.Phases.Remove(p);
            refreshControls();
        }

        private void AddPhase(Phase p)
        {
            WritebackPhases();
            var index = this.Unten.PhaseBatch.Phases.IndexOf(p);
            if (index < 0) return;
            this.Unten.PhaseBatch.Phases.Insert(index, p);
            refreshControls();
        }

        private void AddLastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WritebackPhases();
            this.Unten.PhaseBatch.Phases.Add(new Phase()
            {
                Name = "NewPhase",
                Speed = 0.5f,
                Accelation = 0.5f,
                TriggerInitializer = t => t.SpeedReached(0.5f),
            });
            refreshControls();
        }

        private void PhaseEditWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            WritebackPhases();
        }
    }
}
