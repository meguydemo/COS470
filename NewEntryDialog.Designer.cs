namespace Register
{
    partial class NewEntryDialog
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
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.ledgerPageControl = new Register.LedgerPageControl();
            this.userEntryControl = new Register.UserEntryControl();
            this.SuspendLayout();
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(488, 23);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(100, 23);
            this.buttonSubmit.TabIndex = 5;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.ButtonSubmit_Click);
            // 
            // ledgerPageControl
            // 
            this.ledgerPageControl.Location = new System.Drawing.Point(12, 6);
            this.ledgerPageControl.Name = "ledgerPageControl";
            this.ledgerPageControl.Size = new System.Drawing.Size(445, 40);
            this.ledgerPageControl.TabIndex = 6;
            this.ledgerPageControl.TargetLedger = "";
            this.ledgerPageControl.TargetPage = "";
            // 
            // userEntryControl
            // 
            this.userEntryControl.LabelCredit = "Credit";
            this.userEntryControl.LabelDate = "Date";
            this.userEntryControl.LabelDebit = "Debit";
            this.userEntryControl.LabelDetails = "Details";
            this.userEntryControl.LabelMemo = "Memo.";
            this.userEntryControl.Location = new System.Drawing.Point(12, 52);
            this.userEntryControl.Name = "userEntryControl";
            this.userEntryControl.Size = new System.Drawing.Size(576, 91);
            this.userEntryControl.TabIndex = 7;
            // 
            // NewEntryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 157);
            this.Controls.Add(this.userEntryControl);
            this.Controls.Add(this.ledgerPageControl);
            this.Controls.Add(this.buttonSubmit);
            this.Name = "NewEntryDialog";
            this.Text = "New Entry";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonSubmit;
        private LedgerPageControl ledgerPageControl;
        private UserEntryControl userEntryControl;
    }
}