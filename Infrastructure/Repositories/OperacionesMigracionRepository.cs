using System.Data;
using System.Text.Json;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Migracion asistida de las operaciones de las rutas existentes al catalogo (script SQL 128). Las validaciones salen como SqlException (52540, 52610 a 52619).</summary>
public class OperacionesMigracionRepository : IOperacionesMigracionRepository
{
    private readonly IDbConnectionFactory _db;
    public OperacionesMigracionRepository(IDbConnectionFactory db) => _db = db;
    private static readonly CommandType SP = CommandType.StoredProcedure;

    public async Task<MigracionResumen> ResumenAsync()
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<MigracionResumen>("PROC_INGENIERIA_OPERACIONES_MIGRACION_RESUMEN", commandType: SP)).FirstOrDefault() ?? new MigracionResumen();
    }

    public async Task<List<MigracionGrupo>> PropuestaAsync(string? ruta)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<MigracionGrupo>("PROC_INGENIERIA_OPERACIONES_MIGRACION_PROPUESTA", new { COD_RUTA_PRODUCCION = string.IsNullOrWhiteSpace(ruta) ? null : ruta }, commandType: SP)).ToList();
    }

    public async Task<MigracionResultado> AplicarAsync(MigracionAplicarDto d, string codUsuario, string codEstacion)
    {
        var json = JsonSerializer.Serialize(d.Grupos.Select(g => new { g.COD_PROCESO, COD_SUB_PROCESO = string.IsNullOrWhiteSpace(g.COD_SUB_PROCESO) ? null : g.COD_SUB_PROCESO, g.COD_TIPO_OPERACION_PRODUCCION, g.ACCION, g.COD_OPERACION, g.DES_OPERACION }));
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<MigracionResultado>("PROC_INGENIERIA_OPERACIONES_MIGRACION_APLICAR",
            new { JSON = json, COD_RUTA_PRODUCCION = string.IsNullOrWhiteSpace(d.COD_RUTA_PRODUCCION) ? null : d.COD_RUTA_PRODUCCION, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP)).FirstOrDefault() ?? new MigracionResultado();
    }

    public async Task<MigracionDeshacerResultado> DeshacerAsync(string? ruta, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<MigracionDeshacerResultado>("PROC_INGENIERIA_OPERACIONES_MIGRACION_DESHACER",
            new { COD_RUTA_PRODUCCION = string.IsNullOrWhiteSpace(ruta) ? null : ruta, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP)).FirstOrDefault() ?? new MigracionDeshacerResultado();
    }
}
