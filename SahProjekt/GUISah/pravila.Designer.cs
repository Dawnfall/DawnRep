namespace GUISah
{
    partial class FPravilaIgre
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
            this.PBPravila = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PBPravila)).BeginInit();
            this.SuspendLayout();
            // 
            // PBPravila
            // 
            this.PBPravila.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PBPravila.Location = new System.Drawing.Point(0, 0);
            this.PBPravila.Name = "PBPravila";
            this.PBPravila.Size = new System.Drawing.Size(626, 729);
            this.PBPravila.TabIndex = 0;
            this.PBPravila.TabStop = false;
            // 
            // FPravilaIgre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 729);
            this.Controls.Add(this.PBPravila);
            this.Name = "FPravilaIgre";
            this.Text = "Pravila igre";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FPravilaIgre_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.PBPravila)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PBPravila;
    }
}