using System.ComponentModel;
using System.Diagnostics;

namespace TicketFinder
{
    public class URL
    {
        public string displayName;
        public string url;
        public bool range;
        public bool nonnum;

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
            return this.displayName + '\t' + this.url + '\t' + (this.range ? "T" : "F") + '\t' + (this.nonnum ? "T" : "F");
        }

        public static bool TryParseFileData(string s, out string displayName, out string url, out bool range, out bool nonnum)
        {
            string[] info = s.Split('\t');
            displayName = info.Length > 0 ? info[0] : "";
            url = info.Length > 1 ? info[1] : "";
            range = info.Length > 2 ? info[2] == "T" : true;
            nonnum = info.Length > 3 ? info[3] == "T" : false;
            return info.Length >= 2;
        }

        public static bool TryParseFileData(string s, out URL url)
        {
            bool result = TryParseFileData(s, out string displayName, out string urlStr, out bool range, out bool nonnum);
            url = new URL() { displayName = displayName, url = urlStr, range = range, nonnum = nonnum };
            return result;
        }
    }
}
