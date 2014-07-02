using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//Zum Speichern
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ProjectX
{

    public partial class Form1 : Form
    {
        //Spielbrettobjekt brett: siehe cbrett.cs
        public cbrett brett; //Spielbrettobjekt für Zugriff auf Felder
        
        public ceditor spieleditor;

        //Konstanten
        public const Int32 max_level = 50;
        public const Int32 button_width = 250;
        public const Int32 button_height = 92;

        //Grafiken/Bitmaps
        private Graphics gFlaeche , g; //Zeichenfläche in der PictureBox
        private Bitmap terrain, rock, castle, captured, end, mainmenu; //Bitmaps für Texturen

        //Hauptmenu
        private Bitmap cattura,tmp, button_start, button_editor,button_beenden,button_weiter;

        //Editor
        private Bitmap editor_level,editor_radius,editor_players, editor_map,chk_on, chk_off,help;

        //Buffer für flackerfreies Spielen
        public BufferedGraphicsContext con;
        public BufferedGraphics buffer;

        private SolidBrush selector;
        private KeyEventArgs key;
        private Int32 Mover_count;

        //Action-Menu
        private Font ActionText,SettingsText;
        private Brush Balkenfarbe;
        private Int32 ActionAlpha;
        private Int32 balken;
        private SolidBrush SBblack, SBwhite;
        private Int32 action_x, action_y;
        private String action;
        private Boolean hover_button_attack;

        //Speichern - Snapshot Style
        //Speichern mit 'S', Laden mit 'L'
        private String save;
        private BinaryFormatter bin;
        private FileStream stream;
        public cbrett save_info;

        //Benachrichtigungen
        private String Nachricht;
        private Boolean Notifier_Enabled;
        private Int32 Notifier_Ticks;
        private Int32 Notifier_Long;

        //Anleitung
        private String manual_page;


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
            button_weiter = new Bitmap(Properties.Resources.button_weiter);
            editor_level = new Bitmap(Properties.Resources.editor_level);
            editor_radius = new Bitmap(Properties.Resources.editor_radius);
            editor_players = new Bitmap(Properties.Resources.editor_players);
            editor_map = new Bitmap(Properties.Resources.editor_map);
            chk_off = new Bitmap(Properties.Resources.chk_off);
            chk_on = new Bitmap(Properties.Resources.chk_on);
            help = new Bitmap(Properties.Resources.book);

            cattura = new Bitmap(Properties.Resources.cattura); 

            //Brush(es)
            selector = new SolidBrush(Color.FromArgb(120, 255, 255, 0));
            SBblack = new SolidBrush(Color.FromArgb(ActionAlpha, 0, 0, 0));
            SBwhite = new SolidBrush(Color.FromArgb(ActionAlpha + 80, 255, 255, 255));

            //Aktions-Menü
            ActionText = new Font("Tahoma", 10, FontStyle.Bold);
            SettingsText = new Font("Tahoma", 20, FontStyle.Bold);
            ActionAlpha = 0;
            action = "Attacke!";
            hover_button_attack = false;


            //Setze Fenster auf max Fenstergröße
            this.Height = Screen.PrimaryScreen.Bounds.Height-70;
            this.Width = Screen.PrimaryScreen.Bounds.Height-70;

            Notifier_Ticks = 0;
            Notifier_Enabled = false;
            Notifier_Long = 0;
            manual_page = "Intro";

            save = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!Directory.Exists(save + "\\Cattura"))
            {
                Directory.CreateDirectory(save + "\\Cattura");
                MessageBox.Show("Du scheinst Cattura zum ersten Mal zu spielen. Bitte nimm dir kurz Zeit für die Grundlagen", "Cattura");
                Anleitung.Start();
            }
            else
            {
                Hauptmenu.Start();
            }
            save += "\\Cattura\\game.save";
            //Starte Haupmenu

        }


        private void Spielbrett_Tick(object sender, EventArgs e)
        {
            try
            {
                buffer = con.Allocate(pbSpielbrett.CreateGraphics(), pbSpielbrett.DisplayRectangle);
                gFlaeche = pbSpielbrett.CreateGraphics();

                //Das Spielfeld zeichnen
                for (Int32 i = 0; i < brett.getRange(); i++)
                    for (Int32 j = 0; j < brett.getRange(); j++)
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

                //Spieler zeichnen
                for (Int32 i = 0; i < brett.getplayers(); i++)
                {
                    buffer.Graphics.DrawImage(brett.spieler[i].getBitmap(), brett.spieler[i].getpos_x() * brett.getflen() + brett.spieler[i].getanimationoffset_x(), brett.spieler[i].getpos_y() * brett.getflen() + brett.spieler[i].getanimationoffset_y(), brett.getflen() - 1, brett.getflen() - 1);
                    if (brett.spieler[i].getPlayerNr() != brett.getactive() && brett.Agetpos_x() == brett.spieler[i].getpos_x() && brett.Agetpos_y() == brett.spieler[i].getpos_y())
                    {
                        brett.runterfallen(i);
                    }
                }

                //Overlays zeichnen
                for (Int32 i = 0; i < brett.getRange(); i++)
                    for (Int32 j = 0; j < brett.getRange(); j++)
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

                //Zonen zeichnen
                Pen Dashpen = new Pen(Color.White);
                Dashpen.DashPattern = new float[] { 5.0F, 5.0F };

                for(Int32 i=1;i<=2;i++)
                    buffer.Graphics.DrawLine(Dashpen, new Point(0, brett.getZonefield(i) * brett.getflen()-1), new Point(brett.getflen() * brett.getRange(), brett.getZonefield(i) * brett.getflen()-1));


                Dashpen.Dispose();

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

                //Zeichne Benachrichtigungen, falls aktiviert
                Notifier();

                //Buffer in gFlaeche zeichnen
                buffer.Render();
                buffer.Render(gFlaeche);

                //Freigeben
                buffer.Dispose();
                gFlaeche.Dispose();

                if (brett.LevelCleared())
                {
                    Int32 max = 0;
                    for (Int32 i = 0; i < brett.getplayers(); i++)
                        if (brett.spieler[i].getPoints() > brett.spieler[max].getPoints()) max = i;
                    Nachricht = "Sieger: Spieler " + (brett.spieler[max].getPlayerNr()+1).ToString();
                    Notifier_Ticks = 0;
                    Notifier_Enabled = true;
                    brett.nextlevel(this);
                }
            }
            catch { }
           
        }

        public void Notifier()
        {
            if (Notifier_Enabled && Notifier_Ticks < 100)
            {
                Notifier_Ticks++;
                buffer.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, 0, 0, 0)), 20, 20, 200+Notifier_Long, 35);
                buffer.Graphics.DrawString(Nachricht, new System.Drawing.Font("Tahoma",15), Brushes.Red, new Rectangle(25, 23, 180+Notifier_Long, 25));
            }
            else
            {
                Notifier_Ticks = 0;
                Notifier_Long = 0;
                Notifier_Enabled = false;
            }
        }

        private void pbSpielbrett_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //Selektor setzen
                if (Spiel.Enabled)
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
                    if (IsBookButton(e))
                        help = new Bitmap(Properties.Resources.book_hover);
                    else
                        help = new Bitmap(Properties.Resources.book);
                }
            }
            catch { }
        }
        public Boolean IsBookButton(MouseEventArgs e)
        {
            if (e.X >= 10 && e.X <= 100 && e.Y >= pbSpielbrett.Height - 100 && e.Y <= pbSpielbrett.Height - 10)
                return true;
            return false;
        }

        //Update das Aktions-Menü auf Burgen
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

        //Zeige das Aktions-Menü
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
            buffer.Graphics.DrawString("Kondition: " + brett.Agetmovement().ToString() + " (+" + Convert.ToInt32(brett.getRange() - brett.getLevel() / 10).ToString()+")", ActionText, Brushes.White, new Rectangle(pbSpielbrett.Width / 2 - 60, pbSpielbrett.Height - 25, 200, 20));
            buffer.Graphics.DrawString("Punkte: " + brett.AgetPoints().ToString(), ActionText, Brushes.White, new Rectangle(pbSpielbrett.Width - 200, pbSpielbrett.Height - 25, 200, 20));
        }

        //Wenn die Formgröße verändert wird, passe die Picturebox daran an.
        private void Form1_Resize(object sender, EventArgs e)
        {
            //if (this.WindowState != FormWindowState.Minimized)
                //spielbrett.Start();

                pbSpielbrett.Height = this.Height - 27;
                pbSpielbrett.Width = this.Width - 3;
        }

        public void Neues_Spiel()
        {

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
                    //Beende Haupmenu
                    Hauptmenu.Stop();
                    pbSpielbrett.BackgroundImage = null;

                    //Initialisiere das Spielbrett
                    brett = new cbrett(this);

                    //Erstelle eine Karte
                    brett.generate_map();

                    //Spieler initialisieren
                    brett.setmaxplayers(2);
                    brett.initPlayers();

                    //Starte in Level x
                    for (Int32 i = 1; i < Convert.ToInt32(1); i++)
                        brett.nextlevel(this);             

                    //Timer starten
                    Spiel.Start();

                    //Starte in Runde 1 (Nicht 0)
                    brett.Anextturn();
                }
                catch (Exception ex)
                { MessageBox.Show("Unerwarteter Fehler. Scheinbar gab es ein Fehler beim erstellen der Map. Bitte versuche es erneut.\nDebug:\n\n" + ex.Message.ToString(), "Fehler beim Erstellen der Karte"); pbSpielbrett.BackgroundImage = Properties.Resources.background; Hauptmenu.Start(); }    
        }


        private void pbSpielbrett_MouseClick(object sender, MouseEventArgs e)
        {
            //Clicks für Buttons usw.
            try
            {
                //Ingame Mausklicks
                if (Spiel.Enabled)
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

                //Hauptmenü-Klicks
                else if(Hauptmenu.Enabled)
                {   //Im Hautpmenu
                    if (IsStartbutton(e))
                    {
                        if (File.Exists(save))
                        {
                            Hauptmenu.Stop();
                            Spiel_Laden.Start();
                        }
                        else
                            Neues_Spiel();
                    }
                    if (IsEditorButton(e))
                        Start_Editor();
                    if (IsExitButton(e))
                       this.Close();
                    if (IsBookButton(e))
                    {
                        Hauptmenu.Stop();
                        Anleitung.Start();
                    }
                }
                else if (Anleitung.Enabled)
                {
                    Anleitung_Next();
                }

                //Editor-Klicks
                else if (Editor.Enabled)
                {
                    //Checkbox
                    if (spieleditor.getpage() != "Karte")
                    {
                        if (e.X >= pbSpielbrett.Width / 2 + 85 && e.X <= pbSpielbrett.Width / 2 + 133)
                            if (e.Y >= 350 && e.Y <= 398)
                                spieleditor.toggle_map();
                        //Weiterbutton
                        if (e.X >= 10 && e.X <= +260)
                            if (e.Y >= 450 && e.Y <= 542)
                                spieleditor.nextpage();

                        //Leveldown
                        if (DownRow(e))
                        {
                            if (e.Y >= 68 && e.Y <= 96)
                                spieleditor.sub_level();
                            if (e.Y >= 168 && e.Y <= 196)
                                spieleditor.sub_radius();
                            if (e.Y >= 268 && e.Y <= 296)
                                spieleditor.sub_spieler();
                        }
                        if (UpRow(e))
                        {
                            if (e.Y >= 68 && e.Y <= 96)
                                spieleditor.add_level();
                            if (e.Y >= 168 && e.Y <= 196)
                                spieleditor.add_radius();
                            if (e.Y >= 268 && e.Y <= 296)
                                spieleditor.add_spieler();
                        }
                    }
                    else
                    {
                        brett.toggleFeld(e.X / brett.getflen(), e.Y / brett.getflen());
                    }
                }
                else if (Spiel_Laden.Enabled)
                {
                    if (e.Y >= pbSpielbrett.Height / 2 + 60 && e.Y <= pbSpielbrett.Height / 2 + 90)
                    {
                        if (e.X >= pbSpielbrett.Width / 2 - 100 && e.X <= pbSpielbrett.Width / 2 - 50)
                        {
                            Laden();
                            brett.fix_window(this);
                            Spiel_Laden.Stop();
                            Spiel.Start();
                        }
                        if (e.X >= pbSpielbrett.Width / 2 && e.X <= pbSpielbrett.Width / 2 + 75)
                        {
                            Neues_Spiel();
                        }  
                    }
                    
                }
            }
            catch /*(NullReferenceException)*/{}
        }

        public Boolean DownRow(MouseEventArgs e)
        {
            if (e.X >= pbSpielbrett.Width / 2 + 130 && e.X <= pbSpielbrett.Width / 2 + 180)
                return true;
            return false;
        }
        public Boolean UpRow(MouseEventArgs e)
        {
            if (e.X >= pbSpielbrett.Width / 2 + 180 && e.X <= pbSpielbrett.Width / 2 + 230)
                return true;
            return false;
        }


        //Greife aktive Burg an
        public void Attack_Castle()
        {
            Random zufall = new Random(); //Für krittischen Schaden
            brett.Asetmovement(brett.Agetmovement() - 2);
            Int32 dmg = brett.AgetAttack();
            if (zufall.Next(0, 101) <= brett.AgetCrit()) dmg *= 3; //Wenn Krittischer Hit, Schaden mal 3
            brett.Attack(brett.Agetpos_x(), brett.Agetpos_y(), dmg);
            brett.AsetPoints(10);

            //Burg besiegt
            if (brett.getLife(brett.Agetpos_x(), brett.Agetpos_y()) == 0)
                brett.AsetPoints(40);
        }

        //Starte Spiel-Editor
        public void Start_Editor()
        {
            spieleditor = new ceditor();
            Hauptmenu.Stop();
            Editor.Start();
        }

        //Wenn die Maus in der Spalte der Hauptmenü-Buttons ist (Verkürzung der Buttonprüfung in IsStartbutton usw.)
        public Boolean IsMainMenuButtonRow(MouseEventArgs e)
        {
            if (e.X >= pbSpielbrett.Width / 2 - ((button_width / 2) - 3) && e.X <= pbSpielbrett.Width / 2 + ((button_width / 2) - 3))
                return true;
            return false;
        }

        //Wenn die Koordinaten innerhalb des Startbuttons sind
        public Boolean IsStartbutton(MouseEventArgs e)
        {
            if (IsMainMenuButtonRow(e))
            {
                if (e.Y >= pbSpielbrett.Height / 2 - button_height + 3 && e.Y <= pbSpielbrett.Height / 2 - 6)
                    return true;
                else
                    return false;
            }
            return false;
        }
        //Wenn die Koordinaten innerhalb des Editorbuttons sind
        public Boolean IsEditorButton(MouseEventArgs e)
        {
            if (IsMainMenuButtonRow(e))
            {
                if (e.Y >= pbSpielbrett.Height / 2 + 10 + 3 && e.Y <= pbSpielbrett.Height / 2 + button_height+4)
                    return true;
                else
                    return false;
            }
            return false;
        }
        //Wenn die Koordinaten innerhalb des "Beenden"-Buttons sind
        public Boolean IsExitButton(MouseEventArgs e)
        {
            if (IsMainMenuButtonRow(e))
            {
                if (e.Y >= pbSpielbrett.Height / 2 + 24+ button_height && e.Y <= pbSpielbrett.Height / 2 + 10+ (2*button_height)+6)
                    return true;
                else
                    return false;
            }
            return false;
        }

        //Beim loslassen der Taste (Einem Tastendruck)
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Spiel.Enabled)
                {
                    if (!Mover.Enabled && e.KeyCode!= Keys.Space)
                    {
                        key = e;
                        Mover_count = 0;
                        if (brett.Agetmovement() > 0 || e.KeyCode == Keys.E || e.KeyCode == Keys.S || e.KeyCode == Keys.L)
                            Mover.Start();
                    }
                    if (e.KeyCode == Keys.Space && brett.Agetmovement() >= 2 && brett.getLife(brett.Agetpos_x(), brett.Agetpos_y()) > 0)
                        Attack_Castle();
                }
                if (Editor.Enabled)
                {
                    if (spieleditor != null)
                    {
                        if (spieleditor.getpage() == "Karte")
                        {
                            if (e.KeyCode == Keys.E)
                            {
                                if (!brett.LevelCleared())
                                    spieleditor.nextpage();
                                else
                                    MessageBox.Show("Es muss mindestens eine Burg vorhanden sein");
                            }
                        }
                    }
                }
            }
            catch { /*NullReferenceException abfangen*/ }
        }

        //Bewegungsanimationen und Tastendrucke verarbeiten
        private void Mover_Tick(object sender, EventArgs e)
        {           
            Mover_count++;

                switch (key.KeyCode)
                {
                    case Keys.Up:
                            brett.hochlaufen();
                        break;
                    case Keys.Down:
                            brett.runterfallen(brett.getactive());
                        break;
                    case Keys.Left:
                            brett.linkslaufen();
                        break;
                    case Keys.Right:
                            brett.rechtslaufen();
                        break;
                    case Keys.E:
                        if (Mover_count == 1)
                        {
                            if (brett.Agetmovement() == 0)
                                next();
                            else if (brett.AgetOneStep())
                                next();
                            else
                            {
                                Nachricht = "Es muss mindestens ein Schritt gegangen werden";
                                Notifier_Enabled = true;
                                Notifier_Ticks = 0;
                                Notifier_Long = 300;
                            }
                        }
                            
                        break;
                    case Keys.S:
                        if (Mover_count == 1)
                            Speichern();
                        break;
                    case Keys.L:
                        if (Mover_count == 1)
                            Laden();
                        break;
            }
            //Nach dem 10. Durchgang beenden   
            if (Mover_count == 10) { Mover.Stop(); Mover.Dispose(); brett.Aclean_target(); ActionAlpha = 0; }

            //Aktions-Menü darf wieder sichtbar werden
            if (brett.AgetActionHide()) brett.AtoggleActionHide();
        }

        //Verbietet Sichtbarkeit des Aktions-Menü
        public void close_action()
        {
            //Schließe Action-Menu
            if (!brett.AgetActionHide() && brett.Agetmovement() > 0)
            {
                brett.AtoggleActionHide();
                ActionAlpha = 0;
            }
        }

        public void Speichern()
        {
            Notifier_Ticks = 0;
            Nachricht = "Spiel gespeichert";
            //Speichern
            bin = new BinaryFormatter();
            stream = new FileStream(save, FileMode.Create);
            bin.Serialize(stream, brett);
            stream.Close();
            Notifier_Enabled = true;
        }

        public void Laden()
        {
            Notifier_Ticks = 0;
            try
            {
                Nachricht = "Spiel geladen";
                BinaryFormatter bin = new BinaryFormatter();
                FileStream stream = new FileStream(save, FileMode.Open);
                brett = (cbrett)bin.Deserialize(stream);
                stream.Close();
            }
            catch
            {
                Nachricht = "Kein Spielstand";
            }
            Notifier_Enabled = true;

        }

        //Hauptmenü
        private void Hauptmenu_Tick(object sender, EventArgs e)
        {

            try
            {
                if (!Spiel.Enabled || brett==null)
                {
                    g = pbSpielbrett.CreateGraphics();
                    tmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                    Graphics h = Graphics.FromImage(tmp);

                    //Zeichne Buttons
                    h.DrawImage(mainmenu, 0, 0, pbSpielbrett.Width, pbSpielbrett.Height);
                    h.DrawImage(cattura, pbSpielbrett.Width / 2 - button_width, 50, 500, 120);
                    h.DrawImage(button_start, pbSpielbrett.Width / 2 - button_width/2, pbSpielbrett.Height / 2 - button_height,button_width,button_height);
                    h.DrawImage(button_editor, pbSpielbrett.Width / 2 - 125, pbSpielbrett.Height / 2 + 10, button_width,button_height);
                    h.DrawImage(button_beenden, pbSpielbrett.Width / 2 - 125, pbSpielbrett.Width / 2 + 10 +button_height, button_width,button_height);
                    h.DrawImage(help, 10, pbSpielbrett.Height - 100, 90, 90);
                    g.DrawImage(tmp, 0, 0);
                    
                    //Freigeben
                    h.Dispose();
                    g.Dispose();
                    tmp.Dispose();    
                }
            }
            catch { }
        }

        //Spiel-Editor
        private void Editor_Tick(object sender, EventArgs e)
        {
            g = pbSpielbrett.CreateGraphics();
            tmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Graphics h = Graphics.FromImage(tmp);
            try
            {
                if (!Spiel.Enabled)
                {
                    if (spieleditor.getpage() == "Einstellungen")
                    {
                        h.DrawImage(mainmenu, 0, 0, pbSpielbrett.Width, pbSpielbrett.Height);

                        //Level
                        h.DrawString(spieleditor.getlevel().ToString(), SettingsText, Brushes.Black, new Rectangle(pbSpielbrett.Width / 2 + 90, 65, 100, 50));
                        h.DrawImage(new Bitmap(Properties.Resources.Down), pbSpielbrett.Width / 2 + 130, 68, 50, 28);
                        h.DrawImage(new Bitmap(Properties.Resources.Up), pbSpielbrett.Width / 2 + 180, 65, 50, 28);
                        h.DrawImage(editor_level, 10, 50, 298, 52);

                        //Radius
                        h.DrawString(spieleditor.getradius().ToString(), SettingsText, Brushes.Black, new Rectangle(pbSpielbrett.Width / 2 + 90, 165, 100, 50));
                        h.DrawImage(new Bitmap(Properties.Resources.Down), pbSpielbrett.Width / 2 + 130, 168, 50, 28);
                        h.DrawImage(new Bitmap(Properties.Resources.Up), pbSpielbrett.Width / 2 + 180, 165, 50, 28);
                        h.DrawImage(editor_radius, 10, 150, 298, 52);

                        //Spieleranzahl
                        h.DrawString(spieleditor.getspieler().ToString(), SettingsText, Brushes.Black, new Rectangle(pbSpielbrett.Width / 2 + 90, 265, 100, 50));
                        h.DrawImage(new Bitmap(Properties.Resources.Down), pbSpielbrett.Width / 2 + 130, 268, 50, 28);
                        h.DrawImage(new Bitmap(Properties.Resources.Up), pbSpielbrett.Width / 2 + 180, 265, 50, 28);
                        h.DrawImage(editor_players, 10, 250, 298, 52);

                        //Karte
                        h.DrawImage(editor_map, 10, 350, 400, 52);
                        if (spieleditor.getmap())
                            h.DrawImage(chk_on, pbSpielbrett.Width / 2 + 85, 350, 48, 48);
                        else
                            h.DrawImage(chk_off, pbSpielbrett.Width / 2 + 85, 350, 48, 48);

                        //Button Weiter
                        h.DrawImage(button_weiter, 10, 450, 250, 92);
                    }
                    if (spieleditor.getpage() == "Generieren")
                    {
                        if (brett == null) brett = new cbrett(this);
                        brett.loadgame(spieleditor.getlevel(), spieleditor.getradius(), spieleditor.getspieler(), this);

                        if (spieleditor.getmap())
                            spieleditor.flushmap(brett);
                        else
                            brett.generate_map();
                        spieleditor.nextpage();
                    }
                    if (spieleditor.getpage() == "Karte")
                    {
                        //Das Spielfeld zeichnen
                        for (Int32 i = 0; i < brett.getRange(); i++)
                            for (Int32 j = 0; j < brett.getRange(); j++)
                                //Das originale Feld durchgehen
                                switch (brett.getRealFeld(i, j))
                                {
                                    case 0:
                                        h.DrawImage(terrain, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                                        break;

                                    case 1:
                                        h.DrawImage(castle, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                                        break;

                                    case 2:
                                        h.DrawImage(rock, i * brett.getflen(), j * brett.getflen(), brett.getflen() - 1, brett.getflen() - 1);
                                        break;
                                }
                    }
                    if (spieleditor.getpage() == "Start")
                    {
                        brett.initPlayers();
                        pbSpielbrett.BackgroundImage = null;
                        brett.FeldtoMap();

                        //Timer starten
                        Spiel.Start();

                        //Starte in Runde 1 (Nicht 0)
                        brett.Anextturn();

                        Editor.Stop();
                    }
                    if (spieleditor.getpage() == "Bereit")
                        spieleditor.nextpage();

                }
            }
            catch /*(Exception ex)*/
            {
                //MessageBox.Show(ex.Message);
            }

            try
            {
                g.DrawImage(tmp, 0, 0);
            }
            catch /*(ArgumentException ae)*/
            { /*Während die MessageBox steht, wird hier ein Fehler geworfen*/}
            
            //Freigeben
            h.Dispose();
            g.Dispose();
            tmp.Dispose(); 
        }

        private void Spiel_Laden_Tick(object sender, EventArgs e)
        {
            if (!Spiel.Enabled && !Hauptmenu.Enabled)
            {
                g = pbSpielbrett.CreateGraphics();
                tmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                Graphics h = Graphics.FromImage(tmp);

                try
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    FileStream stream = new FileStream(save, FileMode.Open);
                    save_info = (cbrett)bin.Deserialize(stream);
                    stream.Close();

                    h.DrawImage(mainmenu, 0, 0, pbSpielbrett.Width, pbSpielbrett.Height);
                    h.DrawString("Spielstand gefunden", SettingsText, Brushes.Black, new Rectangle(10, 10, 300, 35));
                    h.DrawString("Level: " + save_info.getLevel(), new Font("Tahoma",15,FontStyle.Bold), Brushes.Black, new Rectangle(pbSpielbrett.Width/2-100, pbSpielbrett.Height/4, 200, 25));
                    h.DrawString("Spieler: " + save_info.getplayers(), new Font("Tahoma", 15, FontStyle.Bold), Brushes.Black, new Rectangle(pbSpielbrett.Width / 2 - 100, pbSpielbrett.Height / 4 + 30, 200, 25));
                    h.DrawString("Zug: " + save_info.AgetTurn(), new Font("Tahoma", 15, FontStyle.Bold), Brushes.Black, new Rectangle(pbSpielbrett.Width / 2 - 100, pbSpielbrett.Height / 4 + 60, 200, 25));
                    
                    h.DrawString("Möchten Sie das Spiel laden?", SettingsText, Brushes.Black, new Rectangle(pbSpielbrett.Width / 2 - 210, pbSpielbrett.Height / 2, 430, 50));
                    h.DrawString("Ja", SettingsText, Brushes.Black, new Rectangle(pbSpielbrett.Width / 2 - 100, pbSpielbrett.Height / 2 + 60, 100, 50));
                    h.DrawString("Nein", SettingsText, Brushes.Black, new Rectangle(pbSpielbrett.Width / 2, pbSpielbrett.Height / 2 + 60, 100, 50));

                    g.DrawImage(tmp,0,0);
                }
                catch
                { }
            }
        }

        private void Anleitung_Tick(object sender, EventArgs e)
        {
            if (!Hauptmenu.Enabled && !Spiel.Enabled)
            {
                g = pbSpielbrett.CreateGraphics();
                tmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                Graphics h = Graphics.FromImage(tmp);

                try
                {
                    h.DrawImage(mainmenu, 0, 0, pbSpielbrett.Width, pbSpielbrett.Height);
                    h.DrawImage(cattura, pbSpielbrett.Width / 2 - button_width, 50, 500, 120);

                    switch (manual_page)
                    {
                        case "Intro":
                            h.DrawImage(new Bitmap(Properties.Resources.introText), pbSpielbrett.Width/2 - 300, pbSpielbrett.Height/2 - 50, 600, 100);
                            break;
                        case "Ziel":
                            h.DrawImage(new Bitmap(Properties.Resources.Ziel),pbSpielbrett.Width/2 - 299, pbSpielbrett.Height/2 - 64, 598, 128);
                            break;
                        case "Steuerung":
                            h.DrawImage(new Bitmap(Properties.Resources.Steuerung), pbSpielbrett.Width / 2 - 300, pbSpielbrett.Height / 2 - 200, 600, 400);
                            break;
                    }

                    g.DrawImage(tmp, 0, 0);
                }
                catch { }
            }
        }

        public void Anleitung_Next()
        {
            switch (manual_page)
            {
                case "Intro":
                    manual_page = "Ziel";
                    break;
                case "Ziel":
                    manual_page = "Steuerung";
                    break;
                case "Steuerung":
                    Anleitung.Stop();
                    Hauptmenu.Start();
                    break;
            }
        }
    }  
}
