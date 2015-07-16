using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sah;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string[,] sahec1 =
            {{"bT",".",".",".","bK","bL","bS","bT"},
             {".",".","." ,".",".","." ,"." ,"."},
             {"." ,"." ,"." ,"." ,"." ,"." ,"." ,"." },
             {"." ,"." ,"." ,"." ,"." ,"." ,"." ,"." },
             {"." ,"." ,"." ,"." ,"." ,"." ,"." ,"." },
             {"." ,"." ,"." ,"." ,"." ,"." ,"." ,"." },
             {"." ,"." ,"." ,"." ,"." ,".",".","."},
             {"cT",".",".",".","cK","cL","cS","cT"}};
            Sahovnica A = new Sahovnica(sahec1, "c");
            
            string[,] sahec2 =
            {{"bK","bS","cD",".",".",".",".","."},
             {".",".","." ,".",".","." ,"." ,"."},
             {"." ,"cK" ,"." ,"." ,"." ,"." ,"." ,"." },
             {"." ,"." ,"." ,"." ,"." ,"." ,"." ,"." },
             {"." ,"." ,"." ,"." ,"." ,"." ,"." ,"." },
             {"." ,"." ,"." ,"." ,"." ,"." ,"." ,"." },
             {".",".",".",".",".",".",".","."},
             {".",".",".",".",".",".",".","."}};
            Sahovnica B = new Sahovnica(sahec2, "b");

            Sahovnica C = new Sahovnica();

            C.PovejPozicijoZaIzpis();
            
            */
           // Console.WriteLine(B.jeMatAliPat());

            //Console.WriteLine(A.PreveriVelRokada());

            //IzpisPozicije(A);
            //IzpišiVsePoteze(A);

            //Console.WriteLine();

            //IzpisPozicije(B);
            //IzpisiVseFigure(B);
            //IzpišiVsePoteze(B);

            //Console.WriteLine(B.PreveriMalRokada());
            //Console.WriteLine(B.PreveriVelRokada());

            //Figura fig = C.vrniFiguroNaPolju(1, 0);

        /*
            if (fig != null)
            {
                if (C.IzracunajPoteze().ContainsKey(fig))
                {
                    foreach (int[] polje in C.IzracunajPoteze()[fig])
                    {
                        Console.WriteLine("" + polje[0] + polje[1]);
                    }
                }
            }

            Console.WriteLine(C.vrniFiguroNaPolju(0, 1).IzpisFigure());

            string ime = "PB1";
            Console.WriteLine(""+ime[2]+" "+ (ime[1] - 'A'));
        
        */
        }
        /// <summary>
        ///metoda,ki izpiše niz 8x8,ki predstavlja pozicijo 
        /// </summary>
        /// <param name="Sah">Object: objekt Sahovnica</param>
        static public void IzpisPozicije(Sahovnica Sah)
        {
            string pozicija = Sah.PovejPozicijoZaIzpis();
            Console.WriteLine(pozicija);
        }

        /// <summary>
        /// metoda,ki za vse figure na sahovnici izpiše njihovo lokacijo,barvo in vrsto
        /// </summary>
        /// <param name="Sah">Object: objekt Sahovnica</param>
        static public void IzpisiVseFigure(Sahovnica Sah)
        {
            string vseFig = Sah.NastejFigure();
            Console.WriteLine(vseFig);
        }

        /// <summary>
        /// metoda,ki izpiše vse možne premike figur
        /// </summary>
        /// <param name="Sah">Object: objekt Sahovnica</param>
        static public void IzpisiMoznaPoteze(Sahovnica Sah)
        {
            Dictionary<Figura, List<int[]>> poteze = Sah.IzracunajPoteze();

            foreach (var pair in poteze)
            {
                string fig = pair.Key.IzpisFigure();
                string kam = "";
                foreach (int[] enaPoteza in pair.Value)
                {
                    kam += "" + enaPoteza[0] + enaPoteza[1] + " , ";
                }

                Console.WriteLine(fig + ": " + kam);
            }
        }

        /// <summary>
        /// meotda,ki izpiše vsa delovanja figur
        /// </summary>
        /// <param name="S">Object: objekt Sahovnica</param>
        static public void IzpišiVsePoteze(Sahovnica S)
        {
            foreach (var pair in S.VsePoteze())
            {
                Figura F = pair.Key;
                List<int[]> poteze = pair.Value;
                IzpišiPotezeEneFigure(F, poteze);

            }
        }

        static public void IzpišiPotezeEneFigure(Figura F, List<int[]> poteze)
        {
            string izpis = F.IzpisFigure() + ": ";
            foreach (int[] poteza in poteze)
            {
                izpis += "" + poteza[0] + poteza[1] + " , ";
            }

            Console.WriteLine(izpis);
        }
    }
}
