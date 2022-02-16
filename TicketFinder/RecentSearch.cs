namespace TicketFinder
{
    public class RecentSearch
    {
        public string query;
        public string button;

        public string ToFileData()
        {
            return this.query + '\t' + this.button;
        }

        public static bool TryParseFileData(string s, out string query, out string button)
        {
            string[] info = s.Split('\t');
            query = info.Length > 0 ? info[0] : "";
            button = info.Length > 1 ? info[1] : "";
            return info.Length == 2;
        }

        public static bool TryParseFileData(string s, out RecentSearch recentSearch)
        {
            bool result = TryParseFileData(s, out string query, out string button);
            recentSearch = new RecentSearch() { query = query, button = button };
            return result;
        }
    }
}
