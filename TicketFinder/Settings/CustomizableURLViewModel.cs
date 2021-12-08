using System;
using System.Windows.Forms;

namespace TicketFinder.Settings
{
    public partial class CustomizableURLViewModel : UserControl
    {
        public CustomizableURLViewModel()
        {
            InitializeComponent();
        }

        public string DisplayName
        {
            get => TBXshortname.Text;
            set => TBXshortname.Text = value;
        }

        public string URL
        {
            get => TBXlongname.Text;
            set => TBXlongname.Text = value;
        }

        public event Action<CustomizableURLViewModel> DeleteButtonPressed;

        private void BTNdelete_Click(object sender, EventArgs e)
        {
            DeleteButtonPressed(this);
        }

        public event Action<CustomizableURLViewModel> DataChanged;

        private void TBXshortname_TextChanged(object sender, EventArgs e)
        {
            DataChanged?.Invoke(this);
        }

        private void TBXlongname_TextChanged(object sender, EventArgs e)
        {
            DataChanged?.Invoke(this);
        }

        public void ClearValidationErrors()
        {
            TBXshortname.BorderStyle = ButtonBorderStyle.None;
            toolTip1.SetToolTip(TBXshortname, null);
            TBXlongname.BorderStyle = ButtonBorderStyle.None;
            toolTip1.SetToolTip(TBXlongname, null);
        }

        public void SetValidationError(string message, bool forDisplayName = true)
        {
            if (forDisplayName)
            {
                TBXshortname.BorderStyle = ButtonBorderStyle.Solid;
                toolTip1.SetToolTip(TBXshortname, message);
            }
            else
            {
                TBXlongname.BorderStyle = ButtonBorderStyle.Solid;
                toolTip1.SetToolTip(TBXlongname, message);
            }
        }

        public event Action<CustomizableURLViewModel> MoveUpPressed;

        private void BTNmoveup_Click(object sender, EventArgs e)
        {
            MoveUpPressed(this);
        }

        public bool MoveUpEnabled
        {
            get => BTNmoveup.Enabled;
            set => BTNmoveup.Enabled = value;
        }

        public event Action<CustomizableURLViewModel> MoveDownPressed;

        private void BTNmovedown_Click(object sender, EventArgs e)
        {
            MoveDownPressed(this);
        }

        public bool MoveDownEnabled
        {
            get => BTNmovedown.Enabled;
            set => BTNmovedown.Enabled = value;
        }
    }
}
