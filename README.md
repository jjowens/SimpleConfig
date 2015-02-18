# SimpleConfig
It reads and sets your config file of your application or website. C#

# App Settings

You can get or set App settings by running the following commands.

Get Settings. Strongly Typed 
```csharp
SimpleConfig.AppConfig.getConfig("FullName"); // GETS VALUE AS STRING
SimpleConfig.AppConfig.getConfigAsInt("Age"); // GETS VALUE AS INT
SimpleConfig.AppConfig.getConfigAsBool("Debug"); // GETS VALUE AS BOOL
```

Set Settings. Strongly Typed 
```csharp
string appPath = @"C:\test\testconsole.exe" // SET FILE PATH TO APPLICATION, NOT CONFIG FILE

// ADD NEW APP SETTING - BOOK
SimpleConfig.AppConfig.SetConfig(appPath, "Book", "Bleak House by Charles Dickens");

// CHANGE APP SETTING - FOO
SimpleConfig.AppConfig.SetConfig(appPath, "Foo", "Manchester");

// REMOVE CONFIG - ACCOUNTNUMBER
SimpleConfig.AppConfig.RemoveConfig(appPath, "AccountNumber");

// REMOVE CONFIG BY VALUE
SimpleConfig.AppConfig.RemoveConfigByValue(appPath, "Checklist.txt", true);
```

# Database Settings

You can get or set Database connection strings by running the following commands.

```csharp
// ADD NEW CONNECTION STRING
SimpleConfig.DBConfig.SetConnection(appPath, "videogames", "Data Source=.;Initial Catalog=gameconsoles;IntegratedSecurity=True");

// UPDATE NEW CONNECTION STRING
SimpleConfig.DBConfig.SetConnection(appPath, "Airlines", "Data Source=.;Initial Catalog=FlightsAbroadLtd;IntegratedSecurity=True");

// REMOVE CONNECTION STRING
SimpleConfig.DBConfig.RemoveConnection(appPath, "Foo");

// GET CONNECTION STRING
SimpleConfig.DBConfig.getConnection("videogames");
```
