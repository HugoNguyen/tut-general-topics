using System.Data;

namespace bookify.application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    /// <summary>
    /// Return connection to database
    /// </summary>
    /// <returns></returns>
    IDbConnection CreateConnection();
}
