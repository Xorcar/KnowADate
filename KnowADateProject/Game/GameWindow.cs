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
    public partial class GameWindow : Form
    {
        private List<Player> players;
        private Player curPlayer;
        private Dictionary<Player, bool> ifEnteredPassword;
        private List<Event> deck;
        public static GameWindow gameWin;

        public GameWindow(List<String> _players, List<String> packs, String lang)
        {
            InitializeComponent();
            FormPlayersList(_players);
            formDeck(packs, lang);
            giveHands();
            InitializeListView();
            gameWin = this;
            formPasswordList();
            this.Enabled = false;
            checkPasswords();
            InitializeComboBox(players.First());
            curPlayer = players.First();

            BackgroundImage = Image.FromFile(CreateGame.folderLocation + "\\Pictures\\background.jpg");
        }

        private void FormPlayersList(List<String> _players)
        {
            players = new List<Player>();
            foreach(String curPlayerStr in _players)
            {
                players.Add(new Player(curPlayerStr));
            }
        }

        private void formPasswordList()
        {
            ifEnteredPassword = new Dictionary<Player, bool>();
            foreach(Player pl in players)
            {
                if(pl.isBot == false && pl.ID != "N")
                {
                    ifEnteredPassword.Add(pl, false);
                }
            }
        }

        private void checkPasswords()
        {
            foreach (Player pl in players)
            {
                if (pl.isBot || pl.ID == "N") continue;
                else if (ifEnteredPassword[pl] == false)
                {
                    curPlayer = pl;
                    PasswordWindow pasWin = new PasswordWindow(
                        pl.ID, players.IndexOf(pl), pl.name);
                    pasWin.Show();
                    return;
                }
            }
            this.Enabled = true;
            curPlayer = players.First();
        }

        public void passwordChecked()
        {
            ifEnteredPassword[curPlayer] = true;
            checkPasswords();
        }

        private void formDeck(List<String> packs, String lang)
        {
            DBGame dataBase = DBGame.conn();
            deck = new List<Event>();
            foreach(String curPack in packs)
            {
                deck.AddRange(dataBase.getEventsOfPack(curPack, lang));
            }
        }

        private void giveHands()
        {
            Random rnd = new Random();
            foreach (Player curPlayer in players)
            {
                for (int i = 0; i < 5; i++)
                {
                    int curIter = rnd.Next(deck.Count);
                    curPlayer.addEvent(deck[curIter]);
                    deck.Remove(deck[curIter]);
                }
            }

        }

        private void InitializeListView()
        {
            listView1.View = View.Details;
            listView1.LabelEdit = true;
            listView1.AllowColumnReorder = true;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("name", 150);
            listView1.Columns.Add("year", 40);
            listView1.Columns.Add("picture", 80);

            Random rnd = new Random();
            int curIter = rnd.Next(deck.Count);
            FirstFillListView(deck[curIter]);
            deck.Remove(deck[curIter]);
        }

        private void FirstFillListView(Event ev)
        {
            listView1.Items.Clear();

            ListViewItem firstRow = new ListViewItem("<first row>");
            firstRow.SubItems.Add("");
            firstRow.SubItems.Add("");

            ListViewItem lvi = new ListViewItem(ev.name);
            lvi.SubItems.Add(ev.year.ToString());
            lvi.SubItems.Add(ev.picture);

            listView1.Items.Add(firstRow);
            listView1.Items.Add(lvi);
        }

        private void InitializeComboBox(Player player)
        {
            label1.Text = 
                "" + players.IndexOf(curPlayer) + '.' +
                curPlayer.name + "'s move. Hand:";

            comboBox1.Items.Clear();
            foreach(Event curEv in player.hand)
            {
                comboBox1.Items.Add(curEv.name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void putEvent(String putEvName, int pos)
        {
            Event putEv = new Game.Event(new String[5]);
            foreach (Event handEv in curPlayer.hand)
            {
                if (handEv.name == putEvName)
                {
                    putEv = handEv;
                    break;
                }
            }

            if (pos == 0)
            {
                if (putEv.year <= Convert.ToInt32(listView1.Items[1].SubItems[1].Text))
                {
                    rightMove(putEv, pos + 1);
                }
                else
                {
                    wrongMove(putEv);
                }
            }

            else if(pos == listView1.Items.Count - 1)
            {
                if (putEv.year >= Convert.ToInt32(listView1.Items[listView1.Items.Count - 1].SubItems[1].Text))
                {
                    rightMove(putEv, pos + 1);
                }
                else
                {
                    wrongMove(putEv);
                }
            }

            else
            {
                if (putEv.year >= Convert.ToInt32(listView1.Items[pos].SubItems[1].Text) &&
                    putEv.year <= Convert.ToInt32(listView1.Items[pos + 1].SubItems[1].Text) )
                {
                    rightMove(putEv, pos + 1);
                }
                else
                {
                    wrongMove(putEv);
                }
            }
        }

        private void wrongMove(Event oldEv)
        {
            curPlayer.addPoints(-3);
            Random rnd = new Random();
            int curIter = rnd.Next(deck.Count);
            curPlayer.changeEvent(oldEv, deck[curIter]);
            deck.Remove(deck[curIter]);
            curPlayer = players[(players.IndexOf(curPlayer) + 1) % players.Count];
            InitializeComboBox(curPlayer);
        }

        private void rightMove(Event putEv, int pos)
        {
            curPlayer.addPoints(5);
            curPlayer.removeEvent(putEv);
            ListViewItem lvi = new ListViewItem(putEv.name);
            lvi.SubItems.Add(putEv.year.ToString());
            lvi.SubItems.Add(putEv.picture);
            listView1.Items.Insert(pos, lvi);
            if (!checkWinningCondition())
            {
                curPlayer = players[(players.IndexOf(curPlayer) + 1) % players.Count];
                InitializeComboBox(curPlayer);
            }
        }

        private bool checkWinningCondition()
        {
            if (curPlayer.hand.Count == 0)
            {
                this.Hide();
                MessageBox.Show("Game Over!\n" +
                    "Winner is " + players.IndexOf(curPlayer) +
                    '.' + curPlayer.name + "!");
                this.Close();
                return true;
            }
            else return false;
        }

        private void botMove()
        {
            Random rand = new Random();
            int dif = 0;
            
            dif = curPlayer.hand.First().difficulty + listView1.Items.Count;
            
            int strenght = 0;
            switch (curPlayer.ID)
            {
                case "W": strenght = 1;
                    break;
                case "M": strenght = 2;
                    break;
                case "S": strenght = 3;
                    break;
            }
            int range = (int)(dif * 3) - curPlayer.hand.First().difficulty;
            for (int i = 0; i < strenght * 2; i++)
            {
                int _try = rand.Next(range);
                if (_try > dif)
                {
                    searchRightMove4Bot();
                    return;
                }
            }
            wrongMove(curPlayer.hand.First());
        }

        private void searchRightMove4Bot()
        {
            Event curEv = curPlayer.hand.First();
            
            for (int pos = 0; pos < listView1.Items.Count; pos++)
            {
                if (pos == 0)
                {
                    if (curEv.year <= Convert.ToInt32(listView1.Items[1].SubItems[1].Text))
                    {
                        rightMove(curEv, pos + 1);
                        return;
                    }
                }

                else if (pos == listView1.Items.Count - 1)
                {
                    if (curEv.year >= Convert.ToInt32(listView1.Items[listView1.Items.Count - 1].SubItems[1].Text))
                    {
                        rightMove(curEv, pos + 1);
                        return;
                    }
                }

                else
                {
                    if (curEv.year >= Convert.ToInt32(listView1.Items[pos].SubItems[1].Text) &&
                        curEv.year <= Convert.ToInt32(listView1.Items[pos + 1].SubItems[1].Text))
                    {
                        rightMove(curEv, pos + 1);
                        return;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (curPlayer.isBot)
            {
                botMove();
                return;
            }

            String chosenHand = comboBox1.SelectedItem.ToString();
            
            int selectedInd = listView1.SelectedIndices[0];

            putEvent(chosenHand, selectedInd);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String chosenHandStr = comboBox1.SelectedItem.ToString();
            Event chosenEv = new Game.Event(new String[5]);
            foreach (Event handEv in curPlayer.hand)
            {
                if (handEv.name == chosenHandStr)
                {
                    chosenEv = handEv;
                    break;
                }
            }

            PictureView picWin = new PictureView(chosenEv.picture, chosenEv.name);
            picWin.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String chosenEvName = listView1.SelectedItems[0].SubItems[0].Text;
            String chosenEvPic = listView1.SelectedItems[0].SubItems[2].Text;
            PictureView picWin = new PictureView(chosenEvPic, chosenEvName);
            picWin.Show();
        }
    }
}
