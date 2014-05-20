using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectX
{
    public class cbrett
    {
        //Konstanten
        private const Int32 default_range = 8;

        //Map dient zur Darstellung und Feld enthält die tatsächlichen Werte
        private Int32[,] Feld, Map;

        //Begrenzer
        private Int32 max_disabled, max_castle, max_range;
        
        //Zähler für Generierung
        private Int32 count_disabled, count_castle;

        //Breite, bzw. Höhe eines Feldes
        private Int32 flen;

        private Int32 level; 

        private Int32 hover_count;

        //Allgemeiner Konstruktor      
        public cbrett(Form1 f)
        {
            count_disabled = 0;
            count_castle = 0;
            level = 0;

            max_range = Convert.ToInt32(f.tbRange.Text); //tbRange wurde public gemacht, für Interaktion
            
            Feld = new Int32[max_range, max_range];
            Map = new Int32[max_range, max_range];

            flen = f.Length / max_range;

            max_disabled = max_range * max_range / 3;
            max_castle = max_range * max_range / 3;

            hover_count = 0;
        }

        public void setFeld(Int32 i, Int32 j, Int32 val)
        {
            if (i < max_range && j < max_range)
                Map[i, j] = val;
        }

        public void resetFeld(Int32 i, Int32 j)
        {
            if (i < max_range && j < max_range)
                Map[i, j] = Feld[i, j];
            hoverclean();
        }
        public void generate_map()
        {
            Random zufall = new Random();

            //Zufällig Felder befüllen
            for (Int32 i = 0; i < max_range; i++)
            {
                for (Int32 j = 0; j < max_range; j++)
                {
                    Map[i, j] = zufall.Next(3);

                    switch (Map[i, j])
                    {
                        case 1:
                            if ((count_castle) < max_castle)
                                count_castle++;
                            else
                                Map[i, j] = 0;
                            break;

                        case 2:
                            if ((count_disabled) < max_disabled)
                                count_disabled++;
                            else
                                Map[i, j] = 0;
                            break;
                    }
                }
            }
            //<- Ende Felder befüllen


            //Deaktivierte Felder am Spielrand entfernen
            for (Int32 i = 0; i < max_range; i++)
            {
                for (Int32 j = 0; j < max_range; j++)
                {
                    if (i == 0 || j == 0 || i == max_range - 1 || j == max_range - 1)
                        if (Map[i, j] == 2)
                            Map[i, j] = 0;
                }
            }
            //<- Ende Deaktivierte Felder


            //Vertikale Doppelfelder minimieren
            for (Int32 i = 0; i < max_range; i++)
            {
                for (Int32 j = 0; j < max_range; j++)
                {
                    /*if (i > 0 && i < max_range - 1)
                        if (Map[i, j] == Map[i+1, j])
                            Map[i, j] = 0;
                    */
                    if (j > 0 && j < max_range - 1)
                        if (Map[i, j] == Map[i, j + 1])
                            Map[i, j] = 0;
                }

            }

            //Eingesperrte Burgen freilegen
            for (Int32 i = 0; i < max_range; i++)
            {
                for (Int32 j = 0; j < max_range; j++)
                {
                    if (Map[i, j] == 1)
                    {
                        if ((i > 0 && j > 0) && (i < max_range - 1 && j < max_range - 1))
                        {
                            if (Map[i + 1, j] == 2 && Map[i, j + 1] == 2 && Map[i - 1, j] == 2 && Map[i, j - 1] == 2)
                                Map[i, j] = 0;
                        }
                    }
                }
            }
            //<- Ende eingesperrte Burgen

            //Feld = Map;
            for (Int32 i = 0; i < max_range; i++)
            {
                for (Int32 j = 0; j < max_range; j++)
                {
                    Feld[i, j] = Map[i, j];
                }
            }
        }

        public void hoverup()
        {hover_count++;}
        public Int32 hoverstate()
        { return hover_count; }
        public void hoverclean()
        { hover_count = 0; }

        public Int32 getRange()
        { return max_range; }

        public Int32 getflen()
        { return flen; }

        public Int32 getFeld(Int32 a, Int32 b)
        {
            if (a >= max_range) a = max_range-1;
            if (b >= max_range) b = max_range-1;
            if (a < 0) a = 0;
            if (b < 0) b = 0;
            return Map[a, b]; 
        }

        public Int32 getRealFeld(Int32 a, Int32 b)
        {
            if (a >= max_range) a = max_range - 1;
            if (b >= max_range) b = max_range - 1;
            if (a < 0) a = 0;
            if (b < 0) b = 0;
            return Feld[a, b];
        }

    }
}
