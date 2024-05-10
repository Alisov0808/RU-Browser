using CefSharp;
using CefSharp.DevTools.Autofill;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Controls;
using System.Windows.Forms;
using TIMBrowser;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static TIMBrowser.Settings;
using Control = System.Windows.Forms.Control;
using Image = System.Drawing.Image;
using TextBox = System.Windows.Forms.TextBox;

namespace C_Browser
{
    public partial class Form1 : Form
    {
        bool check = false;
        ChromiumWebBrowser chromium;
        Settings.SettingPar setp;
        string adress;
        public bool checus = false;
        public ImageList list = new ImageList();
        


        public Form1()
        {
            //list.ImageSize = new Size(16, 16);
           
            InitializeComponent();
           
          
            // textBox1.BackColor = Color.FromArgb();
        }
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static bool IsConnectedToInternet()
        {
            int Desc;
         
            return InternetGetConnectedState(out Desc, 0);
        }
        public void AddHistory(string site)
        {
            if (setp.saveHist)
            {
                if (setp.saveDate)
                {
                    DateTime dt = DateTime.UtcNow;
                    System.IO.File.AppendAllText("browser/history.txt", "\n" + site + "\t" + dt.ToString("HH:mm dd.MM.yy"));
                }
                else
                    System.IO.File.AppendAllText("browser/history.txt", "\n" + site);
            }
        }
        public ChromiumWebBrowser getCurrentBrowser()
        {
            return (ChromiumWebBrowser)tabControl1.SelectedTab.Controls[0];
        }
        public void ico()
        {
            WebClient wc = new WebClient();

            try
            {
                Exception ex = new Exception();
                MemoryStream memorystream = new MemoryStream(wc.DownloadData("http://" + new Uri(getCurrentBrowser().Address.ToString()).Host + "/favicon.ico"));
                Icon icon = new Icon(memorystream);

                string i = Convert.ToString(list.Images.Count);
                list.Images.Add(i, icon.ToBitmap());
                //list.ImageSize = new Size(icon.Width, icon.Height);
                tabControl1.ImageList = list;
                tabControl1.SelectedTab.ImageIndex = list.Images.Count - 1;
            }
            catch 
            {
                
            }
           
        }
        public void Form1_Load(object sender, EventArgs e)
        {

            
            try
            {
                bool a = IsConnectedToInternet();
                if (a == false)
                {

                    PictureBox pictureBox = new PictureBox();

                    // Задаем режим размера, чтобы изображение подгонялось под размеры PictureBox
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    // Загружаем изображение
                    pictureBox.Image = Image.FromFile("error.png");

                    // Устанавливаем размер и положение PictureBox на форме
                    pictureBox.Width = tabPage1.Width;
                    pictureBox.Height = tabPage1.Height;
                    pictureBox.Left = 10;
                    pictureBox.Top = 10;

                    TabPage tab = new TabPage();
                    // Добавляем PictureBox на форму
                    tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                    tab.Controls.Add(pictureBox);
                    tabControl1.TabPages.Add(tab);
                    tabControl1.SelectedTab = tab;
                   
                }
                int a0 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/c.txt"));
                int a1 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/c1.txt"));
                int a2 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/c2.txt"));
                textBox1.BackColor = Color.FromArgb(a0, a1, a2);
                int a00 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/b.txt"));
                int a11 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/b1.txt"));
                int a22 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/b2.txt"));
                this.BackColor = Color.FromArgb(a00, a11, a22);
                int a000 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/s1.txt"));
                int a111 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/s2.txt"));
                int a222 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/s3.txt"));
                comboBox1.BackColor = Color.FromArgb(a000, a111, a222);
                comboBox1.SelectedItem = "Быстрый доступ";

                string[] dos = System.IO.File.ReadAllLines("browser/dostyp.txt");

                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(dos);
                setp = JsonSerializer.Deserialize<SettingPar>(System.IO.File.ReadAllText("browser/settings.json"));

            }
            catch (Exception)
            {
                setp = new SettingPar
                {
                    searchSys = "Яндекс",
                    startPage = "ya.ru",
                    saveHist = true,
                    saveType = "Адрес",
                    saveDate = false,
                    saveCash = true,
                    saveDown = true,
                    saveURL = "URL",
                    Style = "Flat"

                };
            }
            CefSettings set = new CefSettings();
            Directory.CreateDirectory(@"C:\Users\Public\IE 12 Cashe");
            Directory.CreateDirectory(@"C:\Users\Public\IE 12 Cashe\Cookes");
            CefSharp.Cookie c = new CefSharp.Cookie();
            if (setp.Style == "Popup")
            {
                button1.FlatStyle = FlatStyle.Popup;
                button2.FlatStyle = FlatStyle.Popup;
                button3.FlatStyle = FlatStyle.Popup;
                button4.FlatStyle = FlatStyle.Popup;
                button5.FlatStyle = FlatStyle.Popup;
                button6.FlatStyle = FlatStyle.Popup;
                button7.FlatStyle = FlatStyle.Popup;
                button8.FlatStyle = FlatStyle.Popup;
                button9.FlatStyle = FlatStyle.Popup;
                button10.FlatStyle = FlatStyle.Popup;
                button11.FlatStyle = FlatStyle.Popup;

            }
            if (setp.Style == "Standard")
            {
                button1.FlatStyle = FlatStyle.Standard;
                button2.FlatStyle = FlatStyle.Standard;
                button3.FlatStyle = FlatStyle.Standard;
                button4.FlatStyle = FlatStyle.Standard;
                button5.FlatStyle = FlatStyle.Standard;
                button6.FlatStyle = FlatStyle.Standard;
                button7.FlatStyle = FlatStyle.Standard;
                button8.FlatStyle = FlatStyle.Standard;
                button9.FlatStyle = FlatStyle.Standard;
                button10.FlatStyle = FlatStyle.Standard;
                button11.FlatStyle = FlatStyle.Standard;

            }
            if (setp.Style == "Flat")
            {
                button1.FlatStyle = FlatStyle.Flat;
                button2.FlatStyle = FlatStyle.Flat;
                button3.FlatStyle = FlatStyle.Flat;
                button4.FlatStyle = FlatStyle.Flat;
                button5.FlatStyle = FlatStyle.Flat;
                button6.FlatStyle = FlatStyle.Flat;
                button7.FlatStyle = FlatStyle.Flat;
                button8.FlatStyle = FlatStyle.Flat;
                button9.FlatStyle = FlatStyle.Flat;
                button10.FlatStyle = FlatStyle.Flat;
                button11.FlatStyle = FlatStyle.Flat;

            }
            if (setp.saveURL == "URL")
            {
                System.IO.File.WriteAllText("browser/c.txt", "yu");
            }


            else if (setp.saveURL == "Название файла")
            {
                System.IO.File.WriteAllText("browser/c.txt", "ys");
            }
            if (setp.saveCash == true)
            {
                set.CachePath = @"C:\Users\Public\IE 12 Cashe";
                c.Domain = textBox1.Text;
                c.Path = @"C:\Users\Public\IE 12 Cashe\Cookes";

            }


            Cef.Initialize(set);

            string tes = setp.startPage;

            chromium = new ChromiumWebBrowser(setp.startPage);
            chromium.AddressChanged += Chromium_AddressChanged;
            chromium.TitleChanged += Chromium_TitleChanged;
            FullScreen.DisplayHandler displayer = new FullScreen.DisplayHandler();
            chromium.DisplayHandler = displayer;
            chromium.Dock = DockStyle.Fill;
            chromium.Scale(9090);
            CefSharp.Example.Handlers.ExtensionHandler extension = new CefSharp.Example.Handlers.ExtensionHandler();
            C_Browser0.CustomMenuHandler customMenu = new C_Browser0.CustomMenuHandler();
            chromium.MenuHandler = customMenu;
            CefSharp.Example.DownloadHandler downloadHandler = new CefSharp.Example.DownloadHandler();
            chromium.DownloadHandler = downloadHandler;
            chromium.Margin = Padding.Empty;
            Ded.MyCustomLifeSpanHandler myCustomLifeSpanHandler = new Ded.MyCustomLifeSpanHandler();
            chromium.LifeSpanHandler = myCustomLifeSpanHandler;
            tabControl1.SelectedTab.Controls.Add(chromium);


        }

