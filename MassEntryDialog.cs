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
    public partial class MassEntryDialog : Form
    {
        public MassEntryDialog()
        {
            InitializeComponent();
        }
        private Dictionary<string, string[]> index = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> Index
        {
            get { return index; }
            set { index = value; ledgerPageControl.BindIndex(index); }
        }
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
        public List<Entry> UserEntries = new List<Entry>();
    }
}
