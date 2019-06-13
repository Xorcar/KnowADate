using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Class for connection to data base. Made to connect to mysql data base via ODBC driver.
    /// To change DB connection, is needed to edit this class file.
    /// </summary>
    class DBGame
    {
        private OdbcConnection connection;
        private static DBGame singleton;

        private DBGame()
        {
            string MyConString = "DRIVER={MySQL ODBC 8.0 Unicode Driver};" +
                "SERVER=localhost;" +
                "DATABASE=KnowADate;" +
                "UID=player;" +
                "PASSWORD=;" +
                 "OPTION=3";
            connection = new OdbcConnection(MyConString);
        }

        public static DBGame conn()
        {
            if (singleton == null)
            {
                singleton = new DBGame();
            }
            return singleton;
        }

        //get all events' data
        public List<String[]> getData()
        {
            List<String[]> list = new List<String[]>();

            OdbcCommand myCommand = new OdbcCommand("SELECT * FROM eventinfo", connection);

            connection.Open();
            OdbcDataReader reader = myCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    String[] strMas = new String[8];
                    for (int i = 0; i < 8; i++)
                    {
                        strMas[i] = reader.GetString(i);
                    }
                    list.Add(strMas);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return list;
        }

        //get all players' data
        public List<String> getPlayers()
        {
            List<String> list = new List<String>();

            OdbcCommand myCommand = new OdbcCommand("SELECT id, name FROM players", connection);

            connection.Open();
            OdbcDataReader reader = myCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    String strCur = "";
                    strCur = reader.GetString(0);
                    strCur = strCur + '.' + reader.GetString(1);
                    
                    list.Add(strCur);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }
            return list;
        }

        //get all packs' data
        public List<String[]> getPacks()
        {
            List<String[]> list = new List<String[]>();

            OdbcCommand myCommand = new OdbcCommand("SELECT * FROM packs", connection);

            connection.Open();
            OdbcDataReader reader = myCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    String[] curStr = new String[2] { "", "" };
                    curStr[0] = reader.GetString(0);
                    curStr[1] = reader.GetString(1);
                    list.Add(curStr);
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return list;
        }

        //get list of languages
        public List<String> getLanguages()
        {
            List<String> langs = new List<String>();
            langs.Add("En");
            langs.Add("Ua");
            langs.Add("Ru");

            return langs;
        }

        //get id, year, difficulty and picture from selected
        //pack and selected language
        public List<Event> getEventsOfPack(String pack, String lang)
        {
            List<Event> list = new List<Event>();
            if (lang == "En") lang = "name";

            OdbcCommand myCommand = new OdbcCommand(
                "SELECT id, " + lang + ", year, dif, picture FROM eventinfo " +
                "WHERE pack = \"" + pack + "\"", connection);
            
            connection.Open();
            OdbcDataReader reader = myCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    String[] curStr = new String[5] { "", "", "", "", "" };
                    for (int i = 0; i < 5; i++)
                    {
                        curStr[i] = reader.GetString(i);
                    }
                    list.Add(new Event(curStr));
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return list;
        }

        //add points to selected player
        public void addPoints(String ID, int points)
        {
            OdbcCommand myCommand = new OdbcCommand(
                "CALL add_points(" + ID + ", " + points +
                ");"
                , connection);

            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();
        }

        //check selected text with password of selected player
        public bool checkPassword(String playerId, String enteredPassword)
        {
            OdbcCommand myCommand = new OdbcCommand(
                "SELECT password FROM players WHERE id = " +
                playerId
                , connection);

            connection.Open();
            OdbcDataReader reader = myCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    String curPassword = reader.GetString(0);
                    if (curPassword.Equals(enteredPassword)) return true;
                }
            }
            finally
            {
                reader.Close();
                connection.Close();
            }
            return false;
        }

    }
}
