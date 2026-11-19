using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PermisosApiRepository : IPermisosApiRepository
{
    private readonly IDbConnectionFactory _db;
    public PermisosApiRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PermisoApi>> ShowallAsync(string COD_ROL_USUARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PermisoApi>(
            SP.SCHEMASeguridad.PermisosApi.ShowallPorRol,
            new { COD_ROL_USUARIO }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Timeout corto: esto corre en el camino crítico de cada POST. Si la base tarda,
    /// es preferible quedarse sin saber (null → se exige cualquiera de los dos permisos) que
    /// dejar al usuario esperando en un chequeo de autorización.</summary>
    public async Task<bool?> ExisteRegistroAsync(string DES_OBJETO, string json)
    {
        using var conn = _db.CreateConnection();
        return await conn.ExecuteScalarAsync<bool?>(
            SP.SCHEMASeguridad.PermisosApi.ExisteRegistro,
            new { DES_OBJETO, DES_JSON = json },
            commandType: CommandType.StoredProcedure, commandTimeout: 5);
    }

    /// <summary>Mismo timeout corto que ExisteRegistroAsync y por el mismo motivo: esto
    /// corre en el camino crítico de cada POST/PUT/DELETE de las entidades que declaren
    /// [ExigeFlujo]. Preferible "no sé" (null, null) a dejar al usuario esperando.</summary>
    public async Task<(short? CodTipoEstado, short? CodEstado)> EstadoActualAsync(string DES_OBJETO, string json)
    {
        using var conn = _db.CreateConnection();
        var r = await conn.QueryFirstOrDefaultAsync(
            SP.SCHEMASeguridad.PermisosApi.EstadoActual,
            new { DES_OBJETO, DES_JSON = json },
            commandType: CommandType.StoredProcedure, commandTimeout: 5);
        if (r is null) return (null, null);
        var d = (IDictionary<string, object?>)r;
        short? tipo = d.TryGetValue("COD_TIPO_ESTADO", out var t) && t is not null ? Convert.ToInt16(t) : null;
        short? estado = d.TryGetValue("COD_ESTADO", out var e) && e is not null ? Convert.ToInt16(e) : null;
        return (tipo, estado);
    }

    public async Task AuditarAsync(string codUsuario, string codRol, string desEntidad, string verbo, string ruta, string estacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(
            SP.SCHEMASeguridad.PermisosApi.AuditoriaInsert,
            new
            {
                COD_USUARIO = codUsuario,
                COD_ROL_USUARIO = codRol,
                DES_OBJETO = desEntidad,
                DES_VERBO = verbo,
                // La ruta se recorta al ancho de la columna: una URL larga con muchos
                // parámetros no debe hacer fallar el registro de auditoría.
                DES_RUTA = ruta.Length > 400 ? ruta[..400] : ruta,
                COD_ESTACION = estacion
            },
            commandType: CommandType.StoredProcedure);
    }
}
