using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesTemporadasRepository : IEntidadesTemporadasRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesTemporadasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EntidadesTemporadas>> ShowallAsync(decimal codEntidad)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesTemporadas>(SP.SCHEMAComercial.EntidadesTemporadas.Showall, new { COD_ENTIDAD = codEntidad }, commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesTemporadas?> ShowbyidAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesTemporadas>(SP.SCHEMAComercial.EntidadesTemporadas.Showbyid, new { COD_ENTIDAD = COD_ENTIDAD, NUM_TEMPORADA = NUM_TEMPORADA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(decimal codEntidad)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.EntidadesTemporadas.Showcombox, new { COD_ENTIDAD = codEntidad });
    }

    public async Task<IEnumerable<EntidadesTemporadas>> SearchAsync(EntidadesTemporadasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesTemporadas>(SP.SCHEMAComercial.EntidadesTemporadas.Search, new { dto.COD_ENTIDAD, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesTemporadasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_TEMPORADA", dto.NUM_TEMPORADA);
        p.Add("DES_TEMPORADA", dto.DES_TEMPORADA);
        p.Add("NUM_ANO", dto.NUM_ANO);
        p.Add("NUM_MES", dto.NUM_MES);
        p.Add("MES_FINAL", dto.MES_FINAL);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesTemporadas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesTemporadas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesTemporadas.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesTemporadas.Delete, new { COD_ENTIDAD = COD_ENTIDAD, NUM_TEMPORADA = NUM_TEMPORADA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesTemporadas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesTemporadas.Delete, new { COD_ENTIDAD = COD_ENTIDAD, NUM_TEMPORADA = NUM_TEMPORADA })
        };
    }
}

    /// <summary>PROC_COMERCIAL_ENTIDADES_TEMPORADAS_LOADCOMBOS: años + meses en un solo viaje
    /// para EntidadesTemporadasDialog. Ambos SHOWCOMBOX de origen (ANOS/MESES) devuelven su clave
    /// real como NUM_ANO/NUM_MES (no COD_*), así que se mapean con la columna explícita en vez de
    /// la convención genérica COD_/DES_ — nunca por nombre literal "Id"/"Texto".</summary>
    public async Task<EntidadesTemporadasLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(SP.SCHEMAComercial.EntidadesTemporadas.Loadcombos, commandType: CommandType.StoredProcedure);

        var anos = MapComboPorColumnas(await multi.ReadAsync<dynamic>(), "NUM_ANO", "DES_ANO");
        var meses = MapComboPorColumnas(await multi.ReadAsync<dynamic>(), "NUM_MES", "DES_MES");

        return new EntidadesTemporadasLoadCombosResult { Anos = anos, Meses = meses };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem usando el nombre exacto de columna de código
    /// y de descripción (caso puntual: NUM_ANO/NUM_MES no siguen la convención COD_*) — nunca por
    /// nombre literal "Id"/"Texto".</summary>
    private static List<ComboboxItem> MapComboPorColumnas(IEnumerable<dynamic> filas, string colId, string colTexto)
    {
        var resultado = new List<ComboboxItem>();
        foreach (var fila in filas)
        {
            var d = (IDictionary<string, object>)fila;
            resultado.Add(new ComboboxItem
            {
                Id = d.TryGetValue(colId, out var id) ? id?.ToString() ?? "" : "",
                Texto = d.TryGetValue(colTexto, out var tx) ? tx?.ToString() ?? "" : ""
            });
        }
        return resultado;
    }

}