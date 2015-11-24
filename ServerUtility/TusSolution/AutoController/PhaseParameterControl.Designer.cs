namespace AutoController
{
    partial class PhaseParameterControl
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
            this.CurrentSpeedProgressBar = new System.Windows.Forms.ProgressBar();
            this.SpeedScrollBar = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.SpeedValueLabel = new System.Windows.Forms.Label();
            this.CurrentSpeedValueLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.AccelationScrollBar = new System.Windows.Forms.HScrollBar();
            this.AccelationValueLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.WaitByTimeTriggerWaitingTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.BlockReachedTriggerThisVehicleCheckBox = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.BlockReachedTriggerVehicleTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.BlockReachedTriggerBlockTextBox = new System.Windows.Forms.TextBox();
            this.SpeedReachedTriggerRadioButton = new System.Windows.Forms.RadioButton();
            this.WaitByTimeTriggerRadioButton = new System.Windows.Forms.RadioButton();
            this.BlockTriggerRadioButton = new System.Windows.Forms.RadioButton();
            this.StayGoSignalCheckBox = new System.Windows.Forms.CheckBox();
            this.DistanceValueLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.StayDistanceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StayDistanceNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // CurrentSpeedProgressBar
            // 
            this.CurrentSpeedProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentSpeedProgressBar.Location = new System.Drawing.Point(47, 29);
            this.CurrentSpeedProgressBar.Maximum = 1000;
            this.CurrentSpeedProgressBar.Name = "CurrentSpeedProgressBar";
            this.CurrentSpeedProgressBar.Size = new System.Drawing.Size(393, 23);
            this.CurrentSpeedProgressBar.TabIndex = 0;
            // 
            // SpeedScrollBar
            // 
            this.SpeedScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeedScrollBar.Location = new System.Drawing.Point(47, 9);
            this.SpeedScrollBar.Maximum = 1000;
            this.SpeedScrollBar.Minimum = 100;
            this.SpeedScrollBar.Name = "SpeedScrollBar";
            this.SpeedScrollBar.Size = new System.Drawing.Size(393, 17);
            this.SpeedScrollBar.TabIndex = 1;
            this.SpeedScrollBar.Value = 100;
            this.SpeedScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SpeedScrollBar_Scroll);
            this.SpeedScrollBar.MouseCaptureChanged += new System.EventHandler(this.SpeedScrollBar_MouseCaptureChanged);
            this.SpeedScrollBar.MouseEnter += new System.EventHandler(this.SpeedScrollBar_MouseEnter);
            this.SpeedScrollBar.MouseLeave += new System.EventHandler(this.SpeedScrollBar_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "速度";
            // 
            // SpeedValueLabel
            // 
            this.SpeedValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeedValueLabel.AutoSize = true;
            this.SpeedValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SpeedValueLabel.Location = new System.Drawing.Point(457, 14);
            this.SpeedValueLabel.Name = "SpeedValueLabel";
            this.SpeedValueLabel.Size = new System.Drawing.Size(43, 14);
            this.SpeedValueLabel.TabIndex = 4;
            this.SpeedValueLabel.Text = "設定値";
            // 
            // CurrentSpeedValueLabel
            // 
            this.CurrentSpeedValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentSpeedValueLabel.AutoSize = true;
            this.CurrentSpeedValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CurrentSpeedValueLabel.Location = new System.Drawing.Point(457, 38);
            this.CurrentSpeedValueLabel.Name = "CurrentSpeedValueLabel";
            this.CurrentSpeedValueLabel.Size = new System.Drawing.Size(43, 14);
            this.CurrentSpeedValueLabel.TabIndex = 5;
            this.CurrentSpeedValueLabel.Text = "現在値";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "加速度";
            // 
            // AccelationScrollBar
            // 
            this.AccelationScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AccelationScrollBar.Location = new System.Drawing.Point(47, 54);
            this.AccelationScrollBar.Maximum = 1000;
            this.AccelationScrollBar.Name = "AccelationScrollBar";
            this.AccelationScrollBar.Size = new System.Drawing.Size(393, 17);
            this.AccelationScrollBar.TabIndex = 7;
            this.AccelationScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.AccelationScrollBar_Scroll);
            this.AccelationScrollBar.MouseCaptureChanged += new System.EventHandler(this.AccelationScrollBar_MouseCaptureChanged);
            this.AccelationScrollBar.MouseLeave += new System.EventHandler(this.AccelationScrollBar_MouseLeave);
            // 
            // AccelationValueLabel
            // 
            this.AccelationValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AccelationValueLabel.AutoSize = true;
            this.AccelationValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AccelationValueLabel.Location = new System.Drawing.Point(457, 59);
            this.AccelationValueLabel.Name = "AccelationValueLabel";
            this.AccelationValueLabel.Size = new System.Drawing.Size(43, 14);
            this.AccelationValueLabel.TabIndex = 8;
            this.AccelationValueLabel.Text = "設定値";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.WaitByTimeTriggerWaitingTextBox);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.BlockReachedTriggerThisVehicleCheckBox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.BlockReachedTriggerVehicleTextBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.BlockReachedTriggerBlockTextBox);
            this.groupBox1.Controls.Add(this.SpeedReachedTriggerRadioButton);
            this.groupBox1.Controls.Add(this.WaitByTimeTriggerRadioButton);
            this.groupBox1.Controls.Add(this.BlockTriggerRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(5, 124);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 116);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "トリガー";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(194, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(204, 12);
            this.label13.TabIndex = 12;
            this.label13.Text = "現在の速度が設定値に到達するまで待機";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Location = new System.Drawing.Point(12, 88);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(477, 2);
            this.label12.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(302, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 12);
            this.label11.TabIndex = 10;
            this.label11.Text = "秒経過するまで待機";
            // 
            // WaitByTimeTriggerWaitingTextBox
            // 
            this.WaitByTimeTriggerWaitingTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WaitByTimeTriggerWaitingTextBox.Location = new System.Drawing.Point(196, 65);
            this.WaitByTimeTriggerWaitingTextBox.Name = "WaitByTimeTriggerWaitingTextBox";
            this.WaitByTimeTriggerWaitingTextBox.Size = new System.Drawing.Size(100, 19);
            this.WaitByTimeTriggerWaitingTextBox.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Location = new System.Drawing.Point(12, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(477, 2);
            this.label10.TabIndex = 8;
            // 
            // BlockReachedTriggerThisVehicleCheckBox
            // 
            this.BlockReachedTriggerThisVehicleCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BlockReachedTriggerThisVehicleCheckBox.AutoSize = true;
            this.BlockReachedTriggerThisVehicleCheckBox.Location = new System.Drawing.Point(196, 40);
            this.BlockReachedTriggerThisVehicleCheckBox.Name = "BlockReachedTriggerThisVehicleCheckBox";
            this.BlockReachedTriggerThisVehicleCheckBox.Size = new System.Drawing.Size(66, 16);
            this.BlockReachedTriggerThisVehicleCheckBox.TabIndex = 7;
            this.BlockReachedTriggerThisVehicleCheckBox.Text = "この列車";
            this.BlockReachedTriggerThisVehicleCheckBox.UseVisualStyleBackColor = true;
            this.BlockReachedTriggerThisVehicleCheckBox.CheckedChanged += new System.EventHandler(this.BlockReachedTriggerThisVehicleCheckBox_CheckedChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(194, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "列車名";
            // 
            // BlockReachedTriggerVehicleTextBox
            // 
            this.BlockReachedTriggerVehicleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BlockReachedTriggerVehicleTextBox.Location = new System.Drawing.Point(241, 15);
            this.BlockReachedTriggerVehicleTextBox.Name = "BlockReachedTriggerVehicleTextBox";
            this.BlockReachedTriggerVehicleTextBox.Size = new System.Drawing.Size(100, 19);
            this.BlockReachedTriggerVehicleTextBox.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(347, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "閉塞名";
            // 
            // BlockReachedTriggerBlockTextBox
            // 
            this.BlockReachedTriggerBlockTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BlockReachedTriggerBlockTextBox.Location = new System.Drawing.Point(389, 15);
            this.BlockReachedTriggerBlockTextBox.Name = "BlockReachedTriggerBlockTextBox";
            this.BlockReachedTriggerBlockTextBox.Size = new System.Drawing.Size(100, 19);
            this.BlockReachedTriggerBlockTextBox.TabIndex = 3;
            // 
            // SpeedReachedTriggerRadioButton
            // 
            this.SpeedReachedTriggerRadioButton.AutoSize = true;
            this.SpeedReachedTriggerRadioButton.Location = new System.Drawing.Point(12, 93);
            this.SpeedReachedTriggerRadioButton.Name = "SpeedReachedTriggerRadioButton";
            this.SpeedReachedTriggerRadioButton.Size = new System.Drawing.Size(71, 16);
            this.SpeedReachedTriggerRadioButton.TabIndex = 2;
            this.SpeedReachedTriggerRadioButton.TabStop = true;
            this.SpeedReachedTriggerRadioButton.Text = "列車速度";
            this.SpeedReachedTriggerRadioButton.UseVisualStyleBackColor = true;
            this.SpeedReachedTriggerRadioButton.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // WaitByTimeTriggerRadioButton
            // 
            this.WaitByTimeTriggerRadioButton.AutoSize = true;
            this.WaitByTimeTriggerRadioButton.Location = new System.Drawing.Point(12, 66);
            this.WaitByTimeTriggerRadioButton.Name = "WaitByTimeTriggerRadioButton";
            this.WaitByTimeTriggerRadioButton.Size = new System.Drawing.Size(71, 16);
            this.WaitByTimeTriggerRadioButton.TabIndex = 1;
            this.WaitByTimeTriggerRadioButton.TabStop = true;
            this.WaitByTimeTriggerRadioButton.Text = "経過時間";
            this.WaitByTimeTriggerRadioButton.UseVisualStyleBackColor = true;
            // 
            // BlockTriggerRadioButton
            // 
            this.BlockTriggerRadioButton.AutoSize = true;
            this.BlockTriggerRadioButton.ForeColor = System.Drawing.Color.Black;
            this.BlockTriggerRadioButton.Location = new System.Drawing.Point(12, 19);
            this.BlockTriggerRadioButton.Name = "BlockTriggerRadioButton";
            this.BlockTriggerRadioButton.Size = new System.Drawing.Size(71, 16);
            this.BlockTriggerRadioButton.TabIndex = 0;
            this.BlockTriggerRadioButton.TabStop = true;
            this.BlockTriggerRadioButton.Text = "列車位置";
            this.BlockTriggerRadioButton.UseVisualStyleBackColor = true;
            // 
            // StayGoSignalCheckBox
            // 
            this.StayGoSignalCheckBox.AutoSize = true;
            this.StayGoSignalCheckBox.Location = new System.Drawing.Point(17, 92);
            this.StayGoSignalCheckBox.Name = "StayGoSignalCheckBox";
            this.StayGoSignalCheckBox.Size = new System.Drawing.Size(82, 16);
            this.StayGoSignalCheckBox.TabIndex = 10;
            this.StayGoSignalCheckBox.Text = "先行列車が";
            this.StayGoSignalCheckBox.UseVisualStyleBackColor = true;
            this.StayGoSignalCheckBox.CheckedChanged += new System.EventHandler(this.StayGoSignalCheckBox_CheckedChanged);
            // 
            // DistanceValueLabel
            // 
            this.DistanceValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DistanceValueLabel.AutoSize = true;
            this.DistanceValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DistanceValueLabel.Location = new System.Drawing.Point(457, 91);
            this.DistanceValueLabel.Name = "DistanceValueLabel";
            this.DistanceValueLabel.Size = new System.Drawing.Size(43, 14);
            this.DistanceValueLabel.TabIndex = 11;
            this.DistanceValueLabel.Text = "現在値";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(401, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "Distance";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "閉塞進むまで待機";
            // 
            // StayDistanceNumericUpDown
            // 
            this.StayDistanceNumericUpDown.Location = new System.Drawing.Point(96, 89);
            this.StayDistanceNumericUpDown.Name = "StayDistanceNumericUpDown";
            this.StayDistanceNumericUpDown.Size = new System.Drawing.Size(69, 19);
            this.StayDistanceNumericUpDown.TabIndex = 15;
            // 
            // PhaseParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.StayDistanceNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DistanceValueLabel);
            this.Controls.Add(this.StayGoSignalCheckBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AccelationValueLabel);
            this.Controls.Add(this.AccelationScrollBar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CurrentSpeedValueLabel);
            this.Controls.Add(this.SpeedValueLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SpeedScrollBar);
            this.Controls.Add(this.CurrentSpeedProgressBar);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "PhaseParameterControl";
            this.Size = new System.Drawing.Size(509, 243);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StayDistanceNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar CurrentSpeedProgressBar;
        private System.Windows.Forms.HScrollBar SpeedScrollBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label SpeedValueLabel;
        private System.Windows.Forms.Label CurrentSpeedValueLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.HScrollBar AccelationScrollBar;
        private System.Windows.Forms.Label AccelationValueLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton WaitByTimeTriggerRadioButton;
        private System.Windows.Forms.RadioButton BlockTriggerRadioButton;
        private System.Windows.Forms.RadioButton SpeedReachedTriggerRadioButton;
        private System.Windows.Forms.CheckBox StayGoSignalCheckBox;
        private System.Windows.Forms.Label DistanceValueLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox BlockReachedTriggerVehicleTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox BlockReachedTriggerBlockTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox WaitByTimeTriggerWaitingTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox BlockReachedTriggerThisVehicleCheckBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown StayDistanceNumericUpDown;
    }
}
