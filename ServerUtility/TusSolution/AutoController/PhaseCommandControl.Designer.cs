namespace AutoController
{
    partial class PhaseCommandControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EditPhaseNameButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PhaseNameLabel = new System.Windows.Forms.Label();
            this.phaseParameterControl1 = new AutoController.PhaseParameterControl();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.phaseParameterControl1);
            this.groupBox1.Location = new System.Drawing.Point(3, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(518, 266);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "パラメータ";
            // 
            // EditPhaseNameButton
            // 
            this.EditPhaseNameButton.Location = new System.Drawing.Point(527, 31);
            this.EditPhaseNameButton.Name = "EditPhaseNameButton";
            this.EditPhaseNameButton.Size = new System.Drawing.Size(150, 23);
            this.EditPhaseNameButton.TabIndex = 2;
            this.EditPhaseNameButton.Text = "Phase名編集";
            this.EditPhaseNameButton.UseVisualStyleBackColor = true;
            this.EditPhaseNameButton.Click += new System.EventHandler(this.EditPhaseNameButton_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Location = new System.Drawing.Point(1, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(673, 2);
            this.label1.TabIndex = 1;
            // 
            // PhaseNameLabel
            // 
            this.PhaseNameLabel.AutoSize = true;
            this.PhaseNameLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PhaseNameLabel.Location = new System.Drawing.Point(16, 10);
            this.PhaseNameLabel.Name = "PhaseNameLabel";
            this.PhaseNameLabel.Size = new System.Drawing.Size(91, 15);
            this.PhaseNameLabel.TabIndex = 3;
            this.PhaseNameLabel.Text = "PhaseName";
            // 
            // phaseParameterControl1
            // 
            this.phaseParameterControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.phaseParameterControl1.Location = new System.Drawing.Point(3, 15);
            this.phaseParameterControl1.Margin = new System.Windows.Forms.Padding(0);
            this.phaseParameterControl1.Name = "phaseParameterControl1";
            this.phaseParameterControl1.Padding = new System.Windows.Forms.Padding(2);
            this.phaseParameterControl1.Size = new System.Drawing.Size(512, 248);
            this.phaseParameterControl1.TabIndex = 0;
            this.phaseParameterControl1.Load += new System.EventHandler(this.phaseParameterControl1_Load);
            // 
            // PhaseCommandControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PhaseNameLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EditPhaseNameButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "PhaseCommandControl";
            this.Size = new System.Drawing.Size(680, 302);
            this.Load += new System.EventHandler(this.PhaseCommandControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private PhaseParameterControl phaseParameterControl1;
        private System.Windows.Forms.Button EditPhaseNameButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label PhaseNameLabel;
    }
}
