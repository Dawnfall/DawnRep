using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sah;

namespace GUISah
{
    public partial class Sah : Form
    {
        string mod;
        string doPrograma; //pot do programa

        bool prikazPomoci; // ali polja možnih premikov obarvamo

        bool ponujenRemi;
        bool prekiniIgro;
        bool predajIgro;

        public bool jeIgra; // če poteka igra
        public bool jeOznaceno;  // če je označena figura s katero lahko naredimo premik
        public bool izbranKralj;  // če je izbran kralj
        public bool izbranKmetNaSedmi; //če je kmet pred promocijo

        public bool oznacenEP;   //če je možen EP
        public PictureBox poljeEP; // polje za jemanje EP

        public PictureBox[] TSahovnica = new PictureBox[64]; //tabela vseh GUI polj

        public string[] slikeFigur = new string[12]; //tabela vseh slik K,D,T,L,S,P(0-6 bele figure,7-12 crne figure)

        public Partija novaPartija; //partija,ki se trnutno izvaja
        public Sahovnica trenutnaPoz;//pozicija,ki jo prikazujemo

        public Dictionary<Figura, List<int[]>> moznePoteze = new Dictionary<Figura, List<int[]>>();

        public List<PictureBox> obarvanaPolja = new List<PictureBox>(); //obarvana polja,ko pritisnemo na figuro z možnimi premiki
        public PictureBox izbranoPolje = new PictureBox();  //polje,ki je trenutno izbrano

        public int indeksPozicije;


        public Sah()
        {
            InitializeComponent();

            string lokacija = System.Reflection.Assembly.GetEntryAssembly().Location;
            string mapa = System.IO.Path.GetDirectoryName(lokacija);

            this.prikazPomoci = true;

            int dol = mapa.Length;
            doPrograma = mapa.Substring(0, dol - 17);

            //potrebne slike
            this.slikeFigur[0] = doPrograma + @"\figure\kraljBeli.ico"; this.slikeFigur[6] = doPrograma + @"\figure\kraljCrni.ico";
            this.slikeFigur[1] = doPrograma + @"\figure\damaBeli.ico"; this.slikeFigur[7] = doPrograma + @"\figure\damaCrni.ico";
            this.slikeFigur[2] = doPrograma + @"\figure\trdnjavaBeli.ico"; this.slikeFigur[8] = doPrograma + @"\figure\trdnjavaCrni.ico";
            this.slikeFigur[3] = doPrograma + @"\figure\lovecBeli.ico"; this.slikeFigur[9] = doPrograma + @"\figure\lovecCrni.ico";
            this.slikeFigur[4] = doPrograma + @"\figure\skakacBeli.ico"; this.slikeFigur[10] = doPrograma + @"\figure\skakacCrni.ico";
            this.slikeFigur[5] = doPrograma + @"\figure\kmetBeli.ico"; this.slikeFigur[11] = doPrograma + @"\figure\kmetCrni.ico";

            this.Mod = "osnovno";
            this.jeIgra = false;
            this.NastaviZacStanje();
        }

        /// <summary>
        /// lastnost , ki na vmesniku nastavi ustrezno stanje elementov
        /// </summary>
        public string Mod
        {
            get { return this.mod; }
            set
            {
                this.mod = value;
                this.NastaviVmesnik();
            }
        }

        /// <summary>
        /// metoda,postavi figure na zacetna polja in razveljavi vse parametre
        /// </summary>
        public void NastaviZacStanje()
        {
            this.trenutnaPoz = new Sahovnica();

            NapolniTabeloPolj(); //ustvarimo tabelo polj in slovar informacij

            VstaviSlike(); //dodamo figure na sliko

            this.jeOznaceno = false; // ni označena nobena figura
            this.izbranKralj = false; //je izbran kralj na zacetnem polju
            this.izbranKmetNaSedmi = false; // je izrbran kmet na predzadnji vrsti
            this.oznacenEP = false;  //ali je izbran kmet in ima moznost EP

            this.obarvanaPolja.Clear();

        }

