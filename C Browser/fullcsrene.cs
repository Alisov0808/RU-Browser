using CefSharp;
using CefSharp.DevTools.Browser;
using CefSharp.Enums;
using CefSharp.Structs;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace FullScreen
{
    public class DisplayHandler : IDisplayHandler
    {
        private Control parent;
        private Form fullScreenForm;
        void IDisplayHandler.OnAddressChanged(IWebBrowser browserControl, AddressChangedEventArgs addressChangedArgs)
        {
        }
        void IDisplayHandler.OnTitleChanged(IWebBrowser browserControl, TitleChangedEventArgs titleChangedArgs)
        {
        }
        void IDisplayHandler.OnFaviconUrlChange(IWebBrowser browserControl, IBrowser browser, IList<string> urls)
        {
        }
        void IDisplayHandler.OnFullscreenModeChange(IWebBrowser browserControl, IBrowser browser, bool fullscreen)
        {
            var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;

            chromiumWebBrowser.BeginInvoke(new MethodInvoker(() =>
            {
                if (fullscreen)
                {
                    parent = chromiumWebBrowser.Parent;
                    System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
                    parent.Controls.Remove(chromiumWebBrowser);
                    fullScreenForm = new Form();
                    fullScreenForm.FormBorderStyle = FormBorderStyle.None;
                    fullScreenForm.WindowState = FormWindowState.Maximized;
                    fullScreenForm.Controls.Add(chromiumWebBrowser);
                    fullScreenForm.ShowDialog(parent.FindForm());






                }
                else
                {
                    fullScreenForm.Controls.Remove(chromiumWebBrowser);
                    parent.Controls.Add(chromiumWebBrowser);
                    fullScreenForm.Close();
                    fullScreenForm.Dispose();
                    fullScreenForm = null;
                }
            }));
                
            
        }
        
        void IDisplayHandler.OnStatusMessage(IWebBrowser browserControl, StatusMessageEventArgs statusMessageArgs)
        {
        }
        bool IDisplayHandler.OnConsoleMessage(IWebBrowser browserControl, ConsoleMessageEventArgs consoleMessageArgs)
        {
            return false;
        }

        public bool OnAutoResize(IWebBrowser chromiumWebBrowser, IBrowser browser, Size newSize)
        {
            return true;
        }

        public bool OnCursorChange(IWebBrowser chromiumWebBrowser, IBrowser browser, IntPtr cursor, CursorType type, CursorInfo customCursorInfo)
        {
            return false;
        }

        public void OnLoadingProgressChange(IWebBrowser chromiumWebBrowser, IBrowser browser, double progress)
        {
            
        }

        public bool OnTooltipChanged(IWebBrowser chromiumWebBrowser, ref string text)
        {
            return true;
        }
    }
}