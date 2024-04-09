namespace Register
{
    partial class MassEntryDialog
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
            this.ledgerPageControl = new Register.LedgerPageControl();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ledgerPageControl
            // 
            this.ledgerPageControl.Location = new System.Drawing.Point(13, 13);
            this.ledgerPageControl.Name = "ledgerPageControl";
            this.ledgerPageControl.Size = new System.Drawing.Size(445, 44);
            this.ledgerPageControl.TabIndex = 0;
            this.ledgerPageControl.TargetLedger = "";
            this.ledgerPageControl.TargetPage = "";
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(465, 33);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(121, 23);
            this.buttonSubmit.TabIndex = 2;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            // 
            // MassEntryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 243);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.ledgerPageControl);
            this.Name = "MassEntryDialog";
            this.Text = "MassEntryDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private LedgerPageControl ledgerPageControl;
        private System.Windows.Forms.Button buttonSubmit;
    }
}