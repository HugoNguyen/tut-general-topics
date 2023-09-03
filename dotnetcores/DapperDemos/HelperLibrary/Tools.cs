using System.Configuration;

namespace HelperLibrary;

public static class Tools
{
    public static string GetConnectionString(string name = "DapperDB")
    {
        return ConfigurationManager.ConnectionStrings[name].ConnectionString;
    }
}