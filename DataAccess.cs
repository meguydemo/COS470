using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Register
{
    internal class DataAccess
    {
        public string ConnectionString { get; set; }
        public static string FormatConnectionString(string databasePath, bool newDatabaseFile)
        {
            if (newDatabaseFile)
            {
                return $"Data Source={databasePath};Version=3;New=True;";
            }
            else
            {
                return $"Data Source={databasePath};Version=3;New=False;";
            }
        }
        public DataAccess(string databasePath)
        {
            ConnectionString = FormatConnectionString(databasePath, false);
            LoadLedgers(); // populates attributes Index and Ledgers
        }
        public void UpdatePath(string databasePath)
        {
            ConnectionString = FormatConnectionString(databasePath, false);
        }














        public Dictionary<string, List<string>> Index { get; private set; }
        private Dictionary<string, List<string>> GetDatabaseIndex()
        {
            List<string> unformattedTableNames = new List<string>();
            using (IDbConnection cnn = new SQLiteConnection(ConnectionString))
            {
                var output = cnn.Query<string>("SELECT name FROM sqlite_master WHERE type='table'", new DynamicParameters());
                foreach (var obj in output.ToList())
                {
                    unformattedTableNames.Add(obj);
                }
            }
            var index = new Dictionary<string, List<string>>();
            foreach (string tableName in unformattedTableNames)
            {
                var lp = tableName.Split('_');
                string ledger = lp[0];
                string page = lp[1];
                if (index.Keys.Contains(ledger))
                {
                    index[ledger].Add(page);
                }
                else
                {
                    var l = new List<string>();
                    l.Add(page);
                    index.Add(ledger, l);
                }
            }
            return index;
        }
        public List<Ledger> Ledgers { get; private set; }
        public int LargestID { get; private set; }
        public void LoadLedgers()
        {
            if (ConnectionString == null)
            {
                System.Diagnostics.Debug.WriteLine("No connection string.");
            }
            else
            {
                Index = GetDatabaseIndex();
                Ledgers = new List<Ledger>();
                LargestID = 0;
                using (IDbConnection cnn = new SQLiteConnection(ConnectionString))
                {
                    foreach (string ledger in Index.Keys)
                    {
                        // Set up empty ledger
                        Ledger newLedger = new Ledger(ledger);
                        newLedger.AddPage(Ledger.Summary, new List<Entry>());

                        // Loop through each page & add it + contents to ledger
                        foreach (string page in Index[ledger])
                        {
                            string tableNameFormatted = $"{ledger}_{page}";
                            var data = cnn.Query<Entry>($"SELECT * FROM \"{tableNameFormatted}\"", new DynamicParameters());
                            newLedger.AddPage(page, data.ToList());

                            // Add each page's data individually to summary
                            // and compare the ids to the LargestID 
                            foreach (var entry in newLedger.Pages[page])
                            {
                                newLedger.Pages[Ledger.Summary].Add(entry);
                                if (entry.ID > LargestID)
                                {
                                    LargestID = entry.ID;
                                }
                            }
                        }

                        // Add complete ledger to Ledgers
                        Ledgers.Add(newLedger);
                    }
                }
            }
        }

















        private static readonly string DefaultTableCreationString = "(\"ID\" INTEGER NOT NULL, " +
                                                                     "\"Date\" TEXT NOT NULL, " +
                                                                     "\"Memorandum\" TEXT NOT NULL, " +
                                                                     "\"Details\" TEXT, " +
                                                                     "\"Debit\" NUMERIC, " +
                                                                     "\"Credit\" NUMERIC, " +
                                                                     "PRIMARY KEY(\"ID\"))";
        public static void CreateNewDatabase(string fileName, string directory, Dictionary<string, List<string>> dict)
        {
            string connectionString = FormatConnectionString($"{directory}/{fileName}.db", true);
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                // for each ledger...
                foreach (string ledger in dict.Keys)
                {
                    // and for each page...
                    foreach (string page in dict[ledger])
                    {
                        string creationString = $"CREATE TABLE \"{ledger}_{page}\" " + DefaultTableCreationString;
                        cnn.Query(creationString);
                    }
                }
            }
        }
        public void AddEntry(string ledger, string page, Entry entry)
        {
            string tableName = $"\"{ledger}_{page}\"";
            // Connect to the database
            using (IDbConnection cnn = new SQLiteConnection(ConnectionString))
            {
                cnn.Open();
                var query = cnn.CreateCommand();
                query.CommandText = $"INSERT INTO {tableName}(ID, Date, Memorandum, Details, Debit, Credit) " +
                    "VALUES(@id, @dateString, @memo, @details, @debit, @credit)";
                query.Parameters.Add(new SQLiteParameter("@id", LargestID + 1));
                query.Parameters.Add(new SQLiteParameter("@dateString", entry.DateString));
                query.Parameters.Add(new SQLiteParameter("@memo", entry.Memorandum));
                query.Parameters.Add(new SQLiteParameter("@details", entry.Details));
                query.Parameters.Add(new SQLiteParameter("@debit", entry.Debit));
                query.Parameters.Add(new SQLiteParameter("@credit", entry.Credit));
                query.ExecuteNonQuery();
            }
        }
        public void DeleteEntry(string ledger, string page, int id)
        {

            string tableName = $"\"{ledger}_{page}\"";
            using (IDbConnection cnn = new SQLiteConnection(ConnectionString))
            {
                cnn.Open();
                var query = cnn.CreateCommand();
                query.CommandText = $"DELETE FROM {tableName} WHERE \"ID\" = {id}";
                query.ExecuteNonQuery();
            }
        }















        // Things that probably do not work anymore
        public static void AddTable(string databasePath, string tableName)
        {
            string connectionString = FormatConnectionString(databasePath, false);
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                // Attempt to add a new table
                string creationString = $"CREATE TABLE {tableName} " + DefaultTableCreationString;
                cnn.Query(creationString);
            }
        }

    }
}