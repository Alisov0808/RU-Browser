using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace add
{
    public class ExtensionHandler : IExtensionHandler
    {
        public bool CanAccessBrowser(IExtension extension, IBrowser browser, bool includeIncognito, IBrowser targetBrowser)
        {
            return true;
        }

        public void Dispose()
        {
        }

        public IBrowser GetActiveBrowser(IExtension extension, IBrowser browser, bool includeIncognito)
        {
            return browser;
        }

        public bool GetExtensionResource(IExtension extension, IBrowser browser, string file, IGetExtensionResourceCallback callback)
        {
            return true;
        }

        public bool OnBeforeBackgroundBrowser(IExtension extension, string url, IBrowserSettings settings)
        {

            return true;
        }

        public bool OnBeforeBrowser(IExtension extension, IBrowser browser, IBrowser activeBrowser, int index, string url, bool active, IWindowInfo windowInfo, IBrowserSettings settings)
        {
            browser.ShowDevTools();
            return true;
        }

        public void OnExtensionLoaded(IExtension extension)
        {
        }

        public void OnExtensionLoadFailed(CefErrorCode errorCode)
        {
        }

        public void OnExtensionUnloaded(IExtension extension)
        {
        }
    }
}