        /// <summary>
        /// postavi vse PictureBox-e v tabelo
        /// </summary>
        public void NapolniTabeloPolj()
        {
            this.TSahovnica[0] = PA1; this.TSahovnica[1] = PB1; this.TSahovnica[2] = PC1; this.TSahovnica[3] = PD1; this.TSahovnica[4] = PE1; this.TSahovnica[5] = PF1; this.TSahovnica[6] = PG1; this.TSahovnica[7] = PH1;
            this.TSahovnica[8] = PA2; this.TSahovnica[9] = PB2; this.TSahovnica[10] = PC2; this.TSahovnica[11] = PD2; this.TSahovnica[12] = PE2; this.TSahovnica[13] = PF2; this.TSahovnica[14] = PG2; this.TSahovnica[15] = PH2;
            this.TSahovnica[16] = PA3; this.TSahovnica[17] = PB3; this.TSahovnica[18] = PC3; this.TSahovnica[19] = PD3; this.TSahovnica[20] = PE3; this.TSahovnica[21] = PF3; this.TSahovnica[22] = PG3; this.TSahovnica[23] = PH3;
            this.TSahovnica[24] = PA4; this.TSahovnica[25] = PB4; this.TSahovnica[26] = PC4; this.TSahovnica[27] = PD4; this.TSahovnica[28] = PE4; this.TSahovnica[29] = PF4; this.TSahovnica[30] = PG4; this.TSahovnica[31] = PH4;
            this.TSahovnica[32] = PA5; this.TSahovnica[33] = PB5; this.TSahovnica[34] = PC5; this.TSahovnica[35] = PD5; this.TSahovnica[36] = PE5; this.TSahovnica[37] = PF5; this.TSahovnica[38] = PG5; this.TSahovnica[39] = PH5;
            this.TSahovnica[40] = PA6; this.TSahovnica[41] = PB6; this.TSahovnica[42] = PC6; this.TSahovnica[43] = PD6; this.TSahovnica[44] = PE6; this.TSahovnica[45] = PF6; this.TSahovnica[46] = PG6; this.TSahovnica[47] = PH6;
            this.TSahovnica[48] = PA7; this.TSahovnica[49] = PB7; this.TSahovnica[50] = PC7; this.TSahovnica[51] = PD7; this.TSahovnica[52] = PE7; this.TSahovnica[53] = PF7; this.TSahovnica[54] = PG7; this.TSahovnica[55] = PH7;
            this.TSahovnica[56] = PA8; this.TSahovnica[57] = PB8; this.TSahovnica[58] = PC8; this.TSahovnica[59] = PD8; this.TSahovnica[60] = PE8; this.TSahovnica[61] = PF8; this.TSahovnica[62] = PG8; this.TSahovnica[63] = PH8;
        }

        /// <summary>
        /// metoda,ki vstavi ustrezne slike v PictureBox-e
        /// </summary>
        public void VstaviSlike()
        {
            int i = 0;

            int barva;
            int figura;

            foreach (string polje in this.trenutnaPoz.vrniSahovnico())
            {
                if (polje == ".")
                    TSahovnica[i].ImageLocation = null;
                else
                {
                    if (polje[0] == 'b')
                        barva = 0;
                    else
                        barva = 6;

                    if (polje[1] == 'K')
                        figura = 0;
                    else if (polje[1] == 'D')
                        figura = 1;
                    else if (polje[1] == 'T')
                        figura = 2;
                    else if (polje[1] == 'L')
                        figura = 3;
                    else if (polje[1] == 'S')
                        figura = 4;
                    else
                        figura = 5;

                    this.TSahovnica[i].ImageLocation = this.slikeFigur[barva + figura];
                }
                i++;
            }
        }

        /// <summary>
        /// pomozna metoda,ki ob kliku na ustrezno figuro spremeni ozadja možnih polj premika in obratno
        /// </summary>
        public void NastaviRdecaOzadja()
        {
            //nastavljena polja oarva rdeče
            foreach (PictureBox polje in obarvanaPolja)
            {
                polje.BackColor = Color.Red;
            }
        }

        /// <summary>
        /// metoda,ki ozadja nastavi na prvotno barvo
        /// </summary>
        public void RazveljaviOzadja(bool tudiIzbranoPolje = false)
        {
            if (tudiIzbranoPolje == true)
                this.obarvanaPolja.Add(this.izbranoPolje);

            foreach (PictureBox polje in this.obarvanaPolja)
            {
                string ime = polje.Name;
                if (ime[1] == 'A' || ime[1] == 'C' || ime[1] == 'E' || ime[1] == 'G')
                {
                    if (ime[2] % 2 == 1)
                        polje.BackColor = Color.SandyBrown;
                    else
                        polje.BackColor = Color.White;
                }
                else
                {
                    if (ime[2] % 2 == 1)
                        polje.BackColor = Color.White;
                    else
                        polje.BackColor = Color.SandyBrown;
                }
            }
        }

        /// <summary>
        /// metoda,ki ob kliku na sahovnico ustrezna polja obarva rdeče in oranžno
        /// </summary>
        /// <param name="oznacenoPolje">PictureBox: polje na katerega smo kliknili</param>
        public void NastaviObarvana(PictureBox oznacenoPolje)
        {
            string ime = oznacenoPolje.Name;
            Figura fig = trenutnaPoz.vrniFiguroNaPolju(ime[2] - '1', ime[1] - 'A');

            //če figura na polju obstaja
            if (fig != null)
            {
                // če je kmet...če na ustrezni vrsti...če so mozni EP...
                if (fig.IzpisFigure() == this.trenutnaPoz.kdo + "P" && (fig.barvaFigure() == "b" && fig.x == 4) || (fig.barvaFigure() == "c" && fig.x == 3))
                    if (this.trenutnaPoz.mozniEP != null)
                    {
                        int novX;
                        if (this.trenutnaPoz.kdo == "b")
                            novX = (fig.x + 1) * 8;
                        else
                            novX = (fig.x - 1) * 8;

                        foreach (int[] eP in this.trenutnaPoz.mozniEP)
                        {
                            if (fig.y == eP[0])
                            {
                                this.obarvanaPolja.Add(this.TSahovnica[novX + eP[1]]);
                                this.oznacenEP = true;
                                this.poljeEP = this.TSahovnica[novX + eP[1]];
                            }
                        }
                    }
                //v možnih potezah najde njene možne premike
                if (this.moznePoteze.ContainsKey(fig))
                {
                    foreach (int[] polje in moznePoteze[fig])
                    {
                        //in nastavi katera polja se morajo obarvati
                        this.obarvanaPolja.Add(this.TSahovnica[polje[0] * 8 + polje[1]]);
                    }
                }
                if (this.obarvanaPolja.Count() != 0)
                {
                    //kliknjeno polje postane oranžno
                    oznacenoPolje.BackColor = Color.Orange;

                    this.izbranoPolje = oznacenoPolje;

                    if (this.prikazPomoci)
                        this.NastaviRdecaOzadja();

                    this.jeOznaceno = true;
                }
            }
        }

