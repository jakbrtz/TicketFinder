
namespace TicketFinder.Settings
{
    partial class SettingsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BTNhelp = new System.Windows.Forms.Button();
            this.BTNadd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BTNcanel = new System.Windows.Forms.Button();
            this.BTNsave = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.BTNimport = new System.Windows.Forms.Button();
            this.BTNexport = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // BTNhelp
            // 
            this.BTNhelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BTNhelp.Location = new System.Drawing.Point(174, 295);
            this.BTNhelp.Name = "BTNhelp";
            this.BTNhelp.Size = new System.Drawing.Size(75, 23);
            this.BTNhelp.TabIndex = 14;
            this.BTNhelp.Text = "Help";
            this.BTNhelp.UseVisualStyleBackColor = true;
            this.BTNhelp.Click += new System.EventHandler(this.BTNhelp_Click);
            // 
            // BTNadd
            // 
            this.BTNadd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BTNadd.Location = new System.Drawing.Point(12, 295);
            this.BTNadd.Name = "BTNadd";
            this.BTNadd.Size = new System.Drawing.Size(75, 23);
            this.BTNadd.TabIndex = 12;
            this.BTNadd.Text = "Add";
            this.BTNadd.UseVisualStyleBackColor = true;
            this.BTNadd.Click += new System.EventHandler(this.BTNadd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "URL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Display Name";
            // 
            // BTNcanel
            // 
            this.BTNcanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTNcanel.Location = new System.Drawing.Point(579, 295);
            this.BTNcanel.Name = "BTNcanel";
            this.BTNcanel.Size = new System.Drawing.Size(75, 23);
            this.BTNcanel.TabIndex = 17;
            this.BTNcanel.Text = "Cancel";
            this.BTNcanel.UseVisualStyleBackColor = true;
            this.BTNcanel.Click += new System.EventHandler(this.BTNcanel_Click);
            // 
            // BTNsave
            // 
            this.BTNsave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTNsave.Location = new System.Drawing.Point(417, 295);
            this.BTNsave.Name = "BTNsave";
            this.BTNsave.Size = new System.Drawing.Size(75, 23);
            this.BTNsave.TabIndex = 15;
            this.BTNsave.Text = "Save";
            this.BTNsave.UseVisualStyleBackColor = true;
            this.BTNsave.Click += new System.EventHandler(this.BTNsave_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 25);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(642, 264);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // BTNimport
            // 
            this.BTNimport.Location = new System.Drawing.Point(93, 295);
            this.BTNimport.Name = "BTNimport";
            this.BTNimport.Size = new System.Drawing.Size(75, 23);
            this.BTNimport.TabIndex = 13;
            this.BTNimport.Text = "Import";
            this.BTNimport.UseVisualStyleBackColor = true;
            this.BTNimport.Click += new System.EventHandler(this.BTNimport_Click);
            // 
            // BTNexport
            // 
            this.BTNexport.Location = new System.Drawing.Point(498, 295);
            this.BTNexport.Name = "BTNexport";
            this.BTNexport.Size = new System.Drawing.Size(75, 23);
            this.BTNexport.TabIndex = 16;
            this.BTNexport.Text = "Export";
            this.BTNexport.UseVisualStyleBackColor = true;
            this.BTNexport.Click += new System.EventHandler(this.BTNexport_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "TicketFinder.txt";
            this.openFileDialog1.Filter = "All Files (*.*)|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "TicketFinder.txt";
            this.saveFileDialog1.Filter = "Any kind of text, I don\'t care|*.*";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 330);
            this.Controls.Add(this.BTNexport);
            this.Controls.Add(this.BTNimport);
            this.Controls.Add(this.BTNhelp);
            this.Controls.Add(this.BTNadd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.BTNsave);
            this.Controls.Add(this.BTNcanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BTNhelp;
        private System.Windows.Forms.Button BTNadd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTNcanel;
        private System.Windows.Forms.Button BTNsave;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button BTNimport;
        private System.Windows.Forms.Button BTNexport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}