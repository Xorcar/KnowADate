using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// An historcal event.
    /// Contains data like event ID, name, year, difficulty and path to its picture.
    /// </summary>
    class Event
    {
        public int ID { get; }
        public String name { get; }
        public int year { get; }
        public int difficulty { get; }
        public String picture { get; }

        public Event(String[] strMas)
        {
            ID = Convert.ToInt32(strMas[0]);
            name = strMas[1];
            year = Convert.ToInt32(strMas[2]);
            difficulty = Convert.ToInt32(strMas[3]);
            picture = strMas[4];
        }
    }
}
