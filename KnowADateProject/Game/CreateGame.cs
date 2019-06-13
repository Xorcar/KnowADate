using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class CreateGame : Form
    {
        public static String folderLocation;
        private ComboBox[] playersCB;

        public static CreateGame win;

        public CreateGame()
        {
            win = this;
            folderLocation = "D:\\KnowADate";
            InitializeComponent();
            InitializeComboBoxes();
            InitializeListView();
            LoadList();
            
            BackgroundImage = Image.FromFile(folderLocation + "\\Pictures\\background.jpg");
        }
        
        private void InitializeComboBoxes()
        {
            DBGame dataBase = DBGame.conn();
            List<String> langs = dataBase.getLanguages();
            String[] masObj = langs.ToArray();
            comboBox9.Items.AddRange(masObj);
            comboBox9.SelectedItem = langs.First();

            playersCB = new ComboBox[8];
            playersCB[0] = comboBox1;
            playersCB[1] = comboBox2;
            playersCB[2] = comboBox3;
            playersCB[3] = comboBox4;
            playersCB[4] = comboBox5;
            playersCB[5] = comboBox6;
            playersCB[6] = comboBox7;
            playersCB[7] = comboBox8;

            List<String> players = new List<String>();
            players.Add("<empty>");
            players.AddRange(dataBase.getPlayers());
            players.Add("N.Noname");
            players.Add("W.Computer");
            players.Add("M.Computer");
            players.Add("S.Computer");
            String[] playersMas = players.ToArray();

            for(int i = 0; i < 8; i++)
            {
                playersCB[i].Items.AddRange(playersMas);
                playersCB[i].SelectedItem = "<empty>";
            }
        }

        private void InitializeListView()
        {
            listView1.View = View.Details;
            listView1.LabelEdit = true;
            listView1.AllowColumnReorder = true;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("ID", 25);
            listView1.Columns.Add("name", 90);
        }

        private void LoadList()
        {
            listView1.Items.Clear();
            DBGame dataBase = DBGame.conn();
            List<String[]> packList = dataBase.getPacks();
            foreach (String[] strMas in packList)
            {
                ListViewItem lvi = new ListViewItem(strMas[0]);
                lvi.SubItems.Add(strMas[1]);
                listView1.Items.Add(lvi);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<String> players = new List<String>();

            for (int i = 0; i < 8; i++)
            {
                String chosenPlayer = playersCB[i].SelectedItem.ToString();
                if (chosenPlayer != "<empty>")
                {
                    players.Add(chosenPlayer);
                }
            }

            List<String> packs = new List<String>();
            ListView.SelectedListViewItemCollection items = listView1.SelectedItems;
            foreach (ListViewItem item in items)
            {
                String curPack = item.SubItems[1].Text;
                packs.Add(curPack);
            }

            String lang = comboBox9.SelectedItem.ToString();
            GameWindow window2 = new GameWindow(players, packs, lang);
            window2.Show();
        }
    }
}
