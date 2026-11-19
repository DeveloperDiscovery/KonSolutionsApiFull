using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Administracion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CargosRepository : ICargosRepository
{
    private readonly IDbConnectionFactory _db;
    public CargosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Cargos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Cargos>(SP.SCHEMAAdministracion.Cargos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Cargos?> ShowbyidAsync(int COD_CARGO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Cargos>(SP.SCHEMAAdministracion.Cargos.Showbyid, new { COD_CARGO = COD_CARGO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAAdministracion.Cargos.Showcombox);
    }

    /// <summary>Detecta la columna de código (primera "COD"/"Id") y de descripción
    /// (primera "DES"/"NOMBRE"/"Texto") por convención, igual que AreasRepository.</summary>
    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas)
    {
        var resultado = new List<ComboboxItem>();
        foreach (var fila in filas)
        {
            var d = (IDictionary<string, object>)fila;
            var colId = d.Keys.FirstOrDefault(k => k.Equals("Id", StringComparison.OrdinalIgnoreCase))
                     ?? d.Keys.FirstOrDefault(k => k.StartsWith("COD", StringComparison.OrdinalIgnoreCase));
            var colTexto = d.Keys.FirstOrDefault(k => k.Equals("Texto", StringComparison.OrdinalIgnoreCase))
                        ?? d.Keys.FirstOrDefault(k => k.StartsWith("DES", StringComparison.OrdinalIgnoreCase) || k.StartsWith("NOMBRE", StringComparison.OrdinalIgnoreCase));
            resultado.Add(new ComboboxItem
            {
                Id    = colId is not null ? d[colId]?.ToString() ?? "" : "",
                Texto = colTexto is not null ? d[colTexto]?.ToString() ?? "" : ""
            });
        }
        return resultado;
    }

    public async Task<IEnumerable<Cargos>> SearchAsync(CargosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Cargos>(SP.SCHEMAAdministracion.Cargos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CargosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("DES_CARGO", dto.DES_CARGO);
        p.Add("COD_TIPO_CARGO", dto.COD_TIPO_CARGO);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAdministracion.Cargos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAAdministracion.Cargos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAdministracion.Cargos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CargosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("DES_CARGO", dto.DES_CARGO);
        p.Add("COD_TIPO_CARGO", dto.COD_TIPO_CARGO);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAdministracion.Cargos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAAdministracion.Cargos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAdministracion.Cargos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_CARGO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAdministracion.Cargos.Delete, new { COD_CARGO = COD_CARGO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAAdministracion.Cargos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAdministracion.Cargos.Delete, new { COD_CARGO = COD_CARGO })
        };
    }
}

}