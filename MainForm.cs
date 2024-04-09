using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;

namespace Register
{
    public partial class MainForm : Form
    {
        // DataAccess, Database, etc.
        public static string databasePath = string.Empty;
        private DataAccess database;

        // Ledgers and ledger information
        public Dictionary<string, string[]> Index;
        public Ledger TargetLedger { get; private set; }

        // DateTime bounds for grouping database
        public DateTime GroupStartDate
        {
            get { return dateTimePickerStartDate.Value; }
            private set { dateTimePickerStartDate.Value = value; } 
        }
        public DateTime GroupEndDate 
        {
            get { return dateTimePickerEndDate.Value; }
            private set { dateTimePickerEndDate.Value = value; } 
        }

        // Final running balance, displayed in label
        public double RunningBalance
        {
            get { return Int32.Parse(labelRunningBalance.Text.Substring(17)); }
            set { labelRunningBalance.Text = $"Running balance: {value}"; }
        }
        // ToolStrip events
        private void ToolStripMenuItem_FileNew_Clicked(object sender, EventArgs e)
        {
            if (TargetLedger.TargetPage == Ledger.Summary)
            {
                // Cannot add an entry to the summary table because it doesn't actually exist in the
                // database; display some sort of error on the screen.
                System.Diagnostics.Debug.WriteLine("Cannot add a new entry to a 'Summary' table.");
            }
            else
            {
                // First, have the user specify the ledgers and ledger pages in a dialog.
                string dialogCollectionName = string.Empty;
                Dictionary<string, List<string>> dialogLedgerPages = new Dictionary<string, List<string>>();
                using (var dialog = new NewDatabaseDialog())
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        dialogCollectionName = dialog.CollectionName;
                        dialogLedgerPages = dialog.LedgerPages;
                    }
                }

