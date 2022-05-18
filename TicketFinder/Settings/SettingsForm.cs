using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TicketFinder.Validation;

namespace TicketFinder.Settings
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            foreach (string buttonInfo in Properties.Settings.Default.Buttons)
            {
                if (URL.TryParseFileData(buttonInfo, out string displayName, out string url))
                {
                    NewVM(displayName, url);
                }
            }
            RefreshReorderingButtons();
        }

        /// <summary>
        /// Add a row to the table
        /// </summary>
        /// <param name="displayName">Automatically fill in the name</param>
        /// <param name="url">Automatically fill in the url</param>
        void NewVM(string displayName, string url)
        {
            CustomizableURLViewModel vm = new CustomizableURLViewModel { 
                DisplayName = displayName,
                URL = url,
            };
            vm.DataChanged += Vm_DataChanged;
            vm.DeleteButtonPressed += Vm_DeleteButtonPressed;
            vm.MoveUpPressed += Vm_MoveUpPressed;
            vm.MoveDownPressed += Vm_MoveDownPressed;
            flowLayoutPanel1.Controls.Add(vm);
        }

        /// <summary>
        /// Action that occurs when data has changed in a row
        /// </summary>
        /// <param name="vm">The row object which contained the data</param>
        private void Vm_DataChanged(CustomizableURLViewModel vm)
        {
            RedoValidationChecks();
        }

        /// <summary>
        /// Put error borders around invalid data and remove error borders from valid data
        /// </summary>
        void RedoValidationChecks()
        {
            foreach (CustomizableURLViewModel row in flowLayoutPanel1.Controls)
            {
                row.ClearValidationErrors();

                if (!string.IsNullOrWhiteSpace(row.DisplayName))
                {
                    foreach (CustomizableURLViewModel other in flowLayoutPanel1.Controls)
                    {
                        if (other != row && row.DisplayName == other.DisplayName)
                        {
                            row.SetValidationError("You have two urls with the name");
                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(row.DisplayName) && !string.IsNullOrWhiteSpace(row.URL))
                {
                    row.SetValidationError("Name cannot be empty");
                }
                if (row.DisplayName.Contains('\t'))
                {
                    row.SetValidationError("Name cannot contain a tab character");
                }
                if (row.URL.Contains('\t'))
                {
                    row.SetValidationError("URL cannot contain a tab character", forDisplayName: false);
                }
            }
        }

        /// <summary>
        /// Action that occurs when the up button is pressed on a row
        /// </summary>
        /// <param name="row">The row object which contained the button</param>
        private void Vm_MoveUpPressed(CustomizableURLViewModel row)
        {
            flowLayoutPanel1.Controls.SetChildIndex(row, flowLayoutPanel1.Controls.IndexOf(row) - 1);
            RefreshReorderingButtons();
        }

        /// <summary>
        /// Action that occurs when the down button is pressed on a row
        /// </summary>
        /// <param name="row">The row object which contained the button</param>
        private void Vm_MoveDownPressed(CustomizableURLViewModel row)
        {
            flowLayoutPanel1.Controls.SetChildIndex(row, flowLayoutPanel1.Controls.IndexOf(row) + 1);
            RefreshReorderingButtons();
        }

        /// <summary>
        /// Make sure that the up/down buttons are enabled or disabled appropriately
        /// </summary>
        private void RefreshReorderingButtons()
        {
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                CustomizableURLViewModel row = flowLayoutPanel1.Controls[i] as CustomizableURLViewModel;
                row.MoveDownEnabled = i < flowLayoutPanel1.Controls.Count - 1;
                row.MoveUpEnabled = i > 0;
            }
        }

        /// <summary>
        /// Action that occurs when the delete button is pressed in a row
        /// </summary>
        /// <param name="row">The row object which contained the button</param>
        private void Vm_DeleteButtonPressed(CustomizableURLViewModel row)
        {
            flowLayoutPanel1.Controls.Remove(row);
            RefreshReorderingButtons();
            RedoValidationChecks();
        }

        /// <summary>
        /// When the add button is pressed, create a new empty row
        /// </summary>
        private void BTNadd_Click(object sender, EventArgs e)
        {
            NewVM("", "");
            RefreshReorderingButtons();
            flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1].Focus();
        }

        /// <summary>
        /// When the import button is pressed, let the user select a file that will populate the table
        /// </summary>
        private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            saveFileDialog1.FileName = openFileDialog1.FileName;

            flowLayoutPanel1.Controls.Clear();

            using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
            {
                while (!sr.EndOfStream)
                {
                    if (URL.TryParseFileData(sr.ReadLine(), out string displayName, out string url))
                    {
                        NewVM(displayName, url);
                    }
                }
            }
        }

        /// <summary>
        /// Display the help window
        /// </summary>
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Help.HelpForm().ShowDialog();
        }

        /// <summary>
        /// When the save button is pressed, validate the table and save the data
        /// </summary>
        private void BTNsave_Click(object sender, EventArgs e)
        {
            // Remove empty rows
            List<CustomizableURLViewModel> rowsThatNeedDeleting = new List<CustomizableURLViewModel>();
            foreach (CustomizableURLViewModel vm in flowLayoutPanel1.Controls)
            {
                if (string.IsNullOrWhiteSpace(vm.DisplayName) && string.IsNullOrWhiteSpace(vm.URL))
                {
                    rowsThatNeedDeleting.Add(vm);
                }
            }
            foreach (CustomizableURLViewModel vm in rowsThatNeedDeleting)
            {
                flowLayoutPanel1.Controls.Remove(vm);
            }
            if (rowsThatNeedDeleting.Count > 0)
            {
                RefreshReorderingButtons();
            }

            List<URL> data = GetDataFromScreen();

            // Make sure the data is valid
            var validationResult = Validate(data);
            if (validationResult == ValidationResult.DestroyEverything)
            {
                Properties.Settings.Default.Reset();
                DialogResult = DialogResult.Abort;
                this.Close();
            }
            if (validationResult != ValidationResult.Success)
            {
                return;
            }            

            // Save this in AppData
            Properties.Settings.Default.Buttons.Clear();
            foreach (URL url in data)
            {
                Properties.Settings.Default.Buttons.Add(url.ToFileData());
            }
            Properties.Settings.Default.Save();

            // Close the window
            DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Transform the data into objects that are easier to reference
        /// </summary>
        List<URL> GetDataFromScreen()
        {
            List<URL> data = new List<URL>();
            foreach (CustomizableURLViewModel vm in flowLayoutPanel1.Controls)
            {
                data.Add(new URL { displayName = vm.DisplayName, url = vm.URL });
            }
            return data;
        }

        /// <summary>
        /// When the export button is pressed, save the table to a file
        /// </summary>
        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            openFileDialog1.FileName = saveFileDialog1.FileName;

            using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
            {
                var data = GetDataFromScreen();
                foreach (URL url in data)
                {
                    sw.WriteLine(url.ToFileData());
                }
            }
        }

        /// <summary>
        /// When the close button is pressed, close without saving
        /// </summary>
        private void BTNcanel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Check if all the URLs in a list have valid values
        /// </summary>
        public ValidationResult Validate(List<URL> urls)
        {
            string errMsg = "";

            // Check for duplicates
            for (int i = 0; i < urls.Count; i++)
                for (int j = 0; j < i; j++)
                    if (urls[i].displayName == urls[j].displayName)
                        errMsg = $"You can't have two urls with the name \"{urls[i].displayName}\"";

            // Check for empty names
            for (int i = 0; i < urls.Count; i++)
                if (string.IsNullOrWhiteSpace(urls[i].displayName))
                    errMsg = $"\"{urls[i].url}\" should have a display name";

            // Check for invalid characters
            for (int i = 0; i < urls.Count; i++)
                if (urls[i].displayName.Contains('\t') || urls[i].url.Contains('\t'))
                    errMsg = $"\"{urls[i].displayName}\" is not allowed to have a tab in it";

            // If there isn't an error then this is allowed
            if (string.IsNullOrWhiteSpace(errMsg))
            {
                return ValidationResult.Success;
            }

            // Normal users see an error message and ignore them
            // We need to capture their attention by insulting them
            errMsg += "\n\n";
            errMsg += validationErrors < insults.Length 
                ? insults[validationErrors] 
                : "You can't save until this is fixed";

            // Report the problem to the user
            MessageBox.Show(errMsg, "Validation Error");

            validationErrors++;

            // If we've run out of insults then fight the user
            if (validationErrors == insults.Length)
            {
                using (Validation.Validation harderValidation = new Validation.Validation())
                {
                    Visible = false;
                    DialogResult result = harderValidation.ShowDialog();
                    Visible = true;
                    if (result != DialogResult.OK)
                    {
                        return ValidationResult.DestroyEverything;
                    }
                }
            }

            return ValidationResult.Fail;
        }

        static int validationErrors = 0;
        static readonly string[] insults = new string[] {
            "You irresponsible human",
            "You have no respect for databases",
            "What is so difficult to understand?",
            "you moron",
            "Do you want to fight or something?",
        };

        public enum ValidationResult { Success, Fail, DestroyEverything }
    }
}
