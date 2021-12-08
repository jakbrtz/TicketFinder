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
    }
}