        /// <summary>
        /// metoda,ki po premiku razveljavi ustrezne parametre
        /// </summary>
        public void Osvezi()
        {
            if (this.BPrekliči.Visible == true)
                this.BPrekliči_Click(null, null);

            VstaviSlike();

            this.moznePoteze = trenutnaPoz.IzracunajPoteze();

            //izpis poteze na zaslon
            int stVsehPotez = this.novaPartija.Poteze.Count();
            int poteza = this.novaPartija.KateraPoteza();
            ListViewItem trenutnaPoteza = new ListViewItem("" + poteza);

            //če je potezo naredil beli in je sedaj na vrsti črni
            if (this.trenutnaPoz.kdo == "c")
            {
                trenutnaPoteza.SubItems.Add(this.novaPartija.PotezeZaIzpis[stVsehPotez - 1]);
            }
            else
            {
                this.LVPartija.Items.RemoveAt(poteza - 1);
                trenutnaPoteza.SubItems.Add(this.novaPartija.PotezeZaIzpis[stVsehPotez - 2]);
                trenutnaPoteza.SubItems.Add(this.novaPartija.PotezeZaIzpis[stVsehPotez - 1]);
            }
            this.LVPartija.Items.Add(trenutnaPoteza);

            this.novaPartija.dodajPozicijo(trenutnaPoz.PovejPozicijoZaIzpis());
            this.novaPartija.vsePozicije.Add(trenutnaPoz);

            this.RazveljaviOzadja(true);

            this.jeOznaceno = false;
            this.izbranoPolje = null;
            this.izbranKralj = false;
            this.izbranKmetNaSedmi = false;
            this.obarvanaPolja.Clear();
            this.poljeEP = null;
            this.oznacenEP = false;

            //preveri če je konec
            if (moznePoteze.Count == 0 && this.trenutnaPoz.mozniEP == null)
            {
                if (trenutnaPoz.jeMatAliPat() == true)
                    this.KonecIgre("mat");
                else
                    this.KonecIgre("pat");
            }
            if (this.trenutnaPoz.pravilo50 == 100)
                this.KonecIgre("remi100");

            if (this.novaPartija.steviloPonovitev == 3)
                this.KonecIgre("remi3");

        }

        /// <summary>
        /// ko pride do rokade
        /// </summary>
        /// <param name="polje">PictureBox: definira vrsto rokade</param>
        public void Rokiraj(PictureBox polje)
        {
            if (polje == PG8 || polje == PG1)
            {
                this.trenutnaPoz = trenutnaPoz.IzvediMaloRokado();
                this.novaPartija.DodajPotezoRokada(false);
            }
            else
            {
                this.trenutnaPoz = trenutnaPoz.IzvediVelRokado();
                this.novaPartija.DodajPotezoRokada(true);
            }
        }

        /// <summary>
        /// pogledamo ali premik kmeta vodi v promocijo
        /// </summary>
        /// <param name="polje">PictureBox: izbrano polje</param>
        public void aliJeKmetNaSedmi(PictureBox polje)
        {
            int x = int.Parse("" + polje.Name[2]) - 1;
            int y = polje.Name[1] - 'A';

            if (x == 1)
            {
                if (trenutnaPoz.vrniSahovnico()[x, y][0] == 'c')
                    this.izbranKmetNaSedmi = trenutnaPoz.vrniSahovnico()[x, y][1] == 'P';
            }
            else if (x == 6)
            {
                if (trenutnaPoz.vrniSahovnico()[x, y][0] == 'b')
                    this.izbranKmetNaSedmi = trenutnaPoz.vrniSahovnico()[x, y][1] == 'P';
            }
            else
                this.izbranKmetNaSedmi = false;
        }

        /// <summary>
        /// ko pride do promocije
        /// </summary>
        /// <param name="polje">PictureBox: polje promocije</param>
        /// <param name="kaj">string: v kaj promoviramo</param>
        public void Promocija(PictureBox polje, string kaj = "D")
        {
            int x = izbranoPolje.Name[2] - '1';
            int y = izbranoPolje.Name[1] - 'A';

            int novX = polje.Name[2] - '1';
            int novY = polje.Name[1] - 'A';

            Console.WriteLine(this.izbranoPolje.Name);
            Console.WriteLine(polje.Name);

            trenutnaPoz = trenutnaPoz.Promocija(kaj, y, novY);
            this.novaPartija.dodajPotezoPromocija(x, y, novX, novY, kaj);

            this.Mod = "novaPartija";
            this.Osvezi();
        }

