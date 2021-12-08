
namespace TicketFinder.Settings
{
    partial class CustomizableURLViewModel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BTNdelete = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BTNmoveup = new System.Windows.Forms.Button();
            this.BTNmovedown = new System.Windows.Forms.Button();
            this.TBXlongname = new TicketFinder.Settings.TextboxWithBorder();
            this.TBXshortname = new TicketFinder.Settings.TextboxWithBorder();
            this.SuspendLayout();
            // 
            // BTNdelete
            // 
            this.BTNdelete.Location = new System.Drawing.Point(539, 2);
            this.BTNdelete.Name = "BTNdelete";
            this.BTNdelete.Size = new System.Drawing.Size(75, 23);
            this.BTNdelete.TabIndex = 4;
            this.BTNdelete.Text = "Delete";
            this.toolTip1.SetToolTip(this.BTNdelete, "Remove this row from the list");
            this.BTNdelete.UseVisualStyleBackColor = true;
            this.BTNdelete.Click += new System.EventHandler(this.BTNdelete_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 0;
            // 
            // BTNmoveup
            // 
            this.BTNmoveup.Location = new System.Drawing.Point(481, 2);
            this.BTNmoveup.Name = "BTNmoveup";
            this.BTNmoveup.Size = new System.Drawing.Size(23, 23);
            this.BTNmoveup.TabIndex = 2;
            this.BTNmoveup.Text = "▲";
            this.toolTip1.SetToolTip(this.BTNmoveup, "Move this row higher on the list");
            this.BTNmoveup.UseVisualStyleBackColor = true;
            this.BTNmoveup.Click += new System.EventHandler(this.BTNmoveup_Click);
            // 
            // BTNmovedown
            // 
            this.BTNmovedown.Location = new System.Drawing.Point(510, 2);
            this.BTNmovedown.Name = "BTNmovedown";
            this.BTNmovedown.Size = new System.Drawing.Size(23, 23);
            this.BTNmovedown.TabIndex = 3;
            this.BTNmovedown.Text = "▼";
            this.toolTip1.SetToolTip(this.BTNmovedown, "Move this row lower on the list");
            this.BTNmovedown.UseVisualStyleBackColor = true;
            this.BTNmovedown.Click += new System.EventHandler(this.BTNmovedown_Click);
            // 
            // TBXlongname
            // 
            this.TBXlongname.BorderColor = System.Drawing.Color.Red;
            this.TBXlongname.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.TBXlongname.Location = new System.Drawing.Point(110, 4);
            this.TBXlongname.Name = "TBXlongname";
            this.TBXlongname.Size = new System.Drawing.Size(365, 20);
            this.TBXlongname.TabIndex = 1;
            this.TBXlongname.TextChanged += new System.EventHandler(this.TBXlongname_TextChanged);
            // 
            // TBXshortname
            // 
            this.TBXshortname.BorderColor = System.Drawing.Color.Red;
            this.TBXshortname.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.TBXshortname.Location = new System.Drawing.Point(4, 4);
            this.TBXshortname.Name = "TBXshortname";
            this.TBXshortname.Size = new System.Drawing.Size(100, 20);
            this.TBXshortname.TabIndex = 0;
            this.TBXshortname.TextChanged += new System.EventHandler(this.TBXshortname_TextChanged);
            // 
            // CustomizableURLViewModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BTNmovedown);
            this.Controls.Add(this.BTNmoveup);
            this.Controls.Add(this.BTNdelete);
            this.Controls.Add(this.TBXlongname);
            this.Controls.Add(this.TBXshortname);
            this.Name = "CustomizableURLViewModel";
            this.Size = new System.Drawing.Size(619, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TicketFinder.Settings.TextboxWithBorder TBXshortname;
        private TicketFinder.Settings.TextboxWithBorder TBXlongname;
        private System.Windows.Forms.Button BTNdelete;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button BTNmoveup;
        private System.Windows.Forms.Button BTNmovedown;
    }
}
