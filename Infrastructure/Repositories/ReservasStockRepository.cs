using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ReservasStockRepository : IReservasStockRepository
{
    private readonly IDbConnectionFactory _db;
    public ReservasStockRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>Un combo vacío manda "" y "" NO es lo mismo que NULL: el SP entiende NULL como
    /// "no filtres por esto", mientras que "" filtraría por un código que no existe y devolvería
    /// cero filas.
    ///
    /// Y el parámetro sólo se agrega si trae valor. Un parámetro que no se manda es un parámetro
    /// que no puede romper nada: si el script de SQL todavía no se corrió en esa base, el listado
    /// sin filtro sigue funcionando igual que siempre en vez de reventar con "has too many
    /// arguments specified" — que es exactamente lo que nos pasó con PRODUCTOS_SHOWALL.</summary>
    private static void AgregarSiTieneValor(DynamicParameters p, string nombre, string? valor)
    {
        if (!string.IsNullOrWhiteSpace(valor)) p.Add(nombre, valor);
    }

    public async Task<IEnumerable<ReservasStock>> ShowallAsync(ReservasStockSearchDto? filtros = null)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        AgregarSiTieneValor(p, "COD_TIPO_RESERVA", filtros?.COD_TIPO_RESERVA);
        AgregarSiTieneValor(p, "COD_PRODUCTO",     filtros?.COD_PRODUCTO);
        return await conn.QueryAsync<ReservasStock>(SP.SCHEMALogistica.ReservasStock.Showall, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<ReservasStock?> ShowbyidAsync(int NUM_RESERVA_STOCK)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ReservasStock>(SP.SCHEMALogistica.ReservasStock.Showbyid, new { NUM_RESERVA_STOCK = NUM_RESERVA_STOCK }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        // Mismo caso que Requerimientos: la clave es NUM_RESERVA_STOCK, no COD_*, y por
        // convención el lector tomaba COD_TIPO_RESERVA como identificador.
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.ReservasStock.Showcombox,
            columnaId: "NUM_RESERVA_STOCK", columnaTexto: "DES_RESERVA_STOCK");
    }

    public async Task<IEnumerable<ReservasStock>> SearchAsync(ReservasStockSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("SEARCH", dto.Search);
        AgregarSiTieneValor(p, "COD_TIPO_RESERVA", dto.COD_TIPO_RESERVA);
        return await conn.QueryAsync<ReservasStock>(SP.SCHEMALogistica.ReservasStock.Search, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ReservasStockInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_RESERVA_STOCK", dto.NUM_RESERVA_STOCK);
        p.Add("DES_RESERVA_STOCK", dto.DES_RESERVA_STOCK);
        p.Add("FEC_RESERVA_STOCK", dto.FEC_RESERVA_STOCK);
        p.Add("NUM_REQUERIMIENTO_STOCK", dto.NUM_REQUERIMIENTO_STOCK);
        p.Add("COD_TIPO_RESERVA", dto.COD_TIPO_RESERVA);
        // El SP ya no trae default para este parámetro (SCRIPdb_v28): sin agregarlo acá,
        // CUALQUIER alta falla con "Procedure expects parameter '@FEC_ATENCION'...".
        p.Add("FEC_ATENCION", dto.FEC_ATENCION);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        // Se lee como fila suelta y no como WriteResult porque el SP devuelve la clave con su
        // propio nombre —NUM_RESERVA_STOCK— y Dapper no tiene cómo saber que eso es el Codigo.
        // El mismo resultset sirve para el camino del CATCH, que trae ErrorNumber/ErrorMessage.
        var fila = await conn.QueryFirstOrDefaultAsync(SP.SCHEMALogistica.ReservasStock.Insert, p, commandType: CommandType.StoredProcedure);
        return LeerAlta(fila);
    }

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    /// <summary>Traduce el resultset del alta: o trae la clave que generó la base, o trae el
    /// error del CATCH. Sin esto el número se perdía y el llamador no sabía qué documento acababa
    /// de crear — que es justo lo que hace falta para insertarle los ítems.</summary>
    private static WriteResult LeerAlta(dynamic? fila)
    {
        if (fila is null) return new WriteResult { Estado = "OK", AffectedRows = 1 };
        var campos = (IDictionary<string, object?>)fila;

        if (campos.TryGetValue("ErrorNumber", out var num) && num is not null)
            return new WriteResult
            {
                Estado = "ERROR",
                AffectedRows = 0,
                ErrorNumber = Convert.ToInt32(num),
                Mensaje = campos.TryGetValue("ErrorMessage", out var msg) ? msg?.ToString() ?? "" : "",
                ErrorProcedure = campos.TryGetValue("ErrorProcedure", out var sp) ? sp?.ToString() ?? "" : ""
            };

        return new WriteResult
        {
            Estado = "OK",
            AffectedRows = 1,
            Codigo = campos.TryGetValue("NUM_RESERVA_STOCK", out var cod) ? cod?.ToString() ?? "" : ""
        };
    }

    public async Task<WriteResult> UpdateAsync(ReservasStockUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_RESERVA_STOCK", dto.NUM_RESERVA_STOCK);
        p.Add("DES_RESERVA_STOCK", dto.DES_RESERVA_STOCK);
        p.Add("FEC_RESERVA_STOCK", dto.FEC_RESERVA_STOCK);
        p.Add("NUM_REQUERIMIENTO_STOCK", dto.NUM_REQUERIMIENTO_STOCK);
        p.Add("COD_TIPO_RESERVA", dto.COD_TIPO_RESERVA);
        p.Add("FEC_ATENCION", dto.FEC_ATENCION);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.ReservasStock.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ReservasStock.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ReservasStock.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_RESERVA_STOCK)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.ReservasStock.Delete, new { NUM_RESERVA_STOCK = NUM_RESERVA_STOCK }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ReservasStock.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ReservasStock.Delete, new { NUM_RESERVA_STOCK = NUM_RESERVA_STOCK })
        };
    }
}

}