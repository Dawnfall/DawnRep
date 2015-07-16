namespace GUISah
{
    partial class FPomoc
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
            this.PBPomoc = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PBPomoc)).BeginInit();
            this.SuspendLayout();
            // 
            // PBPomoc
            // 
            this.PBPomoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PBPomoc.Location = new System.Drawing.Point(0, 0);
            this.PBPomoc.Name = "PBPomoc";
            this.PBPomoc.Size = new System.Drawing.Size(1264, 578);
            this.PBPomoc.TabIndex = 0;
            this.PBPomoc.TabStop = false;
            // 
            // FPomoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 737);
            this.Controls.Add(this.PBPomoc);
            this.Name = "FPomoc";
            this.Text = "Pomoč";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FPomoc_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.PBPomoc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PBPomoc;

    }
}