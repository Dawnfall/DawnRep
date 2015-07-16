namespace GUISah
{
    partial class FPromocija
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
            this.TLPPromocija = new System.Windows.Forms.TableLayoutPanel();
            this.PBDama = new System.Windows.Forms.PictureBox();
            this.PBTrdnjava = new System.Windows.Forms.PictureBox();
            this.PBLovec = new System.Windows.Forms.PictureBox();
            this.PBSkakac = new System.Windows.Forms.PictureBox();
            this.TLPPromocija.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBDama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBTrdnjava)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBLovec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBSkakac)).BeginInit();
            this.SuspendLayout();
            // 
            // TLPPromocija
            // 
            this.TLPPromocija.ColumnCount = 2;
            this.TLPPromocija.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLPPromocija.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLPPromocija.Controls.Add(this.PBDama, 0, 0);
            this.TLPPromocija.Controls.Add(this.PBTrdnjava, 1, 0);
            this.TLPPromocija.Controls.Add(this.PBLovec, 0, 1);
            this.TLPPromocija.Controls.Add(this.PBSkakac, 1, 1);
            this.TLPPromocija.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLPPromocija.Location = new System.Drawing.Point(0, 0);
            this.TLPPromocija.Name = "TLPPromocija";
            this.TLPPromocija.RowCount = 2;
            this.TLPPromocija.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLPPromocija.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLPPromocija.Size = new System.Drawing.Size(184, 162);
            this.TLPPromocija.TabIndex = 0;
            // 
            // PBDama
            // 
            this.PBDama.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PBDama.Location = new System.Drawing.Point(3, 3);
            this.PBDama.Name = "PBDama";
            this.PBDama.Size = new System.Drawing.Size(86, 75);
            this.PBDama.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBDama.TabIndex = 0;
            this.PBDama.TabStop = false;
            this.PBDama.Click += new System.EventHandler(this.PBDama_Click);
            // 
            // PBTrdnjava
            // 
            this.PBTrdnjava.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PBTrdnjava.Location = new System.Drawing.Point(95, 3);
            this.PBTrdnjava.Name = "PBTrdnjava";
            this.PBTrdnjava.Size = new System.Drawing.Size(86, 75);
            this.PBTrdnjava.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBTrdnjava.TabIndex = 1;
            this.PBTrdnjava.TabStop = false;
            this.PBTrdnjava.Click += new System.EventHandler(this.PBTrdnjava_Click);
            // 
            // PBLovec
            // 
            this.PBLovec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PBLovec.Location = new System.Drawing.Point(3, 84);
            this.PBLovec.Name = "PBLovec";
            this.PBLovec.Size = new System.Drawing.Size(86, 75);
            this.PBLovec.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBLovec.TabIndex = 2;
            this.PBLovec.TabStop = false;
            this.PBLovec.Click += new System.EventHandler(this.PBLovec_Click);
            // 
            // PBSkakac
            // 
            this.PBSkakac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PBSkakac.Location = new System.Drawing.Point(95, 84);
            this.PBSkakac.Name = "PBSkakac";
            this.PBSkakac.Size = new System.Drawing.Size(86, 75);
            this.PBSkakac.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBSkakac.TabIndex = 3;
            this.PBSkakac.TabStop = false;
            this.PBSkakac.Click += new System.EventHandler(this.PBSkakac_Click);
            // 
            // FPromocija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 162);
            this.ControlBox = false;
            this.Controls.Add(this.TLPPromocija);
            this.Name = "FPromocija";
            this.Text = "Promocija";
            this.TLPPromocija.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PBDama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBTrdnjava)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBLovec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBSkakac)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLPPromocija;
        private System.Windows.Forms.PictureBox PBDama;
        private System.Windows.Forms.PictureBox PBTrdnjava;
        private System.Windows.Forms.PictureBox PBLovec;
        private System.Windows.Forms.PictureBox PBSkakac;
    }
}