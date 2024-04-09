namespace Register
{
    partial class UserEntryControl
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
            this.labelDate = new System.Windows.Forms.Label();
            this.maskedTextBoxDate = new System.Windows.Forms.MaskedTextBox();
            this.labelMemo = new System.Windows.Forms.Label();
            this.textBoxMemo = new System.Windows.Forms.TextBox();
            this.labelDebit = new System.Windows.Forms.Label();
            this.labelCredit = new System.Windows.Forms.Label();
            this.textBoxDebit = new System.Windows.Forms.TextBox();
            this.textBoxCredit = new System.Windows.Forms.TextBox();
            this.labelDetails = new System.Windows.Forms.Label();
            this.textBoxDetails = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(3, 0);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(36, 16);
            this.labelDate.TabIndex = 0;
            this.labelDate.Text = "Date";
            // 
            // maskedTextBoxDate
            // 
            this.maskedTextBoxDate.Location = new System.Drawing.Point(0, 20);
            this.maskedTextBoxDate.Mask = "00/00/0000";
            this.maskedTextBoxDate.Name = "maskedTextBoxDate";
            this.maskedTextBoxDate.Size = new System.Drawing.Size(100, 22);
            this.maskedTextBoxDate.TabIndex = 1;
            this.maskedTextBoxDate.ValidatingType = typeof(System.DateTime);
            this.maskedTextBoxDate.TextChanged += new System.EventHandler(this.maskedTextBoxDate_TextChanged);
            // 
            // labelMemo
            // 
            this.labelMemo.AutoSize = true;
            this.labelMemo.Location = new System.Drawing.Point(115, 0);
            this.labelMemo.Name = "labelMemo";
            this.labelMemo.Size = new System.Drawing.Size(48, 16);
            this.labelMemo.TabIndex = 2;
            this.labelMemo.Text = "Memo.";
            // 
            // textBoxMemo
            // 
            this.textBoxMemo.Location = new System.Drawing.Point(112, 20);
            this.textBoxMemo.Name = "textBoxMemo";
            this.textBoxMemo.Size = new System.Drawing.Size(251, 22);
            this.textBoxMemo.TabIndex = 3;
            this.textBoxMemo.TextChanged += new System.EventHandler(this.textBoxMemo_TextChanged);
            // 
            // labelDebit
            // 
            this.labelDebit.AutoSize = true;
            this.labelDebit.Location = new System.Drawing.Point(375, 0);
            this.labelDebit.Name = "labelDebit";
            this.labelDebit.Size = new System.Drawing.Size(39, 16);
            this.labelDebit.TabIndex = 4;
            this.labelDebit.Text = "Debit";
            // 
            // labelCredit
            // 
            this.labelCredit.AutoSize = true;
            this.labelCredit.Location = new System.Drawing.Point(480, -1);
            this.labelCredit.Name = "labelCredit";
            this.labelCredit.Size = new System.Drawing.Size(42, 16);
            this.labelCredit.TabIndex = 5;
            this.labelCredit.Text = "Credit";
            // 
            // textBoxDebit
            // 
            this.textBoxDebit.Location = new System.Drawing.Point(370, 20);
            this.textBoxDebit.Name = "textBoxDebit";
            this.textBoxDebit.Size = new System.Drawing.Size(100, 22);
            this.textBoxDebit.TabIndex = 6;
            this.textBoxDebit.TextChanged += new System.EventHandler(this.textBoxDebit_TextChanged);
            // 
            // textBoxCredit
            // 
            this.textBoxCredit.Location = new System.Drawing.Point(477, 19);
            this.textBoxCredit.Name = "textBoxCredit";
            this.textBoxCredit.Size = new System.Drawing.Size(97, 22);
            this.textBoxCredit.TabIndex = 7;
            this.textBoxCredit.TextChanged += new System.EventHandler(this.textBoxCredit_TextChanged);
            // 
            // labelDetails
            // 
            this.labelDetails.AutoSize = true;
            this.labelDetails.Location = new System.Drawing.Point(0, 49);
            this.labelDetails.Name = "labelDetails";
            this.labelDetails.Size = new System.Drawing.Size(49, 16);
            this.labelDetails.TabIndex = 8;
            this.labelDetails.Text = "Details";
            // 
            // textBoxDetails
            // 
            this.textBoxDetails.Location = new System.Drawing.Point(0, 68);
            this.textBoxDetails.Name = "textBoxDetails";
            this.textBoxDetails.Size = new System.Drawing.Size(574, 22);
            this.textBoxDetails.TabIndex = 9;
            this.textBoxDetails.TextChanged += new System.EventHandler(this.textBoxDetails_TextChanged);
            // 
            // UserEntryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxDetails);
            this.Controls.Add(this.labelDetails);
            this.Controls.Add(this.textBoxCredit);
            this.Controls.Add(this.textBoxDebit);
            this.Controls.Add(this.labelCredit);
            this.Controls.Add(this.labelDebit);
            this.Controls.Add(this.textBoxMemo);
            this.Controls.Add(this.labelMemo);
            this.Controls.Add(this.maskedTextBoxDate);
            this.Controls.Add(this.labelDate);
            this.Name = "UserEntryControl";
            this.Size = new System.Drawing.Size(573, 91);
            this.Load += new System.EventHandler(this.UserEntryControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxDate;
        private System.Windows.Forms.Label labelMemo;
        private System.Windows.Forms.TextBox textBoxMemo;
        private System.Windows.Forms.Label labelDebit;
        private System.Windows.Forms.Label labelCredit;
        private System.Windows.Forms.TextBox textBoxDebit;
        private System.Windows.Forms.TextBox textBoxCredit;
        private System.Windows.Forms.Label labelDetails;
        private System.Windows.Forms.TextBox textBoxDetails;
    }
}
