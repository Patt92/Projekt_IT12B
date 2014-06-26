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
        //Spielbrettobjekt brett: siehe cbrett.cs
        public cbrett brett; //Spielbrettobjekt für Zugriff auf Felder

        //Konstanten
        public const Int32 max_level = 50;

        //Grafiken/Bitmaps
        private Graphics gFlaeche; //Zeichenfläche in der PictureBox
        private Bitmap terrain, rock, castle, captured, end; //Bitmaps für Texturen

        //Buffer für flackerfreies Spielen
        public BufferedGraphicsContext con;
        public BufferedGraphics buffer;

        private Boolean input_validater; //Prüfvariable für Eingaben im Lvl-Editor
        private SolidBrush selector;
        private KeyEventArgs key;
        private Int32 Mover_count;

        //Action-Menu
        private Font ActionText;
        private Brush Balkenfarbe;
        private Int32 ActionAlpha;
        private Int32 balken;
        private SolidBrush SBblack, SBwhite;
        private Int32 action_x, action_y;
        private String action;

        public Form1()
        {
            InitializeComponent();

            //Buffermanager initialisieren
            con = BufferedGraphicsManager.Current;

            //Grafiken laden
            terrain = new Bitmap(Properties.Resources.boden);
            rock = new Bitmap(Properties.Resources.rock);
            castle = new Bitmap(Properties.Resources.castle);
            end = new Bitmap(Properties.Resources.endturn);
            captured = new Bitmap(Properties.Resources.castle_captured);

            //Brush(es)
            selector = new SolidBrush(Color.FromArgb(120, 255, 255, 0));
            ActionText = new Font("Tahoma", 10, FontStyle.Bold);
            ActionAlpha = 0;
            action = "Attacke!";
        }

        private void Spielbrett_Tick(object sender, EventArgs e) //Das muss überarbeitet werden, hatte bei mir nur so funktioniert
        {
            try
            {
                zeichnen();
            }
            catch { }
           
        }

        public void spieler_zeichnen()
        {
            //Spieler zeichnen
            for (Int32 i = 0; i < brett.getplayers(); i++)
                buffer.Graphics.DrawImage(brett.spieler[i].getBitmap(), brett.spieler[i].getpos_x() * brett.getflen() + brett.spieler[i].getanimationoffset_x(), brett.spieler[i].getpos_y() * brett.getflen() + brett.spieler[i].getanimationoffset_y(), brett.getflen() - 1, brett.getflen() - 1);
        }

        private void pbSpielbrett_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //Selektor setzen
                if (brett.getgamestate() == 1)
                    if (brett.getFeld(e.X / brett.getflen(), e.Y / brett.getflen()) != 3)
                    {
                        brett.setFeld(e.X / brett.getflen(), e.Y / brett.getflen(), 3);

                        //Der Status des alten Feldes auf Original-Wert, damit das blinken des Selektors aufhört
                        brett.resetFeld(brett.getSelectorx(), brett.getSelectory());
                        brett.setSelectorx(e.X / brett.getflen());
                        brett.setSelectory(e.Y / brett.getflen());
                    }
            }
            catch { }

        }


        public void zeichnen()
        {
            buffer = con.Allocate(pbSpielbrett.CreateGraphics(), pbSpielbrett.DisplayRectangle);
            gFlaeche = pbSpielbrett.CreateGraphics();

            //Das Spielfeld zeichnen
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
                            if(brett.getLife(i,j)>0)
                                buffer.Graphics.DrawImage(castle, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                            else
                                buffer.Graphics.DrawImage(captured, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                        break;

                        case 2:
                            buffer.Graphics.DrawImage(rock, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                        break;
                    }
                }
            }
            
            //Spieler zeichnen
            spieler_zeichnen();

            //Overlays zeichnen
            for (Int32 i = 0; i < brett.getRange(); i++)
            {
                for (Int32 j = 0; j < brett.getRange(); j++)
                {
                    //Das sichtbare Feld durchgehen
                    switch (brett.getFeld(i, j))
                    {
                        //selectstate ist für das Blinken zuständig
                        case 3:
                            if (brett.selectstate() > 0 && brett.selectstate() < 20)
                            {
                                buffer.Graphics.FillRectangle(selector, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                            }
                            brett.countselector();
                            if (brett.selectstate() >= 40) brett.selectorclean();
                            break;
                    }
                }
            }

            if (brett.getRealFeld(brett.Agetpos_x(), brett.Agetpos_y()) == 1)
            {
                if (!brett.AgetActionHide() && !Mover.Enabled)
                {
                    UpdateAction();
                    show_action();
                }
            }
            else if (!brett.AgetActionHide())
                brett.AtoggleActionHide();
                //   hide_panel(pnlAction);

            //Buffer in gFlaeche zeichnen
            buffer.Render();
            buffer.Render(gFlaeche);
            play();    
        }

        public void UpdateAction()
        {
            //Neues ActionPanel
            Int32 x, y;

            if (brett.Agetpos_x() <= brett.getRange() / 2)
                x = brett.Agetpos_x() * brett.getflen() + brett.getflen();
            else
                x = brett.Agetpos_x() * brett.getflen() - 138;

            //Positioniere Infofeld Y
            if (brett.Agetpos_y() <= brett.getRange() / 2)
                y = brett.Agetpos_y() * brett.getflen();
            else
                y = brett.Agetpos_y() * brett.getflen() - 150;

            
            if (ActionAlpha < 150)
                ActionAlpha += 25;

            SBblack = new SolidBrush(Color.FromArgb(ActionAlpha, 0, 0, 0));
            SBwhite = new SolidBrush(Color.FromArgb(ActionAlpha + 80, 255, 255, 255));

            Double lifeinpercentages = (Convert.ToDouble(brett.getLife(brett.Agetpos_x(), brett.Agetpos_y())) / Convert.ToDouble(brett.getMaxLife()));
            balken = Convert.ToInt32(lifeinpercentages * 125);

            //Balkenfarbe einstellen
            if (lifeinpercentages > 0.5) Balkenfarbe = Brushes.Lime;
            else if (lifeinpercentages > 0.3) Balkenfarbe = Brushes.Yellow;
            else Balkenfarbe = Brushes.Red;

            action_x = x;
            action_y = y;
        }

        public void show_action()
        {
            //Zeichne Feld
            buffer.Graphics.DrawRectangle(Pens.Gray, action_x, action_y, 138, 150);
            buffer.Graphics.FillRectangle(SBblack, action_x + 1, action_y + 1, 136, 148);

            //Werte eintragen
            buffer.Graphics.DrawString("Burg:", ActionText, SBwhite, new RectangleF(action_x + 7, action_y + 10, 50, 17));
            buffer.Graphics.DrawString(brett.getLife(brett.Agetpos_x(), brett.Agetpos_y()).ToString() + "/" + brett.getMaxLife().ToString(), ActionText, SBwhite, new RectangleF(action_x + 50, action_y + 10, 82, 17));
            buffer.Graphics.FillRectangle(Balkenfarbe, action_x + 7, action_y + 30, balken, 15);
            buffer.Graphics.DrawString("Angriff: " + brett.AgetAttack().ToString(), ActionText, SBwhite, new RectangleF(action_x + 7, action_y + 53, 138, 17));
            buffer.Graphics.DrawString("Kritisch: " + brett.AgetCrit().ToString() + " %", ActionText, SBwhite, new RectangleF(action_x + 7, action_y + 75, 138, 17));
            
            //Button
            buffer.Graphics.DrawRectangle(Pens.Gray, action_x+6, action_y+105, 125, 35);
            buffer.Graphics.FillRectangle(SBblack, action_x + 7, action_y + 106, 123, 33);
            buffer.Graphics.DrawString(action, ActionText, Brushes.White, new RectangleF(action_x + 10, action_y + 115, 125, 33));
            //Ende neues ActionPanel
        }

        //Spielschleife
        public void play()
        {
            if (!Mover.Enabled)
            {
                push_stats();
                if (brett.Agetmovement() == 0)
                    pnlEnd.Visible = true;

                UpdateAction();
            }
            else if(!brett.AgetActionHide())
                brett.AtoggleActionHide();

            if (brett.LevelCleared())
            {
                brett.nextlevel(this,pbSpielbrett.Width);
            }
        }

        public void UpdateActionPanel()
        {

            //ENTFERNEN!!
            //lblLifegauge.Text = brett.getLife(brett.Agetpos_x(), brett.Agetpos_y()) + "/" + brett.getMaxLife();
            //lblAngriff.Text = brett.AgetAttack().ToString();
            //lblKritisch.Text = brett.AgetCrit().ToString() + " %";
            //Double lifeinpercentages = (Convert.ToDouble(brett.getLife(brett.Agetpos_x(), brett.Agetpos_y())) / Convert.ToDouble(brett.getMaxLife()));
            //pnlLeben.Width = Convert.ToInt32(lifeinpercentages * 125);
            //if(lifeinpercentages>0.5) pnlLeben.BackColor = Color.Lime;
            //else if(lifeinpercentages>0.3) pnlLeben.BackColor = Color.Yellow;
            //else pnlLeben.BackColor = Color.Red;
        }

        //public void hide_panel(Panel p)
        //{p.Width = 0;  p.Height = 0;}

        //public void show_panel(Panel p, Int32 w, Int32 h)
        //{p.Width = w;  p.Height = h;}


        //Nächster Zug
        public void next()
        {
           if(pnlEnd.Visible) pnlEnd.Visible = false;
           action = "Attacke!";
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
                    action = "2 Kondition nötig";
                else if (action != "Attacke!")
                    action = "Attacke!";
            }
            else action = "Bereits erobert!";
           

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
            
            try
            {
                //Spieleranzahl ermitteln
                if (!(Convert.ToInt32(tbPlayers.Text) <= 4 && Convert.ToInt32(tbPlayers.Text) >= 2 && tbPlayers.Text!=String.Empty))
                {
                    input_validater = false;
                    MessageBox.Show("Die Spieleranzahl ist ungültig. Die Anzahl muss zwischen 2 und 4 liegen", "Fehler");
                }

                //Spielfeldradius prüfen
                if (Convert.ToInt32(tbRange.Text) < 6 || Convert.ToInt32(tbRange.Text) > 45)//hier wird eine Zusatzvariable eingefügt
                {
                    MessageBox.Show("Fehler mit dem Spielfeldradius. Der Radius zwischen 8 und 45 sein", "Fehler mit dem Spielfeldradius", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    input_validater = false;
                }

                //Level prüfen
                if (Convert.ToInt32(tbLevel.Text) < 0 || Convert.ToInt32(tbLevel.Text) > max_level)
                {
                    MessageBox.Show("Fehler mit dem Level. Das Level muss zwischen 1 und " + max_level + " liegen");
                    input_validater = false;
                }
            }
            catch (FormatException f)
            {
                input_validater = false;
                MessageBox.Show("Überprüfen Sie die die Eingabefelder auf Korrektheit!\nSie können auch den \"Zurücksetzen\"-Button verwenden.", f.Message, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            if (input_validater == true)
            {
                try
                {
                    //Neues Spiel initialisieren
                    
                    //Passe das Spiel an die Fenstergröße an
                    if (pbSpielbrett.Width < this.Width && this.Width >= 763 && this.Height>=763)
                    {
                        if (this.Width > this.Height - 80)
                        {
                            pbSpielbrett.Height = this.Height;
                            pbSpielbrett.Width = this.Height;
                            pbSpielbrett.Height -= 100;
                            pbSpielbrett.Width -= 100;
                        }
                        else if (this.Width >= 763 && this.Height >= 763)
                        {
                            pbSpielbrett.Height = this.Width;
                            pbSpielbrett.Width = this.Width;
                        }
                        pnlInfo.Location = new Point(pbSpielbrett.Width/2 - 374, this.Height - 100);
                    }

                    brett = new cbrett(this, pbSpielbrett.Width);
                    brett.generate_map();

                    pnlInfo.Visible = true;
                    brett.setmaxplayers(Convert.ToInt32(tbPlayers.Text));

                    //Spieler initialisieren
                    brett.initPlayers();

                    //Starte in Level x
                    for (Int32 i = 1; i < Convert.ToInt32(tbLevel.Text); i++)
                    {
                        brett.nextlevel(this, pbSpielbrett.Width);
                    }

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
            try
            {
                if (brett.getgamestate() == 1)
                {
                    if (brett.getSelectorx() == brett.Agetpos_x() && brett.getSelectory() == brett.Agetpos_y())
                    {
                        if (brett.getRealFeld(brett.Agetpos_x(), brett.Agetpos_y()) == 1 && brett.AgetActionHide())
                            brett.AtoggleActionHide();
                    }
                    if (e.X >= action_x + 6 && e.X <= action_x + 131 && e.Y >= action_y + 105 && e.Y <= action_y + 140)
                    {
                        if (brett.Agetmovement() > 1 && brett.getLife(brett.Agetpos_x(), brett.Agetpos_y()) > 0)
                        {
                            brett.Asetmovement(brett.Agetmovement() - 2);
                            brett.Attack(brett.Agetpos_x(), brett.Agetpos_y(), brett.AgetAttack());
                            brett.AsetPoints(10);

                            //Burg besiegt
                            if (brett.getLife(brett.Agetpos_x(), brett.Agetpos_y()) == 0)
                            {
                                brett.AsetPoints(40);
                            }
                        }
                    }
                      
                }
            }
            catch /*(NullReferenceException)*/{}
        }

        public void hide_options()
        {
            gbLevelEditor.Visible = false;
            btnStart.Visible = false;
            btnStart.Enabled = false;
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            tbRange.Text = "8";
            tbPlayers.Text = "2";
            tbLevel.Text = "1";
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!Mover.Enabled && brett.getgamestate() == 1)
                {
                    key = e;
                    Mover_count = 0;
                    Mover.Start();
                }
            }
            catch { /*NullReferenceException abfangen*/ }
        }

        private void Mover_Tick(object sender, EventArgs e)
        {
            //Halte den Ticker nach 9 Ticks an
            if (Mover_count >= 9) { Mover.Stop(); Mover.Dispose(); brett.Aclean_target(); }

            Mover_count++;

            if (!brett.AgetActionHide())
                brett.AtoggleActionHide();

            if (Mover_count == 1)
            {

                switch (key.KeyCode)
                {
                    case Keys.Up:
                            brett.hochlaufen();
                        break;
                    case Keys.Down:
                            brett.runterfallen();
                        break;
                    case Keys.Left:
                            brett.linkslaufen();
                        break;
                    case Keys.Right:
                            brett.rechtslaufen();
                        break;
                    case Keys.E:
                        next();
                        break;
                    case Keys.A:
                        brett.nextlevel(this, pbSpielbrett.Width);
                        break;
                }
            }
            
            if (brett.AgetActionHide()) brett.AtoggleActionHide();
        }

        private void btnAttack_Click(object sender, EventArgs e)
        {
            //ENTFERNEN!!!

        }

        private void pnlEnd_Paint(object sender, PaintEventArgs e)
        {
            pnlEnd.CreateGraphics().DrawImage(end, 0, 0, 326, 44);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            brett.AtoggleActionHide();
            //hide_panel(pnlAction);
        }

        private void pbSpielbrett_Paint(object sender, PaintEventArgs e)
        {

        }
    }  
}
