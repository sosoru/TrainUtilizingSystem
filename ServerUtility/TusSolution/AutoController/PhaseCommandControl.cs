using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Tus.AutoController;
using Tus.AutoController.Deserialized;

namespace AutoController
{
    public partial class PhaseCommandControl : UserControl
    {
        public PhaseCommandControl()
        {
            InitializeComponent();
        }

        public void ApplyPhaseData(Phase phase, Unten unten)
        {
            this.phaseParameterControl1.ApplyPhaseData(phase, unten);

            this.PhaseNameLabel.Text = phase.Name;
        }

        public void ExtractPhaseData(ref Phase phase, Unten unten)
        {
            this.phaseParameterControl1.ExtractPhaseData(ref phase, unten);

            phase.Name = this.PhaseNameLabel.Text;
        }

        public static PhaseCommandControl CreateFromPhase(Phase phase, Unten unten)
        {
            var cnt = new PhaseCommandControl();
            cnt.ApplyPhaseData(phase, unten);
            return cnt;
        }

        private void PhaseCommandControl_Load(object sender, EventArgs e)
        {

        }

        private void phaseParameterControl1_Load(object sender, EventArgs e)
        {

        }

        private void EditPhaseNameButton_Click(object sender, EventArgs e)
        {
            var input = Interaction.InputBox("Phase名を入力してください", DefaultResponse: this.PhaseNameLabel.Text);

            // if input is empty, the user may cancelled the input box. 
            if (input != "") this.PhaseNameLabel.Text = input;
        }

    }
}