        /// <summary>
        /// ko se igra konča(mat,pat ali pravila
        /// </summary>
        /// <param name="rez">string: razlog konca</param>
        public void KonecIgre(string rez)
        {

            if (rez == "mat")
            {
                if (this.trenutnaPoz.kdo == "b")
                {
                    novaPartija.Rezultat = "0-1";
                }
                else
                {
                    novaPartija.Rezultat = "1-0";
                }
            }

            else if (rez == "pat")
            {
                novaPartija.Rezultat = "0.5-0.5";
            }

            else if (rez == "predaja")
            {
                if (this.trenutnaPoz.kdo == "b")
                {
                    novaPartija.Rezultat = "0-1";
                }
                else
                {
                    novaPartija.Rezultat = "1-0";
                }

            }

            else if (rez == "remi100" || rez == "remi3" || rez == "remi")
            {
                novaPartija.Rezultat = "0.5-0.5";
            }

            this.Mod = "konec";

            this.novaPartija.ZapisiNaDatoteko(this.doPrograma + @"\igre.txt");
        }

        /// <summary>
        /// ko ne gre za rokado ali promocijo
        /// </summary>
        /// <param name="polje">PictureBox: polje premika</param>
        public void NavadenPremik(PictureBox polje)
        {
            string imeIzb = this.izbranoPolje.Name;

            Figura fig = trenutnaPoz.vrniFiguroNaPolju(imeIzb[2] - '1', imeIzb[1] - 'A');

            string imeNovo = polje.Name;
            int novX = imeNovo[2] - '1'; int novY = imeNovo[1] - 'A';

            if (oznacenEP && polje == poljeEP)
            {
                this.trenutnaPoz = this.trenutnaPoz.IzvediEP(fig.y, this.poljeEP.Name[1] - 'A');
            }

            else
            {
                this.trenutnaPoz = trenutnaPoz.Premik(novX, novY, fig);
                trenutnaPoz.Naslednji();
            }

            this.novaPartija.DodajPotezo(fig.ImeFigure(), fig.x, fig.y, novX, novY);
        }

        /// <summary>
        /// ko kliknemo na že označeno figuro in jo razveljvi
        /// </summary>
        public void Razveljavi()
        {
            //rdeča (+ izbrano oranžno) ozadja resetira
            this.RazveljaviOzadja(true);

            //ustrezne parametre resetira
            this.jeOznaceno = false;
            this.obarvanaPolja.Clear();
            this.izbranoPolje = null;
            this.izbranKralj = false;
            this.izbranKmetNaSedmi = false;
            this.poljeEP = null;
        }

        /// <summary>
        /// metoda,ki se pokliče ko nardimo premik kmeta na zadnjo vrsto,odpre novo Formo
        /// </summary>
        /// <param name="polje">PictureBox: polje promocije</param>
        public void IzbiraFigure(PictureBox polje)
        {
            this.Mod = "promocija";

            FPromocija okno;
            if (this.trenutnaPoz.kdo == "b")
                okno = new FPromocija(this.slikeFigur[1], this.slikeFigur[2], this.slikeFigur[3], this.slikeFigur[4], polje);
            else
                okno = new FPromocija(this.slikeFigur[7], this.slikeFigur[8], this.slikeFigur[9], this.slikeFigur[10], polje);
            okno.Show();
        }

        /// <summary>
        /// metoda,ki se pokliče ob kliku na polja na sahovnici
        /// </summary>
        /// <param name="klikPolje">PictureBox: kliknjeno polje</param>
        public void ObKliku(PictureBox klikPolje)
        {
            //samo če igra poteka
            if (this.jeIgra == true)
            {
                //če smo že označili figuro
                if (jeOznaceno)
                {
                    //s pritiskom na isto polje razveljavimo
                    if (klikPolje == this.izbranoPolje)
                        Razveljavi();

                    //na eno izmed rdečih pa naredimo premik
                    else if (this.obarvanaPolja.Contains(klikPolje))
                    {
                        //če smo kliknili na rokado
                        if (this.izbranKralj && (klikPolje == PG8 || klikPolje == PC8 || klikPolje == PC1 || klikPolje == PG1))
                            this.Rokiraj(klikPolje);

                        //če bo prišlo do promocije
                        else if (izbranKmetNaSedmi)
                        {
                            this.IzbiraFigure(klikPolje);
                            return;
                        }

                        //navaden premik
                        else
                        {
                            NavadenPremik(klikPolje);
                        }
                        this.Osvezi();
                    }
                }

                //če še nismo označili figure
                else
                {
                    //če smo kliknili na kralja in je v partiji še možna mala rokada
                    if ((klikPolje == PE8 && this.trenutnaPoz.moznaMalaRokadaCrni) || (klikPolje == PE1 && trenutnaPoz.moznaMalaRokadaBeli))
                    {
                        //če je možna mala rokada v trenutni poziciji
                        if (trenutnaPoz.PreveriMalRokada())
                        {
                            this.izbranKralj = true;
                            if (klikPolje == PE8)
                                this.obarvanaPolja.Add(PG8);
                            else
                                this.obarvanaPolja.Add(PG1);
                        }
                    }

                    //če smo kliknili na kralja in je v partiji še možna velika rokada
                    if ((klikPolje == PE8 && this.trenutnaPoz.moznaVelikaRokadaCrni) || (klikPolje == PE1 && trenutnaPoz.moznaVelikaRokadaBeli))
                    {
                        //če je možna velika rokada v trenutni poziciji
                        if (trenutnaPoz.PreveriVelRokada())
                        {
                            this.izbranKralj = true;
                            if (klikPolje == PE1)
                                this.obarvanaPolja.Add(PC1);
                            else
                                this.obarvanaPolja.Add(PC8);
                        }
                    }

                   //če smo kliknili na predzadnjo vrsto
                    else if (klikPolje.Name[2] == '2' || klikPolje.Name[2] == '7')
                        this.aliJeKmetNaSedmi(klikPolje);

                    this.NastaviObarvana(klikPolje);
                }
            }
        }

