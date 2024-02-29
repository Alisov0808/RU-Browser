using CefSharp;
using CefSharp.WinForms;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Policy;
using System.Text.Json;
using System.Web.UI;
using System.Windows.Forms;
using TIMBrowser;
using static TIMBrowser.Settings;
using add;
using C_Browser0;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;
using CefSharp.WinForms.Internals;
using Win32Interop.Structs;

namespace C_Browser
{

    public partial class Form1 : Form
    {
        bool check = false;
        ChromiumWebBrowser chromium;
        Settings.SettingPar setp;
        string adress;
        public bool checus = false;


        public Form1()
        {
            
            InitializeComponent();
          
            // textBox1.BackColor = Color.FromArgb();
        }

        public void AddHistory(string site)
        {
            if (setp.saveHist)
            {
                if (setp.saveDate)
                {
                    DateTime dt = DateTime.UtcNow;
                    File.AppendAllText("browser/history.txt", "\n" + site + "\t" + dt.ToString("HH:mm dd.MM.yy"));
                }
                else
                    File.AppendAllText("browser/history.txt", "\n" + site);
            }
        }

        public void Form1_Load(object sender, EventArgs e)
        {

            
            try
            {
                
                int a0 = Convert.ToInt32(File.ReadAllText("browser/rgb/c.txt"));
                int a1 = Convert.ToInt32(File.ReadAllText("browser/rgb/c1.txt"));
                int a2 = Convert.ToInt32(File.ReadAllText("browser/rgb/c2.txt"));
                textBox1.BackColor = Color.FromArgb(a0, a1, a2);
                int a00 = Convert.ToInt32(File.ReadAllText("browser/rgb/b.txt"));
                int a11 = Convert.ToInt32(File.ReadAllText("browser/rgb/b1.txt"));
                int a22 = Convert.ToInt32(File.ReadAllText("browser/rgb/b2.txt"));
                this.BackColor = Color.FromArgb(a00, a11, a22);
                
                setp = JsonSerializer.Deserialize<SettingPar>(File.ReadAllText("browser/settings.json"));

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

            }
            if (setp.saveURL == "URL")
            {
                File.WriteAllText("browser/c.txt", "yu");
            }
           

            else if (setp.saveURL == "Название файла") 
            {
                File.WriteAllText("browser/c.txt", "ys");
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
            add.ExtensionHandler extension = new add.ExtensionHandler();
             
            C_Browser0.CustomMenuHandler customMenu = new C_Browser0.CustomMenuHandler();
            chromium.MenuHandler = customMenu;
            CefSharp.Example.DownloadHandler downloadHandler = new CefSharp.Example.DownloadHandler();
            chromium.DownloadHandler = downloadHandler;
            chromium.Margin = Padding.Empty;
            

            tabControl1.SelectedTab.Controls.Add(chromium);


        }

        private void Chromium_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            
            this.Invoke(new MethodInvoker(() =>
            {
                tabControl1.SelectedTab.Text = e.Title;
                tabControl1.SelectedTab.Text = e.Title;
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
                   textBox1.Text = e.Address;
                   adress = e.Address;
               }));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser crome = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (crome != null && crome.CanGoForward)
            {
                crome.Forward();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
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

           
            TabPage tab = new TabPage();
            tab.Text = "Новая вкладка";
            ChromiumWebBrowser chromium = new ChromiumWebBrowser("https://" + setp.startPage);
            tab.Controls.Add(chromium);
            tabControl1.TabPages.Add(tab);
            tabControl1.SelectedTab = tab;
            chromium.AddressChanged += Chromium_AddressChanged;
            chromium.TitleChanged += Chromium_TitleChanged;
            

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
                crome.Back();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser crome = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (crome != null)
            {
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
                File.AppendAllText("browser/save.txt", "\n" + title);
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
    }
}

    
