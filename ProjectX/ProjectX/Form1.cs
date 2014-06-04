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
        //Selbstdefinierte Klassen siehe <Klassennamen>.cs
        public cbrett brett; //Spielbrettobjekt für Zugriff auf Felder
        public cplayer[] spieler; //Spielerobjekte unbegrenzte Anzahl

        //Grafiken/Bitmaps
        private Graphics gFlaeche; //Zeichenfläche in der PictureBox
        private Bitmap terrain, rock, castle; //Bitmaps für Texturen
        
        private Int32 game_state; //Status im Spiel
        private Int32 Mover_count;

        //Buffer für flackerfreies Spielen
        public BufferedGraphicsContext con;
        public BufferedGraphics buffer;

        private Boolean input_validater; //Prüfvariable für Eingaben im Lvl-Editor
        private SolidBrush selector;
        private KeyEventArgs key;

        public Form1()
        {
            InitializeComponent();
            game_state = 0;

            con = BufferedGraphicsManager.Current;

            //Grafiken laden
            terrain = new Bitmap(Properties.Resources.stone);
            rock = new Bitmap(Properties.Resources.rock);
            castle = new Bitmap(Properties.Resources.house);
            
            //Brush(es)
            selector = new SolidBrush(Color.FromArgb(120, 255, 255, 0));

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
            { /*spielbrett.Stop(); game_state = 0; MessageBox.Show("Fehler: " + ex.Message);*/ }
        }

        public void spieler_zeichnen()
        {
            //Spieler zeichnen
            for (Int32 i = 0; i < brett.getplayers(); i++)
            {
                buffer.Graphics.FillRectangle(Brushes.Blue, spieler[i].getpos_x() * brett.getflen() + spieler[i].getanimationoffset_x(), spieler[i].getpos_y() * brett.getflen() + spieler[i].getanimationoffset_y(), brett.getflen() - 1, brett.getflen() - 1);
            }
        }

        private void pbSpielbrett_MouseMove(object sender, MouseEventArgs e)
        {
            //Auswahl im Feld Methode
            if (game_state > 0)
            {
                if (brett.getFeld(e.X / brett.getflen(),e.Y / brett.getflen())!=3)
                {
                    brett.setFeld(e.X / brett.getflen(), e.Y / brett.getflen(), 3);
                    
                    //Der Status des alten Feldes aufOriginal-Wert, damit das blinken des Selektors aufhört
                    brett.resetFeld(brett.getSelectorx(), brett.getSelectory());
                    brett.setSelectorx(e.X / brett.getflen());
                    brett.setSelectory(e.Y / brett.getflen());
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
                    //Das originale Feld durchgehen
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

                    //Das sichtbare Feld durchgehen
                    switch (brett.getFeld(i, j))
                    {
                        case 3:
                            if (brett.selectstate() < 2)
                            {
                                buffer.Graphics.FillRectangle(selector, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                            }
                            brett.drawselector();
                            if (brett.selectstate() == 3) brett.selectorclean();
                            break;
                    }
                }
                
                spieler_zeichnen();
            }
            //Buffer in GFlaeche zeichnen
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
                if (!(Convert.ToInt32(tbPlayers.Text) <= 4 && Convert.ToInt32(tbPlayers.Text) >= 2 && tbPlayers.Text!=String.Empty))
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
                MessageBox.Show("Überprüfen Sie die die Eingabefelder auf Korrektheit!\nSie können auch den \"Zurücksetzen\"-Button verwenden.", f.Message, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            //<- Ende Spieleranzahl ermitteln

            if (input_validater == true)
            {
                try
                {
                    brett = new cbrett(this, pbSpielbrett.Width);
                    //Neues Spiel initialisieren
                    brett.generate_map();
                    labels_on();
                    game_state = 1;
                    brett.setmaxplayers(Convert.ToInt32(tbPlayers.Text));

                    //Spieler initialisieren
                    spieler = new cplayer[brett.getplayers()];
                    for (Int32 i = 0; i < brett.getplayers(); i++)
                        spieler[i] = new cplayer(i, brett);

                    //<- Ende Neues Spiel                  
                    hide_options();
                    
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

        public void hide_options()
        {
            gbLevelEditor.Visible = false;
            btnStart.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbRange.Text = "8";
            tbPlayers.Text = "2";
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (!Mover.Enabled && brett != null) //&& (e.KeyCode == Keys.Up||e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                key = e;
                Mover_count = 0;
                Mover.Start();
            }
        }

        private void Mover_Tick(object sender, EventArgs e)
        {
            if (Mover_count >= 9) { Mover.Stop(); Mover.Dispose(); spieler[brett.getactive()].clean_target(); }
            Mover_count++;
            switch (key.KeyCode)
            {
                case Keys.Up:
                    if (spieler[brett.getactive()].getpos_y() > 0)
                    {
                        if (Mover_count == 1 && spieler[brett.getactive()].gettarget_y() == -1 && brett.getRealFeld(spieler[brett.getactive()].getpos_x(), spieler[brett.getactive()].getpos_y()-1) != 2)
                            spieler[brett.getactive()].settarget(spieler[brett.getactive()].getpos_x(), spieler[brett.getactive()].getpos_y()-1);

                        if (spieler[brett.getactive()].gettarget_x() != -1 && spieler[brett.getactive()].gettarget_y() != -1)
                        {
                            if (spieler[brett.getactive()].getpos_y() > spieler[brett.getactive()].gettarget_y())
                             hochlaufen(); 
                        }
                    }
                    break;
                case Keys.Down:
                    if (spieler[brett.getactive()].getpos_y() < brett.getRange() - 1)
                    {
                        if(brett.getRealFeld(spieler[brett.getactive()].getpos_x(), spieler[brett.getactive()].getpos_y() + 1) != 2)
                        {
                            while (brett.getRealFeld(spieler[brett.getactive()].getpos_x(), spieler[brett.getactive()].getpos_y() + 1) != 2 && spieler[brett.getactive()].getpos_y() < brett.getRange() -1)
                            {
                                spieler[brett.getactive()].setpos(spieler[brett.getactive()].getpos_x(), spieler[brett.getactive()].getpos_y() + 1);
                                runterfallen();
                            }
                        }
                    }
                    break;
                case Keys.Left:
                    if (spieler[brett.getactive()].getpos_x() > 0)
                    {
                        if (Mover_count == 1 && spieler[brett.getactive()].gettarget_x() == -1 && brett.getRealFeld(spieler[brett.getactive()].getpos_x()-1, spieler[brett.getactive()].getpos_y()) != 2)
                            spieler[brett.getactive()].settarget(spieler[brett.getactive()].getpos_x()-1, spieler[brett.getactive()].getpos_y());

                        if (spieler[brett.getactive()].gettarget_x() != -1 && spieler[brett.getactive()].gettarget_y() != -1)
                        {
                            if (spieler[brett.getactive()].getpos_x() > spieler[brett.getactive()].gettarget_x())
                                linkslaufen();
                        }
                    }
                    break;
                case Keys.Right:
                    if (spieler[brett.getactive()].getpos_x() < brett.getRange()-1)
                    {
                        if (Mover_count == 1 && spieler[brett.getactive()].gettarget_x() == -1 && brett.getRealFeld(spieler[brett.getactive()].getpos_x() +1, spieler[brett.getactive()].getpos_y()) != 2)
                            spieler[brett.getactive()].settarget(spieler[brett.getactive()].getpos_x() + 1, spieler[brett.getactive()].getpos_y());

                        if (spieler[brett.getactive()].gettarget_x() != -1 && spieler[brett.getactive()].gettarget_y() != -1)
                        {
                            if (spieler[brett.getactive()].getpos_x() < spieler[brett.getactive()].gettarget_x())
                                rechtslaufen();
                        }
                    }
                    break;

            }
            //spieler[brett.getactive()].setmovement(spieler[brett.getactive()].getmovement() - 1);
        }


        //Methoden zum Laufen, nur für Animation
        public void hochlaufen()
        {
            if ((spieler[brett.getactive()].getpos_y() * brett.getflen() + spieler[brett.getactive()].getanimationoffset_y() - brett.getStep()) > (spieler[brett.getactive()].gettarget_y() * brett.getflen()))
                spieler[brett.getactive()].setanimationoffset_y(spieler[brett.getactive()].getanimationoffset_y() - brett.getStep());
        }
        public void linkslaufen()
        {
            if ((spieler[brett.getactive()].getpos_x() * brett.getflen() + spieler[brett.getactive()].getanimationoffset_x() - brett.getStep()) > (spieler[brett.getactive()].gettarget_x() * brett.getflen()))
                spieler[brett.getactive()].setanimationoffset_x(spieler[brett.getactive()].getanimationoffset_x() - brett.getStep());
        }
        public void rechtslaufen()
        {
            if ((spieler[brett.getactive()].getpos_x() * brett.getflen() + spieler[brett.getactive()].getanimationoffset_x() + brett.getStep()) < (spieler[brett.getactive()].gettarget_x() * brett.getflen()))
                spieler[brett.getactive()].setanimationoffset_x(spieler[brett.getactive()].getanimationoffset_x() + brett.getStep());
        }
        public void runterfallen()
        {
            if ((spieler[brett.getactive()].getpos_y() * brett.getflen() + spieler[brett.getactive()].getanimationoffset_y() + brett.getStep()) < (spieler[brett.getactive()].gettarget_y() * brett.getflen()))
                spieler[brett.getactive()].setanimationoffset_y(spieler[brett.getactive()].getanimationoffset_y() + brett.getStep());
        }
    }  
}