        #region Polja

        #region H8
        private void PH8_Click(object sender, EventArgs e)
        {
            this.ObKliku(PH8);
        }
        #endregion

        #region G8
        private void PG8_Click(object sender, EventArgs e)
        {
            this.ObKliku(PG8);
        }
        #endregion

        #region F8
        private void PF8_Click(object sender, EventArgs e)
        {
            this.ObKliku(PF8);
        }
        #endregion

        #region E8
        private void PE8_Click(object sender, EventArgs e)
        {
            this.ObKliku(PE8);

        }
        #endregion

        #region D8
        private void PD8_Click(object sender, EventArgs e)
        {
            this.ObKliku(PD8);
        }
        #endregion

        #region C8
        private void PC8_Click(object sender, EventArgs e)
        {
            this.ObKliku(PC8);
        }
        #endregion

        #region B8
        private void PB8_Click(object sender, EventArgs e)
        {
            this.ObKliku(PB8);
        }
        #endregion

        #region A8
        private void PA8_Click(object sender, EventArgs e)
        {
            this.ObKliku(PA8);
        }
        #endregion

        #region H7
        private void PH7_Click(object sender, EventArgs e)
        {
            this.ObKliku(PH7);
        }
        #endregion

        #region G7
        private void PG7_Click(object sender, EventArgs e)
        {
            this.ObKliku(PG7);
        }
        #endregion

        #region F7
        private void PF7_Click(object sender, EventArgs e)
        {
            this.ObKliku(PF7);
        }
        #endregion

        #region E7
        private void PE7_Click(object sender, EventArgs e)
        {
            this.ObKliku(PE7);
        }
        #endregion

        #region D7
        private void PD7_Click(object sender, EventArgs e)
        {
            this.ObKliku(PD7);
        }
        #endregion

        #region C7
        private void PC7_Click(object sender, EventArgs e)
        {
            this.ObKliku(PC7);
        }
        #endregion

        #region B7
        private void PB7_Click(object sender, EventArgs e)
        {
            this.ObKliku(PB7);
        }
        #endregion

        #region A7
        private void PA7_Click(object sender, EventArgs e)
        {
            this.ObKliku(PA7);
        }
        #endregion

        #region H6
        private void PH6_Click(object sender, EventArgs e)
        {
            this.ObKliku(PH6);
        }
        #endregion

        #region G6
        private void PG6_Click(object sender, EventArgs e)
        {
            this.ObKliku(PG6);
        }
        #endregion

        #region F6
        private void PF6_Click(object sender, EventArgs e)
        {
            this.ObKliku(PF6);
        }
        #endregion

        #region E6
        private void PE6_Click(object sender, EventArgs e)
        {
            this.ObKliku(PE6);
        }
        #endregion

        #region D6
        private void PD6_Click(object sender, EventArgs e)
        {
            this.ObKliku(PD6);
        }
        #endregion

        #region C6
        private void PC6_Click(object sender, EventArgs e)
        {
            this.ObKliku(PC6);
        }
        #endregion

        #region B6
        private void PB6_Click(object sender, EventArgs e)
        {
            this.ObKliku(PB6);
        }
        #endregion

        #region A6
        private void PA6_Click(object sender, EventArgs e)
        {
            this.ObKliku(PA6);
        }
        #endregion

        #region H5
        private void PH5_Click(object sender, EventArgs e)
        {
            this.ObKliku(PH5);
        }
        #endregion

        #region G5
        private void PG5_Click(object sender, EventArgs e)
        {
            this.ObKliku(PG5);
        }
        #endregion

        #region F5
        private void PF5_Click(object sender, EventArgs e)
        {
            this.ObKliku(PF5);
        }
        #endregion

        #region E5
        private void PE5_Click(object sender, EventArgs e)
        {
            this.ObKliku(PE5);
        }
        #endregion

        #region D5
        private void PD5_Click(object sender, EventArgs e)
        {
            this.ObKliku(PD5);
        }
        #endregion

        #region C5
        private void PC5_Click(object sender, EventArgs e)
        {
            this.ObKliku(PC5);
        }
        #endregion

        #region B5
        private void PB5_Click(object sender, EventArgs e)
        {
            this.ObKliku(PB5);
        }
        #endregion

        #region A5
        private void PA5_Click(object sender, EventArgs e)
        {
            this.ObKliku(PA5);
        }
        #endregion

        #region H4
        private void PH4_Click(object sender, EventArgs e)
        {
            this.ObKliku(PH4);
        }
        #endregion

        #region G4
        private void PG4_Click(object sender, EventArgs e)
        {
            this.ObKliku(PG4);
        }
        #endregion

        #region F4
        private void PF4_Click(object sender, EventArgs e)
        {
            this.ObKliku(PF4);
        }
        #endregion

