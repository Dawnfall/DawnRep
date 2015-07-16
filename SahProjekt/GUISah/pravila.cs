using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUISah
{
    public partial class FPravilaIgre : Form
    {
        public FPravilaIgre(string potDoSlik)
        {
            InitializeComponent();
            this.BringToFront();

            this.PBPravila.ImageLocation = potDoSlik;
        }

        private void FPravilaIgre_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[0].Enabled = true;
        }
    }
}
