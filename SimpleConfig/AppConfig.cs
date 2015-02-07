using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SimpleConfig
{
    public class AppConfig
    {

        public static string ListAllConfigs(string delimiter = "\r\n")
        {
            StringBuilder sb = new StringBuilder();

            var configs = System.Configuration.ConfigurationManager.AppSettings;

            foreach (var c in configs)
            {
                sb.AppendFormat("{0}: ", c);
                sb.AppendFormat("{0}{1}", System.Configuration.ConfigurationManager.AppSettings[c.ToString()], delimiter);
            }

            return sb.ToString();
        }

        #region "##### GET APP SETTINGS"

        public static Dictionary<string,string> GetConfigsAsDictionary(string delimiter = " ")
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            var configs = System.Configuration.ConfigurationManager.AppSettings;

            foreach (var c in configs)
            {
                var key = c.ToString();
                var val = System.Configuration.ConfigurationManager.AppSettings[c.ToString()];

                if (!dict.ContainsKey(c.ToString()))
                {
                    dict.Add(key, val);
                }
            }

            return dict;
        }

        public static string getConfigByName(string name)
        {
            string result = string.Empty;

            result = System.Configuration.ConfigurationManager.AppSettings[name];

            return result;
        }

        public static bool getConfigByNameAsBool(string name)
        {
            string temp = getConfigByName(name);

            bool result = false;

            bool.TryParse(temp, out result);

            return result;
        }

        public static int getConfigByNameAsInt(string name)
        {
            string temp = getConfigByName(name);

            int result = 0;

            int.TryParse(temp, out result);

            return result;
        }

        public static double getConfigByNameAsDouble(string name)
        {
            string temp = getConfigByName(name);

            double result = 0;

            double.TryParse(temp, out result);

            return result;
        }

        public static DateTime getConfigByNameAsDateTime(string name)
        {
            string temp = getConfigByName(name);

            DateTime result = DateTime.Now;

            DateTime.TryParse(temp, out result);

            return result;
        }

        public static List<string> getConfigByNameAsListString(string name, string delimiter)
        {
            string temp = getConfigByName(name);

            List<string> result = temp.Split(new string[] { delimiter }, StringSplitOptions.None).ToList();

            return result;
        }
        #endregion

        #region "##### SET APP SETTINGS"
        public static void SetConfigByName(string applicationPath, string name, string contents)
        {
            System.Configuration.Configuration d = System.Configuration.ConfigurationManager.OpenExeConfiguration(applicationPath);
            
            d.AppSettings.Settings.Remove(name);
            d.AppSettings.Settings.Add(name, contents);

            d.Save(ConfigurationSaveMode.Minimal);
        }
        #endregion

        #region "##### REMOVE APP SETTINGS"
        public static void RemoveConfig(string applicationPath, string name)
        {
            System.Configuration.Configuration d = System.Configuration.ConfigurationManager.OpenExeConfiguration(applicationPath);

            d.AppSettings.Settings.Remove(name);

            d.Save(ConfigurationSaveMode.Minimal);
        }

        public static void RemoveConfigByValue(string applicationPath, string val, bool wildcard)
        {
            System.Configuration.Configuration d = System.Configuration.ConfigurationManager.OpenExeConfiguration(applicationPath);
            bool change = false;


            foreach (System.Configuration.KeyValueConfigurationElement s in d.AppSettings.Settings)
            {
                if (string.IsNullOrEmpty(s.Value))
                {
                    continue;
                }

                // CHECK IF VALUE IS WITHIN STRING
                if (wildcard)
                {
                    if (s.Value.ToLower().Trim().Contains(val.ToLower().Trim()))
                    {
                        d.AppSettings.Settings.Remove(s.Key);
                        change = true;
                    }
                }
                else
                {
                    // CHECK AGAINST WHOLE STRING
                    if (s.Value.ToLower().Trim() == val.ToLower().Trim())
                    {
                        d.AppSettings.Settings.Remove(s.Key);
                        change = true;
                    }
                }

            }

            if (change)
                d.Save(ConfigurationSaveMode.Minimal);
        }
        #endregion

    }
}
