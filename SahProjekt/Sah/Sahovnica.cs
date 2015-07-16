using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sah
{
    public class Sahovnica
    {
        public string[,] sahovnica;
        public string kdo;

        public bool moznaMalaRokadaBeli;
        public bool moznaMalaRokadaCrni;
        public bool moznaVelikaRokadaBeli;
        public bool moznaVelikaRokadaCrni;

        public int kateraPoteza;

        public int pravilo50;

        public List<int[]> mozniEP = new List<int[]>();

        public Dictionary<string, Figura> Figure = new Dictionary<string, Figura>();

        /// <summary>
        /// osnovni konstruktor za začetno pozicijo
        /// </summary>
        public Sahovnica()
        {
            //določa kdo je na potezi
            this.kdo = "b";

            //dvodim. tabela,ki predstavlja pozicijo
            this.sahovnica = new string[,]
            {{"bT","bS","bL","bD","bK","bL","bS","bT"},
            {"bP","bP","bP","bP","bP","bP","bP","bP"},
            {"." ,"." ,"." ,"." ,"." ,"." ,"." ,"." },
            {"." ,"." ,"." ,"." ,"." ,"." ,"." ,"." },
            {"." ,"." ,"." ,"." ,"." ,"." ,"." ,"." },
            {"." ,"." ,"." ,"." ,"." ,"." ,"." ,"." },
            {"cP","cP","cP","cP","cP","cP","cP","cP"},
            {"cT","cS","cL","cD","cK","cL","cS","cT"}};

            //napolni slovar figur glede na sahovnico
            for (int i = 0; i < sahovnica.GetLength(0); i++)
                for (int j = 0; j < sahovnica.GetLength(1); j++)
                {
                    if (sahovnica[i, j] != ".")
                    {
                        Figure.Add("" + i + j, new Figura(i, j, "" + sahovnica[i, j]));
                    }
                }

            this.moznaMalaRokadaBeli = true;
            this.moznaVelikaRokadaBeli = true;
            this.moznaMalaRokadaCrni = true;
            this.moznaVelikaRokadaCrni = true;
            this.kateraPoteza = 0;
            this.pravilo50 = 0;
            this.mozniEP = null;
        }

        /// <summary>
        /// konstruktor,ki spremeni pozicijo
        /// </summary>
        /// <param name="s">string[,]: dvodimenzionalna tabela nizov,ki predstavlja pozicijo</param>
        public Sahovnica(string[,] s, string kdoNaVrsti, bool mozMalBeli, bool mozMalCrni, bool mozVelBeli, bool mozVelCrni, List<int[]> moznaEPJemanja, int prav50,int stPoteza)
        {
            this.kdo = kdoNaVrsti;
            this.sahovnica = s;

            this.moznaMalaRokadaBeli = mozMalBeli;
            this.moznaMalaRokadaCrni = mozMalCrni;
            this.moznaVelikaRokadaBeli = mozVelBeli;
            this.moznaVelikaRokadaCrni = mozVelCrni;
            this.kateraPoteza = stPoteza;
            this.pravilo50 = prav50;
            this.mozniEP = moznaEPJemanja;

            //napolni slovar figur glede na sahovnico
            for (int i = 0; i < sahovnica.GetLength(0); i++)
                for (int j = 0; j < sahovnica.GetLength(1); j++)
                {
                    if (sahovnica[i, j] != ".")
                    {
                        Figure.Add("" + i + j, new Figura(i, j, "" + sahovnica[i, j]));
                    }
                }
        }

        /// <summary>
        /// metoda,ki za vse figure v poziciji izračuna njihove možne poteze
        /// </summary>
        /// <returns>Dictionary: slovar(objekt Figura/vsa polja na katera se lahko premaknejo)</returns>
        public Dictionary<Figura, List<int[]>> IzracunajPoteze()
        {
            //ustvarimo nov slovar
            Dictionary<Figura, List<int[]>> mozPot = new Dictionary<Figura, List<int[]>>();

            //izračunamo vsa delovanja vseh figur
            Dictionary<Figura, List<int[]>> vsePot = VsePoteze();

            bool mozna;//preverja ali je trenutno sah

            //pregledamo vse figure in njihove poteze iz vsePot
            foreach (var pair in vsePot)
            {
                Figura f = pair.Key;

                List<int[]> poteze = pair.Value;
                List<int[]> PotezeEne = new List<int[]>();

                //gremo po vseh potezah figure
                foreach (int[] potezaFig in poteze)
                {
                    //za potezo naredimo premik
                    Sahovnica NovaSahovnica = (Premik(potezaFig[0], potezaFig[1], f));

                    //izracunamo polozaj kralja ter za tem nastavimo,da je na vrsti nasprotnik
                    int[] polozajKralja = NovaSahovnica.KjeKralj();
                    NovaSahovnica.Naslednji();

                    //določimo ali je po potezi v šahu in če ni potezo dodamo
                    mozna = NovaSahovnica.JeFiguraNapadena(polozajKralja);
                    if (!mozna)
                        PotezeEne.Add(potezaFig);
                }

                //če torej imamo možne poteze za določeno figuro,jih dodamo v slovar
                if (PotezeEne.Count > 0)
                    mozPot.Add(pair.Key, PotezeEne);
            }
            return mozPot;
        }

        /// <summary>
        /// metdda,ki izračuna vsa delovanja vseh figur
        /// </summary>
        /// <returns>Dictinary: slovar(objekt Figura/vse poteze te figure)</returns>
        public Dictionary<Figura, List<int[]>> VsePoteze()
        {
            //slovar
            Dictionary<Figura, List<int[]>> poteze = new Dictionary<Figura, List<int[]>>();

            //polnimo slovar s figurami in njihovimi potezami
            foreach (Figura f in Figure.Values)
                if (this.kdo == f.barvaFigure())
                {
                    poteze.Add(f, VsePotezeFigura(f));
                }
            return poteze;
        }

        /// <summary>
        /// metoda,ki za vsako figuro pokliče ustrezno metodo glede na to kakšne vrste figura je
        /// </summary>
        /// <param name="fig">Object: objekt figura</param>
        /// <returns>List: seznam vseh potez te figure</returns>
        public List<int[]> VsePotezeFigura(Figura fig)
        {

            //seznam kjer bomo shranili mozne premike
            List<int[]> vsePot = new List<int[]>();

            //kmet...
            if (fig.ImeFigure() == "P")
            {
                vsePot = Kmet(fig);
            }

            //skakač
            else if (fig.ImeFigure() == "S")
            {
                vsePot = Skakac(fig);
            }

            //lovec
            else if (fig.ImeFigure() == "L")
            {
                vsePot = Lovec(fig);
            }

            //trdnjava
            else if (fig.ImeFigure() == "T")
            {
                vsePot = Trdnjava(fig);
            }

            //dama
            else if (fig.ImeFigure() == "D")
            {
                vsePot = Dama(fig);
            }

            //kralj
            else if (fig.ImeFigure() == "K")
            {
                vsePot = Kralj(fig);
            }

            return vsePot;
        }

        /// <summary>
        /// metoda,ki za kmeta izračuna vsa njegova delovanja(brez EP)
        /// </summary>
        /// <param name="fig">Object: objekt Figura,ki predstavlja kmeta</param>
        /// <returns>list: seznam vseh potez kmeta</returns>
        public List<int[]> Kmet(Figura fig)
        {
            List<int[]> vsePot = new List<int[]>();

            //definiramo jemanja
            int[] J1 = new int[2];
            int[] J2 = new int[2];

            //...beli
            if (fig.barvaFigure() == "b")
            {
                //dva mozna premika
                int[] T1 = new int[2]; T1[0] = fig.x + 1; T1[1] = fig.y;
                int[] T2 = new int[2]; T2[0] = fig.x + 2; T2[1] = fig.y;

                //dve jemanji
                J1[0] = fig.x + 1; J1[1] = fig.y + 1;
                J2[0] = fig.x + 1; J2[1] = fig.y - 1;

                //ce je pred njim prazno polje ga dodamo...
                if (this.sahovnica[T1[0], T1[1]][0] == '.')
                {
                    vsePot.Add(T1);

                    //...ce obe polji pred njim prazni in je na zacetku dodamo se drug premik
                    if (fig.x < 6 && this.sahovnica[T2[0], T2[1]][0] == '.' && fig.x == 1)
                    {
                        vsePot.Add(T2);
                    }
                }
            }

            //...crni
            if (fig.barvaFigure() == "c")
            {
                //dva mozna premika
                int[] T1 = new int[2]; T1[0] = fig.x - 1; T1[1] = fig.y;
                int[] T2 = new int[2]; T2[0] = fig.x - 2; T2[1] = fig.y;

                //dve jemanji
                J1[0] = fig.x - 1; J1[1] = fig.y + 1;
                J2[0] = fig.x - 1; J2[1] = fig.y - 1;

                //ce je pred njim prazno polje ga dodamo...
                if (this.sahovnica[T1[0], T1[1]][0] == '.')
                {
                    vsePot.Add(T1);

                    //...ce obe polji pred njim prazni in je na zacetku dodamo se drug premik
                    if ((fig.x > 1 && this.sahovnica[T2[0], T2[1]][0] == '.') && (fig.x == 6))
                    {
                        vsePot.Add(T2);
                    }
                }
            }

            //preverimo še jemanja...
            int[][] vsaJem = new int[2][]; vsaJem[0] = J1; vsaJem[1] = J2;

            foreach (int[] jem in vsaJem)
            {
                //...ce so znotraj sahovnice
                if ((jem[1] < 8) && (jem[1] >= 0))
                {
                    //...in polje ni prazno ali zasedeno z lastno figuro...dodamo
                    if ((this.sahovnica[jem[0], jem[1]][0] != '.') && ("" + this.sahovnica[jem[0], jem[1]][0] != fig.barvaFigure()))
                    {
                        vsePot.Add(jem);
                    }
                }
            }
            return vsePot;
        }

        /// <summary>
        /// metoda,ki za skakača izračuna vsa njegova delovanja
        /// </summary>
        /// <param name="fig">Object: objekt Figura,ki predstavlja skakača</param>
        /// <returns>list: seznam vseh potez skakača</returns>
        public List<int[]> Skakac(Figura fig)
        {
            List<int[]> vsePot = new List<int[]>();

            //generiramo tabelo tabel,vseh novih koordinat skakača
            int[][] tockaSkak = new int[8][];

            int[] T1 = new int[2]; T1[0] = fig.x + 2; T1[1] = fig.y + 1; tockaSkak[0] = T1;
            int[] T2 = new int[2]; T2[0] = fig.x + 2; T2[1] = fig.y - 1; tockaSkak[1] = T2;
            int[] T3 = new int[2]; T3[0] = fig.x + 1; T3[1] = fig.y + 2; tockaSkak[2] = T3;
            int[] T4 = new int[2]; T4[0] = fig.x + 1; T4[1] = fig.y - 2; tockaSkak[3] = T4;
            int[] T5 = new int[2]; T5[0] = fig.x - 2; T5[1] = fig.y + 1; tockaSkak[4] = T5;
            int[] T6 = new int[2]; T6[0] = fig.x - 2; T6[1] = fig.y - 1; tockaSkak[5] = T6;
            int[] T7 = new int[2]; T7[0] = fig.x - 1; T7[1] = fig.y + 2; tockaSkak[6] = T7;
            int[] T8 = new int[2]; T8[0] = fig.x - 1; T8[1] = fig.y - 2; tockaSkak[7] = T8;

            //pregledamo vsa nova polja
            for (int i = 0; i < 8; i++)
            {
                //če so na šahovnici...
                if ((tockaSkak[i][0] < 8) && (tockaSkak[i][0] >= 0) && (tockaSkak[i][1] < 8) && (tockaSkak[i][1] >= 0))
                {
                    //če na njih ni lastne figure...jih dodamo
                    if ("" + this.sahovnica[tockaSkak[i][0], tockaSkak[i][1]][0] != fig.barvaFigure())
                    {
                        vsePot.Add(tockaSkak[i]);
                    }
                }
            }
            return vsePot;
        }

        /// <summary>
        /// metoda,ki za lovca izračuna vsa njegova delovanja
        /// </summary>
        /// <param name="fig">Object: objekt Figura,ki predstavlja lovca</param>
        /// <returns>list: seznam vseh potez lovca</returns>
        public List<int[]> Lovec(Figura fig)
        {
            List<int[]> vsePot = new List<int[]>();

            //definiramo vse smeri premikov lovca
            string[] vseSmeri = new string[4];
            vseSmeri[0] = "++";
            vseSmeri[1] = "+-";
            vseSmeri[2] = "-+";
            vseSmeri[3] = "--";

            //gremo v vsako smer
            foreach (string smer in vseSmeri)
            {
                //se premikamo po 1
                for (int i = 1; i < 8; i++)
                {
                    //tu shranjujemo koordinate novih polj
                    int novX = 42; int novY = 42;

                    //koordinate so odvisne od smeri in pa števila korakov
                    if (smer == "++")
                    {
                        novX = fig.x + i;
                        novY = fig.y + i;
                    }
                    if (smer == "+-")
                    {
                        novX = fig.x + i;
                        novY = fig.y - i;
                    }
                    if (smer == "-+")
                    {
                        novX = fig.x - i;
                        novY = fig.y + i;
                    }
                    if (smer == "--")
                    {
                        novX = fig.x - i;
                        novY = fig.y - i;
                    }

                    //preverimo nove koordinate,če so na šahovnici
                    if (novX >= 0 && novX < 8 && novY >= 0 && novY < 8)
                    {
                        //tvorimo tabelo iz novih koordinat
                        int[] potLov = new int[2];
                        potLov[0] = novX; potLov[1] = novY;

                        //če je novo polje prazno,je premik možen in nadaljujemo v tej smeri
                        if (this.sahovnica[novX, novY] == ".")
                        {
                            vsePot.Add(potLov);
                        }

                        //če je na polju nasprotnikova figura je premik možen,nadaljne poteze v tej smeri niso možne
                        if ((this.sahovnica[novX, novY] != ".") && ("" + this.sahovnica[novX, novY][0] != fig.barvaFigure()))
                        {
                            vsePot.Add(potLov);
                            break;
                        }

                        //če je na polju lastna figura,premik ni možen in prenehamo s premikanjem v tej smeri
                        if ("" + this.sahovnica[novX, novY][0] == fig.barvaFigure())
                        {
                            break;
                        }
                    }
                }
            }
            return vsePot;
        }

        /// <summary>
        /// metoda,ki za trdnjavo izračuna vsa njena delovanja
        /// </summary>
        /// <param name="fig">Object: objekt Figura,ki predstavlja trdnjavo</param>
        /// <returns>list: seznam vseh potez trdnjave</returns>
        public List<int[]> Trdnjava(Figura fig)
        {
            List<int[]> vsePot = new List<int[]>();

            //4 smeri premikov
            string[] vseSmeri = new string[4];
            vseSmeri[0] = "+0";
            vseSmeri[1] = "-0";
            vseSmeri[2] = "0+";
            vseSmeri[3] = "0-";

            //gremo v vsako smer
            foreach (string smer in vseSmeri)
            {
                //se premikamo po 1
                for (int i = 1; i < 8; i++)
                {
                    //tu shranjujemo koordinate novih polj
                    int novX = 42; int novY = 42;

                    //koordinate so odvisne od smeri in pa števila korakov
                    if (smer == "+0")
                    {
                        novX = fig.x + i;
                        novY = fig.y;
                    }
                    if (smer == "-0")
                    {
                        novX = fig.x - i;
                        novY = fig.y;
                    }
                    if (smer == "0+")
                    {
                        novX = fig.x;
                        novY = fig.y + i;
                    }
                    if (smer == "0-")
                    {
                        novX = fig.x;
                        novY = fig.y - i;
                    }

                    //preverimo nove koordinate,če so na šahovnici
                    if (novX >= 0 && novX < 8 && novY >= 0 && novY < 8)
                    {
                        //tvorimo tabelo iz novih koordinat
                        int[] potLov = new int[2];
                        potLov[0] = novX; potLov[1] = novY;

                        //če je novo polje prazno,je premik možen in nadaljujemo v tej smeri
                        if (this.sahovnica[novX, novY] == ".")
                        {
                            vsePot.Add(potLov);
                        }

                        //če je na polju nasprotnikova figura je premik možen,nadaljne poteze v tej smeri niso možne
                        if ((this.sahovnica[novX, novY] != ".") && ("" + sahovnica[novX, novY][0] != fig.barvaFigure()))
                        {
                            vsePot.Add(potLov);
                            break;
                        }

                        //če je na polju lastna figura,premik ni možen in prenehamo s premikanjem v tej smeri
                        if ("" + this.sahovnica[novX, novY][0] == fig.barvaFigure())
                        {
                            break;
                        }
                    }
                }
            }
            return vsePot;
        }

        /// <summary>
        /// metoda,ki za damo izračuna vsa njena delovanja(unija lovca in topa)
        /// </summary>
        /// <param name="fig">Object: Figura,ki predstavlja damo</param>
        /// <returns>list: seznam vseh potez dame</returns>
        public List<int[]> Dama(Figura fig)
        {
            List<int[]> vsePot = new List<int[]>();

            //spremenimo damo v trdnjavo in izračunamo delovanja
            this.sahovnica[fig.x, fig.y] = this.kdo + "T";
            fig.ime = "T";

            vsePot = Trdnjava(fig);

            //nato spremenimo še v lovca,izračunamo delovanja in dodamo
            this.sahovnica[fig.x, fig.y] = this.kdo + "L";
            fig.ime = "L";

            vsePot.AddRange(Lovec(fig));

            //na koncu še pozicijo vrnemo nazaj
            this.sahovnica[fig.x, fig.y] = this.kdo + "D";
            fig.ime = "D";

            return vsePot;
        }

        /// <summary>
        /// metoda,ki za kralja izračuna vsa njegova delovanja(brez rošad
        /// </summary>
        /// <param name="fig">Object: objekt Figura,ki predstavlja kralja</param>
        /// <returns>list: seznam vseh potez kralja</returns>
        public List<int[]> Kralj(Figura fig)
        {
            List<int[]> vsePot = new List<int[]>();

            //generiramo tabelo tabel,vseh novih koordinat kralja
            int[][] tockaKralj = new int[8][];

            int[] T1 = new int[2]; T1[0] = fig.x + 1; T1[1] = fig.y; tockaKralj[0] = T1;
            int[] T2 = new int[2]; T2[0] = fig.x + 1; T2[1] = fig.y + 1; tockaKralj[1] = T2;
            int[] T3 = new int[2]; T3[0] = fig.x + 1; T3[1] = fig.y - 1; tockaKralj[2] = T3;
            int[] T4 = new int[2]; T4[0] = fig.x - 1; T4[1] = fig.y; tockaKralj[3] = T4;
            int[] T5 = new int[2]; T5[0] = fig.x - 1; T5[1] = fig.y + 1; tockaKralj[4] = T5;
            int[] T6 = new int[2]; T6[0] = fig.x - 1; T6[1] = fig.y - 1; tockaKralj[5] = T6;
            int[] T7 = new int[2]; T7[0] = fig.x; T7[1] = fig.y + 1; tockaKralj[6] = T7;
            int[] T8 = new int[2]; T8[0] = fig.x; T8[1] = fig.y - 1; tockaKralj[7] = T8;

            //pregledamo vsa nova polja
            for (int i = 0; i < 8; i++)
            {
                //če so na šahovnici...
                if ((tockaKralj[i][0] < 8) && (tockaKralj[i][0] >= 0) && (tockaKralj[i][1] < 8) && (tockaKralj[i][1] >= 0))
                {
                    //če na njih ni lastne figure...jih dodamo
                    if ("" + this.sahovnica[tockaKralj[i][0], tockaKralj[i][1]][0] != fig.barvaFigure())
                    {
                        vsePot.Add(tockaKralj[i]);
                    }
                }
            }
            return vsePot;
        }

        /// <summary>
        /// metoda,ki v dani poziciji izračuna položaj kralja(tistega,ki je na potezi)
        /// </summary>
        /// <returns>int[]: tabela,ki označuje položaj kralja</returns>
        public int[] KjeKralj()
        {
            int[] rez = new int[2];

            //pregleda vse figure v slovarju Figure
            foreach (var pair in Figure)
            {
                //če naleti na ustreznega kralja vrne njegove koordinate
                if (pair.Value.IzpisFigure() == this.kdo + "K")
                {
                    rez[0] = pair.Value.x;
                    rez[1] = pair.Value.y;
                    return rez;
                }
            }
            return rez;
        }

        /// <summary>
        /// metoda,ki za trenutno pozicijo izračuna ali je dano polje napadeno s strani igralca na potezi
        /// </summary>
        /// <param name="bar">char: barva igralca na potezi</param>
        /// <returns>bool: vrne logično vrednost, ali je igralec v šahu</returns>
        public bool JeFiguraNapadena(int[] polje)
        {
            //določimo vsa delovanja
            Dictionary<Figura, List<int[]>> poteze;
            poteze = VsePoteze();

            //če se igralčev kralj nahaja na kateremkoli polju delovanja nasprotnikovih figur vrnemo true
            foreach (List<int[]> potezeEne in poteze.Values)
            {
                foreach (int[] pot in potezeEne)
                    if (pot[0] == polje[0] && pot[1] == polje[1])
                        return true;
            }
            //drugače pa false
            return false;
        }

        /// <summary>
        /// metoda,ki pove ali je v poziciji igralec na potezi matiran(predvideva da je nekaj od tega res!)
        /// </summary>
        /// <returns>bool: ali je igralec na potezi matiran(true) ali patiran(false)</returns>
        public bool jeMatAliPat()
        {

            int[] polozajKralja = this.KjeKralj();
            this.Naslednji();
            if (this.JeFiguraNapadena(polozajKralja))
            {
                this.Naslednji();
                return true;
            }
            this.Naslednji();
            return false;
        }

        /// <summary>
        /// metoda,ki naredi premik ene figure na šahovnici s tem da ustvari nov objekt Sahovnica
        /// </summary>
        /// <param name="x">int: vrsta novega polja figure</param>
        /// <param name="y">int: stolpec novega polja figure</param>
        /// <param name="f">Object: objekt Figura,katero želimo premakniti</param>
        /// <returns>Object: objekt Sahovnica,ki predstavlja novo sahovnico</returns>
        public Sahovnica Premik(int novX, int novY, Figura f)
        {
            //novi sahovnici najprej naredimo ustrezno pozicijo
            string[,] novaSahovnica = (string[,])this.sahovnica.Clone();
            novaSahovnica[f.x, f.y] = ".";
            novaSahovnica[novX, novY] = f.IzpisFigure();

            //preverimo ali bo še možno narediti rokade
            bool novaMozMalRokBeli = this.moznaMalaRokadaBeli;
            bool novaMozMalRokCrni = this.moznaMalaRokadaCrni;
            bool novaMozVelRokBeli = this.moznaVelikaRokadaBeli;
            bool novaMozVelRokCrni = this.moznaVelikaRokadaCrni;
            int novPravilo50 = this.pravilo50+1;
            List<int[]> noviMozniEP = new List<int[]>();

            //če je prislo do jemanja ali se premakne kmet...zacnemo pravilo50 steti od zacetka
            if (f.ImeFigure() == "P" || this.sahovnica[novX, novY] != ".")
                novPravilo50 = 0;

            //pogledamo če bodo v naslednji potezi mozni EP
            if (f.ImeFigure() == "P")
            {
                if (this.kdo == "b")
                {
                    if (f.x == 1 && novX == 3)
                    {
                        if (f.y < 7 && f.y > 0)
                        {
                            if (this.sahovnica[3, f.y - 1] == "cP")
                            {
                                noviMozniEP.Add(new int[2] { f.y - 1, f.y });
                            }
                            if (this.sahovnica[3, f.y + 1] == "cP")
                                noviMozniEP.Add(new int[2] { f.y + 1, f.y });
                        }
                        else if (f.y == 7 && this.sahovnica[3, f.y - 1] == "cP")
                            noviMozniEP.Add(new int[2] { f.y - 1, f.y });
                        else if (f.y == 0 && this.sahovnica[3, f.y + 1] == "cP")
                            noviMozniEP.Add(new int[2] { f.y + 1, f.y });
                    }
                }
                if (this.kdo == "c")
                {
                    if (f.x == 6 && novX == 4)
                    {
                        if (f.y < 7 && f.y > 0)
                        {
                            if (this.sahovnica[4, f.y - 1] == "bP")
                                noviMozniEP.Add(new int[2] { f.y - 1, f.y });
                            if (this.sahovnica[4, f.y + 1] == "bP")
                                noviMozniEP.Add(new int[2] { f.y + 1, f.y });
                        }
                        else if (f.y == 7 && this.sahovnica[4, f.y - 1] == "bP")
                            noviMozniEP.Add(new int[2] { f.y - 1, f.y });
                        else if (f.y == 0 && this.sahovnica[4, f.y + 1] == "bP")
                            noviMozniEP.Add(new int[2] { f.y + 1, f.y });
                    }
                }
            }
            if (noviMozniEP.Count() == 0)
                noviMozniEP = null;

            //če beli premakne katero od trdnjav ali kralja
            if (f.x == 0)
            {
                if (f.y == 0)
                    novaMozVelRokBeli = false;

                else if (f.y == 7)
                    novaMozMalRokBeli = false;

                else if (f.y == 4)
                {
                    novaMozMalRokBeli = false;
                    novaMozVelRokBeli = false;
                }
            }
            
            //če črni premakne katero od trdnjav ali kralja 
            else if (f.x == 7)
            {
                if (f.y == 0)
                    novaMozVelRokCrni = false;

                else if (f.y == 7)
                    novaMozMalRokCrni = false;

                else if (f.y == 4)
                {
                    novaMozMalRokCrni = false;
                    novaMozVelRokCrni = false;
                }
            }
            //če je bil izveden premik na katero od mest trdnjav ali kralja
            if (novX == 0)
            {
                if (novY == 0)
                    novaMozVelRokBeli = false;
                else if (novY == 7)
                    novaMozMalRokBeli = false;
                else if (novY == 4)
                {
                    novaMozMalRokBeli = false;
                    novaMozVelRokBeli = false;
                }
            }
            else if (novX == 7)
            {
                if (novY == 0)
                    novaMozVelRokCrni = false;
                else if (novY == 7)
                    novaMozMalRokCrni = false;
                else if (novY == 4)
                {
                    novaMozMalRokCrni = false;
                    novaMozVelRokCrni = false;
                }

            }

            //nato ustvarimo nov objekt Sahovnica z novimi parametri
            Sahovnica sah = new Sahovnica(novaSahovnica, this.kdo, novaMozMalRokBeli, novaMozMalRokCrni, novaMozVelRokBeli, novaMozVelRokCrni, noviMozniEP, novPravilo50,this.kateraPoteza+1);

            return sah;
        }

        /// <summary>
        /// metoda,ki izvrši malo rokado
        /// </summary>
        /// <returns>Sahovnica: vrne nov objekt Sahovnica,ki predstavlja novo pozicijo po mali rokadi</returns>
        public Sahovnica IzvediMaloRokado()
        {
            //preverimo ali bo še možno narediti rokade
            bool novaMozMalRokBeli = this.moznaMalaRokadaBeli;
            bool novaMozMalRokCrni = this.moznaMalaRokadaCrni;
            bool novaMozVelRokBeli = this.moznaVelikaRokadaBeli;
            bool novaMozVelRokCrni = this.moznaVelikaRokadaCrni;

            string naslednji = ".";
            string[,] novaSahovnica = null;
            if (this.kdo == "b")
            {
                novaSahovnica = (string[,])this.sahovnica.Clone();
                novaSahovnica[0, 4] = ".";
                novaSahovnica[0, 5] = "bT";
                novaSahovnica[0, 6] = "bK";
                novaSahovnica[0, 7] = ".";

                naslednji = "c";

                novaMozMalRokBeli = false;
                novaMozVelRokBeli = false;
            }
            else
            {
                novaSahovnica = (string[,])this.sahovnica.Clone();
                novaSahovnica[7, 4] = ".";
                novaSahovnica[7, 5] = "cT";
                novaSahovnica[7, 6] = "cK";
                novaSahovnica[7, 7] = ".";

                naslednji = "b";

                novaMozMalRokCrni = false;
                novaMozVelRokCrni = false;
            }
            Sahovnica sah = new Sahovnica(novaSahovnica, naslednji, novaMozMalRokBeli, novaMozMalRokCrni, novaMozVelRokBeli, novaMozVelRokCrni, null, this.pravilo50,this.kateraPoteza+1);

            return sah;
        }

        /// <summary>
        /// metoda,ki izvrši veliko rokado
        /// </summary>
        /// <returns>Sahovnica: vrne nov objekt Sahovnica,ki predstavlja novo pozicijo po veliki rokadi</returns>
        public Sahovnica IzvediVelRokado()
        {
            //preverimo ali bo še možno narediti rokade
            bool novaMozMalRokBeli = this.moznaMalaRokadaBeli;
            bool novaMozMalRokCrni = this.moznaMalaRokadaCrni;
            bool novaMozVelRokBeli = this.moznaVelikaRokadaBeli;
            bool novaMozVelRokCrni = this.moznaVelikaRokadaCrni;

            string naslednji = ".";
            string[,] novaSahovnica = null;
            if (this.kdo == "b")
            {
                novaSahovnica = (string[,])this.sahovnica.Clone();
                novaSahovnica[0, 4] = ".";
                novaSahovnica[0, 3] = "bT";
                novaSahovnica[0, 2] = "bK";
                novaSahovnica[0, 0] = ".";

                naslednji = "c";

                novaMozMalRokBeli = false;
                novaMozVelRokBeli = false;
            }
            else
            {
                novaSahovnica = (string[,])this.sahovnica.Clone();
                novaSahovnica[7, 4] = ".";
                novaSahovnica[7, 3] = "cT";
                novaSahovnica[7, 2] = "cK";
                novaSahovnica[7, 0] = ".";

                naslednji = "b";

                novaMozMalRokCrni = false;
                novaMozVelRokCrni = false;
            }
            Sahovnica sah = new Sahovnica(novaSahovnica, naslednji, novaMozMalRokBeli, novaMozMalRokCrni, novaMozVelRokBeli, novaMozVelRokCrni, null, this.pravilo50,this.kateraPoteza+1);

            return sah;
        }

        /// <summary>
        /// metoda,ki izvede premik kmeta na zadnjo vrsto in s tem promocijo
        /// </summary>
        /// <param name="vKaj">string: figuro v katero kmet promovira</param>
        /// <param name="y">int: stolpec kmeta</param>
        /// <param name="novY">int: nov stolpec kmeta</param>
        /// <returns>object: nov objekt Sahovnica po promociji</returns>
        public Sahovnica Promocija(string vKaj, int y, int novY)
        {
            //preverimo ali bo še možno narediti rokade
            bool novaMozMalRokBeli = this.moznaMalaRokadaBeli;
            bool novaMozMalRokCrni = this.moznaMalaRokadaCrni;
            bool novaMozVelRokBeli = this.moznaVelikaRokadaBeli;
            bool novaMozVelRokCrni = this.moznaVelikaRokadaCrni;

            string[,] novaSahovnica = (string[,])this.sahovnica.Clone();
            if (this.kdo == "b")
            {
                novaSahovnica[6, y] = ".";
                novaSahovnica[7, novY] = "" + this.kdo + vKaj;

                if (novY == 0)
                    novaMozVelRokCrni = false;
                else if (novY == 7)
                    novaMozMalRokCrni = false;
                else if (novY == 4)
                {
                    novaMozMalRokCrni = false;
                    novaMozVelRokCrni = false;
                }


            }
            else
            {
                novaSahovnica[1, y] = ".";
                novaSahovnica[0, novY] = "" + this.kdo + vKaj;

                if (novY == 0)
                    novaMozVelRokBeli = false;
                else if (novY == 7)
                    novaMozMalRokBeli = false;
                else if (novY == 4)
                {
                    novaMozMalRokBeli = false;
                    novaMozVelRokBeli = false;
                }
            }
            return new Sahovnica(novaSahovnica, this.KdoNaslednji(), novaMozMalRokBeli, novaMozMalRokCrni, novaMozVelRokBeli, novaMozVelRokCrni, null, 0,this.kateraPoteza+1);

        }

        public Sahovnica IzvediEP(int y, int novY)
        {
            int vrsta;
            if (this.kdo == "b")
                vrsta = 4;
            else
                vrsta = 3;

            string[,] novaSahovnica = (string[,])this.sahovnica.Clone();
            novaSahovnica[vrsta, y] = ".";
            novaSahovnica[vrsta, novY] = ".";
            if (vrsta == 4)
                novaSahovnica[5, novY] = "bP";
            else
                novaSahovnica[2, novY] = "cP";

            return new Sahovnica(novaSahovnica, this.KdoNaslednji(), this.moznaMalaRokadaBeli, this.moznaMalaRokadaCrni, this.moznaVelikaRokadaBeli, this.moznaVelikaRokadaCrni, null, 0,this.kateraPoteza+1);
        }

        /// <summary>
        /// metoda,ki preveri,če je v dani poziciji možna mala rokada
        /// </summary>
        /// <returns>bool: je/ni možna</returns>
        public bool PreveriMalRokada()
        {
            if (this.kdo == "b")
            {
                //če sploh lahko beli še izvede malo rokado
                if (this.moznaMalaRokadaBeli == false)
                    return false;

                //vsa polja med trdnjavo in kraljem morajo biti prosta ter kralj in trdnjava na ustreznih mestih
                if (this.sahovnica[0, 4] != "bK" || this.sahovnica[0, 5] != "." || this.sahovnica[0, 6] != "." || this.sahovnica[0, 7] != "bT")
                    return false;

                this.Naslednji();

                //nobeno polje,ki je na kraljevi poti ne sme biti napadeno
                if (JeFiguraNapadena(new int[] { 0, 4 }) == true || JeFiguraNapadena(new int[] { 0, 5 }) == true || JeFiguraNapadena(new int[] { 0, 6 }) == true)
                {
                    this.Naslednji();
                    return false;
                }
                this.Naslednji();
            }

            else if (this.kdo == "c")
            {
                //če sploh lahko crni še izvede malo rokado
                if (this.moznaMalaRokadaCrni == false)
                    return false;

                //vsa polja med trdnjavo in kraljem morajo biti prosta ter kralj in trdnjava na ustreznih mestih
                if (this.sahovnica[7, 4] != "cK" || this.sahovnica[7, 5] != "." || this.sahovnica[7, 6] != "." || this.sahovnica[7, 7] != "cT")
                    return false;

                this.Naslednji();

                //nobeno polje,ki je na kraljevi poti ne sme biti napadeno
                if (JeFiguraNapadena(new int[] { 7, 4 }) == true || JeFiguraNapadena(new int[] { 7, 5 }) == true || JeFiguraNapadena(new int[] { 7, 6 }) == true)
                {
                    this.Naslednji();
                    return false;
                }
                this.Naslednji();
            }

            //če je vse izpoljeno je mala rokada možna
            return true;
        }

        /// <summary>
        /// metoda,ki preveri,če je v dani poziciji moćna velika rokada
        /// </summary>
        /// <returns>bool: je/ni možna</returns>
        public bool PreveriVelRokada()
        {
            if (this.kdo == "b")
            {
                //če sploh lahko beli še izvede veliko rokado
                if (this.moznaVelikaRokadaBeli == false)
                    return false;

                //vsa polja med trdnjavo in kraljem morajo biti prosta ter kralj in trdnjava na ustreznih mestih
                if (this.sahovnica[0, 4] != "bK" || this.sahovnica[0, 3] != "." || this.sahovnica[0, 2] != "." || this.sahovnica[0, 1] != "." || this.sahovnica[0, 0] != "bT")
                    return false;

                this.Naslednji();

                //nobeno polje,ki je na kraljevi poti ne sme biti napadeno
                if (JeFiguraNapadena(new int[] { 0, 4 }) == true || JeFiguraNapadena(new int[] { 0, 3 }) == true || JeFiguraNapadena(new int[] { 0, 2 }) == true)
                {
                    this.Naslednji();
                    return false;
                }
            }

            else if (this.kdo == "c")
            {
                //če sploh lahko crni še izvede veliko rokado
                if (this.moznaVelikaRokadaCrni == false)
                    return false;

                //vsa polja med trdnjavo in kraljem morajo biti prosta ter kralj in trdnjava na ustreznih mestih
                if (this.sahovnica[7, 4] != "cK" || this.sahovnica[7, 3] != "." || this.sahovnica[7, 2] != "." || this.sahovnica[7, 1] != "." || this.sahovnica[7, 0] != "cT")
                    return false;

                this.Naslednji();
                //nobeno polje,ki je na kraljevi poti ne sme biti napadeno
                if (JeFiguraNapadena(new int[] { 7, 4 }) == true || JeFiguraNapadena(new int[] { 7, 3 }) == true || JeFiguraNapadena(new int[] { 7, 2 }) == true)
                {
                    this.Naslednji();
                    return false;
                }
            }

            this.Naslednji();
            //če je vse izpoljeno je velika rokada možna
            return true;
        }

        /// <summary>
        /// metoda,ki da potezo naslednjemu igralcu
        /// </summary>
        public void Naslednji()
        {
            if (this.kdo == "b")
                this.kdo = "c";
            else
                this.kdo = "b";
        }

        /// <summary>
        /// metoda,ki vrne niz,ki označuje barvo igralca,ki ni na potezi
        /// </summary>
        /// <returns>string: barva igralca,ki ni na potezi</returns>
        public string KdoNaslednji()
        {
            if (this.kdo == "b")
                return "c";
            else
                return "b";
        }

        /// <summary>
        /// metoda,ki pozicijo spremeni v ustrezen niz pozicije
        /// </summary>
        /// <returns>string: niz,ki prikaže pozicijo 8x8</returns>
        public string PovejPozicijoZaIzpis()
        {
            string izpis = "";
            int stevec = 1;
            foreach (string polje in this.sahovnica)
            {
                izpis += polje;

                if (polje == ".")
                    izpis += " ";
                if (stevec % 8 == 0)
                    izpis += "\n";
                stevec++;
            }
            return izpis;
        }

        /// <summary>
        /// metoda,ki našteje vse figure,ki so na šahovnici
        /// </summary>
        /// <returns>string: primeren izpis figur</returns>
        public string NastejFigure()
        {
            string izpis = "";
            foreach (var pair in Figure)
            {
                string kje = pair.Key;
                Figura fig = pair.Value;

                izpis += "" + fig.barvaFigure() + fig.ImeFigure() + " : ";
                izpis += kje + "\n";

            }
            return izpis;
        }

        /// <summary>
        /// metoda,ki vrne sahovnico
        /// </summary>
        /// <returns>string[,]: dvodimenzialna tabela,ki predstavlja sahovnico</returns>
        public string[,] vrniSahovnico()
        {
            return this.sahovnica;
        }

        /// <summary>
        /// metoda,ki preveri,če je na danem polju figura igralca,ki je na potezi
        /// </summary>
        /// <param name="vrsta">int: vrsta</param>
        /// <param name="stolp">int: stolpec</param>
        /// <returns>bool: ali je ustrezna figura na polju</returns>
        public bool jeNaPoljuFiguraNaPotezi(int vrsta, int stolp)
        {
            if (this.sahovnica[vrsta, stolp] != ".")
                if ("" + this.sahovnica[vrsta, stolp][0] == this.kdo)
                    return true;
            return false;
        }

        /// <summary>
        /// vrne objekt figure igralca na potezi,če le ta obstaja
        /// </summary>
        /// <param name="vrsta">int: vrsta</param>
        /// <param name="stolp">int: stolpec</param>
        /// <returns>object: Objekt figura, ki predstavlja figuro,ki je na tem polju</returns>
        public Figura vrniFiguroNaPolju(int vrsta, int stolp)
        {
            if (!jeNaPoljuFiguraNaPotezi(vrsta, stolp))
                return null;
            return Figure["" + vrsta + stolp];
        }
    }
}
