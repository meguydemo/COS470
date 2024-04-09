namespace Register
{
    partial class NewDatabaseDialog
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
            this.treeViewLedgerPages = new System.Windows.Forms.TreeView();
            this.textBoxCollectionName = new System.Windows.Forms.TextBox();
            this.buttonSelectPath = new System.Windows.Forms.Button();
            this.labelLedger = new System.Windows.Forms.Label();
            this.labelPage = new System.Windows.Forms.Label();
            this.textBoxLedger = new System.Windows.Forms.TextBox();
            this.textBoxPage = new System.Windows.Forms.TextBox();
            this.buttonAddLedgerPage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeViewLedgerPages
            // 
            this.treeViewLedgerPages.Location = new System.Drawing.Point(12, 12);
            this.treeViewLedgerPages.Name = "treeViewLedgerPages";
            this.treeViewLedgerPages.Size = new System.Drawing.Size(244, 426);
            this.treeViewLedgerPages.TabIndex = 0;
            // 
            // textBoxCollectionName
            // 
            this.textBoxCollectionName.Location = new System.Drawing.Point(262, 12);
            this.textBoxCollectionName.Name = "textBoxCollectionName";
            this.textBoxCollectionName.Size = new System.Drawing.Size(169, 22);
            this.textBoxCollectionName.TabIndex = 1;
            this.textBoxCollectionName.Text = "Collection of Ledgers";
            this.textBoxCollectionName.TextChanged += new System.EventHandler(this.TextBoxCollectionName_TextChanged);
            // 
            // buttonSelectPath
            // 
            this.buttonSelectPath.Location = new System.Drawing.Point(625, 414);
            this.buttonSelectPath.Name = "buttonSelectPath";
            this.buttonSelectPath.Size = new System.Drawing.Size(127, 23);
            this.buttonSelectPath.TabIndex = 2;
            this.buttonSelectPath.Text = "Select Path";
            this.buttonSelectPath.UseVisualStyleBackColor = true;
            this.buttonSelectPath.Click += new System.EventHandler(this.ButtonSelectPath_Click);
            // 
            // labelLedger
            // 
            this.labelLedger.AutoSize = true;
            this.labelLedger.Location = new System.Drawing.Point(263, 64);
            this.labelLedger.Name = "labelLedger";
            this.labelLedger.Size = new System.Drawing.Size(50, 16);
            this.labelLedger.TabIndex = 3;
            this.labelLedger.Text = "Ledger";
            // 
            // labelPage
            // 
            this.labelPage.AutoSize = true;
            this.labelPage.Location = new System.Drawing.Point(466, 64);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(40, 16);
            this.labelPage.TabIndex = 4;
            this.labelPage.Text = "Page";
            // 
            // textBoxLedger
            // 
            this.textBoxLedger.Location = new System.Drawing.Point(266, 83);
            this.textBoxLedger.Name = "textBoxLedger";
            this.textBoxLedger.Size = new System.Drawing.Size(182, 22);
            this.textBoxLedger.TabIndex = 5;
            // 
            // textBoxPage
            // 
            this.textBoxPage.Location = new System.Drawing.Point(469, 83);
            this.textBoxPage.Name = "textBoxPage";
            this.textBoxPage.Size = new System.Drawing.Size(208, 22);
            this.textBoxPage.TabIndex = 6;
            // 
            // buttonAddLedgerPage
            // 
            this.buttonAddLedgerPage.Location = new System.Drawing.Point(683, 83);
            this.buttonAddLedgerPage.Name = "buttonAddLedgerPage";
            this.buttonAddLedgerPage.Size = new System.Drawing.Size(75, 23);
            this.buttonAddLedgerPage.TabIndex = 7;
            this.buttonAddLedgerPage.Text = "Add";
            this.buttonAddLedgerPage.UseVisualStyleBackColor = true;
            this.buttonAddLedgerPage.Click += new System.EventHandler(this.ButtonAddLedgerPage_Click);
            // 
            // NewDatabaseDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 450);
            this.Controls.Add(this.buttonAddLedgerPage);
            this.Controls.Add(this.textBoxPage);
            this.Controls.Add(this.textBoxLedger);
            this.Controls.Add(this.labelPage);
            this.Controls.Add(this.labelLedger);
            this.Controls.Add(this.buttonSelectPath);
            this.Controls.Add(this.textBoxCollectionName);
            this.Controls.Add(this.treeViewLedgerPages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "NewDatabaseDialog";
            this.Text = "New Ledger Set";
            this.Load += new System.EventHandler(this.NewDatabaseDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewLedgerPages;
        private System.Windows.Forms.TextBox textBoxCollectionName;
        private System.Windows.Forms.Button buttonSelectPath;
        private System.Windows.Forms.Label labelLedger;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.TextBox textBoxLedger;
        private System.Windows.Forms.TextBox textBoxPage;
        private System.Windows.Forms.Button buttonAddLedgerPage;
    }
}