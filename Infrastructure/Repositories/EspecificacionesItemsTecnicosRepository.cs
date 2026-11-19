using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EspecificacionesItemsTecnicosRepository : IEspecificacionesItemsTecnicosRepository
{
    private readonly IDbConnectionFactory _db;
    public EspecificacionesItemsTecnicosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EspecificacionesItemsTecnicosTreeList>> TreelistAsync(decimal codEntidad)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EspecificacionesItemsTecnicosTreeList>(
            SP.SCHEMAIngenieria.EspecificacionesTecnicas.Treelist,
            new { COD_ENTIDAD = codEntidad },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<EspecificacionesItemsTecnicosTreeList>> TreelistPorEspecificacionAsync(string? codEspecificacionTecnica)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EspecificacionesItemsTecnicosTreeList>(
            SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.TreelistPorEspecificacion,
            new { COD_ESPECIFICACION_TECNICA = NullSiVacio(codEspecificacionTecnica) },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> UpdateAsync(EspecificacionesItemsTecnicosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        // COD_ITEM_TECNICO es IDENTITY: en creación se manda NULL y el propio INSERT
        // asigna el correlativo; en modificación/eliminación sí viaja el valor existente.
        p.Add("COD_ITEM_TECNICO", NullSiVacio(dto.COD_ITEM_TECNICO));
        p.Add("DES_ITEM_TECNICO", dto.DES_ITEM_TECNICO);
        p.Add("COD_ENTIDAD_DEPENDIENTE", dto.COD_ENTIDAD_DEPENDIENTE);
        p.Add("COD_ESPECIFICACION_TECNICA_DEPENDIENTE", NullSiVacio(dto.COD_ESPECIFICACION_TECNICA_DEPENDIENTE));
        p.Add("COD_ITEM_TECNICO_DEPENDIENTE", NullSiVacio(dto.COD_ITEM_TECNICO_DEPENDIENTE));
        p.Add("FLG_HABILITADO", dto.FLG_HABILITADO ?? false);
        p.Add("FLG_DEFAULT", dto.FLG_DEFAULT ?? false);
        p.Add("VAL_NUMERICO", dto.VAL_NUMERICO);
        p.Add("DES_COLOR", dto.DES_COLOR);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string codEspecificacionTecnica, string codItemTecnico)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Delete,
            new { COD_ESPECIFICACION_TECNICA = codEspecificacionTecnica, COD_ITEM_TECNICO = codItemTecnico },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Delete, new { COD_ESPECIFICACION_TECNICA = codEspecificacionTecnica, COD_ITEM_TECNICO = codItemTecnico })
        };
    }
}

    public async Task<IEnumerable<EspecificacionesItemsTecnicosComboDependiente>> ComboDependientesAsync(
        string codEspecificacionTecnica, string? codEspecificacionTecnicaDependiente, int? codItemTecnicoDependiente, decimal? codEntidadDependiente)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EspecificacionesItemsTecnicosComboDependiente>(
            SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.ComboxDependientes,
            new
            {
                COD_ESPECIFICACION_TECNICA = codEspecificacionTecnica,
                COD_ESPECIFICACION_TECNICA_DEPENDIENTE = NullSiVacio(codEspecificacionTecnicaDependiente),
                COD_ITEM_TECNICO_DEPENDIENTE = codItemTecnicoDependiente,
                COD_ENTIDAD_DEPENDIENTE = codEntidadDependiente
            },
            commandType: CommandType.StoredProcedure);
    }

    private static string? NullSiVacio(string? valor) => string.IsNullOrWhiteSpace(valor) ? null : valor;
}