        #region E4
        private void PE4_Click(object sender, EventArgs e)
        {
            this.ObKliku(PE4);
        }
        #endregion

        #region D4
        private void PD4_Click(object sender, EventArgs e)
        {
            this.ObKliku(PD4);
        }
        #endregion

        #region C4
        private void PC4_Click(object sender, EventArgs e)
        {
            this.ObKliku(PC4);
        }
        #endregion

        #region B4
        private void PB4_Click(object sender, EventArgs e)
        {
            this.ObKliku(PB4);
        }
        #endregion

        #region A4
        private void PA4_Click(object sender, EventArgs e)
        {
            this.ObKliku(PA4);
        }
        #endregion

        #region H3
        private void PH3_Click(object sender, EventArgs e)
        {
            this.ObKliku(PH3);
        }
        #endregion

        #region G3
        private void PG3_Click(object sender, EventArgs e)
        {
            this.ObKliku(PG3);
        }
        #endregion

        #region F3
        private void PF3_Click(object sender, EventArgs e)
        {
            this.ObKliku(PF3);
        }
        #endregion

        #region E3
        private void PE3_Click(object sender, EventArgs e)
        {
            this.ObKliku(PE3);
        }
        #endregion

        #region D3
        private void PD3_Click(object sender, EventArgs e)
        {
            this.ObKliku(PD3);
        }
        #endregion

        #region C3
        private void PC3_Click(object sender, EventArgs e)
        {
            this.ObKliku(PC3);
        }
        #endregion

        #region B3
        private void PB3_Click(object sender, EventArgs e)
        {
            this.ObKliku(PB3);
        }
        #endregion

        #region A3
        private void PA3_Click(object sender, EventArgs e)
        {
            this.ObKliku(PA3);
        }
        #endregion

        #region H2
        private void PH2_Click(object sender, EventArgs e)
        {
            this.ObKliku(PH2);
        }
        #endregion

        #region G2
        private void PG2_Click(object sender, EventArgs e)
        {
            this.ObKliku(PG2);
        }
        #endregion

        #region F2
        private void PF2_Click(object sender, EventArgs e)
        {
            this.ObKliku(PF2);
        }
        #endregion

        #region E2
        private void PE2_Click(object sender, EventArgs e)
        {
            this.ObKliku(PE2);
        }
        #endregion

        #region D2
        private void PD2_Click(object sender, EventArgs e)
        {
            this.ObKliku(PD2);
        }
        #endregion

        #region C2
        private void PC2_Click(object sender, EventArgs e)
        {
            this.ObKliku(PC2);
        }
        #endregion

        #region B2
        private void PB2_Click(object sender, EventArgs e)
        {
            this.ObKliku(PB2);
        }
        #endregion

        #region A2
        private void PA2_Click(object sender, EventArgs e)
        {
            this.ObKliku(PA2);
        }
        #endregion

        #region H1
        private void PH1_Click(object sender, EventArgs e)
        {
            this.ObKliku(PH1);
        }
        #endregion

        #region G1
        private void PG1_Click(object sender, EventArgs e)
        {
            this.ObKliku(PG1);
        }
        #endregion

        #region F1
        private void PF1_Click(object sender, EventArgs e)
        {
            this.ObKliku(PF1);
        }
        #endregion

        #region E1
        private void PE1_Click(object sender, EventArgs e)
        {
            this.ObKliku(PE1);
        }
        #endregion

        #region D1
        private void PD1_Click(object sender, EventArgs e)
        {
            this.ObKliku(PD1);
        }
        #endregion

        #region C1
        private void PC1_Click(object sender, EventArgs e)
        {
            this.ObKliku(PC1);
        }
        #endregion

        #region B1
        private void PB1_Click(object sender, EventArgs e)
        {
            this.ObKliku(PB1);
        }
        #endregion

        #region A1
        private void PA1_Click(object sender, EventArgs e)
        {
            this.ObKliku(PA1);
        }
        #endregion
        #endregion

        /// <summary>
        /// označeno prikaže rdeča polja možnih premikov
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CBPomoc_CheckedChanged(object sender, EventArgs e)
        {
            this.prikazPomoci = !this.prikazPomoci;

            if (this.prikazPomoci == false)
                this.RazveljaviOzadja();
            else
                NastaviRdecaOzadja();
        }

        /// <summary>
        /// ob pritisku na gumb nova Igra,se prične nova partija
        /// </summary>
        /// <param name="sender">object BNovaIgra</param>
        /// <param name="e"></param>
        private void BNovaIgra_Click(object sender, EventArgs e)
        {
            if (this.jeIgra == false)
            {
                this.Mod = "vnosPredIgro";
                this.jeIgra = true;
            }
            else if (this.Mod == "vnosPredIgro")
            {
                this.NastaviZacStanje();

                this.moznePoteze = this.trenutnaPoz.IzracunajPoteze();

                //prebere in pobriše vnosa belega in črnega
                string beli = this.TBvnosBelega.Text;
                string crni = this.TBvnosCrnega.Text;

                novaPartija = new Partija(beli, crni); //začnemo novo partijo
                novaPartija.dodajPozicijo(this.trenutnaPoz.PovejPozicijoZaIzpis());

                this.Mod = "novaPartija";
            }
        }

