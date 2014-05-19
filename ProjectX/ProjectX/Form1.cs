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
            Main_Menu_Ticker.Stop();
        }


        public void zeichnen()
        {
            gFlaeche = pbSpielbrett.CreateGraphics();
            for (Int32 i = 0; i < brett.getRange(); i++)
            {
                for (Int32 j = 0; j < brett.getRange(); j++)
                {
                    gFlaeche.FillRectangle(Brushes.Gray, i *brett.getflen(), j *brett.getflen(), brett.getflen()-5, brett.getflen()-5);
                    
                }
            }
        }

    }
    public class cbrett
    {
        public Int32[,] Feld;

        private Int32 max_range;        // Status im Game                   

        private const Int32 default_range = 8;

        private Int32 flen;

        public cbrett(Int32 size,Int32 r)
        {
            this.max_range = r;

            //Für quadratische Fläche mit Felder Anzahl
            Feld = new Int32[r, r];
            flen = size / max_range;
        }

        public cbrett(Int32 size)
        {
            this.max_range = default_range;
            Feld = new Int32[this.max_range, this.max_range];
            flen = size / max_range;
        }

        public Int32 getRange()
        { return max_range; }

        public Int32 getflen()
        { return flen; }

    }
    
}
