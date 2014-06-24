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

        //Grafiken/Bitmaps
        private Graphics gFlaeche; //Zeichenfläche in der PictureBox
        private Bitmap terrain, rock, castle, end; //Bitmaps für Texturen

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
            terrain = new Bitmap(Properties.Resources.boden);
            rock = new Bitmap(Properties.Resources.rock);
            castle = new Bitmap(Properties.Resources.castle);
            end = new Bitmap(Properties.Resources.endturn);



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
                buffer.Graphics.DrawImage(brett.spieler[i].getBitmap(), brett.spieler[i].getpos_x() * brett.getflen() + brett.spieler[i].getanimationoffset_x(), brett.spieler[i].getpos_y() * brett.getflen() + brett.spieler[i].getanimationoffset_y(), brett.getflen() - 1, brett.getflen() - 1);
                                
                //buffer.Graphics.FillRectangle(Brushes.Blue, spieler[i].getpos_x() * brett.getflen() + spieler[i].getanimationoffset_x(), spieler[i].getpos_y() * brett.getflen() + spieler[i].getanimationoffset_y(), brett.getflen() - 1, brett.getflen() - 1);
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
            play();
            
        }


        //Spielschleife
        public void play()
        {
            if (!Mover.Enabled)
            {
                push_stats();
                if (brett.Agetmovement() == 0)
                {
                    //next();
                    pnlEnd.Visible = true;
                }

                if (brett.getRealFeld(brett.Agetpos_x(), brett.Agetpos_y()) == 1)
                {
                    if (pnlAction.Width == 0)
                    {//Positioniere Infofeld X
                        if (brett.Agetpos_x() <= brett.getRange() / 2)
                            pnlAction.Location = new Point(brett.Agetpos_x() * brett.getflen() + brett.getflen(), pnlAction.Location.Y);
                        else
                            pnlAction.Location = new Point(brett.Agetpos_x() * brett.getflen() - 138, pnlAction.Location.Y);

                        //Positioniere Infofeld Y
                        if (brett.Agetpos_y() <= brett.getRange() / 2)
                            pnlAction.Location = new Point(pnlAction.Location.X, brett.Agetpos_y() * brett.getflen());
                        else
                            pnlAction.Location = new Point(pnlAction.Location.X, brett.Agetpos_y() * brett.getflen()-151);
                    }
                    UpdateActionPanel();
                    
                    if(!brett.AgetActionHide())
                        show_panel(pnlAction, 138, 151);

                }
                else if (pnlAction.Width != 0)
                    hide_panel(pnlAction);

            }
            if (brett.LevelCleared())
            {
                brett.nextlevel(this,pbSpielbrett.Width);
            }
        }

        public void UpdateActionPanel()
        {
            lblLifegauge.Text = brett.getLife(brett.Agetpos_x(), brett.Agetpos_y()) + "/" + brett.getMaxLife(brett.Agetpos_x(), brett.Agetpos_y());
            lblAngriff.Text = brett.AgetAttack().ToString();
            lblKritisch.Text = brett.AgetCrit().ToString() + " %";
            Double lifeinpercentages = (Convert.ToDouble(brett.getLife(brett.Agetpos_x(), brett.Agetpos_y())) / Convert.ToDouble(brett.getMaxLife(brett.Agetpos_x(), brett.Agetpos_y())));
            pnlLeben.Width = Convert.ToInt32(lifeinpercentages * 125);
            if(lifeinpercentages>0.5) pnlLeben.BackColor = Color.Lime;
            else if(lifeinpercentages>0.3) pnlLeben.BackColor = Color.Yellow;
            else pnlLeben.BackColor = Color.Red;
        }

        public void hide_panel(Panel p)
        {p.Width = 0;  p.Height = 0;}

        public void show_panel(Panel p, Int32 w, Int32 h)
        {p.Width = w;  p.Height = h;}


        //Nächster Zug
        public void next()
        {
           pnlEnd.Visible = false;
           btnAttack.Text = "Attacke!";
           brett.nextplayer();
           brett.Anextturn();
        }

        //Aktualisiere alle Labels
        private void push_stats()
        {
            lblcondition.Text = brett.Agetmovement().ToString();
            lblPlayers.Text = (brett.getactive() + 1).ToString();
            lblTurn.Text = brett.AgetTurn().ToString();
            lblPoints.Text = brett.AgetPoints().ToString();
            lblLvl.Text = (brett.getLevel()).ToString();

            if (brett.getLife(brett.Agetpos_x(), brett.Agetpos_y()) > 0)
            {
                if (brett.Agetmovement() < 2)
                    btnAttack.Text = "2 Kondition nötig";
                else if (btnAttack.Text != "Attacke!")
                    btnAttack.Text = "Attacke!";
            }
            else btnAttack.Text = "Bereits erobert!";
           

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
                try
                {
                    gFlaeche.Clear(Color.Black);
                    gFlaeche.Dispose();
                }
                catch
                {/*Kein Grafics-Objekt*/}
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
                    //Neues Spiel initialisieren
                    brett = new cbrett(this, pbSpielbrett.Width);
                    brett.generate_map();

                    pnlInfo.Visible = true;
                    game_state = 1;
                    brett.setmaxplayers(Convert.ToInt32(tbPlayers.Text));

                    //Spieler initialisieren
                    brett.initPlayers();

                    //<- Ende Neues Spiel                  
                    hide_options();
                    
                    //Timer starten
                    spielbrett.Start();

                    //Starte in Runde 1 (Nicht 0)
                    brett.Anextturn();
                }
                catch (Exception ex)
                { MessageBox.Show("Unerwarteter Fehler. Scheinbar gab es ein Fehler beim erstellen der Map. Bitte versuche es erneut.\nDebug:\n\n" + ex.Message.ToString(), "Fehler beim Erstellen der Karte"); }
            }      
        }

        private void pbSpielbrett_MouseClick(object sender, MouseEventArgs e)
        {
            if (brett.getSelectorx() == brett.Agetpos_x() && brett.getSelectory() == brett.Agetpos_y())
            {
                if (brett.getRealFeld(brett.Agetpos_x(), brett.Agetpos_y()) == 1 && brett.AgetActionHide())
                    brett.AtoggleActionHide();
            }
        }

        public void hide_options()
        {
            gbLevelEditor.Visible = false;
            btnStart.Visible = false;
            btnStart.Enabled = false;
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
            if (Mover_count >= 9) { Mover.Stop(); Mover.Dispose(); brett.Aclean_target(); }
            Mover_count++;
            switch (key.KeyCode)
            {
                case Keys.Up:
                    if (brett.Agetpos_y() > 0 && brett.Agetmovement()>1)
                    {
                        if (Mover_count == 1 && brett.Agettarget_y() == -1 && brett.getRealFeld(brett.Agetpos_x(), brett.Agetpos_y() - 1) != 2)
                        {
                            brett.Asettarget(brett.Agetpos_x(), brett.Agetpos_y() - 1);
                            brett.Asetmovement(brett.Agetmovement() - 2);
                        }
                        if (brett.Agettarget_x() != -1 && brett.Agettarget_y() != -1)
                        {
                            if (brett.Agetpos_y() > brett.Agettarget_y())
                             hochlaufen(); 
                        }
                    }
                    break;
                case Keys.Down:
                    if (brett.Agetpos_y() < brett.getRange() - 1 && brett.Agetmovement() > 0)
                    {
                        if (Mover_count == 1) brett.Asetmovement(brett.Agetmovement() - 1);

                        if(brett.getRealFeld(brett.Agetpos_x(), brett.Agetpos_y() + 1) != 2)
                        {
                            while (brett.getRealFeld(brett.Agetpos_x(), brett.Agetpos_y() + 1) != 2 && brett.Agetpos_y() < brett.getRange() -1)
                            {
                                brett.Asetpos(brett.Agetpos_x(), brett.Agetpos_y() + 1);
                                runterfallen();
                            }
                        }
                    }
                    break;
                case Keys.Left:
                    if (brett.Agetpos_x() > 0 && brett.Agetmovement() > 0)
                    {
                        if (Mover_count == 1 && brett.Agettarget_x() == -1 && brett.getRealFeld(brett.Agetpos_x() - 1, brett.Agetpos_y()) != 2)
                        {
                            brett.Asettarget(brett.Agetpos_x() - 1, brett.Agetpos_y());
                            brett.Asetmovement(brett.Agetmovement() - 1);
                        }
                        if (brett.Agettarget_x() != -1 && brett.Agettarget_y() != -1)
                        {
                            if (brett.Agetpos_x() > brett.Agettarget_x())
                                linkslaufen();
                        }
                    }
                    break;
                case Keys.Right:
                    if (brett.Agetpos_x() < brett.getRange() - 1 && brett.Agetmovement() > 0)
                    {
                        if (Mover_count == 1 && brett.Agettarget_x() == -1 && brett.getRealFeld(brett.Agetpos_x() + 1, brett.Agetpos_y()) != 2)
                        {
                            brett.Asettarget(brett.Agetpos_x() + 1, brett.Agetpos_y());
                            brett.Asetmovement(brett.Agetmovement() - 1);
                        }
                        if (brett.Agettarget_x() != -1 && brett.Agettarget_y() != -1)
                        {
                            if (brett.Agetpos_x() < brett.Agettarget_x())
                                rechtslaufen();
                        }
                    }
                    break;
                case Keys.E:
                    if(Mover_count == 1)
                        next();
                    break;
                case Keys.A:
                    if (Mover_count == 1)
                    {
                        for (Int32 i = 0; i < brett.getRange(); i++)
                        {
                            for (Int32 j = 0; j < brett.getRange(); j++)
                            {
                                brett.Attack(i,j,100);
                            }
                        }
                    }
                    break;

            }
            hide_panel(pnlAction);
            if (brett.AgetActionHide()) brett.AtoggleActionHide();
        }


        //Methoden zum Laufen, nur für Animation
        public void hochlaufen()
        {
            if ((brett.Agetpos_y() * brett.getflen() + brett.Agetanimoffset_y() - brett.getStep()) > (brett.Agettarget_y() * brett.getflen()))
                brett.Asetanimoffset_y(brett.Agetanimoffset_y() - brett.getStep());
        }
        public void linkslaufen()
        {
            if ((brett.Agetpos_x() * brett.getflen() + brett.Agetanimoffset_x() - brett.getStep()) > (brett.Agettarget_x() * brett.getflen()))
                brett.Asetanimoffset_x(brett.Agetanimoffset_x() - brett.getStep());
        }
        public void rechtslaufen()
        {
            if ((brett.Agetpos_x() * brett.getflen() + brett.Agetanimoffset_x() + brett.getStep()) < (brett.Agettarget_x() * brett.getflen()))
                brett.Asetanimoffset_x(brett.Agetanimoffset_x() + brett.getStep());
        }
        public void runterfallen()
        {
            if ((brett.Agetpos_y() * brett.getflen() + brett.Agetanimoffset_y() + brett.getStep()) < (brett.Agettarget_y() * brett.getflen()))
                brett.Asetanimoffset_y(brett.Agetanimoffset_y() + brett.getStep());
        }

        private void pbSpielbrett_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAttack_Click(object sender, EventArgs e)
        {
            if (brett.Agetmovement() > 1 && brett.getLife(brett.Agetpos_x(), brett.Agetpos_y())>0)
            {
                brett.Asetmovement(brett.Agetmovement() - 2);
                brett.Attack(brett.Agetpos_x(), brett.Agetpos_y(), brett.AgetAttack());
                brett.AsetPoints(10);

                if (brett.getLife(brett.Agetpos_x(), brett.Agetpos_y()) == 0)
                    brett.AsetPoints(40);
            }

        }

        private void pnlEnd_Paint(object sender, PaintEventArgs e)
        {
            pnlEnd.CreateGraphics().DrawImage(end, 0, 0, 326, 44);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            brett.AtoggleActionHide();
            hide_panel(pnlAction);
        }
    }  
}
