using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectX
{
    public class ceditor
    {
        //Auf welcher Seite sich der Editor befindet
        private String page;
        private Int32 spieler,
                      level,
                      radius;
        Boolean checkbox_map;


        public ceditor()
        {
            page = "Einstellungen";
            spieler = 2;
            level = 1;
            radius = 8;
            checkbox_map = true;
        }

        public String getpage()
        { return page; }

        public Int32 getspieler()
        { return spieler; }

        public Int32 getlevel()
        { return level; }

        public Int32 getradius()
        { return radius; }

        public Boolean getmap()
        { return checkbox_map; }

        public void add_spieler()
        {
            if (spieler < 4)
                spieler++;
        }
        public void sub_spieler()
        {
            if (spieler > 2)
                spieler--;
        }
        public void add_level()
        {
            if (level < 50)
                level++;
        }
        public void sub_level()
        {
            if (level > 1)
                level--;
        }
        public void add_radius()
        {
            if (radius < 45)
                radius++;
        }
        public void sub_radius()
        {
            if (radius > 8)
                radius--;
        }
        public void toggle_map()
        { checkbox_map = !checkbox_map; }

        public void nextpage()
        {
            switch (page)
            {
                case "Einstellungen":
                    page = "Generieren";
                break;

                case "Generieren":
                    page = "Bereit";
                break;

                case "Bereit":
                if (checkbox_map)
                {
                    page = "Karte";
                    System.Windows.Forms.MessageBox.Show("Der Editor kann mit 'E' fertig gestellt werden");
                }
                else
                    page = "Start";
                break;

                case "Karte":
                    page = "Start";
                break;
            }

        }

        public void flushmap(cbrett b)
        {
            for (Int32 i = 0; i < radius-1; i++)
                for (Int32 j = 0; j < radius-1; j++)
                {
                    b.setFeld(i, j, 0);
                    b.setRealFeld(i, j, 0);
                }

        }
    }
}
