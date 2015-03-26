namespace Canasta
{
    partial class Canasta
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.jocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alaturareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iesireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optiuniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iesireToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jocToolStripMenuItem,
            this.optiuniToolStripMenuItem,
            this.iesireToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1232, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // jocToolStripMenuItem
            // 
            this.jocToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creareToolStripMenuItem,
            this.alaturareToolStripMenuItem,
            this.iesireToolStripMenuItem,
            this.testToolStripMenuItem});
            this.jocToolStripMenuItem.Name = "jocToolStripMenuItem";
            this.jocToolStripMenuItem.Size = new System.Drawing.Size(42, 24);
            this.jocToolStripMenuItem.Text = "Joc";
            // 
            // creareToolStripMenuItem
            // 
            this.creareToolStripMenuItem.Name = "creareToolStripMenuItem";
            this.creareToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.creareToolStripMenuItem.Text = "Creare joc nou";
            this.creareToolStripMenuItem.Click += new System.EventHandler(this.creareToolStripMenuItem_Click);
            // 
            // alaturareToolStripMenuItem
            // 
            this.alaturareToolStripMenuItem.Name = "alaturareToolStripMenuItem";
            this.alaturareToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.alaturareToolStripMenuItem.Text = "Alaturare joc existent";
            this.alaturareToolStripMenuItem.Click += new System.EventHandler(this.alaturareToolStripMenuItem_Click);
            // 
            // iesireToolStripMenuItem
            // 
            this.iesireToolStripMenuItem.Name = "iesireToolStripMenuItem";
            this.iesireToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.iesireToolStripMenuItem.Text = "Iesire";
            this.iesireToolStripMenuItem.Click += new System.EventHandler(this.iesireToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // optiuniToolStripMenuItem
            // 
            this.optiuniToolStripMenuItem.Name = "optiuniToolStripMenuItem";
            this.optiuniToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.optiuniToolStripMenuItem.Text = "Optiuni";
            this.optiuniToolStripMenuItem.Click += new System.EventHandler(this.optiuniToolStripMenuItem_Click);
            // 
            // iesireToolStripMenuItem1
            // 
            this.iesireToolStripMenuItem1.Name = "iesireToolStripMenuItem1";
            this.iesireToolStripMenuItem1.Size = new System.Drawing.Size(56, 24);
            this.iesireToolStripMenuItem1.Text = "Iesire";
            this.iesireToolStripMenuItem1.Click += new System.EventHandler(this.iesireToolStripMenuItem1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 630);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1232, 25);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(151, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // Canasta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 655);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1250, 700);
            this.Name = "Canasta";
            this.Text = "Canasta";
            this.SizeChanged += new System.EventHandler(this.onResizeForm);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem jocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alaturareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iesireToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optiuniToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iesireToolStripMenuItem1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
    }
}

