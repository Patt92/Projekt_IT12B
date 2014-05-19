using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectX
{

    public partial class Form1 : Form
    {
        public cbrett brett;

        private Graphics gFlaeche;
        private Int32 Length;
        private Int32 game_state;
        private Int32 old_x, old_y;
        private Bitmap terrain, rock, castle;

        public Form1()
        {
            InitializeComponent();

            Length = pbSpielbrett.Size.Width;
            game_state = 0;
            old_x = 0;
            old_y = 0;
            terrain = new Bitmap(Properties.Resources.stone);
            rock = new Bitmap(Properties.Resources.rock);
            castle = new Bitmap(Properties.Resources.house);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void pbSpielbrett_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Spielbrett_Tick(object sender, EventArgs e) //Das muss überarbeitet werden, hatte bei mir nur so funktioniert
        {
            try
            {   
                zeichnen();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: " + ex.Message);
            }

        }

        public void start_button(Int32 alpha)
        {
        }

        private void pbSpielbrett_MouseMove(object sender, MouseEventArgs e)
        {

            if (game_state > 0)
            {
                if (brett.getFeld(e.X / brett.getflen(),e.Y / brett.getflen())!=3)
                {
                    brett.setFeld(e.X / brett.getflen(), e.Y / brett.getflen(), 3);
                    brett.resetFeld(old_x, old_y);

                    old_x = e.X / brett.getflen();
                    old_y = e.Y / brett.getflen();
                }
                //purge(e.X, e.Y); Alter Befehl
            }
        }


        public void zeichnen()
        {
            gFlaeche = pbSpielbrett.CreateGraphics();
            for (Int32 i = 0; i < brett.getRange(); i++)
            {
                for (Int32 j = 0; j < brett.getRange(); j++)
                {
                    
                    switch(brett.getFeld(i,j))
                    {   case 0:
                            gFlaeche.DrawImage(terrain, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                        break;

                        case 1:
                            gFlaeche.DrawImage(castle, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                        break;

                        case 2:
                            gFlaeche.DrawImage(rock, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                        break;

                        case 3:
                            gFlaeche.FillRectangle(Brushes.Yellow, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                        break;


                    }
                }
            }
            
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
                spielbrett.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (gFlaeche != null)
            {   //hier
                gFlaeche.Clear(Color.Black);
                gFlaeche.Dispose();
            }
            
    

            try
            {
                brett = new cbrett(Length, Convert.ToInt32(tbRange.Text));
                try
                {

                    brett.generate_map();

                    lblKondition.Visible = true;
                    lblLevel.Visible = true;
                    lblPunkte.Visible = true;
                    lblRadius.Visible = true;
                    lblSpieler.Visible = true;
                    lblZug.Visible = true;

                    spielbrett.Start();
                    game_state = 1;
                }
                catch
                {
                    MessageBox.Show("Unerwarteter Fehler. Scheinbar gab es ein Fehler beim erstellen der Map. Bitte versuche es erneut.", "Fehler beim Erstellen der Karte");
                }
                    
            }
            catch
            {
                MessageBox.Show("Der Range ist ungültig. Er sollte zwischen 8 und 20 liegen", "Fehler mit dem Spielfeldradius");
            }
        }

        private void Cleanup_Tick(object sender, EventArgs e)
        {
            
        }

        public void purge(Int32 x, Int32 y)
        {
            for (Int32 i = 0; i < brett.getRange(); i++)
            {
                for (Int32 j = 0; j < brett.getRange(); j++)
                {
                    if (brett.getFeld(i, j) == 3)
                    {
                        if (x / brett.getflen() != i || y / brett.getflen() != j)
                            brett.resetFeld(i, j);
                    }
                }
            }
        }

    }
    public class cbrett
    {
        private Int32[,] Feld;
        private Int32[,] Map;

        private Int32 max_disabled;
        private Int32 max_castle;
        private Int32 count_disabled;
        private Int32 count_castle;
        
        private Int32 max_range;        // Status im Game                   
        private const Int32 default_range = 8;

        public Int32 temp;
        private Int32 flen;

        public cbrett(Int32 size,Int32 r)
        {
            this.max_range = r;

            //Für quadratische Fläche mit Felder Anzahl
            Feld = new Int32[r, r];
            Map = new Int32[r, r];

            flen = size / max_range;
            
            max_disabled = r*r/3;
            max_castle = r*r/3;

            count_disabled = 0;
            count_castle = 0;

        }

        public void setFeld(Int32 i,Int32 j,Int32 val)
        {   if(i<max_range&&j<max_range)
                Map[i,j] = val;
        } 

        public void resetFeld(Int32 i, Int32 j)
        {
            if (i < max_range && j < max_range)
                Map[i, j] = Feld[i, j]; 
        }

        public cbrett(Int32 size)
        {
            this.max_range = default_range;
            Feld = new Int32[this.max_range, this.max_range];
            Map = new Int32[this.max_range, this.max_range];

            flen = size / max_range;
            max_disabled = default_range*2;
            max_castle = default_range*2;
            
            count_disabled = 0;
            count_castle = 0;
        }

        public void generate_map()
        {
            Random zufall = new Random();

            //Zufällig Felder befüllen
            for (Int32 i = 0; i < max_range; i++)
            {
                for (Int32 j = 0; j < max_range; j++)
                {
                    Map[i,j] = zufall.Next(3);

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
                        if (Map[i, j] == Map[i, j+1])
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
                        if ((i > 0 && j > 0) && (i < max_range-1 && j < max_range-1))
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

                    Feld[i,j] = Map[i, j];
                }
            }
        }

        public Int32 getRange()
        { return max_range; }

        public Int32 getflen()
        { return flen; }

        public Int32 getFeld(Int32 a, Int32 b)
        { return Map[a, b]; }

    }
    
}
