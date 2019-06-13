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
    public partial class PictureView : Form
    {
        public PictureView(String path, String name)
        {
            InitializeComponent();
            path = CreateGame.folderLocation + "\\Pictures\\" + path;
            Image img = Image.FromFile(path);
            pictureBox1.Image = img;
            pictureBox1.Size = new Size(img.Width, img.Height);
            this.Text = name;
        }
    }
}
