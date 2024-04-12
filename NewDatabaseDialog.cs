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
    public partial class NewDatabaseDialog : Form
    {
        public string CollectionName { get; private set; }
        public Dictionary<string, List<string>> LedgerPages = new Dictionary<string, List<string>>();
        private Dictionary<string, int> TreeViewIndex = new Dictionary<string, int>();
        public NewDatabaseDialog()
        {
            InitializeComponent();
            treeViewLedgerPages.Nodes.Clear();
            treeViewLedgerPages.Nodes.Add(textBoxCollectionName.Text);
        }
        private void TextBoxCollectionName_TextChanged(object sender, EventArgs e)
        {
            CollectionName = textBoxCollectionName.Text;
            treeViewLedgerPages.Nodes[0].Text = textBoxCollectionName.Text;
        }

        private void ButtonSelectPath_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ButtonAddLedgerPage_Click(object sender, EventArgs e)
        {
            string trimmedLedger = textBoxLedger.Text.Trim();
            string trimmedPage = textBoxPage.Text.Trim();
            if(trimmedLedger != "" && trimmedPage != "")
            {
                if(LedgerPages.ContainsKey(trimmedLedger))
                {
                    // If the list of Keys does contain the passed ledger,
                    // then append the new page to the existing node and dict key
                    LedgerPages[trimmedLedger].Add(textBoxPage.Text.Trim());
                    treeViewLedgerPages.Nodes[0].Nodes[TreeViewIndex[trimmedLedger]].Nodes.Add(trimmedPage);
                    textBoxPage.Text = string.Empty;
                }
                else
                {
                    // If the list of Keys does not contain the passed ledger, 
                    // then a new ledger must be added, first to the Index...
                    TreeViewIndex.Add(trimmedLedger, TreeViewIndex.Count); // 0 if first table

                    // ...then to the dictionary
                    var l = new List<string>();
                    l.Add(textBoxPage.Text.Trim());
                    LedgerPages.Add(trimmedLedger, l);

                    // Finally, update the ui
                    treeViewLedgerPages.Nodes[0].Nodes.Add(trimmedLedger);
                    treeViewLedgerPages.Nodes[0].Nodes[TreeViewIndex[trimmedLedger]].Nodes.Add(trimmedPage);
                    textBoxPage.Text = string.Empty;
                }
            }
        }

        private void NewDatabaseDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
