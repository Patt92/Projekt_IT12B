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
        private Bitmap hFlaeche;

        private Graphics gFlaeche;
        private Int32 Length;
        private Int32 game_state;
        private Point[] play;

        public Form1()
        {
            InitializeComponent();

            Length = pbSpielbrett.Size.Width;
            brett = new cbrett(Length);
            
            game_state = 0;
            brett.generate_map();
            Main_Menu_Ticker.Start();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void pbSpielbrett_Paint(object sender, PaintEventArgs e)
        {
            gFlaeche = this.CreateGraphics();
            gFlaeche.FillRectangle(Brushes.Blue, 50, (Length / 2) - 75, Length - 100, 150);
        }

        private void Main_Menu_Ticker_Tick(object sender, EventArgs e) //Das muss überarbeitet werden, hatte bei mir nur so funktioniert
        {
            try
            {
                /*if (game_state == 0)
                {
                    for(Int32 i=0;i<255;i++)
                    {
                        start_button(i);
                    }
                    Main_Menu_Ticker.Stop();
                }*/
                
                zeichnen();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: " + ex.Message);
            }

            Main_Menu_Ticker.Stop();
        }

        public void start_button(Int32 alpha)
        {
            /*SolidBrush button_color = new SolidBrush(Color.FromArgb(alpha,0, 0, 255));
            gFlaeche = pbSpielbrett.CreateGraphics();
            gFlaeche.FillRectangle(button_color, 50, (Length/2)-75, Length-100, 150);
            button_color = new SolidBrush(Color.FromArgb(alpha, 0, 255, 0));
            gFlaeche.FillPolygon(button_color, play);*/
        }

        private void pbSpielbrett_MouseMove(object sender, MouseEventArgs e)
        {
            //Main_Menu_Ticker.Stop();
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
                            gFlaeche.FillRectangle(Brushes.Gray, i *brett.getflen(), j *brett.getflen(), brett.getflen()-3, brett.getflen()-3);
                        break;

                        case 1:
                            gFlaeche.FillRectangle(Brushes.Brown, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 3, brett.getflen() - 3);
                        break;

                        case 2:
                        gFlaeche.FillRectangle(Brushes.Black, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 3, brett.getflen() - 3);
                        break;
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
            
            max_disabled = r;
            max_castle = r*2;

            count_disabled = 0;
            count_castle = 0;

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
        }

        public Int32 getRange()
        { return max_range; }

        public Int32 getflen()
        { return flen; }

        public Int32 getFeld(Int32 a, Int32 b)
        { return Map[a, b]; }

    }
    
}
