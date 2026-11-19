using System.Data;
using System.Data.Common;
using KonSolutionsApi.Common;

namespace KonSolutionsApi.Infrastructure.Data;

/// <summary>
/// Wraps an open IDbConnection and returns SpTracingCommand instances so every
/// stored procedure call is automatically traced before execution.
/// </summary>
internal sealed class SpTracingConnection : IDbConnection
{
    private readonly IDbConnection    _inner;
    private readonly ILogger          _log;
    private readonly ErrorLogService? _errorLog;

    public SpTracingConnection(IDbConnection inner, ILogger log, ErrorLogService? errorLog = null)
    {
        _inner    = inner;
        _log      = log;
        _errorLog = errorLog;
    }

    // ── CreateCommand — wraps the real command with the tracer ────────────
    public IDbCommand CreateCommand()
    {
        var innerCmd = _inner.CreateCommand();
        // Dapper casts IDbCommand → DbCommand for async; inner is SqlCommand : DbCommand
        return new SpTracingCommand((DbCommand)innerCmd, _log, _errorLog);
    }

    // ── IDbConnection — delegate everything else ──────────────────────────
    [System.Diagnostics.CodeAnalysis.AllowNull]
    public string ConnectionString
    {
        get => _inner.ConnectionString;
        set => _inner.ConnectionString = value!;
    }

    public int             ConnectionTimeout => _inner.ConnectionTimeout;
    public string          Database          => _inner.Database;
    public ConnectionState State             => _inner.State;

    public IDbTransaction BeginTransaction()                          => _inner.BeginTransaction();
    public IDbTransaction BeginTransaction(IsolationLevel iso)        => _inner.BeginTransaction(iso);
    public void           ChangeDatabase(string databaseName)         => _inner.ChangeDatabase(databaseName);
    public void           Close()                                     => _inner.Close();
    public void           Open()                                      => _inner.Open();
    public void           Dispose()                                   => _inner.Dispose();
}
