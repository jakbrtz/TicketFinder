using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TicketFinder.Help;
using TicketFinder.Settings;

namespace TicketFinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Put all the buttons on the form
            ReloadURLs();
            // Make sure the list of recent searches is not null
            if (Properties.Settings.Default.Recent == null) Properties.Settings.Default.Recent = new System.Collections.Specialized.StringCollection();
            // Put the recent history into the context menu
            ReloadHistory();
            // Move the form to the cursor
            int x = Cursor.Position.X - textBox1.Location.X;
            int y = Cursor.Position.Y - textBox1.Height - textBox1.Margin.Top - textBox1.Margin.Bottom - textBox1.Location.Y;
            Rectangle workingArea = Screen.FromPoint(Cursor.Position).WorkingArea;
            if (x < workingArea.X) x = workingArea.X;
            if (y < workingArea.Y) y = workingArea.Y;
            if (x > workingArea.X + workingArea.Width - Width) x = workingArea.X + workingArea.Width - Width;
            if (y > workingArea.Y + workingArea.Height - Height) y = workingArea.Y + workingArea.Height - Height;
            Location = new Point(x, y);
        }

        private readonly List<URL> urls = new List<URL>();
        private readonly List<RecentSearch> recentSearches = new List<RecentSearch>();

        /// <summary>
        /// Get the URLs from the appdata folder, then reload the buttons
        /// </summary>
        void ReloadURLs()
        {
            urls.Clear();
            foreach (string buttonInfo in Properties.Settings.Default.Buttons)
            {
                if (URL.TryParseFileData(buttonInfo, out URL url))
                {
                    urls.Add(url);
                }
            }
            ReloadButtons();
        }

        /// <summary>
        /// Reload the buttons on the form
        /// </summary>
        void ReloadButtons()
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (URL url in urls)
            {
                AddButton(url.displayName, (a, b) => PressButton(url, textBox1.Text));
            }
            AddButton("Edit", SettingsToolStripMenuItem_Click);

            this.Height = this.urls.Count > 2 ? 119: 102;
            flowLayoutPanel1.Height = this.urls.Count > 2 ? 49 : 32;

            ReloadContextMenuForButtons();
        }

        /// <summary>
        /// Adds a button to the form
        /// </summary>
        /// <param name="text">The text on the button</param>
        /// <param name="onClick">The action that happens when the button is pressed</param>
        void AddButton(string text, EventHandler onClick)
        {
            Button button = new Button
            {
                Size = new Size(70, 25),
                Text = text,
                UseVisualStyleBackColor = true,
            };
            button.Click += onClick;
            flowLayoutPanel1.Controls.Add(button);
        }

        /// <summary>
        /// Get the history from the appdata folder then reload the context menu 
        /// </summary>
        private void ReloadHistory()
        {
            recentSearches.Clear();
            foreach (string recentInfo in Properties.Settings.Default.Recent)
            {
                if (RecentSearch.TryParseFileData(recentInfo, out RecentSearch recentSearch))
                {
                    recentSearches.Add(recentSearch);
                }
            }
            ReloadContextMenu();
        }

        /// <summary>
        /// Reload the options in the context menu
        /// </summary>
        private void ReloadContextMenu()
        {
            historyToolStripMenuItem.DropDownItems.Clear();
            historyToolStripMenuItem.Enabled = recentSearches.Count > 0;
            if (historyToolStripMenuItem.Enabled)
            {
                for (int i = 0; i < 10 && i < recentSearches.Count; i++)
                {
                    RecentSearch recent = recentSearches[i];
                    historyToolStripMenuItem.DropDownItems.Add(MakeToolStripMenuItem(recent));
                }
            }

            ReloadContextMenuForButtons();
        }

        /// <summary>
        /// Update the context menu strip for all the buttons
        /// </summary>
        private void ReloadContextMenuForButtons()
        {
            for (int i = 0; i < urls.Count; i++)
            {
                Button button = flowLayoutPanel1.Controls[i] as Button;
                List<ToolStripItem> replacementHistoryStrip = new List<ToolStripItem>();
                int countRecent = 0;
                for (int j = 0; countRecent < 10 && j < recentSearches.Count; j++)
                {
                    RecentSearch recent = recentSearches[j];
                    if (recent.button == urls[i].displayName)
                    {
                        replacementHistoryStrip.Add(MakeToolStripMenuItem(recent));
                        countRecent++;
                    }
                }
                if (countRecent != 0)
                {
                    ContextMenuStrip cloneContextMenuStrip = new ContextMenuStrip();
                    ToolStripMenuItem settings = new ToolStripMenuItem() { Text = settingsToolStripMenuItem.Text };
                    settings.Click += SettingsToolStripMenuItem_Click;
                    cloneContextMenuStrip.Items.Add(settings);
                    ToolStripMenuItem history = new ToolStripMenuItem() { Text = historyToolStripMenuItem.Text };
                    history.DropDownItems.AddRange(replacementHistoryStrip.ToArray());
                    cloneContextMenuStrip.Items.Add(history);
                    ToolStripMenuItem help = new ToolStripMenuItem() { Text = helpToolStripMenuItem.Text };
                    help.Click += HelpToolStripMenuItem_Click;
                    cloneContextMenuStrip.Items.Add(help);
                    button.ContextMenuStrip = cloneContextMenuStrip;
                }
            }
        }

        /// <summary>
        /// Create an option for the 'history' drop down menu
        /// </summary>
        /// <param name="recent"></param>
        private ToolStripMenuItem MakeToolStripMenuItem(RecentSearch recent)
        {
            return MakeToolStripMenuItem(recent.button + " \t" + recent.query, (a, b) => PressRecent(recent));
        }

        /// <summary>
        /// Create an option for a context menu
        /// </summary>
        /// <param name="label">The name that appears on the menu</param>
        /// <param name="onClick">The action that happens when the menu item is clicked</param>
        private ToolStripMenuItem MakeToolStripMenuItem(string label, EventHandler onClick)
        {
            ToolStripMenuItem item = new ToolStripMenuItem
            {
                Text = label
            };
            item.Click += onClick;
            return item;
        }

        /// <summary>
        /// Action that happens when pressing a button on the recent history menu
        /// </summary>
        private void PressRecent(RecentSearch recent)
        {
            foreach (URL url in urls)
            {
                if (url.displayName == recent.button)
                {
                    PressButton(url, recent.query);
                    return;
                }
            }
            MessageBox.Show("The button labelled \"" + recent.button + "\" does not exist anymore.", "Error");
        }

        /// <summary>
        /// When the user enters a special character then a special behaviour should occur
        /// </summary>
        private void TBX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                if (urls.Count > 0)
                {
                    PressButton(GuessURL(textBox1.Text), textBox1.Text);
                }
                else
                {
                    MessageBox.Show("You don't have any projects to visit", "Error");
                }
            }
            else if (e.KeyChar == '\u001b')
            {
                e.Handled = true;
                this.Close();
            }
        }

        /// <summary>
        /// Search through past queries that the user has made. In order to work out what button then intended on pressing.
        /// </summary>
        private URL GuessURL(string query)
        {
            // Start by checking if this query has already been searched for
            foreach (RecentSearch recentSearch in recentSearches)
            {
                if (recentSearch.query == query)
                {
                    foreach (var url in urls)
                    {
                        if (url.displayName == recentSearch.button)
                        {
                            return url;
                        }
                    }
                }
            }

            // If this is a number then check how close it is to other numbers in the history
            if (int.TryParse(query, out int queryNumber))
            {
                // Look at the most recent searches for each URL
                const int depthInHistory = 5;

                // Decide which indices are used to compare ranges
                const int lower = depthInHistory / 3;
                const int middle = depthInHistory / 2;
                const int upper = depthInHistory - lower - 1;

                // Prepare a list to hold all these numbers
                List<int>[] recentNumbers = new List<int>[urls.Count];
                for (int i = 0; i < recentNumbers.Length; i++)
                {
                    recentNumbers[i] = new List<int>(depthInHistory);
                }

                // Go through the history and add numbers to the list
                foreach (RecentSearch recentSearch in recentSearches)
                {
                    for (int i = 0; i < urls.Count; i++)
                    {
                        if (urls[i].displayName == recentSearch.button)
                        {
                            if (urls[i].range && recentNumbers[i].Count < depthInHistory && int.TryParse(recentSearch.query, out int recentNumber))
                            {
                                recentNumbers[i].Add(recentNumber);
                            }
                            break;
                        }
                    }
                }

                // Sort the lists and make sure it's the right size
                for (int i = 0; i < recentNumbers.Length; i++)
                {
                    if (recentNumbers[i].Count == 0)
                    {
                        recentNumbers[i] = null;
                    }
                    else
                    {
                        int j = 0;
                        while (recentNumbers[i].Count < depthInHistory)
                        {
                            recentNumbers[i].Add(recentNumbers[i][j++]);
                        }
                        recentNumbers[i].Sort();
                    }
                }

                // Find the closest median to the query, with some extra conditions
                int bestIndex = -1;
                int bestDifference = int.MaxValue;
                for (int i = 0; i < recentNumbers.Length; i++)
                {
                    // Don't look at urls with no history
                    if (recentNumbers[i] == null) continue;

                    // Get the absolute difference between the searched number and the url's median number
                    int difference = Math.Abs(queryNumber - recentNumbers[i][middle]);

                    // If this isn't an improvement then ignore it
                    if (difference >= bestDifference) continue;

                    // If the difference is too big then ignore it
                    if (difference > Math.Abs(queryNumber) && difference > 50) continue;

                    // If this url's range is too similar to another url's range then ignore it
                    bool rangeIsOkay = true;
                    for (int j = 0; j < i; j++)
                    {
                        if (recentNumbers[j] != null &&
                            recentNumbers[i][lower] < recentNumbers[j][upper] && 
                            recentNumbers[j][lower] < recentNumbers[i][upper])
                        {
                            rangeIsOkay = false;
                            break;
                        }
                    }
                    if (!rangeIsOkay) continue;

                    // Record that this is an improvement
                    bestIndex = i;
                    bestDifference = difference;
                }

                // Return the best url
                if (bestIndex != -1)
                {
                    return urls[bestIndex];
                }
            }
            else
            {
                // This is not a number, so pick the url that is used for non-numeric searches
                foreach (URL url in urls)
                {
                    if (url.nonnum)
                    {
                        return url;
                    }
                }
            }

            // Pick the first URL in the list
            return urls[0];
        }

        /// <summary>
        /// The action to occur when a button is pressed
        /// </summary>
        /// <param name="url">The object associated with the button</param>
        /// <param name="query">The text that is being searched for</param>
        private void PressButton(URL url, string query)
        {
            url.Open(query);
            UpdateRecent(query, url.displayName);
            this.Close();
        }

        /// <summary>
        /// Record that a query has been made
        /// </summary>
        /// <param name="search">What did the user search for</param>
        /// <param name="code">What button did they press</param>
        private void UpdateRecent(string search, string code)
        {
            for (int i = 0; i < recentSearches.Count; i++)
            {
                if (recentSearches[i].query == search)
                {
                    recentSearches.RemoveAt(i);
                    break;
                }
            }
            recentSearches.Insert(0, new RecentSearch { query = search, button = code });

            Properties.Settings.Default.Recent.Clear();
            foreach(RecentSearch recent in recentSearches)
            {
                Properties.Settings.Default.Recent.Add(recent.ToFileData());
            }
            Properties.Settings.Default.Save();
        }

        private bool dontDeactivateForm = false;

        /// <summary>
        /// When the user leaves the form it should disappear
        /// </summary>
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            if (Visible && !dontDeactivateForm)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Open the settings screen then refresh the buttons on this screen
        /// </summary>
        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm form = new SettingsForm())
            {
                Visible = false;
                var result = form.ShowDialog();
                Visible = true;
                ReloadURLs();
                if (result == DialogResult.Abort) this.Close();
            }
        }

        /// <summary>
        /// Open the help window
        /// </summary>
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (HelpForm form = new HelpForm())
            {
                dontDeactivateForm = true;
                form.ShowDialog();
                dontDeactivateForm = false;
            }
        }

        #region make the window draggable

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// Trick the form into thinking it's being dragged when it gets clicked on
        /// </summary>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            const int WM_NCLBUTTONDOWN = 0xA1;
            const int HT_CAPTION = 0x2;

            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            if (sender is TextBox textbox && !string.IsNullOrEmpty(textbox.Text))
            {
                return;
            }

            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        #endregion
    }
}
