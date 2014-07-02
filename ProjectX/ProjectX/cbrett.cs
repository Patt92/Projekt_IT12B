using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ProjectX
{
    [Serializable]
    public class cbrett
    {

        //Map dient zur Darstellung und Feld enthält die tatsächlichen Werte
        private Int32[,] Feld, Map, Castle;
        private Int32[] selector_pos;

        //Begrenzer
        private Int32 max_disabled, max_castle, max_range;
        
        //Zähler für Generierung
        private Int32 count_disabled, count_castle;

        //Breite, bzw. Höhe eines Feldes
        private Int32 flen;
        private Int32 step;

        private Int32 level;
        private Int32 castle_max_life;
        private Int32 selector_draw_count;
        private Int32 active_player;

        //Spielerobjekte
        public cplayer[] spieler;
   
        private Int32 players;

        //Allgemeiner Konstruktor      
        public cbrett(Form1 f)
        {
            level = 1;
            max_range = 8; //tbRange wurde public gemacht, für Interaktion
            init(f);
        }

        public void nextlevel(Form1 f)
        {
            Random zufall = new Random();

            if (max_range <= 45)
            {
                level++;
                max_range = max_range + zufall.Next(0, 2);
                init(f);
                generate_map();
                initPlayers();
                Anextturn();
            }
            else
            {
                MessageBox.Show("Maximallevel erreicht. Herzlichen Glückwunsch!");
            }

        }

        public void init(Form f)
        {
            Feld = new Int32[max_range, max_range];
            Map = new Int32[max_range, max_range];
            Castle = new Int32[max_range, max_range];
            castle_max_life = 20 + level - 1;

            max_disabled = max_range * max_range / 3;
            max_castle = max_range * max_range / 3;

            active_player = 0;
            selector_draw_count = 0;

            selector_pos = new Int32[2];
            Array.Clear(selector_pos, 0, 2);

            flen = (Screen.PrimaryScreen.Bounds.Height - 100) / max_range;
            step = flen / 10;

            f.Width = flen * max_range + 5;
            f.Height = flen * max_range + 30;
        }

        public void fix_window(Form f)
        {
            f.Width = flen * max_range + 5;
            f.Height = flen * max_range + 30;
        }

        public void setFeld(Int32 i, Int32 j, Int32 val)
        {
            if (i < max_range && j < max_range && i>=0 && j>=0)
                Map[i, j] = val;
        }
        public void setRealFeld(Int32 i, Int32 j, Int32 val)
        {
            if (i < max_range && j < max_range && i >= 0 && j >= 0)
                Feld[i, j] = val;
        }

        public void resetFeld(Int32 i, Int32 j)
        {
            if (i < max_range && j < max_range)
                Map[i, j] = Feld[i, j];
            selectorclean();
        }

        public Int32 getLife(Int32 x, Int32 y)
        {
            return Castle[x, y];
        }

        public Int32 getMaxLife()
        { return castle_max_life; }

        public void generate_map()
        {
            Random zufall = new Random();

            count_castle = 0;
            count_disabled = 0;

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
                            {
                                Map[i, j] = 0;
                                Feld[i, j] = 0;
                            }
                        }
                    }
                }
            }
            //<- Ende eingesperrte Burgen


            //Steine auf Startfeldern entfernen
            Map[1, max_range - 2] = 0;
            Map[1, 1] = 0;
            Map[max_range - 2, 1] = 0;
            Map[max_range - 2, max_range - 2] = 0;

            //Feld = Map & Burgen initialisieren;
            for (Int32 i = 0; i < max_range; i++)
            {
                for (Int32 j = 0; j < max_range; j++)
                {
                    Feld[i, j] = Map[i, j];
                    if (Map[i, j] == 1)
                        Castle[i, j] = castle_max_life;
                    else Castle[i, j] = 0;
                }
            }

        }

        public void countselector()
        {selector_draw_count++;}

        public Int32 selectstate()
        { return selector_draw_count; }

        public void selectorclean()
        { selector_draw_count = 0; }

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
        public Int32 getactive()
        { return active_player; }
        public void nextplayer()
        {
            active_player++;
            if (active_player == players)
                active_player = 0;
        }

        public void setmaxplayers(Int32 p)
        { players = p; }

        public Int32 getplayers()
        { return players; }

        public Int32 getStep()
        { return step; }

        //Selector
        public void setSelectorx(Int32 x)
        { selector_pos[0] = x; }

        public void setSelectory(Int32 y)
        { selector_pos[1] = y; }

        public Int32 getSelectorx()
        { return selector_pos[0]; }

        public Int32 getSelectory()
        { return selector_pos[1]; }

        public Int32 getLevel()
        { return level; }

        public Int32 Attack(Int32 x, Int32 y, Int32 attack)
        { 
            Castle[x,y] -= attack; 

            if(Castle[x,y]<0) Castle[x,y] = 0; 
            return Castle[x,y];
        }
        public void initPlayers()
        {
             spieler = new cplayer[getplayers()];
             for (Int32 i = 0; i < getplayers(); i++)
                 spieler[i] = new cplayer(i,this);

             //Bitmaps für Spieler laden
             spieler[0].setBitmap(Properties.Resources.player1);
             spieler[1].setBitmap(Properties.Resources.player2);
             if (players > 2) spieler[2].setBitmap(Properties.Resources.player3);
             if (players > 3) spieler[3].setBitmap(Properties.Resources.player4);
        }
        public void Anextturn()
        {
            spieler[active_player].nextturn(level);
        }

        //Methoden für aktiven Spieler
        public Int32 Agetpos_x()
        { return spieler[active_player].getpos_x(); }

        public Int32 Agetpos_y()
        { return spieler[active_player].getpos_y(); }

        public Int32 Agetmovement()
        { return spieler[active_player].getmovement(); }

        public Bitmap AgetBitmap()
        { return spieler[active_player].getBitmap(); }

        public Int32 AgetAttack()
        { return spieler[active_player].getAttack(); }

        public Int32 AgetCrit()
        { return spieler[active_player].getCrit(); }

        public Int32 AgetTurn()
        { return spieler[active_player].getTurn(); }

        public Int32 AgetPoints()
        { return spieler[active_player].getPoints(); }

        public void AsetPoints(Int32 p)
        { spieler[active_player].setPoints(p); }

        public void Aclean_target()
        { spieler[active_player].clean_target(); }

        public Int32 Agettarget_x()
        { return spieler[active_player].gettarget_x(); }

        public Int32 Agettarget_y()
        { return spieler[active_player].gettarget_y(); }

        public void Asettarget(Int32 x, Int32 y)
        { spieler[active_player].settarget(x, y); }

        public void Asetmovement(Int32 m)
        { spieler[active_player].setmovement(m); }

        public void Asetpos(Int32 x, Int32 y)
        { spieler[active_player].setpos(x, y); }

        public Int32 Agetanimoffset_x()
        { return spieler[active_player].getanimationoffset_x(); }

        public Int32 Agetanimoffset_y()
        { return spieler[active_player].getanimationoffset_y(); }

        public void Asetanimoffset_x(Int32 x)
        { spieler[active_player].setanimationoffset_x(x); }

        public void Asetanimoffset_y(Int32 y)
        { spieler[active_player].setanimationoffset_y(y); }

        public Boolean AgetActionHide()
        { return spieler[active_player].getActionHide(); }

        public void AtoggleActionHide()
        { spieler[active_player].toggleActionHide(); }

        public Boolean LevelCleared()
        {
            Boolean valid = true;
            for (Int32 i = 0; i < max_range; i++)
            {
                for (Int32 j = 0; j < max_range; j++)
                {
                    if (Castle[i, j] != 0) valid = false;
                }
            }
            return valid;
        }

        public void hochlaufen()
        {
            if (Agetpos_y() > 0 && Agetmovement() > getZone(Agetpos_y()))
            {
                if (Agettarget_y() == -1 && getRealFeld(Agetpos_x(), Agetpos_y() - 1) != 2)
                {
                    Asettarget(Agetpos_x(), Agetpos_y() - 1);
                    Asetmovement(Agetmovement() - (getZone(Agetpos_y())+1));
                }
                if (Agettarget_x() != -1 && Agettarget_y() != -1)
                {
                    if (Agetpos_y() > Agettarget_y())
                    {
                        if ((Agetpos_y() * getflen() + Agetanimoffset_y() - getStep()) > (Agettarget_y() * getflen()))
                        {
                            if (!AgetOneStep()) AsetOneStep();
                            Asetanimoffset_y(Agetanimoffset_y() - getStep());
                        }
                    }
                }
            }  
        }

        public void linkslaufen()
        {
            if (Agetpos_x() > 0 && Agetmovement() > 0)
            {
                if (Agettarget_x() == -1 && getRealFeld(Agetpos_x() - 1, Agetpos_y()) != 2)
                {
                    Asettarget(Agetpos_x() - 1, Agetpos_y());
                    Asetmovement(Agetmovement() - 1);
                }
                if (Agettarget_x() != -1 && Agettarget_y() != -1)
                {
                    if (Agetpos_x() > Agettarget_x())
                    {
                        if ((Agetpos_x() * getflen() + Agetanimoffset_x() - getStep()) > (Agettarget_x() * getflen()))
                        {
                            if (!AgetOneStep()) AsetOneStep();
                            Asetanimoffset_x(Agetanimoffset_x() - getStep());
                        }
                    }
                }
            }  
        }

        public void rechtslaufen()
        {
            if (Agetpos_x() < getRange() - 1 && Agetmovement() > 0)
            {
                if (Agettarget_x() == -1 && getRealFeld(Agetpos_x() + 1, Agetpos_y()) != 2)
                {
                    Asettarget(Agetpos_x() + 1, Agetpos_y());
                    Asetmovement(Agetmovement() - 1);
                }
                if (Agettarget_x() != -1 && Agettarget_y() != -1)
                {
                    if (Agetpos_x() < Agettarget_x())
                    {
                        if ((Agetpos_x() * getflen() + Agetanimoffset_x() + getStep()) < (Agettarget_x() * getflen()))
                        {
                            if (!AgetOneStep()) AsetOneStep();
                            Asetanimoffset_x(Agetanimoffset_x() + getStep());
                        }
                    }
                }
            }    
        }

        public void runterfallen(Int32 p)
        {      
                if (getRealFeld(spieler[p].getpos_x(), spieler[p].getpos_y() + 1) != 2)
                {
                    while (getRealFeld(spieler[p].getpos_x(), spieler[p].getpos_y() + 1) != 2 && spieler[p].getpos_y() < getRange() - 1)
                    {
                        spieler[p].setpos(spieler[p].getpos_x(), spieler[p].getpos_y() + 1);

                        if ((spieler[p].getpos_y() * getflen() + spieler[p].getanimationoffset_y() + getStep()) < (spieler[p].gettarget_y() * getflen()))
                        {
                            if (p != active_player)
                            {
                                if (spieler[p].getmovement() > 0) spieler[p].setmovement(spieler[p].getmovement() - 1);
                            }
                            spieler[p].setanimationoffset_y(spieler[p].getanimationoffset_y() + getStep());
                        }
                    }
                }   
        }

        public void loadgame(Int32 l, Int32 r, Int32 p, Form1 f)
        {
            level = l;
            max_range = r;
            players = p;
            init(f);
        }

        public Boolean AgetOneStep()
        { return spieler[active_player].getOneStep(); }

        public void AsetOneStep()
        { spieler[active_player].setOneStep(); }

        public Int32 getZone(Int32 z)
        {
            if (z > max_range-(max_range / 2)+2)
                return 1;
            else if (z <= max_range-((max_range / 2) - 2) && z> 3)
                return 2;
            else
                return 3;
        }

        public Int32 getZonefield(Int32 z)
        {
            switch (z)
            {
                case 1:
                    return max_range-(max_range / 2 - 2);
                case 2://3
                    return 3;
            }
            return 0;
        }

        public void toggleFeld(Int32 x, Int32 y)
        {
            Feld[x, y]++;
            if (Feld[x, y] == 3) Feld[x, y] = 0;
            if (Feld[x, y] == 1) Castle[x, y] = castle_max_life;
            else Castle[x, y] = 0;
        }

        public void FeldtoMap()
        {
            for (Int32 i = 0; i < max_range; i++)
                for (Int32 j = 0; j < max_range; j++)
                {
                    Map[i, j] = Feld[i, j];
                    if (Map[i, j] == 1)
                        Castle[i, j] = castle_max_life;
                    else Castle[i, j] = 0;
                }
        }

    }
}
