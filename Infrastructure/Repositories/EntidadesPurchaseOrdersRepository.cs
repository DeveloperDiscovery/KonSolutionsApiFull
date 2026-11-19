using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

// ── Cabecera ────────────────────────────────────────────────────────────────
public class EntidadesPurchaseOrdersRepository : IEntidadesPurchaseOrdersRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesPurchaseOrdersRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EntidadesPurchaseOrders>> ShowallAsync(decimal codEntidad)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesPurchaseOrders>(
            SP.SCHEMAComercial.EntidadesPurchaseOrders.Showall,
            new { COD_ENTIDAD = codEntidad }, commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesPurchaseOrders?> ShowbyidAsync(decimal codEntidad, string numPurchaseOrder)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesPurchaseOrders>(
            SP.SCHEMAComercial.EntidadesPurchaseOrders.Showbyid,
            new { COD_ENTIDAD = codEntidad, NUM_PURCHASE_ORDER = numPurchaseOrder },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>El SHOWCOMBOX de esta tabla devuelve la fila entera, no un par (Id, Texto), así
    /// que el combo se arma acá con el número de PO y su diseño.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(decimal codEntidad)
    {
        using var conn = _db.CreateConnection();
        var filas = await conn.QueryAsync<EntidadesPurchaseOrders>(
            SP.SCHEMAComercial.EntidadesPurchaseOrders.Showcombox,
            new { COD_ENTIDAD = codEntidad }, commandType: CommandType.StoredProcedure);

        return filas.Select(f => new ComboboxItem
        {
            Id    = f.NUM_PURCHASE_ORDER,
            Texto = string.IsNullOrWhiteSpace(f.DES_DISENO_ENTIDAD)
                        ? f.NUM_PURCHASE_ORDER
                        : $"{f.NUM_PURCHASE_ORDER} — {f.DES_DISENO_ENTIDAD}"
        });
    }

    public async Task<IEnumerable<EntidadesPurchaseOrders>> SearchAsync(EntidadesPurchaseOrdersSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesPurchaseOrders>(
            SP.SCHEMAComercial.EntidadesPurchaseOrders.Search,
            new { COD_ENTIDAD = dto.COD_ENTIDAD, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    private static DynamicParameters Parametros(EntidadesPurchaseOrdersInsertDto d, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", d.COD_ENTIDAD);
        p.Add("NUM_PURCHASE_ORDER", d.NUM_PURCHASE_ORDER);
        p.Add("COD_DISENO", string.IsNullOrWhiteSpace(d.COD_DISENO) ? null : d.COD_DISENO);
        p.Add("DES_DISENO_ENTIDAD", string.IsNullOrWhiteSpace(d.DES_DISENO_ENTIDAD) ? null : d.DES_DISENO_ENTIDAD);
        p.Add("FEC_DISENO_ENTIDAD", d.FEC_DISENO_ENTIDAD);
        // Las banderas siempre con valor: la columna admite NULL, pero un NULL en "itera embalaje"
        // no es "no itera" sino "no se sabe", y eso hay que interpretarlo en cada consulta.
        p.Add("FLG_ITERAR_EMBALAJE", d.FLG_ITERAR_EMBALAJE ?? false);
        p.Add("FLG_PURCHASE_ORDER_GENERICA", d.FLG_PURCHASE_ORDER_GENERICA ?? false);
        // Temporada y colección van tal cual, NULL incluido: acá NULL sí significa algo —
        // "esta PO no pertenece a ninguna temporada"— a diferencia de las banderas de arriba.
        //
        // Una colección sin temporada no existe (la clave de la colección incluye la
        // temporada), así que si llega ese par se manda la colección en NULL en vez de
        // dejar que la FK reviente con un 547 que no explica nada.
        p.Add("NUM_TEMPORADA", d.NUM_TEMPORADA);
        p.Add("NUM_COLECCION", d.NUM_TEMPORADA is null ? null : d.NUM_COLECCION);
        p.Add("COD_TIPO_ESTADO", d.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", d.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        return p;
    }

    public async Task<WriteResult> InsertAsync(EntidadesPurchaseOrdersInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = Parametros(dto, codUsuario, codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.EntidadesPurchaseOrders.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.EntidadesPurchaseOrders.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurchaseOrders.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(EntidadesPurchaseOrdersUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = Parametros(dto, codUsuario, codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.EntidadesPurchaseOrders.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.EntidadesPurchaseOrders.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurchaseOrders.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(decimal codEntidad, string numPurchaseOrder)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", codEntidad);
        p.Add("NUM_PURCHASE_ORDER", numPurchaseOrder);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.EntidadesPurchaseOrders.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.EntidadesPurchaseOrders.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurchaseOrders.Delete, p)
            };
        }
    }
}


// ── Despacho ────────────────────────────────────────────────────────────────
public class EntidadesPurchaseOrdersDespachoRepository : IEntidadesPurchaseOrdersDespachoRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesPurchaseOrdersDespachoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EntidadesPurchaseOrdersDespacho>> ShowallAsync(decimal codEntidad, string numPurchaseOrder)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesPurchaseOrdersDespacho>(
            SP.SCHEMAComercial.EntidadesPurchaseOrdersDespacho.Showall,
            new { COD_ENTIDAD = codEntidad, NUM_PURCHASE_ORDER = numPurchaseOrder },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesPurchaseOrdersDespacho?> ShowbyidAsync(decimal codEntidad, string numPurchaseOrder, int numSecuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesPurchaseOrdersDespacho>(
            SP.SCHEMAComercial.EntidadesPurchaseOrdersDespacho.Showbyid,
            new { COD_ENTIDAD = codEntidad, NUM_PURCHASE_ORDER = numPurchaseOrder, NUM_SECUENCIA = numSecuencia },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<EntidadesPurchaseOrdersDespacho>> SearchAsync(EntidadesPurchaseOrdersDespachoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesPurchaseOrdersDespacho>(
            SP.SCHEMAComercial.EntidadesPurchaseOrdersDespacho.Search,
            new { COD_ENTIDAD = dto.COD_ENTIDAD, NUM_PURCHASE_ORDER = dto.NUM_PURCHASE_ORDER, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    private static DynamicParameters Parametros(EntidadesPurchaseOrdersDespachoInsertDto d, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", d.COD_ENTIDAD);
        p.Add("NUM_PURCHASE_ORDER", d.NUM_PURCHASE_ORDER);
        p.Add("NUM_SECUENCIA", d.NUM_SECUENCIA);
        p.Add("CAN_EMBALAJE", d.CAN_EMBALAJE);
        p.Add("COD_PRODUCTO_EMBALAJE_INNERPACK",
              string.IsNullOrWhiteSpace(d.COD_PRODUCTO_EMBALAJE_INNERPACK) ? null : d.COD_PRODUCTO_EMBALAJE_INNERPACK);
        p.Add("COD_PRODUCTO_EMBALAJE_MASTERPACK",
              string.IsNullOrWhiteSpace(d.COD_PRODUCTO_EMBALAJE_MASTERPACK) ? null : d.COD_PRODUCTO_EMBALAJE_MASTERPACK);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        return p;
    }

    public async Task<WriteResult> InsertAsync(EntidadesPurchaseOrdersDespachoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = Parametros(dto, codUsuario, codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.EntidadesPurchaseOrdersDespacho.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.EntidadesPurchaseOrdersDespacho.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurchaseOrdersDespacho.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(EntidadesPurchaseOrdersDespachoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = Parametros(dto, codUsuario, codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.EntidadesPurchaseOrdersDespacho.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.EntidadesPurchaseOrdersDespacho.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurchaseOrdersDespacho.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(decimal codEntidad, string numPurchaseOrder, int numSecuencia)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", codEntidad);
        p.Add("NUM_PURCHASE_ORDER", numPurchaseOrder);
        p.Add("NUM_SECUENCIA", numSecuencia);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.EntidadesPurchaseOrdersDespacho.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.EntidadesPurchaseOrdersDespacho.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurchaseOrdersDespacho.Delete, p)
            };
        }
    }
}


// ── Especificación técnica ──────────────────────────────────────────────────
public class EntidadesPurchaseOrdersEspecificacionTecnicaRepository
    : IEntidadesPurchaseOrdersEspecificacionTecnicaRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesPurchaseOrdersEspecificacionTecnicaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EntidadesPurchaseOrdersEspecificacionTecnica>> ShowallAsync(
        decimal codEntidad, string numPurchaseOrder, int numSecuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesPurchaseOrdersEspecificacionTecnica>(
            SP.SCHEMAComercial.EntidadesPurchaseOrdersEspecificacionTecnica.Showall,
            new { COD_ENTIDAD = codEntidad, NUM_PURCHASE_ORDER = numPurchaseOrder, NUM_SECUENCIA = numSecuencia },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesPurchaseOrdersEspecificacionTecnica?> ShowbyidAsync(
        decimal codEntidad, string numPurchaseOrder, int numSecuencia, string codEspecificacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesPurchaseOrdersEspecificacionTecnica>(
            SP.SCHEMAComercial.EntidadesPurchaseOrdersEspecificacionTecnica.Showbyid,
            new { COD_ENTIDAD = codEntidad, NUM_PURCHASE_ORDER = numPurchaseOrder,
                  NUM_SECUENCIA = numSecuencia, COD_ESPECIFICACION_TECNICA = codEspecificacion },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<EntidadesPurchaseOrdersEspecificacionTecnica>> SearchAsync(
        EntidadesPurchaseOrdersEspecificacionTecnicaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesPurchaseOrdersEspecificacionTecnica>(
            SP.SCHEMAComercial.EntidadesPurchaseOrdersEspecificacionTecnica.Search,
            new { COD_ENTIDAD = dto.COD_ENTIDAD, NUM_PURCHASE_ORDER = dto.NUM_PURCHASE_ORDER,
                  NUM_SECUENCIA = dto.NUM_SECUENCIA, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    private static DynamicParameters Parametros(
        EntidadesPurchaseOrdersEspecificacionTecnicaInsertDto d, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", d.COD_ENTIDAD);
        p.Add("NUM_PURCHASE_ORDER", d.NUM_PURCHASE_ORDER);
        p.Add("NUM_SECUENCIA", d.NUM_SECUENCIA);
        p.Add("COD_ESPECIFICACION_TECNICA", d.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", d.COD_ITEM_TECNICO);
        p.Add("FLG_HEADER", d.FLG_HEADER ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        return p;
    }

    public async Task<WriteResult> InsertAsync(
        EntidadesPurchaseOrdersEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = Parametros(dto, codUsuario, codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.EntidadesPurchaseOrdersEspecificacionTecnica.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.EntidadesPurchaseOrdersEspecificacionTecnica.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurchaseOrdersEspecificacionTecnica.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(
        EntidadesPurchaseOrdersEspecificacionTecnicaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = Parametros(dto, codUsuario, codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.EntidadesPurchaseOrdersEspecificacionTecnica.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.EntidadesPurchaseOrdersEspecificacionTecnica.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurchaseOrdersEspecificacionTecnica.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(
        decimal codEntidad, string numPurchaseOrder, int numSecuencia, string codEspecificacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", codEntidad);
        p.Add("NUM_PURCHASE_ORDER", numPurchaseOrder);
        p.Add("NUM_SECUENCIA", numSecuencia);
        p.Add("COD_ESPECIFICACION_TECNICA", codEspecificacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.EntidadesPurchaseOrdersEspecificacionTecnica.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.EntidadesPurchaseOrdersEspecificacionTecnica.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurchaseOrdersEspecificacionTecnica.Delete, p)
            };
        }
    }
}
