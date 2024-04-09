using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Register
{
    public class Ledger
    {
        public Ledger(string name)
        {
            Name = name;
            Pages = new Dictionary<string, List<Entry>>();
        }
        public void AddPage(string page, List<Entry> pageData)
        {
            // Entry validation... looks at each entry that is added to the dictionary. 
            foreach (Entry e in pageData)
            {
                if (NewestEntry == null)
                {
                    NewestEntry = e; //-> if there is no NewestEntry...
                }
                else if (DateTime.Compare(NewestEntry.Date, e.Date) < 0)
                {
                    NewestEntry = e; //-> if Entry e is more recent that NewestEntry...
                }
            }

            // Add the page to the ledger
            Pages.Add(page, pageData);
        }


        // "Summary" pages are reserved. Users cannot make a "summary" page when creating a new set
        // of ledgers, and the MainForm responds differently if the selected page is a summary.
        public static readonly string Summary = "Summary";
   

        // Ledger information. Name = the string that lies before the database table name's delimiter
        // ("_"). Dictionary string key = the string that lies after the database table name's delimiter,
        // which is the page name. The List<Entry> values are the list of rows from the database table,
        // and the data that the MainForm uses. 
        public string Name { get; set; }
        public Dictionary<string, List<Entry>> Pages { get; private set; }
              

        // Ledger targeting information. In DataAccess, if this ledger is targeted, these attributes
        // keep important information about where the UI is looking, as well as "NewestEntry" to give
        // quick context to the app about when the last modification was made to the page.
        public string TargetPage { get; set; }
        public Entry TargetEntry { get; set; }
        public Entry NewestEntry { get; private set; }
    }
}
