using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Browser
{
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
        }

        private void Add_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string title;
            title = textBox1.Text;
            File.AppendAllText("browser/save.txt", "\n" + title);
        }
    }
}
