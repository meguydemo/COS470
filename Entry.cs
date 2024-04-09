using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Register
{
    public class Entry : IEquatable<Entry>, IComparable<Entry>
    {
        // Unique identifier for MainForm, DataAccess, and SQLite
        public int ID {  get; set; }
        public override int GetHashCode()
        {
            return ID;
        }




        // Date & DateString information
        public DateTime Date { get; private set; }
        public string DateString { get; private set; }
        public void SetDate(string ds)
        {
            string[] ymd = new string[3];
            ymd[0] = ds.Substring(6);    //year
            ymd[1] = ds.Substring(0, 2); //month
            ymd[2] = ds.Substring(3, 2); //day

            // Construct Date
            DateTime d = new DateTime(Convert.ToInt32(ymd[0]), Convert.ToInt32(ymd[1]), Convert.ToInt32(ymd[2]));
            Date = d;

            // Make sure that the string for the day is formatted correctly
            string stringDay;
            if (Date.Day < 10) stringDay = $"0{Date.Day}";
            else stringDay = $"{Date.Day}";

            // Construct string
            string[] monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthGenitiveNames;
            DateString = $"{Date.Year} {monthNames[Date.Month - 1]} {stringDay}";
        }
        public void SetDate(DateTime d)
        {
            Date = d;

            string stringDay;
            if (Date.Day < 10) stringDay = $"0{Date.Day}";
            else stringDay = $"{Date.Day}";

            string[] monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthGenitiveNames;
            DateString = $"{Date.Year} {monthNames[Date.Month - 1]} {stringDay}";
        }
        
        
        
        
        
        
        
      
        
        
        
        
        
        
        public string Memorandum { get; set; }
        public string Details { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double RunningBalance { get; set; }























        // IEquatable
        public bool Equals(Entry e)
        {
            if (ID == e.ID) return true;
            else return false;
        }









        // IComparable
        public int CompareTo(Entry e)
        {
            // Structure stolen directly from https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort?view=net-8.0 >:)
            if (e == null) return 1;
            else 
            {
                // Compare the dates
                int comparison = DateTime.Compare(this.Date, e.Date);
                if(comparison != 0) // if dates are different
                {
                    return comparison;
                }
                // else if(comparison == 0)
                else // if dates are same, compare debit values
                {
                    return Debit.CompareTo(e.Debit) * -1; // reverse direction
                }
            }
        }
        public static int Compare(Entry e1, Entry e2)
        {
            return e1.CompareTo(e2);
        }















        // Overrides
        public override string ToString()
        {
            return $"{DateString}:{Memorandum}. Debit:[{Debit}]. Credit[{Credit}]";
        }





    }
}
