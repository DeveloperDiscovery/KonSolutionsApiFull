using System.Data;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Common;

namespace KonSolutionsApi.Infrastructure.Data;

public interface IDbConnectionFactory { IDbConnection CreateConnection(); }

public class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly string           _cs;
    private readonly bool             _traceEnabled;
    private readonly ILogger          _log;
    private readonly ErrorLogService? _errorLog;

    public SqlConnectionFactory(IConfiguration config, ILogger<SqlConnectionFactory> log, ErrorLogService? errorLog = null)
    {
        var cs = Environment.GetEnvironmentVariable("KonSolutions_DB_CONNECTION");
        
        if (string.IsNullOrWhiteSpace(cs)) cs = config.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(cs))
            throw new InvalidOperationException(
                "Falta configurar la variable de entorno KonSolutions_DB_CONNECTION (ConnectionStrings:DefaultConnection en appsettings.json queda vacío a propósito, nunca debe llevar la connection string real).");
        _cs = cs;

        _traceEnabled = config.GetValue<bool>("SpTrace:Enabled");
        _log          = log;
        _errorLog     = errorLog;
    }

    public IDbConnection CreateConnection()
    {
        var conn = new SqlConnection(_cs);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SET ARITHABORT ON";
        cmd.ExecuteNonQuery();

        if (_traceEnabled)
            return new SpTracingConnection(conn, _log, _errorLog);

        return conn;
    }
}
