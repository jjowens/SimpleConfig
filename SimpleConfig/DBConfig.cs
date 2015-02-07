using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConfig
{
    public class DBConfig
    {
        public static string ListAllConnections(string delimiter = "\r\n")
        {
            StringBuilder sb = new StringBuilder();

            var configs = System.Configuration.ConfigurationManager.ConnectionStrings;

            foreach (var c in configs)
            {
                sb.AppendFormat("{0}: ", c);
                sb.AppendFormat("{0}{1}", System.Configuration.ConfigurationManager.ConnectionStrings[c.ToString()], delimiter);
            }

            return sb.ToString();
        }

        #region "##### GET CONNECTION STRINGS"

        public static Dictionary<string, string> getConnectionsAsDictionary(string delimiter = " ")
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            var configs = System.Configuration.ConfigurationManager.ConnectionStrings;

            foreach (var c in configs)
            {
                var conn = System.Configuration.ConfigurationManager.ConnectionStrings[c.ToString()];

                if (conn == null)
                {
                    continue;
                }

                if (!dict.ContainsKey(conn.Name))
                {
                    dict.Add(conn.Name, conn.ConnectionString);
                }
            }

            return dict;
        }

        public static List<string> getConnectionsAsList(string delimiter = " ")
        {
            List<string> lst = new List<string>();

            var configs = System.Configuration.ConfigurationManager.ConnectionStrings;

            foreach (var c in configs)
            {
                var conn = System.Configuration.ConfigurationManager.ConnectionStrings[c.ToString()];

                if (conn == null)
                {
                    continue;
                }

                if (!lst.Contains(conn.ConnectionString))
                {
                    lst.Add(conn.ConnectionString);
                }
            }

            return lst;
        }

        public static string getConnection(string name)
        {
            string result = string.Empty;

            result = System.Configuration.ConfigurationManager.AppSettings[name];

            return result;
        }

        #endregion

        #region "##### SET DATABASE CONNECTION"
        public static void SetConnection(string applicationPath, string name, string connString)
        {
            System.Configuration.Configuration d = System.Configuration.ConfigurationManager.OpenExeConfiguration(applicationPath);

            System.Configuration.ConnectionStringSettings conn = new ConnectionStringSettings();
            conn.Name = name;
            conn.ConnectionString = connString;

            d.ConnectionStrings.ConnectionStrings.Remove(name);
            d.ConnectionStrings.ConnectionStrings.Add(conn);

            d.Save(ConfigurationSaveMode.Minimal);
        }
        #endregion

        #region "##### REMOVE DATABASE CONNECTION"
        public static void RemoveConnection(string applicationPath, string name)
        {
            System.Configuration.Configuration d = System.Configuration.ConfigurationManager.OpenExeConfiguration(applicationPath);

            d.AppSettings.Settings.Remove(name);

            d.Save(ConfigurationSaveMode.Minimal);
        }

        //todo: REMOVE DATABASE CONNECTION BY VALUE
        #endregion

    }
}
