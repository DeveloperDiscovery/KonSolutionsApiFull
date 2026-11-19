using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PedidosComercialesEspecificacionesTecnicasBaseRepository : IPedidosComercialesEspecificacionesTecnicasBaseRepository
{
    private readonly IDbConnectionFactory _db;
    public PedidosComercialesEspecificacionesTecnicasBaseRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PedidosComercialesEspecificacionesTecnicasBase>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosComercialesEspecificacionesTecnicasBase>(SP.SCHEMAComercial.PedidosComercialesEspecificacionesTecnicasBase.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PedidosComercialesEspecificacionesTecnicasBase?> ShowbyidAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_ENTIDAD, string COD_GRUPO_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PedidosComercialesEspecificacionesTecnicasBase>(SP.SCHEMAComercial.PedidosComercialesEspecificacionesTecnicasBase.Showbyid, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, COD_TIPO_ENTIDAD = COD_TIPO_ENTIDAD, COD_GRUPO_PRODUCTO = COD_GRUPO_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.PedidosComercialesEspecificacionesTecnicasBase.Showcombox);
    }

    public async Task<IEnumerable<PedidosComercialesEspecificacionesTecnicasBase>> SearchAsync(PedidosComercialesEspecificacionesTecnicasBaseSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosComercialesEspecificacionesTecnicasBase>(SP.SCHEMAComercial.PedidosComercialesEspecificacionesTecnicasBase.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PedidosComercialesEspecificacionesTecnicasBaseInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComercialesEspecificacionesTecnicasBase.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.PedidosComercialesEspecificacionesTecnicasBase.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComercialesEspecificacionesTecnicasBase.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_ENTIDAD, string COD_GRUPO_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComercialesEspecificacionesTecnicasBase.Delete, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, COD_TIPO_ENTIDAD = COD_TIPO_ENTIDAD, COD_GRUPO_PRODUCTO = COD_GRUPO_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.PedidosComercialesEspecificacionesTecnicasBase.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComercialesEspecificacionesTecnicasBase.Delete, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, COD_TIPO_ENTIDAD = COD_TIPO_ENTIDAD, COD_GRUPO_PRODUCTO = COD_GRUPO_PRODUCTO })
        };
    }
}

}