        /// <summary>
        /// ob pritisku na gumb Remi,predlaga remi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BRemi_Click_1(object sender, EventArgs e)
        {
            this.Mod = "prekinitev";
            this.ponujenRemi = true;
        }

        /// <summary>
        /// ob pritisku na gumb Predaj,predlaga predajo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BPredaj_Click(object sender, EventArgs e)
        {
            this.Mod = "prekinitev";
            this.predajIgro = true;
        }

        /// <summary>
        /// ob pritisku na gumb Prekini, predlaga prekinitev partije
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BPrekini_Click(object sender, EventArgs e)
        {
            this.Mod = "prekinitev";
            this.prekiniIgro = true;
        }

        /// <summary>
        /// ob kliku na gumb Potrdi, sprejme predlog(remi,predaja,prekinitev)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BPotrdi_Click(object sender, EventArgs e)
        {
            if (this.prekiniIgro == true)
            {
                this.NastaviZacStanje();
                this.Mod = "osnovno";
            }

            else if (this.ponujenRemi == true)
            {
                this.ponujenRemi = false;
                this.KonecIgre("remi");
            }

            else if (this.predajIgro == true)
            {
                this.predajIgro = false;
                this.KonecIgre("predaja");
            }
        }

        /// <summary>
        /// ob kliku na kumb Prekliči, zavrne predlog(remi,predaja,prekinitev)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BPrekliči_Click(object sender, EventArgs e)
        {
            if (this.Mod == "vnosPredIgro")
            {
                this.Mod = "osnovno";
                this.NastaviZacStanje();
            }
            else
            {
                this.Mod = "novaPartija";
            }
        }

        /// <summary>
        /// ob kliku na gumb Baza, odpre bazo partij na datoteki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BBaza_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FBaza bazaPartij = new FBaza(this.doPrograma + @"\igre.txt");
            bazaPartij.Show();
        }

        /// <summary>
        /// metoda,ki se pokliče ob spremembi moda in nastavi objekte na vmesniku na ustrezno stanje
        /// </summary>
        public void NastaviVmesnik()
        {
            if (mod == "osnovno")
            {
                this.BRazveljavi.Visible = false;
                this.BNaprej.Visible = false;
                this.BNazaj.Visible = false;
                this.LVPartija.Columns[1].Text = "Beli";
                this.LVPartija.Columns[2].Text = "Črni";
                this.jeIgra = false;
                this.BBaza.Enabled = true;
                this.BNovaIgra.Enabled = true;
                this.BRemi.Visible = false;
                this.BPredaj.Visible = false;
                this.BPrekini.Visible = false;
                this.BPrekliči.Visible = false;
                this.BPotrdi.Visible = false;
                this.TBvnosBelega.Enabled = false;
                this.TBvnosCrnega.Enabled = false;
                this.BNovaIgra.Text = "Nova igra";
            }
            else if (mod == "vnosPredIgro")
            {
                this.BNaprej.Visible = false;
                this.BNazaj.Visible = false;
                this.LVPartija.Items.Clear();
                this.jeIgra = true;
                this.TBvnosBelega.Text = "";
                this.TBvnosCrnega.Text = "";
                this.BPotrdi.Visible = false;
                this.BRemi.Visible = false;
                this.BPredaj.Visible = false;
                this.BPrekini.Visible = false;
                this.BPrekliči.Visible = true;
                this.BPotrdi.Visible = false;
                this.TBvnosBelega.Enabled = true;
                this.TBvnosCrnega.Enabled = true;
                this.BBaza.Enabled = false;
                this.BNovaIgra.Text = "Vnesi igralca in začni";
            }
            else if (mod == "novaPartija")
            {
                this.BRazveljavi.Visible = true;
                this.BNaprej.Visible = false;
                this.BNazaj.Visible = false;
                this.BringToFront();
                this.Enabled = true;
                this.LVPartija.Columns[1].Text = this.novaPartija.BeliIme;
                this.LVPartija.Columns[2].Text = this.novaPartija.CrniIme;
                this.BPotrdi.Visible = false;
                this.BRemi.Visible = true;
                this.BPredaj.Visible = true;
                this.BPrekini.Visible = true;
                this.BPrekliči.Visible = false;
                this.BPotrdi.Visible = false;
                this.TBvnosBelega.Enabled = false;
                this.TBvnosCrnega.Enabled = false;
                this.BNovaIgra.Text = "Igra poteka!";
                this.BNovaIgra.Enabled = false;
                this.prekiniIgro = false;
                this.ponujenRemi = false;
                this.predajIgro = false;
            }
            else if (mod == "prekinitev")
            {
                this.BPotrdi.Visible = true;
                this.BPrekliči.Visible = true;
            }
            else if (mod == "baza")
            {
                this.BRazveljavi.Visible = false;
                this.BNaprej.Visible = true;
                this.BNazaj.Visible = true;
                this.LVPartija.Items.Clear();
                this.Enabled = true;
            }
            else if (mod == "promocija")
            {
                this.Enabled = false;
            }
            else if (mod == "konec")
            {
                ListViewItem rezultat = new ListViewItem("");
                rezultat.SubItems.Add(this.novaPartija.Rezultat);
                this.LVPartija.Items.Add(rezultat);

                this.BNaprej.Visible = false;
                this.BNazaj.Visible = false;
                this.BRemi.Visible = false;
                this.BPredaj.Visible = false;
                this.BPrekini.Visible = false;
                this.BPrekliči.Visible = false;
                this.BPotrdi.Visible = false;
                this.BNovaIgra.Text = "Nova igra";
                this.jeIgra = false;
                this.BBaza.Enabled = true;
                this.BNovaIgra.Enabled = true;
            }
        }

