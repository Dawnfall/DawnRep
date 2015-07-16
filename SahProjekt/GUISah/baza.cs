using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GUISah
{
    public partial class FBaza : Form
    {
        List<string> bazaPartij = new List<string>();

        public FBaza(string potDoDatoteke)
        {
            InitializeComponent();
            this.PreberiDatoteko(potDoDatoteke);
        }

        private void PreberiDatoteko(string pot)
        {
            StreamReader dat = File.OpenText(pot);
            while (!dat.EndOfStream)
            {
                string vrsta = dat.ReadLine();
                this.bazaPartij.Add(vrsta);

                string[] vrstaRazdeljena = vrsta.Split(',');
                ListViewItem vrstica = new ListViewItem(vrstaRazdeljena[0]);
                vrstica.SubItems.Add(vrstaRazdeljena[1]);
                vrstica.SubItems.Add(vrstaRazdeljena[2]);
                this.LVBaza.Items.Add(vrstica);
            }
        }

        private void BIzberiPartijo_Click(object sender, EventArgs e)
        {
            if (this.LVBaza.SelectedItems.Count!=0)
            {
                int indeks = LVBaza.SelectedItems[0].Index;
                this.Close();
                Application.OpenForms[0].Enabled = true;
                (Application.OpenForms[0] as Sah).PripraviPregledPartije(this.bazaPartij[indeks]);
            }
        }

        private void FBaza_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[0].Enabled = true;
        }
    }
}
