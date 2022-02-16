using System.ComponentModel;
using System.Diagnostics;

namespace TicketFinder
{
    public class URL
    {
        public string displayName;
        public string url;

        public void Open(string input)
        {
            try
            {
                Process.Start(url + input);
            }
            catch (Win32Exception)
            {
                Process.Start("http://" + url + input);
            }
        }

        public string ToFileData()
        {
            return this.displayName + '\t' + this.url;
        }

        public static bool TryParseFileData(string s, out string displayName, out string url)
        {
            string[] info = s.Split('\t');
            displayName = info.Length > 0 ? info[0] : "";
            url = info.Length > 1 ? info[1] : "";
            return info.Length == 2;
        }

        public static bool TryParseFileData(string s, out URL url)
        {
            bool result = TryParseFileData(s, out string displayName, out string urlStr);
            url = new URL() { displayName = displayName, url = urlStr };
            return result;
        }
    }
}