        public void PripraviPregledPartije(string partija)
        {
            this.BringToFront();
            this.Mod = "baza";
            this.novaPartija = new Partija(partija);
            this.indeksPozicije = 0;


            //izpisemo poteze na zaslon
            for (int i = 0; i < this.novaPartija.Poteze.Count(); i += 2)
            {
                ListViewItem enaPoteza = new ListViewItem("" + (i / 2 + 1));
                enaPoteza.SubItems.Add(this.novaPartija.PotezeZaIzpis[i]);
                if (i + 1 < this.novaPartija.Poteze.Count())
                    enaPoteza.SubItems.Add(this.novaPartija.PotezeZaIzpis[i + 1]);
                this.LVPartija.Items.Add(enaPoteza);
            }


            Sahovnica poz = new Sahovnica();
            this.trenutnaPoz = poz;
            novaPartija.DodajPozicijoZaOgled(poz.sahovnica);
            this.VstaviSlike();

            foreach (string pot in this.novaPartija.Poteze)
            {
                if (pot == "0-0")
                {
                    poz = poz.IzvediMaloRokado();
                }
                else if (pot == "0-0-0")
                {
                    poz = poz.IzvediVelRokado();
                }
                else if (pot.Length == 6)
                {
                    poz = poz.Promocija("" + pot[5], int.Parse("" + pot[2]), int.Parse("" + pot[4]));
                }
                else if (pot[0] == 'P' && pot[4] != pot[2] && poz.sahovnica[int.Parse("" + pot[3]), int.Parse("" + pot[4])] == ".")
                {
                    poz = poz.IzvediEP(int.Parse("" + pot[2]), int.Parse("" + pot[4]));
                }
                else
                {
                    poz = poz.Premik(int.Parse("" + pot[3]), int.Parse("" + pot[4]), new Figura(int.Parse("" + pot[1]), int.Parse("" + pot[2]), poz.sahovnica[int.Parse("" + pot[1]), int.Parse("" + pot[2])]));
                }
                novaPartija.DodajPozicijoZaOgled(poz.sahovnica);
            }

        }

        /// <summary>
        /// ob kliku na gumb naprej,se premaknemo na naslednjo pozicijo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BNaprej_Click(object sender, EventArgs e)
        {
            if (this.indeksPozicije == this.novaPartija.Poteze.Count())
                return;

            this.indeksPozicije++;
            this.trenutnaPoz = new Sahovnica(this.novaPartija.SezPozicijZaPregledPartij[this.indeksPozicije], this.trenutnaPoz.KdoNaslednji(), false, false, false, false, null, 0, 0);
            this.VstaviSlike();
        }

        /// <summary>
        /// ob kliku na gumb nazaj se vrnemo na prejšnjo pozicijo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BNazaj_Click(object sender, EventArgs e)
        {
            if (this.indeksPozicije == 0)
                return;

            this.indeksPozicije--;
            this.trenutnaPoz = new Sahovnica(this.novaPartija.SezPozicijZaPregledPartij[this.indeksPozicije], this.trenutnaPoz.KdoNaslednji(), false, false, false, false, null, 0, 0);
            this.VstaviSlike();
        }

        /// <summary>
        /// ob kliku na gumb se razveljavi zadnja poteza(če je to možno)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BRazveljavi_Click(object sender, EventArgs e)
        {

            //gumb deluje samo med med igro,ko je potez več kot 0
            if (!this.jeIgra || !(this.Mod == "novaPartija") || this.novaPartija.Poteze.Count() == 0)
                return;

            if (this.novaPartija.Poteze.Count() % 2 == 0)
                this.LVPartija.Items[this.novaPartija.KateraPoteza()-1].SubItems[2].Text = "";
            else
                this.LVPartija.Items.RemoveAt(this.novaPartija.KateraPoteza() - 1);
            this.novaPartija.vsePozicije.RemoveAt(this.novaPartija.Poteze.Count());
            this.novaPartija.RazveljaviZadnjoPotezo();
            this.trenutnaPoz = this.novaPartija.vsePozicije[this.novaPartija.Poteze.Count()];
            this.moznePoteze = trenutnaPoz.IzracunajPoteze();
            this.VstaviSlike();

            this.jeOznaceno = false;
            this.izbranoPolje = null;
            this.izbranKralj = false;
            this.izbranKmetNaSedmi = false;
            this.obarvanaPolja.Clear();
            this.poljeEP = null;
            this.oznacenEP = false;

        }

        /// <summary>
        /// ob kliku na gumb pomoč,odpre ustrezno formo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BPomoč_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FPomoc pomoc = new FPomoc(this.doPrograma + @"\figure\pomoc.PNG");
            pomoc.Show();

        }

        /// <summary>
        /// ob kliku na gumb Pravila,odpre ustrezno formo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BPravila_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            FPravilaIgre pravila = new FPravilaIgre(this.doPrograma + @"\figure\pravila.PNG");
            pravila.Show();
        }
    }
}
