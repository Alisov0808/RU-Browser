using System;
using System.IO;
using System.Windows.Forms;
using CefSharp;
using C_Browser;
using TIMBrowser;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.ConstrainedExecution;

namespace CefSharp.Example
{
    public class DownloadHandler : IDownloadHandler
    {
        public event EventHandler<DownloadItem> OnBeforeDownloadFired;

        public event EventHandler<DownloadItem> OnDownloadUpdatedFired;
       
            
     public void OnBeforeDownload(IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            var handler = OnBeforeDownloadFired;
            if (handler != null)
            {
                handler(this, downloadItem);
                
            }

            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    callback.Continue(downloadItem.SuggestedFileName, showDialog: true);
                }
            }
        }

        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            var handler = OnBeforeDownloadFired;
            string a = File.ReadAllText("browser/c.txt");
               if (a=="yu")
                {
                    string title;
                    title = downloadItem.Url.ToString();
                    File.AppendAllText("browser/down.txt", "\n" + title);
                }
            if (a == "ys")
            {
                string title;
                title = downloadItem.SuggestedFileName.ToString();
                File.AppendAllText("browser/down.txt", "\n" + title);
            }

            if (handler != null)
            {
                
               
                handler(this, downloadItem);
               
               
            }

            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    callback.Continue(downloadItem.SuggestedFileName, showDialog: true);
                }
            }
        }

        public void OnDownloadUpdated(IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            var handler = OnDownloadUpdatedFired;
            if (handler != null)
            {
                
                handler(this, downloadItem);
            }
        }

        bool IDownloadHandler.CanDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, string url, string requestMethod)
        { 
           
            return true; 
           
        }

       
       
        void IDownloadHandler.OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            var handler = OnDownloadUpdatedFired;
            if (handler != null)
            {
             
                    
                
                handler(this, downloadItem);
                
            } 
                   
                


           
        }

     
    }
}