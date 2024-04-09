namespace Register
{
    partial class LedgerPageControl
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
            this.labelPages = new System.Windows.Forms.Label();
            this.labelLedger = new System.Windows.Forms.Label();
            this.comboBoxLedger = new System.Windows.Forms.ComboBox();
            this.comboBoxPage = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelPages
            // 
            this.labelPages.AutoSize = true;
            this.labelPages.Location = new System.Drawing.Point(222, 0);
            this.labelPages.Name = "labelPages";
            this.labelPages.Size = new System.Drawing.Size(47, 16);
            this.labelPages.TabIndex = 10;
            this.labelPages.Text = "Pages";
            // 
            // labelLedger
            // 
            this.labelLedger.AutoSize = true;
            this.labelLedger.Location = new System.Drawing.Point(-3, 0);
            this.labelLedger.Name = "labelLedger";
            this.labelLedger.Size = new System.Drawing.Size(57, 16);
            this.labelLedger.TabIndex = 9;
            this.labelLedger.Text = "Ledgers";
            // 
            // comboBoxLedger
            // 
            this.comboBoxLedger.FormattingEnabled = true;
            this.comboBoxLedger.Location = new System.Drawing.Point(0, 19);
            this.comboBoxLedger.Name = "comboBoxLedger";
            this.comboBoxLedger.Size = new System.Drawing.Size(219, 24);
            this.comboBoxLedger.TabIndex = 8;
            this.comboBoxLedger.SelectedIndexChanged += new System.EventHandler(this.comboBoxLedger_SelectedIndexChanged);
            // 
            // comboBoxPage
            // 
            this.comboBoxPage.FormattingEnabled = true;
            this.comboBoxPage.Location = new System.Drawing.Point(225, 19);
            this.comboBoxPage.Name = "comboBoxPage";
            this.comboBoxPage.Size = new System.Drawing.Size(221, 24);
            this.comboBoxPage.TabIndex = 7;
            this.comboBoxPage.SelectedIndexChanged += new System.EventHandler(this.comboBoxPage_SelectedIndexChanged);
            // 
            // LedgerPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelPages);
            this.Controls.Add(this.labelLedger);
            this.Controls.Add(this.comboBoxLedger);
            this.Controls.Add(this.comboBoxPage);
            this.Name = "LedgerPageControl";
            this.Size = new System.Drawing.Size(445, 44);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPages;
        private System.Windows.Forms.Label labelLedger;
        private System.Windows.Forms.ComboBox comboBoxLedger;
        private System.Windows.Forms.ComboBox comboBoxPage;
    }
}
