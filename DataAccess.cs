using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Register
{
    internal class DataAccess
    {////////////////////////   
        //////
        /////
        ////
        /// 
        /// 
        /// 
        public string ConnectionString { get; set; }
        public Dictionary<string, string[]> Index { get; private set; }
        public Ledger[] Ledgers { get; private set; }
        private int LargestID { get; set; } // only data access should be creating ids
        public DataAccess(string databasePath)
        {
            // If DataAccess is initialized, bind the ConnectionString to the instance
            ConnectionString = FormatConnectionString(databasePath, false);
            if (ConnectionString != null)
            {
                Index = LoadIndex(ConnectionString);
                Ledgers = LoadLedgers(ConnectionString, Index);
                LargestID = FindLargestID(); // must have bound info to do this
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No connection string.");
            }
        }
        private int FindLargestID()
        {
            int r = -1;

            // If there are no ledgers
            if(Ledgers.Length == 0 || Ledgers == null)
            {
                return r;
            }

            // If there are ledgers...
            foreach(var ledger in Ledgers)
            {
                // Loop through each entry in each the summary page for the ledger
                foreach(var entry in ledger.Pages[Ledger.Summary])
                {
                    if(entry.ID > r)
                    {
                        r = entry.ID;
                    }
                }
            }

            // Once each ID has been checked, return the r value
            return r;
        }
        //////
        /////
        ////
        /// 
        /// .| Nonstatic, public methods
        /// 
        /// -> 
        /// 
        /// 
        public void AddEntry(string ledger, string page, Entry entry)
        {
            entry.ID = LargestID + 1;                         // LargestID is only instantiated when DataAccess
            AddEntry(ConnectionString, ledger, page, entry);  // constructor has been called and data has been 
            LargestID++;                                      // loaded (even if LargestID == 0).
        }
        public void ModifyEntry(string ledger, string page, Entry newEntry, Entry oldEntry)
        {
            newEntry.ID = oldEntry.ID;                                // simulates modification by keeping original ids
            DeleteEntry(ConnectionString, ledger, page, newEntry.ID); // removes the original entry using static method
            AddEntry(ConnectionString, ledger, page, newEntry);       // replaces it with new entry using static method
        }
        public void DeleteEntry(string ledger, string page, int id)
        {
            DeleteEntry(ConnectionString, ledger, page, id);
        }
        //////
        /////
        ////
        ///
        /// .| Static methods
        ///
        /// -> Formatting methods: FormatConnectionString, FormatLedgerPage.
        ///
        /// .) FormatConnectionString(string databasePath, bool newDatabaseFile): Attempts to return
        ///    a connection string that SQLite can interpret from a path and a boolean, the latter 
        ///    used to determine if a new file is to be created or if an existing file is to be used.
        ///    
        /// .) FormatLedgerPage(string ledger, string page): Attempts to return a string with the 
        ///    following format -> $"{ledger}_{page}".
        ///
        ///
        ///
        /// -> Data access methods: CreateNewDatabase, AddEntry, DeleteEntry.
        ///
        /// .) 
        /// 
        /// 
        /// 
        ///
        private static readonly string DefaultTableCreationString = "(\"ID\" INTEGER NOT NULL, " +
                                                                     "\"Date\" TEXT NOT NULL, " +
                                                                     "\"Memorandum\" TEXT NOT NULL, " +
                                                                     "\"Details\" TEXT, " +
                                                                     "\"Debit\" NUMERIC, " +
                                                                     "\"Credit\" NUMERIC, " +
                                                                     "PRIMARY KEY(\"ID\"))";
        public static string FormatConnectionString(string databasePath, bool newDatabaseFile)
        {
            // If there is no valid database path, throw a null argument exception
            if (string.IsNullOrWhiteSpace(databasePath)) throw new ArgumentNullException("Path cannot be null or empty.");

            // If the path is not null or white space, but has an incorrect extension (or no extension?)...
            string ext = Path.GetExtension(databasePath);
            if (ext != ".db") throw new ArgumentException($"The file must have the extension '.db', not {ext}.");

            // If this connection string is for a new database...
            bool fileExists = File.Exists(databasePath);
            if (newDatabaseFile)
            {
                // Do not throw error, but log it... may eventually want to just throw an error & have caller determine what to do
                if (fileExists) System.Diagnostics.Debug.WriteLine("Attempting to create a new file when one already exists.");
                return $"Data Source={databasePath};Version=3;New=True;";
            }
            // If this connection string is for an existing database...
            else
            {
                if (!fileExists) throw new ArgumentException("File does not exist.");
                return $"Data Source={databasePath};Version=3;New=False;";
            }
        }
        private static string FormatLedgerPage(string ledger, string page)
        {
            return $"{ledger}_{page}"; // to standardize how these are formatted
        }
        public static void CreateNewDatabase(string fileName, string directory, Dictionary<string, string[]> index)
        {
            string connectionString = FormatConnectionString($"{directory}/{fileName}.db", true);
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                // for each ledger...
                foreach (string ledger in index.Keys)
                {
                    // and for each page...
                    foreach (string page in index[ledger])
                    {
                        string creationString = $"CREATE TABLE \"{ledger}_{page}\" " + DefaultTableCreationString;
                        cnn.Query(creationString);
                    }
                }
            }
        }
        private static Dictionary<string, string[]> LoadIndex(string connectionString)
        {
            // Grab all of the table names from the sqlite_master
            List<string> unformattedTableNames = new List<string>();
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                var output = cnn.Query<string>("SELECT name FROM sqlite_master WHERE type='table'", new DynamicParameters());
                foreach (var obj in output)
                {
                    unformattedTableNames.Add(obj);
                }
            }

            // If I include metadata tables, here is where they would need to be removed.
            // doot do-do 
            // Otherwise, the tableName.Split('_') would fail, or the indexing would fail, and cause errors.

            // Construct an index by formatting the recieved table names
            var construct = new Dictionary<string, List<string>>();
            foreach (string tableName in unformattedTableNames)
            {
                string[] ledgerPage = tableName.Split('_');
                string ledger = ledgerPage[0];
                string page = ledgerPage[1];
                if (construct.Keys.Contains(ledger))
                {
                    construct[ledger].Add(page);
                }
                else
                {
                    List<string> newLedgerPages = new List<string>();
                    newLedgerPages.Add(page);
                    construct.Add(ledger, newLedgerPages);
                }
            }

            // Convert the Dictionary<string, List<string>> to a Dictionary<string, string[]>
            var index = new Dictionary<string, string[]>();
            foreach(var key in construct.Keys)
            {
                index.Add(key, construct[key].ToArray());
            }

            // Return the properly formatted index
            return index;
        }
        private static Ledger[] LoadLedgers(string connectionString, Dictionary<string, string[]> index)
        {
            List<Ledger> ledgers = new List<Ledger>();
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                // Loop thru each ledger in the index
                foreach (string ledger in index.Keys)
                {
                    Ledger newLedger = new Ledger(ledger);
                    newLedger.AddPage(Ledger.Summary, new List<Entry>());
                    foreach (string page in index[ledger])
                    {
                        string tableNameFormatted = $"{ledger}_{page}";
                        var data = cnn.Query<Entry>($"SELECT * FROM \"{tableNameFormatted}\"", new DynamicParameters());
                        newLedger.AddPage(page, data.ToList());

                        // Add reference to each page's data individually to summary
                        foreach (var entry in newLedger.Pages[page]) newLedger.Pages[Ledger.Summary].Add(entry);
                    }
                    ledgers.Add(newLedger);
                }
            }
            return ledgers.ToArray();
        }
        private static bool AddEntry(string connectionString, string ledger, string page, Entry entry)
        {
            string tableName = FormatLedgerPage(ledger, page);
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                
                // Format the command text
                var query = cnn.CreateCommand();
                query.CommandText = $"INSERT INTO {tableName}(ID, Date, Memorandum, Details, Debit, Credit) " +
                    "VALUES(@id, @dateString, @memo, @details, @debit, @credit)";

                // Set up the params from the passed entry
                query.Parameters.Add(new SQLiteParameter("@id", entry.ID));
                query.Parameters.Add(new SQLiteParameter("@dateString", entry.DateString));
                query.Parameters.Add(new SQLiteParameter("@memo", entry.Memorandum));
                query.Parameters.Add(new SQLiteParameter("@details", entry.Details));
                query.Parameters.Add(new SQLiteParameter("@debit", entry.Debit));
                query.Parameters.Add(new SQLiteParameter("@credit", entry.Credit));
                
                // Attempt to execute the query with the established parameters
                query.ExecuteNonQuery();
                return true;
            }
        }
        private static bool DeleteEntry(string connectionString, string ledger, string page, int entryID)
        {
            string tableName = FormatLedgerPage(ledger, page);
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                var query = cnn.CreateCommand();
                query.CommandText = $"DELETE FROM {tableName} WHERE \"ID\" = {entryID}";
                query.ExecuteNonQuery();
            }
            return true;
        }
    }
}