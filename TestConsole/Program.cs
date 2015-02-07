using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Test Console";

            // GET INDIVIDUAL SETTINGS
            Console.WriteLine("FullName: {0}", SimpleConfig.AppConfig.getConfigByName("FullName"));
            Console.WriteLine("Age: {0}", SimpleConfig.AppConfig.getConfigByNameAsInt("Age"));
            Console.WriteLine("Debug: {0}", SimpleConfig.AppConfig.getConfigByNameAsBool("Debug"));

            // GET LIST OF STRINGS - BLACKLIST
            var lst = SimpleConfig.AppConfig.getConfigByNameAsListString("blacklist", ";").OrderBy(x => x);
            foreach (var l in lst)
            {
                Console.WriteLine(l);
            }

            // GET DATE RANGE - DATEFROM, DATETO
            var dateFrom = SimpleConfig.AppConfig.getConfigByNameAsDateTime("DateFrom");
            var dateTo = SimpleConfig.AppConfig.getConfigByNameAsDateTime("DateTo");

            var sql = string.Format("SELECT * FROM Appointments WHERE Booked BETWEEN '{0}' AND '{1}'",
                dateFrom.ToShortDateString(), dateTo.ToShortDateString());

            Console.WriteLine("");
            Console.WriteLine(sql);

            Console.WriteLine();

            // GET ALL APP CONFIGS AS DICTIONARY
            var dict = SimpleConfig.AppConfig.GetConfigsAsDictionary();

            foreach (var d in dict)
            {
                Console.WriteLine("Key: {0}. Value: {1}", d.Key, d.Value);
            }

            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            // ADD NEW APP SETTING - BOOK
            SimpleConfig.AppConfig.SetConfigByName(appPath, "Book", "Bleak House by Charles Dickens");

            // CHANGE APP SETTING - FOO
            SimpleConfig.AppConfig.SetConfigByName(appPath, "Foo", "Chelsea");

            // REMOVE CONFIG - ACCOUNTNUMBER
            SimpleConfig.AppConfig.RemoveConfig(appPath, "AccountNumber");

            // REMOVE CONFIG BY VALUE
            SimpleConfig.AppConfig.RemoveConfigByValue(appPath, "Checklist.txt", true);

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
