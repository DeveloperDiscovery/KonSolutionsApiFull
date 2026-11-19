using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposPedidoComercialRepository : ITiposPedidoComercialRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposPedidoComercialRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposPedidoComercial>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPedidoComercial>(SP.SCHEMAComercial.TiposPedidoComercial.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposPedidoComercial?> ShowbyidAsync(int COD_TIPO_PEDIDO_COMERCIAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposPedidoComercial>(SP.SCHEMAComercial.TiposPedidoComercial.Showbyid, new { COD_TIPO_PEDIDO_COMERCIAL = COD_TIPO_PEDIDO_COMERCIAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposPedidoComercial.Showcombox);
    }

    public async Task<IEnumerable<TiposPedidoComercial>> SearchAsync(TiposPedidoComercialSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPedidoComercial>(SP.SCHEMAComercial.TiposPedidoComercial.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposPedidoComercialInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PEDIDO_COMERCIAL", dto.COD_TIPO_PEDIDO_COMERCIAL);
        p.Add("DES_TIPO_PEDIDO_COMERCIAL", dto.DES_TIPO_PEDIDO_COMERCIAL);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("COD_TIPO_ENTIDAD_VENDEDORA", dto.COD_TIPO_ENTIDAD_VENDEDORA);
        p.Add("FLG_PEDIDO_FORMAL", dto.FLG_PEDIDO_FORMAL ?? false);
        p.Add("FLG_BIENES", dto.FLG_BIENES ?? false);
        p.Add("COD_TIPO_COMPROBANTE_PAGO_INTERNO", dto.COD_TIPO_COMPROBANTE_PAGO_INTERNO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposPedidoComercial.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposPedidoComercial.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposPedidoComercial.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposPedidoComercialUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PEDIDO_COMERCIAL", dto.COD_TIPO_PEDIDO_COMERCIAL);
        p.Add("DES_TIPO_PEDIDO_COMERCIAL", dto.DES_TIPO_PEDIDO_COMERCIAL);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("COD_TIPO_ENTIDAD_VENDEDORA", dto.COD_TIPO_ENTIDAD_VENDEDORA);
        p.Add("FLG_PEDIDO_FORMAL", dto.FLG_PEDIDO_FORMAL ?? false);
        p.Add("FLG_BIENES", dto.FLG_BIENES ?? false);
        p.Add("COD_TIPO_COMPROBANTE_PAGO_INTERNO", dto.COD_TIPO_COMPROBANTE_PAGO_INTERNO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposPedidoComercial.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposPedidoComercial.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposPedidoComercial.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_TIPO_PEDIDO_COMERCIAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposPedidoComercial.Delete, new { COD_TIPO_PEDIDO_COMERCIAL = COD_TIPO_PEDIDO_COMERCIAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposPedidoComercial.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposPedidoComercial.Delete, new { COD_TIPO_PEDIDO_COMERCIAL = COD_TIPO_PEDIDO_COMERCIAL })
        };
    }
}

    /// <summary>PROC_COMERCIAL_TIPOS_PEDIDO_COMERCIAL_LOADCOMBOS: tipos de entidad + tipos de
    /// comprobante de pago en un solo viaje para TiposPedidoComercialDialog. Ambos SHOWCOMBOX de
    /// origen ya devuelven columnas reales COD_*/DES_* (sin alias Id/Texto), así que el SP nuevo
    /// puede reejecutarlos directo.</summary>
    public async Task<TiposPedidoComercialLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(SP.SCHEMAComercial.TiposPedidoComercial.Loadcombos, commandType: CommandType.StoredProcedure);

        var tiposEntidad = MapCombo(await multi.ReadAsync<dynamic>());
        var tiposComprobantePago = MapCombo(await multi.ReadAsync<dynamic>());

        return new TiposPedidoComercialLoadCombosResult { TiposEntidad = tiposEntidad, TiposComprobantePago = tiposComprobantePago };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem detectando la columna de código (primera que
    /// empiece con COD_) y la de descripción (primera DES_/NOMBRE_) — nunca por nombre literal
    /// "Id"/"Texto".</summary>
    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas)
    {
        var resultado = new List<ComboboxItem>();
        foreach (var fila in filas)
        {
            var d = (IDictionary<string, object>)fila;
            var colId = d.Keys.FirstOrDefault(k => k.StartsWith("COD_", StringComparison.OrdinalIgnoreCase));
            var colTexto = d.Keys.FirstOrDefault(k => k.StartsWith("DES_", StringComparison.OrdinalIgnoreCase) || k.StartsWith("NOMBRE_", StringComparison.OrdinalIgnoreCase));
            resultado.Add(new ComboboxItem
            {
                Id = colId is not null ? d[colId]?.ToString() ?? "" : "",
                Texto = colTexto is not null ? d[colTexto]?.ToString() ?? "" : ""
            });
        }
        return resultado;
    }

}