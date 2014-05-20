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
        public cplayer[] spieler;
        public Int32 Length,players;

        private Graphics gFlaeche;
        private Int32 game_state;
        private Int32 old_x, old_y;
        private Bitmap terrain, rock, castle;
        private Boolean input_validater;
        private SolidBrush hovering;
        public BufferedGraphicsContext con;
        public BufferedGraphics buffer;

        public Form1()
        {
            InitializeComponent();
            Length = pbSpielbrett.Size.Width;
            
            game_state = 0;
            old_x = 0;old_y = 0;

            con = BufferedGraphicsManager.Current;
            
            hovering = new SolidBrush(Color.FromArgb(120, 255, 255, 0));
            //Grafiken laden
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
            { spielbrett.Stop(); game_state = 0; MessageBox.Show("Fehler: " + ex.Message); }

        }

        public void spieler_zeichnen()
        {

            for (Int32 i = 0; i < players; i++)
            {
               buffer.Graphics.FillRectangle(Brushes.Blue, spieler[i].getpos_x() * brett.getflen(), spieler[i].getpos_y() * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
            }
        }

        private void pbSpielbrett_MouseMove(object sender, MouseEventArgs e)
        {
            //Hover Methode
            if (game_state > 0)
            {
                if (brett.getFeld(e.X / brett.getflen(),e.Y / brett.getflen())!=3)
                {
                    brett.setFeld(e.X / brett.getflen(), e.Y / brett.getflen(), 3);
                    brett.resetFeld(old_x, old_y);

                    old_x = e.X / brett.getflen();
                    old_y = e.Y / brett.getflen();
                }   
            }
        }


        public void zeichnen()
        {
            buffer = con.Allocate(pbSpielbrett.CreateGraphics(), pbSpielbrett.DisplayRectangle);
            gFlaeche = pbSpielbrett.CreateGraphics();

            for (Int32 i = 0; i < brett.getRange(); i++)
            {
                for (Int32 j = 0; j < brett.getRange(); j++)
                {
                    
                    switch(brett.getRealFeld(i,j))
                    {   case 0:
                            buffer.Graphics.DrawImage(terrain, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                        break;

                        case 1:
                        buffer.Graphics.DrawImage(castle, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                        break;

                        case 2:
                        buffer.Graphics.DrawImage(rock, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                        break;

                        
                    }
                    switch (brett.getFeld(i, j))
                    {
                        case 3:
                            if (brett.hoverstate() < 2)
                            {
                                buffer.Graphics.FillRectangle(hovering, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                            }
                            brett.hoverup();
                            if (brett.hoverstate() == 3) brett.hoverclean();
                            break;
                    }
                }

                spieler_zeichnen();
            }

            buffer.Render();
            buffer.Render(gFlaeche);
            
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
                spielbrett.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input_validater = true;

            if (gFlaeche != null)
            {
                gFlaeche.Clear(Color.Black);
                gFlaeche.Dispose();
            }

            //Spieleranzahl ermitteln
            try
            {
                if (Convert.ToInt32(tbPlayers.Text) <= 4 && Convert.ToInt32(tbPlayers.Text) >= 2)
                {
                    players = Convert.ToInt32(tbPlayers.Text);
                }
                else
                {
                    input_validater = false;
                    MessageBox.Show("Die Spieleranzahl ist ungültig. Die Anzahl muss zwischen 2 und 4 liegen", "Fehler");
                }

                if (Convert.ToInt32(tbRange.Text) < 8 || Convert.ToInt32(tbRange.Text) > 45)//hier wird eine Zusatzvariable eingefügt
                {
                    MessageBox.Show("Fehler mit dem Spielfeldradius. Der Radius zwischen 8 und 45 sein", "Fehler mit dem Spielfeldradius", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    input_validater = false;
                }
            }
            catch (FormatException f)
            {
                input_validater = false;
                MessageBox.Show("Überprüfen Sie die die Eingabefelder auf Korrektheit!", f.Message, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            //<- Ende Spieleranzahl ermitteln

            if (input_validater == true)
            {
                try
                {
                    brett = new cbrett(this);
                    //Neues Spiel initialisieren
                    brett.generate_map();
                    labels_on();
                    game_state = 1;

                    //Spieler initialisieren
                    spieler = new cplayer[players];
                    for (Int32 i = 0; i < players; i++)
                        spieler[i] = new cplayer(i, brett);

                    //<- Ende Neues Spiel

                    //Timer starten
                    spielbrett.Start();
                }
                catch (Exception ex)
                { MessageBox.Show("Unerwarteter Fehler. Scheinbar gab es ein Fehler beim erstellen der Map. Bitte versuche es erneut.\nDebug:\n\n" + ex.Message.ToString(), "Fehler beim Erstellen der Karte"); }
            }
        }

        private void pbSpielbrett_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        public void labels_on()
        {
            lblKondition.Visible = true;
            lblLevel.Visible = true;
            lblPunkte.Visible = true;
            lblRadius.Visible = true;
            lblSpieler.Visible = true;
            lblZug.Visible = true;
        }

        public void labels_off()
        {
            lblKondition.Visible = false;
            lblLevel.Visible = false;
            lblPunkte.Visible = false;
            lblRadius.Visible = false;
            lblSpieler.Visible = false;
            lblZug.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbRange.Text = "8";
            tbPlayers.Text = "2";
        }

    }  
}
