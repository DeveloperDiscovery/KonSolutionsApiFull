using System.Data;
using System.Data.Common;
using System.Text;
using KonSolutionsApi.Common;

namespace KonSolutionsApi.Infrastructure.Data;

/// <summary>
/// Wraps a DbCommand and emits a single-line EXEC trace before each stored procedure execution.
/// The trace line can be pasted directly into SSMS to reproduce the exact call.
/// </summary>
internal sealed class SpTracingCommand : DbCommand
{
    private readonly DbCommand        _inner;
    private readonly ILogger          _log;
    private readonly ErrorLogService? _errorLogService;

    public SpTracingCommand(DbCommand inner, ILogger log, ErrorLogService? errorLogService = null)
    {
        _inner           = inner;
        _log             = log;
        _errorLogService = errorLogService;
    }

    // ── Logging ───────────────────────────────────────────────────────────
    /// <summary>Arma la línea "EXEC ... @p = v" o null si no es un stored procedure.</summary>
    private string? BuildExecLine()
    {
        if (_inner.CommandType != CommandType.StoredProcedure) return null;
        try
        {
            var sb = new StringBuilder("EXEC ");
            sb.Append(_inner.CommandText);

            bool first = true;
            foreach (DbParameter p in _inner.Parameters)
            {
                sb.Append(first ? ' ' : ',');
                sb.Append(" @");
                sb.Append(p.ParameterName.TrimStart('@'));
                sb.Append(" = ");
                sb.Append(FormatValue(p.Value, p.DbType));
                first = false;
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            _log.LogWarning("[SP-TRACE-ERROR] No se pudo trazar el SP {Sp}: {Err}", _inner.CommandText, ex.Message);
            return null;
        }
    }

    private void TraceIfSp()
    {
        var exec = BuildExecLine();
        if (exec is null) return;
        _log.LogInformation("[SP] {ExecLine}", exec);
        if (_errorLogService is not null)
            _errorLogService.LogSpTrace(exec);
    }

    /// <summary>Traza el SP incluyendo la duración real medida (ms).</summary>
    private void TraceConDuracion(string? exec, long duracionMs)
    {
        if (exec is null) return;
        if (_errorLogService is not null)
            _errorLogService.LogSpTrace(exec, duracionMs);
        else
            _log.LogInformation("[SP] ({Dur}ms) {ExecLine}", duracionMs, exec);
    }

    private static string FormatValue(object? val, DbType dbType)
    {
        if (val is null || val == DBNull.Value) return "NULL";
        try
        {
        return dbType switch
        {
            DbType.Binary => val is byte[] b && b.Length > 0
                ? $"0x{Convert.ToHexString(b)[..Math.Min(40, b.Length * 2)]}" + (b.Length > 20 ? "…" : "")
                : "NULL",
            DbType.Boolean => Convert.ToBoolean(val) ? "1" : "0",
            DbType.Date    => $"'{Convert.ToDateTime(val):yyyy-MM-dd}'",
            DbType.DateTime or DbType.DateTime2 or DbType.DateTimeOffset
                           => $"'{Convert.ToDateTime(val):yyyy-MM-dd HH:mm:ss}'",
            DbType.Int16 or DbType.Int32 or DbType.Int64
            or DbType.UInt16 or DbType.UInt32 or DbType.UInt64
            or DbType.Decimal or DbType.Double or DbType.Single
            or DbType.Byte or DbType.SByte
                           => val.ToString() ?? "NULL",
            _              => val is byte[] bx
                               ? (bx.Length > 0 ? $"0x{Convert.ToHexString(bx)[..Math.Min(40, bx.Length * 2)]}" : "NULL")
                               : $"'{val.ToString()?.Replace("'", "''") ?? ""}'"
        };
        }
        catch { return "?"; }
    }

    // ── Execute overrides (where tracing happens) ─────────────────────────
    protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
    {
        TraceIfSp();
        return _inner.ExecuteReader(behavior);
    }

    protected override async Task<DbDataReader> ExecuteDbDataReaderAsync(
        CommandBehavior behavior, CancellationToken cancellationToken)
    {
        // Ruta usada por Dapper (QueryAsync/QueryMultipleAsync): medimos la duración
        // real del SP y la enviamos al buffer del Dashboard de Trace de Procesos.
        var exec = BuildExecLine();
        var sw = exec is null ? null : System.Diagnostics.Stopwatch.StartNew();
        var reader = await _inner.ExecuteReaderAsync(behavior, cancellationToken);
        if (sw is not null) { sw.Stop(); TraceConDuracion(exec, sw.ElapsedMilliseconds); }
        return reader;
    }

    public override int ExecuteNonQuery()
    {
        TraceIfSp();
        return _inner.ExecuteNonQuery();
    }

    public override async Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
    {
        TraceIfSp();
        return await _inner.ExecuteNonQueryAsync(cancellationToken);
    }

    public override object? ExecuteScalar()
    {
        TraceIfSp();
        return _inner.ExecuteScalar();
    }

    public override async Task<object?> ExecuteScalarAsync(CancellationToken cancellationToken)
    {
        TraceIfSp();
        return await _inner.ExecuteScalarAsync(cancellationToken);
    }

    // ── DbCommand abstract members — delegate to inner ────────────────────
    [System.Diagnostics.CodeAnalysis.AllowNull]
    public override string  CommandText    { get => _inner.CommandText;    set => _inner.CommandText    = value!; }
    public override int     CommandTimeout { get => _inner.CommandTimeout; set => _inner.CommandTimeout = value; }
    public override CommandType CommandType { get => _inner.CommandType;   set => _inner.CommandType    = value; }
    public override UpdateRowSource UpdatedRowSource { get => _inner.UpdatedRowSource; set => _inner.UpdatedRowSource = value; }
    public override bool DesignTimeVisible { get => _inner.DesignTimeVisible; set => _inner.DesignTimeVisible = value; }

    protected override DbConnection? DbConnection
    {
        get => _inner.Connection;
        set => _inner.Connection = value;
    }

    protected override DbTransaction? DbTransaction
    {
        get => _inner.Transaction;
        set => _inner.Transaction = value;
    }

    protected override DbParameterCollection DbParameterCollection => _inner.Parameters;

    protected override DbParameter CreateDbParameter() => _inner.CreateParameter();

    public override void Cancel()  => _inner.Cancel();
    public override void Prepare() => _inner.Prepare();

    protected override void Dispose(bool disposing)
    {
        if (disposing) _inner.Dispose();
        base.Dispose(disposing);
    }
}