                // Second, have the user specify a path for the database to be saved.
                string directory = string.Empty;
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        directory = dialog.SelectedPath;
                    }
                }

                // Finally, pass all of the dialog information to DataAccess
                DataAccess.CreateNewDatabase(dialogCollectionName, directory, dialogLedgerPages);
            }
        }
        private void ToolStripMenuItem_FileLoad_Clicked(object sender, EventArgs e)
        {
            // Uses an OpenFileDialog instance to allow a user to open a database file from anywhere
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // By default, filters directory contents to show only .db files
                openFileDialog.InitialDirectory = Application.StartupPath;
                openFileDialog.Filter = "SQLite Database files (*.db)|*.db|All files (*.*)|*.*";

                // If "DialogResult.OK", then it binds the path name to databasePath 
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    databasePath = openFileDialog.FileName;
                }
            }

            // Prevents a crash if a user fails to find a database
            if (databasePath != null)
            {
                LoadDatabase(true);
            }
        }
        private void ToolStripMenuItem_FileExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0); // convention; 0 is a clean exit
        }
        private void ToolStripMenuItem_ToolsMassEntry_Click(object sender, EventArgs e)
        {
            if (TargetLedger != null && TargetLedger.TargetPage != "Summary")
            {
                string ledgerName = TargetLedger.Name;
                string pageName = TargetLedger.TargetPage;
                using (var dialog = new MassEntryDialog())
                {

                }
            }
        }
        
        // Function that loads a database from the databasePath.
        public void LoadDatabase(bool reset)
        {
            // If there is a valid path
            if (databasePath != string.Empty)
            {
                database = new DataAccess(databasePath);
                if (reset) ResetUI(); // Target first ledger; reset combo boxes; reset date
                else RefreshUI();     // Retarget original ledger + page
                
                // Construct index with string[] from index with List<string>
                Index = new Dictionary<string, string[]>();
                foreach(string key in database.Index.Keys)
                {
                    Index.Add(key, database.Index[key].ToArray());
                }
            }
        }

        // UI Refreshing & formatting
        private void ResetUI()
        {
            // Reset ledger information
            TargetLedger = database.Ledgers[0];
            comboBoxLedger.DataSource = database.Index.Keys.ToList();
            comboBoxLedger.Text = TargetLedger.Name;

            // Reset Page information
            TargetLedger.TargetPage = TargetLedger.Pages.Keys.ToArray()[0];
            comboBoxPage.DataSource = TargetLedger.Pages.Keys.ToList();
            comboBoxPage.Text = TargetLedger.TargetPage;

            // Reset timing information
            var initialDate = TargetLedger.NewestEntry.Date;
            var firstDayOfTheMonth = new DateTime(initialDate.Year, initialDate.Month, 1);
            var lastDayOfTheMonth = firstDayOfTheMonth.AddMonths(1).AddSeconds(-1);
            GroupStartDate = firstDayOfTheMonth; // setter function updates UI
            GroupEndDate = lastDayOfTheMonth;    // setter function updates UI

            RefreshTargetData();
        }
        private void RefreshUI()
        {
            // Retarget correct ledger
            string targetLedgerName = TargetLedger.Name;
            string targetPageName = TargetLedger.TargetPage;
            foreach (var ledger in database.Ledgers)
            {
                if (targetLedgerName == ledger.Name)
                {
                    TargetLedger = ledger;
                    TargetLedger.TargetPage = targetPageName;
                }
            }

            // The target page may stay the same...
            // But the target entry should be nulled in case it was deleted
            TargetLedger.TargetEntry = null;
            labelTargetEntry.Text = "No entry.";

            RefreshTargetData();
        }
        public void RefreshTargetData()
        {
            // Sort the target data into before, during, and after sections based off of
            // the starting and ending dates (set by DateTimePickers in MainForm)
            var tuple = GroupEntriesByDate(TargetLedger.Pages[TargetLedger.TargetPage]);

            // Calculate the running balance of the "before" entries
            double beforeBalance = CalculateBalance(tuple.Item1);

            // Create an entry object that represents the beforeBalance value 
            Entry entry = new Entry();
            entry.SetDate(dateTimePickerStartDate.Value);
            entry.Memorandum = $"Running balance before {entry.Date}.";
            entry.Details = "Generated by the app; not represented in the database.";
            if (beforeBalance < 0) entry.Credit = Abs(beforeBalance);
            else entry.Debit = beforeBalance;

            // Insert the new entry into the 0th position of the second list,
            var data = tuple.Item2;
            data.Sort(Entry.Compare);
            data.Insert(0, entry);

            // Calculate the proper running balance
            RunningBalance = CalculateBalance(data);

            // Target ledger info
            if (TargetLedger.TargetEntry == null) labelTargetEntry.Text = "No entry.";
            else labelTargetEntry.Text = $"{TargetLedger.TargetEntry.Date.ToString("yyyy/MM/dd")}: {TargetLedger.TargetEntry.Memorandum}";

            // Refresh the view
            dataGridViewTable.DataSource = data;
            FormatColumns();
        }
        private void FormatColumns()
        {
            // Formatting
            dataGridViewTable.Columns["Date"].DefaultCellStyle.Format = "d";
            dataGridViewTable.Columns["Debit"].DefaultCellStyle.Format = "c";
            dataGridViewTable.Columns["Credit"].DefaultCellStyle.Format = "c";
            dataGridViewTable.Columns["RunningBalance"].DefaultCellStyle.Format = "c";
            // Hide unnecessary columns from users
            dataGridViewTable.Columns["ID"].Visible = false;
            dataGridViewTable.Columns["DateString"].Visible = false;
        }

        // Events that handle changing DateTimePickers, as well as sorting events by date
        private void DateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            GroupStartDate = dateTimePickerStartDate.Value;
            RefreshTargetData();
        }
        private void DateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            GroupEndDate = dateTimePickerEndDate.Value;
            RefreshTargetData();
        }
        public Tuple<List<Entry>, List<Entry>, List<Entry>> GroupEntriesByDate(List<Entry> entries)
        {
            // Item1: All entries that fall before the designated start date
            List<Entry> before = new List<Entry>();
            // Item2: All entries that fall between the start and end dates
            List<Entry> during = new List<Entry>();
            // Item3: All entries that fall after the designated end date.
            List<Entry> after = new List<Entry>();

            // Loop thru each entry
            foreach (Entry e in entries)
            {
                // if earlier than start date
                if (DateTime.Compare(e.Date, dateTimePickerStartDate.Value) < 0)
                {
                    //System.Diagnostics.Debug.WriteLine("Adding entry to 'before'.");
                    before.Add(e);
                }
                // if later than end date
                else if (DateTime.Compare(e.Date, dateTimePickerEndDate.Value) > 0)
                {
                    //System.Diagnostics.Debug.WriteLine("Adding entry to 'end'.");
                    after.Add(e);
                }
                // otherwise, it must be in the middle
                else
                {
                    //System.Diagnostics.Debug.WriteLine("Adding entry to 'during'.");
                    during.Add(e);
                }
            }

            // Return the assembled tuple. The "after" Item3 list probably wont ever be needed...
            return new Tuple<List<Entry>, List<Entry>, List<Entry>>(before, during, after);
        }

        // Calculators
        private static double CalculateBalance(List<Entry> entries)
        {
            double r = 0;
            for (int i = 0; i < entries.Count; i++)
            {
                r += entries[i].Debit;
                r -= entries[i].Credit;
                entries[i].RunningBalance = r;
            }
            return r;
        }

        // Initialization
        public MainForm()
        {
            InitializeComponent();
        }

        // Targeting
        private void TargetEntry(object sender, DataGridViewCellEventArgs e)
        {
            // Extracts the row that the target entry is located in
            if (e.RowIndex >= 0)
            {
                DataGridViewRow r = dataGridViewTable.Rows[e.RowIndex];

                // Grabs the "ID" value of that entry
                int rowID = (int)r.Cells["ID"].Value;

                // Searches the targetLedger and targetPage for the entry with that ID
                // if that page is "Summary" (which it is by default) this function will fail...
                foreach (var entry in TargetLedger.Pages[TargetLedger.TargetPage])
                {
                    if (entry.ID == rowID)
                    {
                        TargetLedger.TargetEntry = entry;
                    }
                }

                
                if (TargetLedger.TargetEntry == null) labelTargetEntry.Text = "No entry.";
                else labelTargetEntry.Text = $"{TargetLedger.TargetEntry.Date.ToString("yyyy/MM/dd")}: {TargetLedger.TargetEntry.Memorandum}";
            }
        }
        private void DataGridViewTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TargetEntry(sender, e);
        }
        private void DataGridViewTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TargetEntry(sender, e);
        }

        // Adding, removing, and modifying entries thru UI
        private void ButtonNewEntry_Click(object sender, EventArgs e)
        {
            if (TargetLedger != null && TargetLedger.TargetPage != Ledger.Summary)
            {
                string ledgerName = TargetLedger.Name;       // to prevent foolishness
                string pageName = TargetLedger.TargetPage;   // while dialog is open
                using (var dialog = new NewEntryDialog())
                {
                    // Setup ledger information in the dialog
                    dialog.LedgerYear = TargetLedger.NewestEntry.Date.Year;
                    dialog.Index = Index;
                    dialog.TargetLedger = ledgerName;
                    dialog.TargetPage = pageName;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        // If target page was changed inside dialog, change it here too
                        if (dialog.TargetPage != TargetLedger.TargetPage)
                        {
                            TargetLedger.TargetPage = dialog.TargetPage;
                        }

                        // Attempt to add new entry to database, then reload it
                        database.AddEntry(TargetLedger.Name, TargetLedger.TargetPage, dialog.UserEntry);
                        LoadDatabase(false);
                    }
                }

                RefreshTargetData();
            }
        }
        private void ButtonModifyEntry_Click(object sender, EventArgs e)
        {
            if(TargetLedger.TargetEntry != null)
            {
                System.Diagnostics.Debug.WriteLine($"Modifying entry: {TargetLedger.TargetEntry}.");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No entry to modify.");
            }
        }
        private void ButtonDeleteEntry_Click(object sender, EventArgs e)
        {
            if (TargetLedger.TargetEntry != null)
            {
                string pageName = string.Empty;
                if (TargetLedger.TargetPage == Ledger.Summary)
                {
                    foreach (var page in TargetLedger.Pages.Keys)
                    {
                        foreach (var entry in TargetLedger.Pages[page])
                        {
                            if (entry.ID == TargetLedger.TargetEntry.ID)
                            {
                                pageName = page;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    pageName = TargetLedger.TargetPage;
                }

                database.DeleteEntry(TargetLedger.Name, pageName, TargetLedger.TargetEntry.ID);
                TargetLedger.TargetEntry = null;
                labelTargetEntry.Text = "No entry.";
                LoadDatabase(false);
            }
        }
        
        // MainForm combo boxes for ledgers and pages
        private void ComboBoxLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Targets the ledger
            foreach (var ledger in database.Ledgers)
            {
                if (ledger.Name == comboBoxLedger.Text)
                {
                    TargetLedger = ledger;
                }
            }

            // Targets a new page of the ledger
            comboBoxPage.DataSource = TargetLedger.Pages.Keys.ToList();
            TargetLedger.TargetPage = comboBoxPage.Text;

            // Nullifies previous entry target (if there was one)
            TargetLedger.TargetEntry = null;
            labelTargetEntry.Text = "No entry.";

            // Refreshes the dataGridView
            RefreshTargetData();
        }
        private void ComboBoxPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Targets correct page
            TargetLedger.TargetPage = comboBoxPage.Text;

            // Nullifies previous entry target (if there was one)
            TargetLedger.TargetEntry = null;
            RefreshTargetData();
        }
    }
}
