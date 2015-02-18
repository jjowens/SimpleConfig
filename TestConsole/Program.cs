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

            getAppSettings();
            getDBConnections();

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void getAppSettings()
        {
            // GET INDIVIDUAL SETTINGS
            Console.WriteLine("FullName: {0}", SimpleConfig.AppConfig.getConfig("FullName"));
            Console.WriteLine("Age: {0}", SimpleConfig.AppConfig.getConfigAsInt("Age"));
            Console.WriteLine("Debug: {0}", SimpleConfig.AppConfig.getConfigAsBool("Debug"));

            // GET LIST OF STRINGS - BLACKLIST
            var lst = SimpleConfig.AppConfig.getConfigAsListString("blacklist", ";").OrderBy(x => x);
            foreach (var l in lst)
            {
                Console.WriteLine(l);
            }

            // GET DATE RANGE - DATEFROM, DATETO
            var dateFrom = SimpleConfig.AppConfig.getConfigAsDateTime("DateFrom");
            var dateTo = SimpleConfig.AppConfig.getConfigAsDateTime("DateTo");

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
            SimpleConfig.AppConfig.SetConfig(appPath, "Book", "Bleak House by Charles Dickens");

            // CHANGE APP SETTING - FOO
            SimpleConfig.AppConfig.SetConfig(appPath, "Foo", "Manchester");

            // REMOVE CONFIG - ACCOUNTNUMBER
            SimpleConfig.AppConfig.RemoveConfig(appPath, "AccountNumber");

            // REMOVE CONFIG BY VALUE
            SimpleConfig.AppConfig.RemoveConfigByValue(appPath, "Checklist.txt", true);
        }

         private static void getDBConnections()
        {

            // GET ALL DATABASE CONNECTIONS AS DICTIONARY
            var dict = SimpleConfig.DBConfig.getConnectionsAsDictionary();

            foreach (var d in dict)
            {
                Console.WriteLine("Key: {0}. Value: {1}", d.Key, d.Value);
            }

            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

             Console.WriteLine("AppPath: {0}", appPath);

             // ADD NEW CONNECTION STRING
             SimpleConfig.DBConfig.SetConnection(appPath, "videogames", "Data Source=.;Initial Catalog=gameconsoles;IntegratedSecurity=True");

             // UPDATE NEW CONNECTION STRING
             SimpleConfig.DBConfig.SetConnection(appPath, "Airlines", "Data Source=.;Initial Catalog=FlightsAbroadLtd;IntegratedSecurity=True");

             // REMOVE CONNECTION STRING
             SimpleConfig.DBConfig.RemoveConnection(appPath, "Foo");

             // GET CONNECTION STRING
             Console.WriteLine("Videogames: {0}", SimpleConfig.DBConfig.getConnection("videogames"));


        }


    }
}
