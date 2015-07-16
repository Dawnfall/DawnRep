using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sah
{
    public class Partija
    {
        private string beliIme; // ime belega igralca
        private string crniIme; // ime crnega igralca
        private string rezultat;  // končni rezultat igre

        //slovar vseh pozicij in njihovih ponovitev
        public Dictionary<string, int> Pozicije = new Dictionary<string, int>();

        //sezanm vseh pozicij po vrstenm redu
        public List<string> PozicijeSez = new List<string>();

        //seznam potez
        public List<string> Poteze = new List<string>();  // programu prijazen in za zapis na datoteko
        public List<string> PotezeZaIzpis = new List<string>();  // za izpis na zaslon

        public List<Sahovnica> vsePozicije = new List<Sahovnica>();
        public List<string[,]> SezPozicijZaPregledPartij;

        public int steviloPonovitev;

        /// <summary>
        /// osnovni konstruktor,ki ustvari prazen objekt Partija z imeni obeh igralcev
        /// </summary>
        /// <param name="imeB">string:ime belega igralca</param>
        /// <param name="imeC">string: ime črnega igralca</param>
        public Partija(string imeB, string imeC)
        {
            this.SezPozicijZaPregledPartij = new List<string[,]>();
            this.vsePozicije.Add(new Sahovnica());

            if (imeB == "")
                imeB = "Anonimnež";
            if (imeC == "")
                imeC = "Anonimnež";

            this.BeliIme = imeB;
            this.CrniIme = imeC;

            this.steviloPonovitev = 0;
        }

        /// <summary>
        /// konstruktor, ki ustvari poln objekt Partija iz zapisa partije 
        /// </summary>
        /// <param name="zapisPartije">string: zapis partije, v formatu kot ga zapise metoda ZapisiNaDatoteko()</param>
        public Partija(string zapisPartije)
        {
            this.SezPozicijZaPregledPartij = new List<string[,]>();
            string[] zapis = zapisPartije.Split(',');
            this.BeliIme = zapis[0];
            this.CrniIme = zapis[1];
            this.Rezultat = zapis[2];

            for (int i = 3; i < zapis.Length; i++)
            {
                //rokade
                if (zapis[i] == "0-0")
                    this.DodajPotezoRokada(false);
                else if (zapis[i] == "0-0-0")
                    this.DodajPotezoRokada(true);

                //promocija
                else if (zapis[i].Length != 5)
                {
                    this.dodajPotezoPromocija(int.Parse("" + zapis[i][1]), int.Parse("" + zapis[i][2]), int.Parse("" + zapis[i][3]), int.Parse("" + zapis[i][4]), "" + zapis[i][5]);
                }

                //navaden premik
                else
                {
                    this.DodajPotezo("" + zapis[i][0], int.Parse("" + zapis[i][1]), int.Parse("" + zapis[i][2]), int.Parse("" + zapis[i][3]), int.Parse("" + zapis[i][4]));
                }
                
            }
        }

        /// <summary>
        /// propertiji osnovnih podatkov
        /// </summary>
        public string BeliIme
        {
            get { return this.beliIme; }
            set
            {
                beliIme = value;
            }
        }
        public string CrniIme
        {
            get { return this.crniIme; }
            set
            {
                crniIme = value;
            }
        }
        public string Rezultat
        {
            get { return this.rezultat; }
            set
            {
                this.rezultat = value;
            }
        }

        /// <summary>
        /// metoda s katero dodamo narejeno potezo
        /// </summary>
        /// <param name="barva">char: barva igralca,ki je izvršil potezo</param>
        /// <param name="figura">char: vrsta figure</param>
        /// <param name="starX">int: iz katere vrste</param>
        /// <param name="starY">int: iz katerega stolpca</param>
        /// <param name="novX">int: v katero vrsto</param>
        /// <param name="novY">int: v kater stolpec</param>
        public void DodajPotezo(string figura, int starX, int starY, int novX, int novY)
        {
            string pot = figura + starX + starY + novX + novY;
            Poteze.Add(pot);

            if (figura == "P")
                figura = "";

            string notacija = "abcdefgh";
            PotezeZaIzpis.Add(figura + notacija[starY] + (starX + 1) + " - " + notacija[novY] + (novX + 1));
        }

        /// <summary>
        /// metoda,s katero dodamo potezo male ali velike rokade
        /// </summary>
        /// <param name="kateraRokada">bool: false - mala rokada, true - velika rokada</param>
        public void DodajPotezoRokada(bool kateraRokada)
        {
            if (kateraRokada == false)
            {
                Poteze.Add("0-0");
                PotezeZaIzpis.Add("0 - 0");
            }
            else
            {
                Poteze.Add("0-0-0");
                PotezeZaIzpis.Add("0 - 0 - 0");
            }
        }

        /// <summary>
        /// metoda,ki doda promocijo kmeta
        /// </summary>
        /// <param name="starX">int: iz katere vrste</param>
        /// <param name="starY">int: iz katerega stolpca</param>
        /// <param name="novX">int: v katero vrsto</param>
        /// <param name="novY">int: v kater stolpec</param>
        /// <param name="vKaj">string: figura v katero promoviramo</param>
        public void dodajPotezoPromocija(int starX, int starY, int novX, int novY, string vKaj)
        {
            string pot = "P" + starX + starY + novX + novY + vKaj;
            Poteze.Add(pot);

            string notacija = "abcdefgh";
            PotezeZaIzpis.Add(""+ notacija[starY] + (starX + 1) + " - " + notacija[novY] + (novX + 1) + " =" + vKaj);
        }

        /// <summary>
        /// metoda,ki zapise poteze in jih pripravi za izpis
        /// </summary>
        /// <returns>string: zapis partije po potezah</returns>
        public string zapisPartije()
        {
            string zapis = this.BeliIme + "," + this.CrniIme + "," + this.Rezultat;

            // pregledamo seznam potez in vsako preoblikujemo v ustrezen napis
            foreach (string poteza in this.Poteze)
            {
                {
                    zapis += "," + poteza;
                }
            }
            return zapis;
        }

        /// <summary>
        /// metoda,ki doda pozicijo v slovar
        /// </summary>
        /// <param name="poz">string[,]: dvodimenzionalna tabela,ki predstavlja pozicijo</param>
        public void dodajPozicijo(string poz)
        {
            //dodamo v seznam
            this.PozicijeSez.Add(poz);

            //če pozicija obstaja v slovarju poveča njeno vrednost za 1
            if (Pozicije.ContainsKey(poz))
                Pozicije[poz] += 1;
            //v nasprotnem primeru jo doda z vrednostjo 1
            else
                Pozicije.Add(poz, 1);

            if (Pozicije[poz] > this.steviloPonovitev)
                this.steviloPonovitev = Pozicije[poz];
        }

        /// <summary>
        /// metoda,ki zadnjo potezo in pozicijo izbrise
        /// </summary>
        public void RazveljaviZadnjoPotezo()
        {
            //odstrani zadnjo potezo
            this.Poteze.RemoveAt(this.Poteze.Count() - 1);
            this.PotezeZaIzpis.RemoveAt(this.PotezeZaIzpis.Count() - 1);

            string zadnjaPoz = this.PozicijeSez[this.PozicijeSez.Count() - 1];

            //odstrani zadnjo pozicijo
            this.Pozicije[zadnjaPoz]--;
            this.PozicijeSez.RemoveAt(this.PozicijeSez.Count()-1);
        }

        /// <summary>
        /// metoda,ki partijo zapiše na datoteko
        /// </summary>
        /// <param name="kam">string: pot do datoteke</param>
        public void ZapisiNaDatoteko(string kam)
        {
            string zapis = this.zapisPartije();

            StreamWriter dodajanje = File.AppendText(kam);
            dodajanje.WriteLine(zapis);

            dodajanje.Close();
        }

        public int KateraPoteza()
        {
            int skupaj=this.Poteze.Count();
            if (skupaj == 0)
                return 0;
            else if (skupaj % 2 == 0)
                return skupaj / 2;
            else
                return skupaj / 2 + 1;
        }

        public void DodajPozicijoZaOgled(string[,] poz)
        {
            this.SezPozicijZaPregledPartij.Add(poz);
        }

        public void DodajSahovnico(Sahovnica sah)
        {
            this.vsePozicije.Add(sah);
        }
    }
}
