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
                    saveURL = "URL"
                    
                };
            }
            CefSettings set = new CefSettings();
            Directory.CreateDirectory(@"C:\Users\Public\IE 12 Cashe");
            Directory.CreateDirectory(@"C:\Users\Public\IE 12 Cashe\Cookes");
            CefSharp.Cookie c = new CefSharp.Cookie();
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
            chromium.AddressChanged += Chromium_AddressChanged;
            chromium.TitleChanged += Chromium_TitleChanged;
            tabControl1.TabPages.Add(tab);

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
    }
}
