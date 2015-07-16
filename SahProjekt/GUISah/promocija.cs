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
    public partial class FPromocija : Form
    {
        public PictureBox klikPolje;

        public FPromocija(string dama,string trdnjava,string lovec,string skakac,PictureBox polje)
        {
            InitializeComponent();

            this.klikPolje=polje;
            this.nastaviSlike(dama, trdnjava, lovec, skakac);
        }

        public void nastaviSlike(string dama, string trdnjava, string lovec, string skakac)
        {
            this.PBDama.ImageLocation = dama;
            this.PBLovec.ImageLocation = lovec;
            this.PBTrdnjava.ImageLocation = trdnjava;
            this.PBSkakac.ImageLocation = skakac;
        }

        private void PBDama_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Enabled = true;
            (Application.OpenForms[0] as Sah).Promocija(this.klikPolje, "D");
            this.Close();

        }

        private void PBTrdnjava_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Enabled = true;
            (Application.OpenForms[0] as Sah).Promocija(this.klikPolje, "T");
            this.Close();
        }

        private void PBLovec_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Enabled = true;
            (Application.OpenForms[0] as Sah).Promocija(this.klikPolje, "L");
            this.Close();
        }

        private void PBSkakac_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Enabled = true;
            (Application.OpenForms[0] as Sah).Promocija(this.klikPolje, "S");
            this.Close();
        }


    }
}