        private void Chromium_TitleChanged(object sender, TitleChangedEventArgs e)
        {

            this.Invoke(new MethodInvoker(() =>
            {
                
                 

                TextBox textBoxOnTab=new TextBox();
                textBoxOnTab.Text= tabControl1.TabPages[tabControl1.SelectedIndex].Text;
                textBoxOnTab.MaxLength = 1;
                string a = e.Title;
                int y = a.Length;
                if (a.Length > 20)
                {
                   a= a.Remove(20);

                    
                }
               
              
                  tabControl1.SelectedTab.Text = a;
                

                if (setp.saveType == "Адрес")
                {
                    AddHistory(adress);
                }
                else
                {
                    AddHistory(e.Title);
                }
            }));
        }

        private void Chromium_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
               {
                  
                   tabControl1.SelectedTab.Text = e.Address;
                   textBox1.Text = e.Address;
                   ico();
                   adress = e.Address;
                   bool a = IsConnectedToInternet();
                   if (a == false)
                   {

                       PictureBox pictureBox = new PictureBox();

                       // Задаем режим размера, чтобы изображение подгонялось под размеры PictureBox
                       pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                       // Загружаем изображение
                       pictureBox.Image = Image.FromFile("error.png");

                       // Устанавливаем размер и положение PictureBox на форме
                       pictureBox.Width = tabPage1.Width;
                       pictureBox.Height = tabPage1.Height;
                       pictureBox.Left = 10;
                       pictureBox.Top = 10;

                       TabPage tab = new TabPage();
                       // Добавляем PictureBox на форму
                       tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                       tab.Controls.Add(pictureBox);
                       tabControl1.TabPages.Add(tab);
                       tabControl1.SelectedTab = tab;
                   }
               }));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser crome = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (crome != null && crome.CanGoForward)
            {
                bool a = IsConnectedToInternet();
                if (a == false)
                {

                    PictureBox pictureBox = new PictureBox();

                    // Задаем режим размера, чтобы изображение подгонялось под размеры PictureBox
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    // Загружаем изображение
                    pictureBox.Image = Image.FromFile("error.png");

                    // Устанавливаем размер и положение PictureBox на форме
                    pictureBox.Width = tabPage1.Width;
                    pictureBox.Height = tabPage1.Height;
                    pictureBox.Left = 10;
                    pictureBox.Top = 10;

                    TabPage tab = new TabPage();
                    // Добавляем PictureBox на форму
                    tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                    tab.Controls.Add(pictureBox);
                    tabControl1.TabPages.Add(tab);
                    tabControl1.SelectedTab = tab;
                }
                crome.Forward();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool a = IsConnectedToInternet();
            if (a == false)
            {

                PictureBox pictureBox = new PictureBox();

                // Задаем режим размера, чтобы изображение подгонялось под размеры PictureBox
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                // Загружаем изображение
                pictureBox.Image = Image.FromFile("error.png");

                // Устанавливаем размер и положение PictureBox на форме
                pictureBox.Width = tabPage1.Width;
                pictureBox.Height = tabPage1.Height;
                pictureBox.Left = 10;
                pictureBox.Top = 10;

                TabPage tab = new TabPage();
                // Добавляем PictureBox на форму
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                tab.Controls.Add(pictureBox);
                tabControl1.TabPages.Add(tab);
                tabControl1.SelectedTab = tab;
            }
            ChromiumWebBrowser crome = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (crome != null)
            {
                string sys = "https://www.google.com/search?q=";
                if (setp.searchSys == "Яндекс")
                {

                    sys = "https://yandex.ru/search/?text=";
                    if (textBox1.Text.StartsWith("http") || textBox1.Text.StartsWith("https"))
                    {

                        crome.Load(textBox1.Text);
                    }
                    else
                    {
                        crome.Load(sys + textBox1.Text);

                    }
                }
                else if (setp.searchSys == "Google")
                {
                    sys = "https://www.google.ru/search?q=";
                    if (textBox1.Text.StartsWith("http") || textBox1.Text.StartsWith("https"))
                    {

                        crome.Load(textBox1.Text);
                    }
                    else
                    {
                        crome.Load(sys + textBox1.Text);

                    }
                }
                else if (setp.searchSys == "Mail.ru")
                {
                    sys = "https://mail.ru/search?search_source=mailru_desktop_safe&text=";
                    if (textBox1.Text.StartsWith("http") || textBox1.Text.StartsWith("https"))
                    {

                        crome.Load(textBox1.Text);
                    }
                    else
                    {
                        crome.Load(sys + textBox1.Text);

                    }
                }


            }
        }

        public void button5_Click(object sender, EventArgs e)
        {
            
            Directory.CreateDirectory(@"C:\Users\Public\IE 12 Cashe");
            Directory.CreateDirectory(@"C:\Users\Public\IE 12 Cashe\Cookes");
            CefSharp.Cookie c = new CefSharp.Cookie();
            if (setp.Style == "Popup")
            {
                button1.FlatStyle = FlatStyle.Popup;
                button2.FlatStyle = FlatStyle.Popup;
                button3.FlatStyle = FlatStyle.Popup;
                button4.FlatStyle = FlatStyle.Popup;
                button5.FlatStyle = FlatStyle.Popup;
                button6.FlatStyle = FlatStyle.Popup;
                button7.FlatStyle = FlatStyle.Popup;
                button8.FlatStyle = FlatStyle.Popup;
                button9.FlatStyle = FlatStyle.Popup;
                button10.FlatStyle = FlatStyle.Popup;
                button11.FlatStyle = FlatStyle.Popup;

            }
            if (setp.Style == "Standard")
            {
                button1.FlatStyle = FlatStyle.Standard;
                button2.FlatStyle = FlatStyle.Standard;
                button3.FlatStyle = FlatStyle.Standard;
                button4.FlatStyle = FlatStyle.Standard;
                button5.FlatStyle = FlatStyle.Standard;
                button6.FlatStyle = FlatStyle.Standard;
                button7.FlatStyle = FlatStyle.Standard;
                button8.FlatStyle = FlatStyle.Standard;
                button9.FlatStyle = FlatStyle.Standard;
                button10.FlatStyle = FlatStyle.Standard;
                button11.FlatStyle = FlatStyle.Standard;

            }
            if (setp.Style == "Flat")
            {
                button1.FlatStyle = FlatStyle.Flat;
                button2.FlatStyle = FlatStyle.Flat;
                button3.FlatStyle = FlatStyle.Flat;
                button4.FlatStyle = FlatStyle.Flat;
                button5.FlatStyle = FlatStyle.Flat;
                button6.FlatStyle = FlatStyle.Flat;
                button7.FlatStyle = FlatStyle.Flat;
                button8.FlatStyle = FlatStyle.Flat;
                button9.FlatStyle = FlatStyle.Flat;
                button10.FlatStyle = FlatStyle.Flat;
                button11.FlatStyle = FlatStyle.Flat;

            }
            if (setp.saveURL == "URL")
            {
                System.IO.File.WriteAllText("browser/c.txt", "yu");
            }


            else if (setp.saveURL == "Название файла")
            {
                System.IO.File.WriteAllText("browser/c.txt", "ys");
            }
            if (setp.saveCash == true)
            {
               
                c.Domain = textBox1.Text;
                c.Path = @"C:\Users\Public\IE 12 Cashe\Cookes";

            }

            int a0 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/c.txt"));
            int a1 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/c1.txt"));
            int a2 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/c2.txt"));
            textBox1.BackColor = Color.FromArgb(a0, a1, a2);
            int a00 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/b.txt"));
            int a11 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/b1.txt"));
            int a22 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/b2.txt"));
            this.BackColor = Color.FromArgb(a00, a11, a22);
            int a000 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/s1.txt"));
            int a111 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/s2.txt"));
            int a222 = Convert.ToInt32(System.IO.File.ReadAllText("browser/rgb/s3.txt"));
            comboBox1.BackColor = Color.FromArgb(a000, a111, a222);
            comboBox1.SelectedItem = "Быстрый доступ";

            string[] dos = System.IO.File.ReadAllLines("browser/dostyp.txt");

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(dos);
            TabPage tab = new TabPage();
            tab.Text = "Новая вкладка";
            ChromiumWebBrowser chromium = new ChromiumWebBrowser("https://" + setp.startPage);
            tab.Controls.Add(chromium);
            tabControl1.TabPages.Add(tab);
            tabControl1.SelectedTab = tab;
            chromium.AddressChanged += Chromium_AddressChanged;
            chromium.TitleChanged += Chromium_TitleChanged;
           

            FullScreen.DisplayHandler displayer = new FullScreen.DisplayHandler();
            chromium.DisplayHandler = displayer;
            chromium.Dock = DockStyle.Fill;
            CefSharp.Example.Handlers.ExtensionHandler extension = new CefSharp.Example.Handlers.ExtensionHandler();


            Ded.MyCustomLifeSpanHandler myCustomLifeSpanHandler = new Ded.MyCustomLifeSpanHandler();
            chromium.LifeSpanHandler = myCustomLifeSpanHandler;
            C_Browser0.CustomMenuHandler customMenu = new C_Browser0.CustomMenuHandler();
            chromium.MenuHandler = customMenu;
            CefSharp.Example.DownloadHandler downloadHandler = new CefSharp.Example.DownloadHandler();
            chromium.DownloadHandler = downloadHandler;
            chromium.Margin = Padding.Empty;
            
            bool a = IsConnectedToInternet();
            if (a == false)
            {

                PictureBox pictureBox = new PictureBox();

                // Задаем режим размера, чтобы изображение подгонялось под размеры PictureBox
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                // Загружаем изображение
                pictureBox.Image = Image.FromFile("error.png");

                // Устанавливаем размер и положение PictureBox на форме
                pictureBox.Width = tabPage1.Width;
                pictureBox.Height = tabPage1.Height;
                pictureBox.Left = 10;
                pictureBox.Top = 10;

                TabPage tab2 = new TabPage();
                // Добавляем PictureBox на форму
                tabControl1.TabPages.Remove(tabControl1.SelectedTab); tabControl1.SelectedTab = tab2;
                tab2.Controls.Add(pictureBox);
                tabControl1.TabPages.Add(tab2);

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {

            // button6.Top = tabControl1.SelectedTab.Top;
            // button6.Left = tabControl1.SelectedTab.Left;

            if (tabControl1.TabCount > 1)
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }




        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser crome = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (crome != null && crome.CanGoBack)
            {
                bool a = IsConnectedToInternet();
                if (a == false)
                {

                    PictureBox pictureBox = new PictureBox();

                    // Задаем режим размера, чтобы изображение подгонялось под размеры PictureBox
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    // Загружаем изображение
                    pictureBox.Image = Image.FromFile("error.png");

                    // Устанавливаем размер и положение PictureBox на форме
                    pictureBox.Width = tabPage1.Width;
                    pictureBox.Height = tabPage1.Height;
                    pictureBox.Left = 10;
                    pictureBox.Top = 10;

                    TabPage tab = new TabPage();
                    // Добавляем PictureBox на форму
                    tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                    tab.Controls.Add(pictureBox);
                    tabControl1.TabPages.Add(tab);
                    tabControl1.SelectedTab = tab;
                }
                crome.Back();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser crome = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (crome != null)
            {
                bool a = IsConnectedToInternet();
                if (a == false)
                {

                    PictureBox pictureBox = new PictureBox();

                    // Задаем режим размера, чтобы изображение подгонялось под размеры PictureBox
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    // Загружаем изображение
                    pictureBox.Image = Image.FromFile("error.png");

                    // Устанавливаем размер и положение PictureBox на форме
                    pictureBox.Width = tabPage1.Width;
                    pictureBox.Height = tabPage1.Height;
                    pictureBox.Left = 10;
                    pictureBox.Top = 10;

                    TabPage tab = new TabPage();
                    // Добавляем PictureBox на форму
                    tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                    tab.Controls.Add(pictureBox);
                    tabControl1.TabPages.Add(tab);
                    tabControl1.SelectedTab = tab;
                }
                crome.Reload();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            //textBox1.BackColor = Color.FromArgb(225,225,255);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                string title;
                title = textBox1.Text;
                System.IO.File.AppendAllText("browser/save.txt", "\n" + title);
            }));

        }

        private void button10_Click(object sender, EventArgs e)
        {

            ChromiumWebBrowser crome = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
            crome.Load(setp.startPage);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

            ChromiumWebBrowser crome = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (crome != null)
            {
                string sys = "https://www.google.com/search?q=";
                if (setp.searchSys == "Яндекс")
                {

                    sys = "https://yandex.ru/search/?text=";
                    if (comboBox1.Text.StartsWith("http") || comboBox1.Text.StartsWith("https"))
                    {

                        crome.Load(comboBox1.Text);
                    }
                    else
                    {
                        crome.Load(sys + comboBox1.Text);

                    }
                }
                else if (setp.searchSys == "Google")
                {
                    sys = "https://www.google.ru/search?q=";
                    if (comboBox1.Text.StartsWith("http") || comboBox1.Text.StartsWith("https"))
                    {

                        crome.Load(comboBox1.Text);
                    }
                    else
                    {
                        crome.Load(sys + comboBox1.Text);

                    }
                }
                else if (setp.searchSys == "Mail.ru")
                {
                    sys = "https://mail.ru/search?search_source=mailru_desktop_safe&text=";
                    if (comboBox1.Text.StartsWith("http") || comboBox1.Text.StartsWith("https"))
                    {

                        crome.Load(comboBox1.Text);
                    }
                    else
                    {
                        crome.Load(sys + comboBox1.Text);

                    }
                }


            }
        }
    }
}


