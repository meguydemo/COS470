using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Register
{
    public partial class UserEntryControl : UserControl
    {
        public UserEntryControl() { InitializeComponent(); }

        //
        // Date
        public string LabelDate 
        { 
            get
            {
                return labelDate.Text;
            }
            set
            {
                labelDate.Text = value;
            }
        }
        public string DateText { get; private set; }
        private void maskedTextBoxDate_TextChanged(object sender, EventArgs e){ DateText = maskedTextBoxDate.Text; }
        
        // 
        // Memo.
        public string LabelMemo
        {
            get
            {
                return labelMemo.Text;
            }
            set 
            {
                labelMemo.Text = value;
            }
        }
        public string MemoText { get; private set; }
        private void textBoxMemo_TextChanged(object sender, EventArgs e) { MemoText = textBoxMemo.Text; }

        //
        // Details
        public string LabelDetails
        {
            get
            {
                return labelDetails.Text;
            }
            set
            {
                labelDetails.Text = value;
            }
        }
        public string DetailsText { get; private set; }
        private void textBoxDetails_TextChanged(object sender, EventArgs e) { DetailsText = textBoxDetails.Text; }

        //
        // Debit
        public string LabelDebit
        {
            get
            {
                return labelDebit.Text;
            }
            set
            {
                labelDebit.Text = value;
            }
        }
        public string DebitText { get; private set; }
        private void textBoxDebit_TextChanged(object sender, EventArgs e) { DebitText = textBoxDebit.Text; }

        //
        // Credit
        public string LabelCredit
        {
            get
            {
                return labelCredit.Text;
            }
            set
            {
                labelCredit.Text = value;
            }
        }
        public string CreditText { get; private set; }
        private void textBoxCredit_TextChanged(object sender, EventArgs e) { CreditText = textBoxCredit.Text; }

        private void UserEntryControl_Load(object sender, EventArgs e)
        {

        }
    }
}
