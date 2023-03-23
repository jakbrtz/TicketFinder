
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
            this.CBXrange = new System.Windows.Forms.CheckBox();
            this.RBNnonnum = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // BTNdelete
            // 
            this.BTNdelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTNdelete.Location = new System.Drawing.Point(539, 2);
            this.BTNdelete.Name = "BTNdelete";
            this.BTNdelete.Size = new System.Drawing.Size(75, 23);
            this.BTNdelete.TabIndex = 6;
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
            this.BTNmoveup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTNmoveup.Location = new System.Drawing.Point(481, 2);
            this.BTNmoveup.Name = "BTNmoveup";
            this.BTNmoveup.Size = new System.Drawing.Size(23, 23);
            this.BTNmoveup.TabIndex = 4;
            this.BTNmoveup.Text = "▲";
            this.toolTip1.SetToolTip(this.BTNmoveup, "Move this row higher on the list");
            this.BTNmoveup.UseVisualStyleBackColor = true;
            this.BTNmoveup.Click += new System.EventHandler(this.BTNmoveup_Click);
            // 
            // BTNmovedown
            // 
            this.BTNmovedown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTNmovedown.Location = new System.Drawing.Point(510, 2);
            this.BTNmovedown.Name = "BTNmovedown";
            this.BTNmovedown.Size = new System.Drawing.Size(23, 23);
            this.BTNmovedown.TabIndex = 5;
            this.BTNmovedown.Text = "▼";
            this.toolTip1.SetToolTip(this.BTNmovedown, "Move this row lower on the list");
            this.BTNmovedown.UseVisualStyleBackColor = true;
            this.BTNmovedown.Click += new System.EventHandler(this.BTNmovedown_Click);
            // 
            // TBXlongname
            // 
            this.TBXlongname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBXlongname.BorderColor = System.Drawing.Color.Red;
            this.TBXlongname.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.TBXlongname.Location = new System.Drawing.Point(110, 4);
            this.TBXlongname.Name = "TBXlongname";
            this.TBXlongname.Size = new System.Drawing.Size(249, 20);
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
            // CBXrange
            // 
            this.CBXrange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CBXrange.AutoSize = true;
            this.CBXrange.Checked = true;
            this.CBXrange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBXrange.Location = new System.Drawing.Point(365, 6);
            this.CBXrange.Name = "CBXrange";
            this.CBXrange.Size = new System.Drawing.Size(58, 17);
            this.CBXrange.TabIndex = 2;
            this.CBXrange.Text = "Range";
            this.toolTip1.SetToolTip(this.CBXrange, "If the searched number is close to a number from this URL\'s history, should this " +
        "URL be searched?");
            this.CBXrange.UseVisualStyleBackColor = true;
            this.CBXrange.CheckedChanged += new System.EventHandler(this.CBXrange_CheckedChanged);
            // 
            // RBNnonnum
            // 
            this.RBNnonnum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RBNnonnum.AutoSize = true;
            this.RBNnonnum.Location = new System.Drawing.Point(429, 5);
            this.RBNnonnum.Name = "RBNnonnum";
            this.RBNnonnum.Size = new System.Drawing.Size(46, 17);
            this.RBNnonnum.TabIndex = 3;
            this.RBNnonnum.TabStop = true;
            this.RBNnonnum.Text = "Text";
            this.toolTip1.SetToolTip(this.RBNnonnum, "If something that is not a number gets searched, should this URL be used?");
            this.RBNnonnum.UseVisualStyleBackColor = true;
            this.RBNnonnum.CheckedChanged += new System.EventHandler(this.RBNnonnum_CheckedChanged);
            // 
            // CustomizableURLViewModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RBNnonnum);
            this.Controls.Add(this.CBXrange);
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
        private System.Windows.Forms.CheckBox CBXrange;
        private System.Windows.Forms.RadioButton RBNnonnum;
    }
}
