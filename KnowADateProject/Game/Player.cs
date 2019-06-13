using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Game
{
    class Player
    {
        public List<Event> hand { get; }
        public String name { get; }
        public String ID { get; }
        public bool isBot { get; }

        public Player(String str)
        {
            ID = str.Substring(0, str.IndexOf('.'));
            name = str.Substring(str.IndexOf('.') + 1);
            if(ID == "W" || ID == "M" || ID == "S")
            {
                isBot = true;
            }
            else isBot = false;
            hand = new List<Event>();
        }

        public void removeEvent(Event ev)
        {
            hand.Remove(ev);
        }

        public void addEvent(Event ev)
        {
            hand.Add(ev);
        }

        public void changeEvent(Event _old, Event _new)
        {
            removeEvent(_old);
            addEvent(_new);
        }

        public void addPoints(int points)
        {
            if (ID == "N" || isBot) return;
            DBGame dataBase = DBGame.conn();
            dataBase.addPoints(ID, points);
        }
    }
}
