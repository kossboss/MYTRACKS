namespace MyTracksData
{
    partial class Form1
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
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speedAndTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.distanceAndTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.altitudeAndTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphAllExperimentalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtb = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.graphToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1424, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(42, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.openToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.exitToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.graphToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.speedAndTimeToolStripMenuItem,
            this.distanceAndTimeToolStripMenuItem,
            this.altitudeAndTimeToolStripMenuItem,
            this.graphAllExperimentalToolStripMenuItem});
            this.graphToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(53, 21);
            this.graphToolStripMenuItem.Text = "Graph";
            // 
            // speedAndTimeToolStripMenuItem
            // 
            this.speedAndTimeToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.speedAndTimeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.speedAndTimeToolStripMenuItem.Name = "speedAndTimeToolStripMenuItem";
            this.speedAndTimeToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.speedAndTimeToolStripMenuItem.Text = "Speed and Time";
            this.speedAndTimeToolStripMenuItem.Click += new System.EventHandler(this.speedAndTimeToolStripMenuItem_Click);
            // 
            // distanceAndTimeToolStripMenuItem
            // 
            this.distanceAndTimeToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.distanceAndTimeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.distanceAndTimeToolStripMenuItem.Name = "distanceAndTimeToolStripMenuItem";
            this.distanceAndTimeToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.distanceAndTimeToolStripMenuItem.Text = "Distance and Time";
            this.distanceAndTimeToolStripMenuItem.Click += new System.EventHandler(this.distanceAndTimeToolStripMenuItem_Click);
            // 
            // altitudeAndTimeToolStripMenuItem
            // 
            this.altitudeAndTimeToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.altitudeAndTimeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.altitudeAndTimeToolStripMenuItem.Name = "altitudeAndTimeToolStripMenuItem";
            this.altitudeAndTimeToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.altitudeAndTimeToolStripMenuItem.Text = "Altitude and Time";
            this.altitudeAndTimeToolStripMenuItem.Click += new System.EventHandler(this.altitudeAndTimeToolStripMenuItem_Click);
            // 
            // graphAllExperimentalToolStripMenuItem
            // 
            this.graphAllExperimentalToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.graphAllExperimentalToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.graphAllExperimentalToolStripMenuItem.Name = "graphAllExperimentalToolStripMenuItem";
            this.graphAllExperimentalToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.graphAllExperimentalToolStripMenuItem.Text = "Graph All (Experimental)";
            this.graphAllExperimentalToolStripMenuItem.Click += new System.EventHandler(this.graphAllExperimentalToolStripMenuItem_Click);
            // 
            // rtb
            // 
            this.rtb.Location = new System.Drawing.Point(0, 27);
            this.rtb.Name = "rtb";
            this.rtb.Size = new System.Drawing.Size(1424, 454);
            this.rtb.TabIndex = 1;
            this.rtb.Text = "";
            this.rtb.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1424, 481);
            this.Controls.Add(this.rtb);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "MyTracks - MoreTracks";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rtb;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speedAndTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem distanceAndTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem altitudeAndTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphAllExperimentalToolStripMenuItem;

    }
}

