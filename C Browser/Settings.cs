using C_Browser;
using System;
using System.IO;
using System.Media;
using System.Security.Policy;
using System.Text.Json;
using System.Windows.Forms;

namespace TIMBrowser
{
    public partial class Settings : Form
    {
            public string rgb_S = "";
            public string rgb_S1 = "";
            public string rgb_S2 = "";
        public class SettingPar
        {
            
            public string searchSys { get; set; }
            public string startPage { get; set; }
            public bool saveHist { get; set; }
            public string saveType { get; set; }
            public bool saveDate { get; set; }
            public bool saveCash { get; set; }
            public bool saveDown { get; set; }
            public string saveURL { get; set; }
            public string Style { get; set; }

        }
        public Settings()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SettingPar setp = new SettingPar
            {
                searchSys = comboBox1.Text,
                startPage = comboBox2.Text,
                saveHist = checkBox1.Checked,
                saveType = comboBox3.Text,
                saveDate = checkBox2.Checked,
                saveCash = checkBox3.Checked,
                saveDown = checkBox4.Checked,
                saveURL = comboBox4.Text,
                Style = comboBox5.Text,
            };
            rgb_S = textBox2.Text;
            rgb_S1 = textBox3.Text;
            rgb_S2 = textBox4.Text;
            File.WriteAllText("browser/rgb/c.txt", rgb_S);
            File.WriteAllText("browser/rgb/c1.txt", rgb_S1);
            File.WriteAllText("browser/rgb/c2.txt", rgb_S2);

            File.WriteAllText("browser/rgb/b.txt", textBox5.Text);
            File.WriteAllText("browser/rgb/b1.txt", textBox6.Text);
            File.WriteAllText("browser/rgb/b2.txt", textBox7.Text);
            string json = JsonSerializer.Serialize(setp);
            File.WriteAllText("browser/settings.json", json);
            if (setp.saveURL == "URL")
            {
                File.WriteAllText("browser/c.txt", "yu");
            }
            else if (setp.saveURL == "Название файла")
            {
                File.WriteAllText("browser/c.txt", "ys");
            }
           
        }

        private void Settings_Load(object sender, EventArgs e)
        {

            string[] hist = File.ReadAllLines("browser/history.txt");
            listBox1.Items.AddRange(hist);
            string[] down = File.ReadAllLines("browser/down.txt");
            listBox3.Items.AddRange(down);
            string[] save = File.ReadAllLines("browser/save.txt");
            listBox2.Items.AddRange(save);

            textBox2.Text = File.ReadAllText("browser/rgb/c.txt");
            textBox3.Text = File.ReadAllText("browser/rgb/c1.txt");
            textBox4.Text = File.ReadAllText("browser/rgb/c2.txt");

            textBox5.Text = File.ReadAllText("browser/rgb/b.txt");
            textBox6.Text = File.ReadAllText("browser/rgb/b1.txt");
            textBox7.Text = File.ReadAllText("browser/rgb/b2.txt");
            try
            {
                SettingPar setp = JsonSerializer.Deserialize<SettingPar>(File.ReadAllText("browser/settings.json"));
                comboBox1.Text = setp.searchSys;
                comboBox2.Text = setp.startPage;
                checkBox1.Checked = setp.saveHist;
                comboBox3.Text = setp.saveType;
                checkBox2.Checked = setp.saveDate;
                checkBox3.Checked = setp.saveCash;
                checkBox4.Checked = setp.saveDown;
                 comboBox4.Text= setp.saveURL;
                comboBox5.Text = setp.Style;
            }
            catch (Exception) { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.WriteAllText("browser/history.txt", "");
            listBox1.Items.Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(@"K:\Users\vnuch\Downloads\Internet Explorer 1.0 Commercial (1995) (online-audio-converter.com).wav");
            simpleSound.Play();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            File.WriteAllText("browser/save.txt", "");
            listBox2.Items.Clear();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox2.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Add add = new Add();
            add.Show();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox2.Text;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            File.WriteAllText("browser/down.txt", "");
            listBox3.Items.Clear();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                File.WriteAllText("browser/c.txt", "y");
            }
            else
            {

                File.WriteAllText("browser/c.txt", "n");

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            Directory.Delete(@"C:\Users\Public\IE 12 Cashe\Cache",true);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

       

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox2.Text = "215";
            textBox3.Text = "228";
            textBox4.Text = "242";

            textBox5.Text = "185";
            textBox6.Text = "209";
            textBox7.Text = "234";
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
