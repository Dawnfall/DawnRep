namespace GUISah
{
    partial class FBaza
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
            this.TLPBaza = new System.Windows.Forms.TableLayoutPanel();
            this.LBaza = new System.Windows.Forms.Label();
            this.BIzberiPartijo = new System.Windows.Forms.Button();
            this.LVBaza = new System.Windows.Forms.ListView();
            this.CBeli = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CCrni = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CRez = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TLPBaza.SuspendLayout();
            this.SuspendLayout();
            // 
            // TLPBaza
            // 
            this.TLPBaza.ColumnCount = 1;
            this.TLPBaza.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLPBaza.Controls.Add(this.LBaza, 0, 0);
            this.TLPBaza.Controls.Add(this.BIzberiPartijo, 0, 2);
            this.TLPBaza.Controls.Add(this.LVBaza, 0, 1);
            this.TLPBaza.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLPBaza.Location = new System.Drawing.Point(0, 0);
            this.TLPBaza.Name = "TLPBaza";
            this.TLPBaza.RowCount = 3;
            this.TLPBaza.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.TLPBaza.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.TLPBaza.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.TLPBaza.Size = new System.Drawing.Size(410, 566);
            this.TLPBaza.TabIndex = 0;
            // 
            // LBaza
            // 
            this.LBaza.AutoSize = true;
            this.LBaza.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LBaza.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LBaza.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBaza.ForeColor = System.Drawing.Color.Red;
            this.LBaza.Location = new System.Drawing.Point(3, 0);
            this.LBaza.Name = "LBaza";
            this.LBaza.Size = new System.Drawing.Size(404, 56);
            this.LBaza.TabIndex = 0;
            this.LBaza.Text = "Izberi partijo";
            this.LBaza.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BIzberiPartijo
            // 
            this.BIzberiPartijo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BIzberiPartijo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BIzberiPartijo.ForeColor = System.Drawing.Color.Black;
            this.BIzberiPartijo.Location = new System.Drawing.Point(3, 511);
            this.BIzberiPartijo.Name = "BIzberiPartijo";
            this.BIzberiPartijo.Size = new System.Drawing.Size(404, 52);
            this.BIzberiPartijo.TabIndex = 1;
            this.BIzberiPartijo.Text = "Izberi partijo";
            this.BIzberiPartijo.UseVisualStyleBackColor = true;
            this.BIzberiPartijo.Click += new System.EventHandler(this.BIzberiPartijo_Click);
            // 
            // LVBaza
            // 
            this.LVBaza.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CBeli,
            this.CCrni,
            this.CRez});
            this.LVBaza.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LVBaza.FullRowSelect = true;
            this.LVBaza.GridLines = true;
            this.LVBaza.Location = new System.Drawing.Point(3, 59);
            this.LVBaza.MultiSelect = false;
            this.LVBaza.Name = "LVBaza";
            this.LVBaza.Size = new System.Drawing.Size(404, 446);
            this.LVBaza.TabIndex = 2;
            this.LVBaza.UseCompatibleStateImageBehavior = false;
            this.LVBaza.View = System.Windows.Forms.View.Details;
            // 
            // CBeli
            // 
            this.CBeli.Text = "Beli";
            this.CBeli.Width = 153;
            // 
            // CCrni
            // 
            this.CCrni.Text = "Črmi";
            this.CCrni.Width = 141;
            // 
            // CRez
            // 
            this.CRez.Text = "Rezultat";
            this.CRez.Width = 187;
            // 
            // FBaza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 566);
            this.Controls.Add(this.TLPBaza);
            this.Name = "FBaza";
            this.Text = "Baza partij";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FBaza_FormClosing);
            this.TLPBaza.ResumeLayout(false);
            this.TLPBaza.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLPBaza;
        private System.Windows.Forms.Label LBaza;
        private System.Windows.Forms.Button BIzberiPartijo;
        private System.Windows.Forms.ListView LVBaza;
        private System.Windows.Forms.ColumnHeader CBeli;
        private System.Windows.Forms.ColumnHeader CCrni;
        private System.Windows.Forms.ColumnHeader CRez;
    }
}