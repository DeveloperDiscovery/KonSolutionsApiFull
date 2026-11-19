using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EspecificacionesTecnicasItemsRepository : IEspecificacionesTecnicasItemsRepository
{
    private readonly IDbConnectionFactory _db;
    public EspecificacionesTecnicasItemsRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EspecificacionesTecnicasItems>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EspecificacionesTecnicasItems>(SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EspecificacionesTecnicasItems?> ShowbyidAsync(string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EspecificacionesTecnicasItems>(SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Showbyid, new { COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA, COD_ITEM_TECNICO = COD_ITEM_TECNICO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Showcombox);
    }

    public async Task<IEnumerable<EspecificacionesTecnicasItems>> SearchAsync(EspecificacionesTecnicasItemsSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EspecificacionesTecnicasItems>(SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EspecificacionesTecnicasItemsInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("DES_ITEM_TECNICO", dto.DES_ITEM_TECNICO);
        p.Add("DES_ITEM_TECNICO_SINGULAR", dto.DES_ITEM_TECNICO_SINGULAR);
        p.Add("VAL_ITEM_TECNICO_LISTA", dto.VAL_ITEM_TECNICO_LISTA);
        p.Add("NUM_ESTRUCTURA_ESPECIFICACION_TECNICA", dto.NUM_ESTRUCTURA_ESPECIFICACION_TECNICA);
        p.Add("COD_ESPECIFICACION_TECNICA_PADRE", dto.COD_ESPECIFICACION_TECNICA_PADRE);
        p.Add("COD_ITEM_TECNICO_PADRE", dto.COD_ITEM_TECNICO_PADRE);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("FLG_DEFAULT", dto.FLG_DEFAULT ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("COD_ENTIDAD_DEPENDIENTE", dto.COD_ENTIDAD_DEPENDIENTE);
        p.Add("COD_ESPECIFICACION_TECNICA_DEPENDIENTE", dto.COD_ESPECIFICACION_TECNICA_DEPENDIENTE);
        p.Add("COD_ITEM_TECNICO_DEPENDIENTE", dto.COD_ITEM_TECNICO_DEPENDIENTE);
        p.Add("DES_COLOR", dto.DES_COLOR);
        p.Add("FLG_HABILITADO", dto.FLG_HABILITADO);
        p.Add("VAL_NUMERICO", dto.VAL_NUMERICO);
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

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EspecificacionesTecnicasItemsUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("DES_ITEM_TECNICO", dto.DES_ITEM_TECNICO);
        p.Add("DES_ITEM_TECNICO_SINGULAR", dto.DES_ITEM_TECNICO_SINGULAR);
        p.Add("VAL_ITEM_TECNICO_LISTA", dto.VAL_ITEM_TECNICO_LISTA);
        p.Add("NUM_ESTRUCTURA_ESPECIFICACION_TECNICA", dto.NUM_ESTRUCTURA_ESPECIFICACION_TECNICA);
        p.Add("COD_ESPECIFICACION_TECNICA_PADRE", dto.COD_ESPECIFICACION_TECNICA_PADRE);
        p.Add("COD_ITEM_TECNICO_PADRE", dto.COD_ITEM_TECNICO_PADRE);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("FLG_DEFAULT", dto.FLG_DEFAULT ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("COD_ENTIDAD_DEPENDIENTE", dto.COD_ENTIDAD_DEPENDIENTE);
        p.Add("COD_ESPECIFICACION_TECNICA_DEPENDIENTE", dto.COD_ESPECIFICACION_TECNICA_DEPENDIENTE);
        p.Add("COD_ITEM_TECNICO_DEPENDIENTE", dto.COD_ITEM_TECNICO_DEPENDIENTE);
        p.Add("DES_COLOR", dto.DES_COLOR);
        p.Add("FLG_HABILITADO", dto.FLG_HABILITADO);
        p.Add("VAL_NUMERICO", dto.VAL_NUMERICO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Delete, new { COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA, COD_ITEM_TECNICO = COD_ITEM_TECNICO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.EspecificacionesTecnicasItems.Delete, new { COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA, COD_ITEM_TECNICO = COD_ITEM_TECNICO })
        };
    }
}

}