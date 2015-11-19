namespace AutoController
{
    partial class PhaseEditWindow
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.PhaseLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.レイアウトテストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stackPhasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddLastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PhaseLayoutPanel
            // 
            this.PhaseLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PhaseLayoutPanel.AutoScroll = true;
            this.PhaseLayoutPanel.Location = new System.Drawing.Point(1, 35);
            this.PhaseLayoutPanel.Name = "PhaseLayoutPanel";
            this.PhaseLayoutPanel.Size = new System.Drawing.Size(743, 356);
            this.PhaseLayoutPanel.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.レイアウトテストToolStripMenuItem,
            this.AddToolStripMenuItem,
            this.DeleteToolStripMenuItem,
            this.AddLastToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(747, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // レイアウトテストToolStripMenuItem
            // 
            this.レイアウトテストToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stackPhasesToolStripMenuItem});
            this.レイアウトテストToolStripMenuItem.Name = "レイアウトテストToolStripMenuItem";
            this.レイアウトテストToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.レイアウトテストToolStripMenuItem.Text = "レイアウトテスト";
            // 
            // stackPhasesToolStripMenuItem
            // 
            this.stackPhasesToolStripMenuItem.Name = "stackPhasesToolStripMenuItem";
            this.stackPhasesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.stackPhasesToolStripMenuItem.Text = "StackPhasesTest";
            this.stackPhasesToolStripMenuItem.Click += new System.EventHandler(this.stackPhasesToolStripMenuItem_Click);
            // 
            // AddToolStripMenuItem
            // 
            this.AddToolStripMenuItem.Name = "AddToolStripMenuItem";
            this.AddToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.AddToolStripMenuItem.Text = "追加";
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.DeleteToolStripMenuItem.Text = "削除";
            // 
            // AddLastToolStripMenuItem
            // 
            this.AddLastToolStripMenuItem.Name = "AddLastToolStripMenuItem";
            this.AddLastToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.AddLastToolStripMenuItem.Text = "末尾に追加";
            this.AddLastToolStripMenuItem.Click += new System.EventHandler(this.AddLastToolStripMenuItem_Click);
            // 
            // PhaseEditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 391);
            this.Controls.Add(this.PhaseLayoutPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PhaseEditWindow";
            this.Text = "DummyPlug";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PhaseEditWindow_FormClosed);
            this.Load += new System.EventHandler(this.PhaseEditWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem レイアウトテストToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stackPhasesToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel PhaseLayoutPanel;
        private System.Windows.Forms.ToolStripMenuItem AddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddLastToolStripMenuItem;
    }
}

