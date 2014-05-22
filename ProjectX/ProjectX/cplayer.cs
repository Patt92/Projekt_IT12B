using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectX
{
    public class cplayer
    {
        private Int32 points;
        private Int32[] position;
        private Int32 turn;
        private Int32 movement;
        private String name;
        private Int32 player_nr;

        public cplayer(Int32 pnr, cbrett b)
        {
            points = 0;
            turn = 0;
            movement = 0;
            player_nr = pnr;
            name = "Player " + pnr.ToString();

            int x, y;

            //Startposition bestimmen                       
            //3,2
            //1,4

            /*if (pnr % 2 == 1)
                x = 1;
            else
                x = b.getRange() - 2;

            if (pnr == 2 || pnr == 3)
                y = 1;
            else
                y = b.getRange() - 2;
            */

            Int32 Length = b.getflen()*b.getRange() + b.getRange();

            if (pnr % 2 == 1)
                x = b.getflen();
            else
                x = Length - (2*b.getflen())-b.getRange();

            if (pnr == 2 || pnr == 3)
                y = b.getflen();
            else
                y = Length - (2*b.getflen())-b.getRange();
           


            position = new Int32[2];
            position[0] = x;
            position[1] = y;
            
        }

        public Int32 getpos_x()
        {
            return position[0];
        }
        public Int32 getpos_y()
        {
            return position[1];
        }

        public void setpos(Int32 _x, Int32 _y)
        {
            position[0] = _x;
            position[1] = _y;
        }
        public Int32 getmovement()
        { return movement; }
        public void setmovement(Int32 m)
        { movement = m; }

    }
}
