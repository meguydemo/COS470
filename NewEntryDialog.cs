using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Register
{
    public partial class NewEntryDialog : Form
    {
        private Dictionary<string, string[]> index = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> Index 
        {
            get { return index; } 
            set { index = value; ledgerPageControl.BindIndex(index); }
        }
        public int LedgerYear { get; set; }
        public string TargetLedger 
        {
            get { return ledgerPageControl.TargetLedger; }
            set { ledgerPageControl.TargetLedger = value; } 
        }
        public string TargetPage 
        {
            get { return ledgerPageControl.TargetPage; } 
            set { ledgerPageControl.TargetPage = value; }
        }
        
        public Entry UserEntry = null;

        public NewEntryDialog()
        {
            InitializeComponent();
        }
        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            // Assign values to UserEntry
            UserEntry = new Entry();
            UserEntry.SetDate(userEntryControl.DateText);
            UserEntry.Memorandum = userEntryControl.MemoText;
            UserEntry.Details = userEntryControl.DetailsText;
            if (String.IsNullOrWhiteSpace(userEntryControl.DebitText)) 
            { UserEntry.Debit = 0; } else { UserEntry.Debit = Double.Parse(userEntryControl.DebitText); }
            if (String.IsNullOrWhiteSpace(userEntryControl.CreditText)) 
            { UserEntry.Credit = 0; } else { UserEntry.Credit = Double.Parse(userEntryControl.CreditText); }

            // Validate user entry and either close app or display an error
            ValidateUserEntry();
        }
        private void ValidateUserEntry()
        {
            // If there is no memorandum...
            if(string.IsNullOrEmpty(UserEntry.Memorandum))
            {
                System.Diagnostics.Debug.WriteLine("NewEntryDialog: Entry cannot have a null or empty memorandum.");
                userEntryControl.LabelMemo = $"Memo (Required)";
            }
            // If the entry is not from the correct year...
            else if (UserEntry.Date.Year != LedgerYear)
            {
                System.Diagnostics.Debug.WriteLine($"NewEntryDialog: Entry must be from the same year as target ledger ({LedgerYear}).");
                userEntryControl.LabelDate = $"Date ({LedgerYear})";
            }
            // If there is a debit and credit value
            else if(UserEntry.Debit > 0 && UserEntry.Credit > 0)
            {
                System.Diagnostics.Debug.WriteLine($"NewEntryDialog: Entry debit ({UserEntry.Debit}) and credit({UserEntry.Credit}) cannot both have a value.");
                userEntryControl.LabelDebit = "";
                userEntryControl.LabelCredit = "";
            }
            // User entry validated
            else
            {
                System.Diagnostics.Debug.WriteLine("NewEntryDialog: User entry properly validated.");
                DialogResult = DialogResult.OK;
                return;
            }

            // If any of the invalid clauses are hit, the program will not return...
            // so nullify in the invalid UserEntry to prevent it from accidentally being delivered to MainForm
            UserEntry = null;
        }
    }
}
