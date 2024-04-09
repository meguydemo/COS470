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
    public partial class LedgerPageControl : UserControl
    {
        public LedgerPageControl()
        {
            InitializeComponent();
        }
        private bool hasBoundIndex = false;
        public void BindIndex(Dictionary<string, string[]> index)
        {
            Index = index;
            hasBoundIndex = true;
            comboBoxLedger.DataSource = index.Keys.ToList();
            TargetLedger = Index.Keys.ToList()[0];
            comboBoxPage.DataSource = Index[TargetLedger];
            TargetPage = Index[TargetLedger][0];
        }
        public Dictionary<string, string[]> Index { get; private set; }
        public string TargetLedger 
        { 
            get { return comboBoxLedger.Text; }
            set { if (hasBoundIndex) comboBoxLedger.Text = value; } 
        }
        public string TargetPage 
        { 
            get { return comboBoxPage.Text; } 
            set { if (hasBoundIndex) comboBoxPage.Text = value; }
        }
        private void comboBoxLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            TargetLedger = comboBoxLedger.Text;
            comboBoxPage.DataSource = Index[TargetLedger];
            TargetPage = Index[TargetLedger][0];
        }
        private void comboBoxPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            TargetPage = comboBoxPage.Text;
        }
    }
}
