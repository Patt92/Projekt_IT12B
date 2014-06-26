using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ProjectX
{
    public class cplayer
    {
        private Int32 points;
        private Int32[] position;
        private Int32[] target_position;
        private Int32[] animation_position;

        private Int32 turn;
        private Int32 movement;
        private String name;
        private Int32 player_nr;
        private Int32 max_range;
        private Int32 flen;
        private Bitmap bild;

        private Int32 attack;
        private Int32 critical;

        private bool action_hide;
        
        public void setBitmap(Bitmap b)
        {
            bild = b;
        }
        public Bitmap getBitmap()
        { return bild; }

        public cplayer(Int32 pnr, cbrett b)
        {
            points = 0;
            turn = 0;
            movement = 0;
            player_nr = pnr;
            name = "Player " + pnr.ToString();
            attack = 8 + b.getLevel();
            critical = 33;
            if (attack < 3) attack = 3;

            max_range = b.getRange();
            flen = b.getflen();

            //Alle Spieler auf Startfelder setzen
            place_players(b.getRange());

            //Target -1 = Kein Ziel (Clean Value)
            target_position = new Int32[2];
            target_position[0] = -1;
            target_position[1] = -1;

            //Für Bewegungsanimation position+- Schritte
            animation_position = new Int32[2];
            Array.Clear(animation_position, 0, 2);

        }
        public void place_players(Int32 range)
        {
            int x, y;

            //Startfelder für Spieler 1-4 setzen
            if (player_nr % 2 == 1)
                x = 1;
            else
                x = range - 2;

            if (player_nr == 2 || player_nr == 3)
                y = 1;
            else
                y = range - 2;

            //Spieler Position im Feld[x,y]
            position = new Int32[2];
            position[0] = x;
            position[1] = y;
        }

        //Spielerpositions-Methoden:        
        
        //Aktuelle Position
        public Int32 getpos_x()
        {   return position[0]; }
        
        public Int32 getpos_y()
        {   return position[1]; }

        public void setpos(Int32 _x, Int32 _y)
        {   position[0] = _x;
            position[1] = _y;   }
        
        //Ziel-Position
        public Int32 gettarget_x()
        {   return target_position[0]; }

        public Int32 gettarget_y()
        {   return target_position[1]; }

        public void settarget(Int32 _x, Int32 _y)
        {   target_position[0] = _x;
            target_position[1] = _y;   }

        public void clean_target()
        {
            //Wenn Ziel gesetzt, also != -1
            if(gettarget_x()!=-1&&gettarget_y()!=-1)
                setpos(gettarget_x(), gettarget_y());
            
            target_position[0] = -1;
            target_position[1] = -1;
            
            Array.Clear(animation_position, 0, 2);
                        
        }

        //Animations-Methoden (Für die Berechnung der Bewegungsoffsets)
        public Int32 getanimationoffset_x()
        { return animation_position[0]; }

        public Int32 getanimationoffset_y()
        { return animation_position[1]; }

        public void setanimationoffset_x(Int32 x)
        { if(x>=(-1*flen) || x<=flen )
                animation_position[0] = x; }

        public void setanimationoffset_y(Int32 y)
        { if (y >= (-1 * flen) || y <= flen)
                animation_position[1] = y; }

        //Punkte, Bewegungspunkte uvm. später
        public Int32 getmovement()
        { return movement; }

        public void setmovement(Int32 m)
        { movement = m; }

        public void nextturn(Int32 level)
        {
            turn++;
            setmovement(movement+Convert.ToInt32(max_range - level/10));
            if (movement <3 ) setmovement(3);
        }
        public Int32 getTurn()
        { return turn; }

        public Int32 getPoints()
        { return points; }

        public void setPoints(Int32 p)
        { points += p; }

        public Int32 getCrit()
        { return critical; }

        public Int32 getAttack()
        { return attack; }

        public void toggleActionHide()
        { action_hide = !action_hide; }

        public Boolean getActionHide()
        { return action_hide; }

    }
}
