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
        private Graphics gFlaeche , g; //Zeichenfläche in der PictureBox
        private Bitmap terrain, rock, castle, captured, end, mainmenu; //Bitmaps für Texturen

        //Hauptmenu
        private Bitmap cattura,tmp, button_start, button_editor,button_beenden;

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
        private Boolean hover_button_attack;

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
            mainmenu = new Bitmap(Properties.Resources.background);
            button_start = new Bitmap(Properties.Resources.button_spielstart);
            button_editor = new Bitmap(Properties.Resources.button_editor);
            button_beenden = new Bitmap(Properties.Resources.button_end);

            cattura = new Bitmap(Properties.Resources.cattura); 

            //Brush(es)
            selector = new SolidBrush(Color.FromArgb(120, 255, 255, 0));
            ActionText = new Font("Tahoma", 10, FontStyle.Bold);
            ActionAlpha = 0;
            action = "Attacke!";
            hover_button_attack = false;

            SBblack = new SolidBrush(Color.FromArgb(ActionAlpha, 0, 0, 0));
            SBwhite = new SolidBrush(Color.FromArgb(ActionAlpha + 80, 255, 255, 255));

            //Setze Fenster auf max Fenstergröße
            this.Height = Screen.PrimaryScreen.Bounds.Height-70;
            this.Width = Screen.PrimaryScreen.Bounds.Height-70;

            //Starte Haupmenu
            Hauptmenu.Start();

        }

        private void Spielbrett_Tick(object sender, EventArgs e) //Das muss überarbeitet werden, hatte bei mir nur so funktioniert
        {
            try
            {
                buffer = con.Allocate(pbSpielbrett.CreateGraphics(), pbSpielbrett.DisplayRectangle);
                gFlaeche = pbSpielbrett.CreateGraphics();

                //Das Spielfeld zeichnen
                for (Int32 i = 0; i < brett.getRange(); i++)
                {
                    for (Int32 j = 0; j < brett.getRange(); j++)
                    {
                        //Das originale Feld durchgehen
                        switch (brett.getRealFeld(i, j))
                        {
                            case 0:
                                buffer.Graphics.DrawImage(terrain, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                                break;

                            case 1:
                                if (brett.getLife(i, j) > 0)
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
                for (Int32 i = 0; i < brett.getplayers(); i++)
                    buffer.Graphics.DrawImage(brett.spieler[i].getBitmap(), brett.spieler[i].getpos_x() * brett.getflen() + brett.spieler[i].getanimationoffset_x(), brett.spieler[i].getpos_y() * brett.getflen() + brett.spieler[i].getanimationoffset_y(), brett.getflen() - 1, brett.getflen() - 1);
        
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
                    if(!Mover.Enabled)
                        UpdateAction();

                    if (!brett.AgetActionHide())
                        show_action();
                }
                else if (!brett.AgetActionHide())
                    brett.AtoggleActionHide();

                //Aktualisiere Infoleiste unten
                push_stats();

                //Buffer in gFlaeche zeichnen
                buffer.Render();
                buffer.Render(gFlaeche);

                //Freigeben
                buffer.Dispose();
                gFlaeche.Dispose();

                if (brett.LevelCleared())
                    brett.nextlevel(this);
            }
            catch { }
           
        }

        private void pbSpielbrett_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //Selektor setzen
                if (spielbrett.Enabled)
                {
                    if (brett.getgamestate() == 1)
                    {
                        if (brett.getFeld(e.X / brett.getflen(), e.Y / brett.getflen()) != 3)
                        {
                            brett.setFeld(e.X / brett.getflen(), e.Y / brett.getflen(), 3);

                            //Der Status des alten Feldes auf Original-Wert, damit das blinken des Selektors aufhört
                            brett.resetFeld(brett.getSelectorx(), brett.getSelectory());
                            brett.setSelectorx(e.X / brett.getflen());
                            brett.setSelectory(e.Y / brett.getflen());
                        }

                        //Hover-Button im Action-Menu
                        if (!brett.AgetActionHide() && (e.X >= (action_x + 7) && e.X <= (action_x + 130) && e.Y >= (action_y + 106) && e.Y <= (action_y + 139)))
                            hover_button_attack = true;

                        else if (hover_button_attack == true)
                            hover_button_attack = false;

                    }
                }

                //Hover-Button im Hauptmenu
                if (Hauptmenu.Enabled)
                {
                    if (IsStartbutton(e))
                        button_start = new Bitmap(Properties.Resources.button_spielstart_hover);
                    else
                        button_start = new Bitmap(Properties.Resources.button_spielstart);
                    if (IsEditorButton(e))
                        button_editor = new Bitmap(Properties.Resources.button_editor_hover);
                    else
                        button_editor = new Bitmap(Properties.Resources.button_editor);
                    if (IsExitButton(e))
                        button_beenden = new Bitmap(Properties.Resources.button_end_hover);
                    else
                        button_beenden = new Bitmap(Properties.Resources.button_end);

                }
            }
            catch { }
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

            if (!brett.AgetActionHide())
            {
                if (ActionAlpha < 150)
                    ActionAlpha += 10;
            }

            SBblack = new SolidBrush(Color.FromArgb(ActionAlpha, 0, 0, 0));
            SBwhite = new SolidBrush(Color.FromArgb(ActionAlpha + 80, 255, 255, 255));

            Double lifeinpercentages = (Convert.ToDouble(brett.getLife(brett.Agetpos_x(), brett.Agetpos_y())) / Convert.ToDouble(brett.getMaxLife()));
            balken = Convert.ToInt32(lifeinpercentages * 125);

            //Balkenfarbe einstellen
            if (lifeinpercentages > 0.5) Balkenfarbe = new SolidBrush(Color.FromArgb(ActionAlpha+105, Color.Lime));
            else if (lifeinpercentages > 0.3) Balkenfarbe = Brushes.Yellow;
            else Balkenfarbe = Brushes.Red;

            action_x = x;
            action_y = y;

            if (brett.getLife(brett.Agetpos_x(), brett.Agetpos_y()) > 0)
            {
                if (brett.Agetmovement() < 2)
                    action = "2 Kondition nötig";
                else if (action != "Attacke!")
                    action = "Attacke!";
            }
            else action = "Bereits erobert!";
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
            if (hover_button_attack)
            {
                buffer.Graphics.FillRectangle(Brushes.Orange, action_x + 7, action_y + 106, 123, 33);
                buffer.Graphics.DrawString(action, ActionText, SBblack, new RectangleF(action_x + 10, action_y + 115, 125, 33));
            }
            else
            {
                buffer.Graphics.FillRectangle(SBblack, action_x + 7, action_y + 106, 123, 33);
                buffer.Graphics.DrawString(action, ActionText, SBwhite, new RectangleF(action_x + 10, action_y + 115, 125, 33));
            }
            //Ende neues ActionPanel
        }

        //Nächster Zug
        public void next()
        {
           action = "Attacke!";
           brett.nextplayer();
           brett.Anextturn();
        }

        //Aktualisiere die Infoleiste unten
        private void push_stats()
        {
            //MessageBox.Show("Das Fenster ist: " + this.Width.ToString() + "x" + this.Height.ToString() + "\nDie PB: " + pbSpielbrett.Width.ToString() + "x" + pbSpielbrett.Height.ToString());

            //Rahmen
            buffer.Graphics.DrawRectangle(Pens.Black, 0 ,pbSpielbrett.Height - 30, pbSpielbrett.Width, 30);
            buffer.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(175, 0, 0, 0)), 0, pbSpielbrett.Height - 30, pbSpielbrett.Width , 30);

            //Texte
            buffer.Graphics.DrawString("Spieler " + (brett.getactive() + 1).ToString(), ActionText, Brushes.White, new Rectangle(10, pbSpielbrett.Height - 25, 100, 20));
            buffer.Graphics.DrawString("Kondition: " + brett.Agetmovement().ToString(), ActionText, Brushes.White, new Rectangle(pbSpielbrett.Width/2-60, pbSpielbrett.Height - 25, 200, 20));
            buffer.Graphics.DrawString("Punkte: " + brett.AgetPoints().ToString(), ActionText, Brushes.White, new Rectangle(pbSpielbrett.Width - 200, pbSpielbrett.Height - 25, 200, 20));

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //if (this.WindowState != FormWindowState.Minimized)
                //spielbrett.Start();

                pbSpielbrett.Height = this.Height - 27;
                pbSpielbrett.Width = this.Width - 3;
               
        }

        public void Neues_Spiel()
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

            if (input_validater == true)
            {
                try
                {
                    //Beende Haupmenu
                    Hauptmenu.Stop();
                    pbSpielbrett.BackgroundImage = null;

                    //Neues Spiel initialisieren

                    brett = new cbrett(this);
                    brett.generate_map();

                    brett.setmaxplayers(2);

                    //Spieler initialisieren
                    brett.initPlayers();

                    //Starte in Level x
                    for (Int32 i = 1; i < Convert.ToInt32(1); i++)
                    {
                        brett.nextlevel(this);
                    }

                    //<- Ende Neues Spiel                  

                    //Timer starten
                    spielbrett.Start();

                    //Starte in Runde 1 (Nicht 0)
                    brett.Anextturn();
                }
                catch (Exception ex)
                { MessageBox.Show("Unerwarteter Fehler. Scheinbar gab es ein Fehler beim erstellen der Map. Bitte versuche es erneut.\nDebug:\n\n" + ex.Message.ToString(), "Fehler beim Erstellen der Karte"); pbSpielbrett.BackgroundImage = Properties.Resources.background; Hauptmenu.Start(); }
            }      
        }

        private void pbSpielbrett_MouseClick(object sender, MouseEventArgs e)
        {
            //Clicks für Buttons usw.
            try
            {
                if (spielbrett.Enabled)
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
                            Attack_Castle();
                        }
                    }
                }
                else if(Hauptmenu.Enabled)
                {   //Im Hautpmenu
                        if(IsStartbutton(e))
                            Neues_Spiel();
                        if (IsExitButton(e))
                            this.Close();
                }
            }
            catch /*(NullReferenceException)*/{}

        }

        public void Attack_Castle()
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

        public Boolean IsMainMenuButtonRow(MouseEventArgs e)
        {
            if (e.X >= pbSpielbrett.Width / 2 - 122 && e.X <= pbSpielbrett.Width / 2 + 120)
                return true;
            return false;
        }

        public Boolean IsStartbutton(MouseEventArgs e)
        {
            if (IsMainMenuButtonRow(e))
            {
                if (e.Y >= pbSpielbrett.Height / 2 - 72 && e.Y <= pbSpielbrett.Height / 2 + 14)
                    return true;
                else
                    return false;
            }
            return false;
        }
        public Boolean IsEditorButton(MouseEventArgs e)
        {
            if (IsMainMenuButtonRow(e))
            {
                if (e.Y >= pbSpielbrett.Height / 2 + 35 && e.Y <= pbSpielbrett.Height / 2 + 122)
                    return true;
                else
                    return false;
            }
            return false;
        }
        public Boolean IsExitButton(MouseEventArgs e)
        {
            if (IsMainMenuButtonRow(e))
            {
                if (e.Y >= pbSpielbrett.Height / 2 + 150 && e.Y <= pbSpielbrett.Height / 2 + 235)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (spielbrett.Enabled)
                {
                    if (!Mover.Enabled && brett.getgamestate() == 1 && e.KeyCode!= Keys.Space)
                    {
                        key = e;
                        Mover_count = 0;
                        if (brett.Agetmovement() > 0 || e.KeyCode == Keys.E)
                        {
                            Mover.Start();
                        }
                    }
                    if (e.KeyCode == Keys.Space && brett.Agetmovement() >= 2 && brett.getLife(brett.Agetpos_x(), brett.Agetpos_y()) > 0)
                        Attack_Castle();
                }
                else if (Hauptmenu.Enabled)
                {
                    
                }
            }
            catch { /*NullReferenceException abfangen*/ }
        }

        private void Mover_Tick(object sender, EventArgs e)
        {           
            Mover_count++;

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
                        if(Mover_count==1)
                            next();
                        break;
                    case Keys.A:
                            brett.nextlevel(this);
                        break;
            }
                if (Mover_count == 10) { Mover.Stop(); Mover.Dispose(); brett.Aclean_target(); ActionAlpha = 0; }

            if (brett.AgetActionHide()) brett.AtoggleActionHide();
        }

        public void close_action()
        {
            //Schließe Action-Menu
            if (!brett.AgetActionHide() && brett.Agetmovement() > 0)
            {
                brett.AtoggleActionHide();
                ActionAlpha = 0;
            }
        }

        private void Hauptmenu_Tick(object sender, EventArgs e)
        {

            try
            {
                if (!spielbrett.Enabled || brett==null)
                {
                    g = pbSpielbrett.CreateGraphics();
                    
                    tmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                    Graphics h = Graphics.FromImage(tmp);
                    
                    h.DrawImage(mainmenu, 0, 0, pbSpielbrett.Width, pbSpielbrett.Height);
                    h.DrawImage(cattura, pbSpielbrett.Width / 2 - 250, 50, 500, 120);
                    h.DrawImage(button_start, pbSpielbrett.Width / 2 - 125, pbSpielbrett.Height / 2 - 75,250,92);
                    h.DrawImage(button_editor, pbSpielbrett.Width / 2 - 125, pbSpielbrett.Height / 2 + 35, 250, 92);
                    h.DrawImage(button_beenden, pbSpielbrett.Width / 2 - 125, pbSpielbrett.Width / 2 + 135, 250, 92);
                    g.DrawImage(tmp, 0, 0);
                    
                    //Freigeben
                    h.Dispose();
                    g.Dispose();
                    tmp.Dispose();    
                }
            }
            catch { }
        }     
    }  
}
