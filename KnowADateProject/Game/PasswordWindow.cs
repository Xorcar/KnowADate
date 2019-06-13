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
    public partial class PasswordWindow : Form
    {
        private String ID;

        public PasswordWindow(String _ID, int number, String name)
        {
            InitializeComponent();
            label1.Text = "" + number +
                '.' + name + ",\nplease enter your password:";
            ID = _ID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String enteredPassword = textBox1.Text;
            DBGame dataBase = DBGame.conn();
            if (dataBase.checkPassword(ID, enteredPassword))
            {
                GameWindow.gameWin.passwordChecked();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong password, try again");
            }
        }
    }
}
