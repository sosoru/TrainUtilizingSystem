namespace AutoController
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.編集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.設定のロードToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.設定のセーブToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartButton = new System.Windows.Forms.Button();
            this.CurrentVehicleParamenterGroupBox = new System.Windows.Forms.GroupBox();
            this.phaseParameterControl1 = new AutoController.PhaseParameterControl();
            this.StopButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.CurrentStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PhaseBatchTabControl = new System.Windows.Forms.TabControl();
            this.VehicleNameComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ServerAddressTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RefreshIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.RefreshStartCheckBox = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.CurrentVehicleParamenterGroupBox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshIntervalNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.編集ToolStripMenuItem,
            this.設定のロードToolStripMenuItem,
            this.設定のセーブToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(517, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 編集ToolStripMenuItem
            // 
            this.編集ToolStripMenuItem.Name = "編集ToolStripMenuItem";
            this.編集ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.編集ToolStripMenuItem.Text = "編集";
            this.編集ToolStripMenuItem.Click += new System.EventHandler(this.編集ToolStripMenuItem_Click);
            // 
            // 設定のロードToolStripMenuItem
            // 
            this.設定のロードToolStripMenuItem.Name = "設定のロードToolStripMenuItem";
            this.設定のロードToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.設定のロードToolStripMenuItem.Text = "設定のロード";
            this.設定のロードToolStripMenuItem.Click += new System.EventHandler(this.設定のロードToolStripMenuItem_Click);
            // 
            // 設定のセーブToolStripMenuItem
            // 
            this.設定のセーブToolStripMenuItem.Name = "設定のセーブToolStripMenuItem";
            this.設定のセーブToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.設定のセーブToolStripMenuItem.Text = "設定のセーブ";
            this.設定のセーブToolStripMenuItem.Click += new System.EventHandler(this.設定のセーブToolStripMenuItem_Click);
            // 
            // StartButton
            // 
            this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StartButton.Location = new System.Drawing.Point(0, 215);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(243, 23);
            this.StartButton.TabIndex = 26;
            this.StartButton.Text = "運転開始";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // CurrentVehicleParamenterGroupBox
            // 
            this.CurrentVehicleParamenterGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentVehicleParamenterGroupBox.Controls.Add(this.phaseParameterControl1);
            this.CurrentVehicleParamenterGroupBox.Location = new System.Drawing.Point(0, 83);
            this.CurrentVehicleParamenterGroupBox.Name = "CurrentVehicleParamenterGroupBox";
            this.CurrentVehicleParamenterGroupBox.Size = new System.Drawing.Size(504, 126);
            this.CurrentVehicleParamenterGroupBox.TabIndex = 27;
            this.CurrentVehicleParamenterGroupBox.TabStop = false;
            this.CurrentVehicleParamenterGroupBox.Text = "現在の値";
            // 
            // phaseParameterControl1
            // 
            this.phaseParameterControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.phaseParameterControl1.Location = new System.Drawing.Point(3, 15);
            this.phaseParameterControl1.Margin = new System.Windows.Forms.Padding(1);
            this.phaseParameterControl1.Name = "phaseParameterControl1";
            this.phaseParameterControl1.Size = new System.Drawing.Size(498, 108);
            this.phaseParameterControl1.TabIndex = 0;
            // 
            // StopButton
            // 
            this.StopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StopButton.Enabled = false;
            this.StopButton.Location = new System.Drawing.Point(249, 215);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(265, 23);
            this.StopButton.TabIndex = 28;
            this.StopButton.Text = "運転停止";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurrentStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 437);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(517, 22);
            this.statusStrip1.TabIndex = 29;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // CurrentStatusLabel
            // 
            this.CurrentStatusLabel.Name = "CurrentStatusLabel";
            this.CurrentStatusLabel.Size = new System.Drawing.Size(65, 17);
            this.CurrentStatusLabel.Text = "現在の状況";
            this.CurrentStatusLabel.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // PhaseBatchTabControl
            // 
            this.PhaseBatchTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PhaseBatchTabControl.Location = new System.Drawing.Point(0, 244);
            this.PhaseBatchTabControl.Name = "PhaseBatchTabControl";
            this.PhaseBatchTabControl.SelectedIndex = 0;
            this.PhaseBatchTabControl.Size = new System.Drawing.Size(517, 190);
            this.PhaseBatchTabControl.TabIndex = 30;
            // 
            // VehicleNameComboBox
            // 
            this.VehicleNameComboBox.FormattingEnabled = true;
            this.VehicleNameComboBox.Location = new System.Drawing.Point(100, 57);
            this.VehicleNameComboBox.Name = "VehicleNameComboBox";
            this.VehicleNameComboBox.Size = new System.Drawing.Size(121, 20);
            this.VehicleNameComboBox.TabIndex = 31;
            this.VehicleNameComboBox.SelectedIndexChanged += new System.EventHandler(this.VehicleNameComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 32;
            this.label1.Text = "列車名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "サーバーアドレス";
            // 
            // ServerAddressTextBox
            // 
            this.ServerAddressTextBox.Location = new System.Drawing.Point(100, 32);
            this.ServerAddressTextBox.Name = "ServerAddressTextBox";
            this.ServerAddressTextBox.Size = new System.Drawing.Size(121, 19);
            this.ServerAddressTextBox.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(249, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 35;
            this.label3.Text = "更新間隔";
            // 
            // RefreshIntervalNumericUpDown
            // 
            this.RefreshIntervalNumericUpDown.Location = new System.Drawing.Point(309, 31);
            this.RefreshIntervalNumericUpDown.Name = "RefreshIntervalNumericUpDown";
            this.RefreshIntervalNumericUpDown.Size = new System.Drawing.Size(120, 19);
            this.RefreshIntervalNumericUpDown.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(435, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 37;
            this.label4.Text = "秒";
            // 
            // RefreshStartCheckBox
            // 
            this.RefreshStartCheckBox.AutoSize = true;
            this.RefreshStartCheckBox.Location = new System.Drawing.Point(251, 61);
            this.RefreshStartCheckBox.Name = "RefreshStartCheckBox";
            this.RefreshStartCheckBox.Size = new System.Drawing.Size(72, 16);
            this.RefreshStartCheckBox.TabIndex = 38;
            this.RefreshStartCheckBox.Text = "更新開始";
            this.RefreshStartCheckBox.UseVisualStyleBackColor = true;
            this.RefreshStartCheckBox.CheckedChanged += new System.EventHandler(this.RefreshStartCheckBox_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 459);
            this.Controls.Add(this.RefreshStartCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.RefreshIntervalNumericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ServerAddressTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VehicleNameComboBox);
            this.Controls.Add(this.PhaseBatchTabControl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.CurrentVehicleParamenterGroupBox);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "DummyPlug";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.CurrentVehicleParamenterGroupBox.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshIntervalNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 編集ToolStripMenuItem;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.GroupBox CurrentVehicleParamenterGroupBox;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel CurrentStatusLabel;
        private PhaseParameterControl phaseParameterControl1;
        private System.Windows.Forms.TabControl PhaseBatchTabControl;
        private System.Windows.Forms.ComboBox VehicleNameComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ServerAddressTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown RefreshIntervalNumericUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox RefreshStartCheckBox;
        private System.Windows.Forms.ToolStripMenuItem 設定のロードToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 設定のセーブToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}