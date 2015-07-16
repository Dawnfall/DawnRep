using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sah
{
    public class Figura
    {
        public int x;
        public int y;
        public string ime;
        public string barva;

        /// <summary>
        /// konstruktor,ki usvtvari figuro
        /// </summary>
        /// <param name="pozX">int: vrsta</param>
        /// <param name="pozY">int: stolp</param>
        /// <param name="imeFig">string: vrsta figure</param>
        public Figura(int pozX, int pozY, string imeFig)
        {
            this.x = pozX; // vrsta
            this.y = pozY; // stolpec
            this.ime = ""+imeFig[1]; // ime figure
            this.barva = ""+imeFig[0]; //barva figue
        }

        /// <summary>
        /// metoda,ki vrne vrsto figure
        /// </summary>
        /// <returns>char: vrsta figure</returns>
        public string ImeFigure()
        {
            return this.ime;
        }

        /// <summary>
        /// metoda,ki vrne barvo figure
        /// </summary>
        /// <returns>char: barva figure</returns>
        public string barvaFigure()
        {
            return this.barva;
        }

        public string IzpisFigure()
        {
            return ("" + this.barva + this.ime);
        }
    }
}
