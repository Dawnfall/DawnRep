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
    public partial class FPomoc : Form
    {
        public FPomoc(string potDoSlik)
        {
            InitializeComponent();
            this.BringToFront();

            this.PBPomoc.ImageLocation=potDoSlik;
        }

        private void FPomoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[0].Enabled = true;
        }
    }
}
