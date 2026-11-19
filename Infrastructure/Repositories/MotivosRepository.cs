using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MotivosRepository : IMotivosRepository
{
    private readonly IDbConnectionFactory _db;
    public MotivosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Motivos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Motivos>(SP.SCHEMAEstandar.Motivos.Showall, new { COD_TIPO_MOTIVO = (int?)null }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Motivos>> ShowByTipoAsync(int codTipoMotivo)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Motivos>(SP.SCHEMAEstandar.Motivos.Showall, new { COD_TIPO_MOTIVO = codTipoMotivo }, commandType: CommandType.StoredProcedure);
    }

    public async Task<Motivos?> ShowbyidAsync(int COD_TIPO_MOTIVO, int COD_MOTIVO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Motivos>(SP.SCHEMAEstandar.Motivos.Showbyid, new { COD_TIPO_MOTIVO = COD_TIPO_MOTIVO, COD_MOTIVO = COD_MOTIVO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.Motivos.Showcombox);
    }

    public async Task<IEnumerable<Motivos>> SearchAsync(MotivosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Motivos>(SP.SCHEMAEstandar.Motivos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MotivosInsertDto dto, string codUsuario, string codEstacion)
    {
        dto.TrimStrings(); // RTRIM de todos los campos string antes de grabar
        using var conn = _db.CreateConnection();

        // El SP calcula el siguiente COD_MOTIVO internamente cuando recibe NULL (insert nuevo).
        // En edición se pasa el código existente tal cual.
        int? codMotivo = dto.EsNuevo ? null : dto.COD_MOTIVO;
        if (!dto.EsNuevo && !dto.COD_MOTIVO.HasValue)
            return new WriteResult { Estado = "ERROR", AffectedRows = 0,
                Mensaje = "No se recibió COD_MOTIVO para la edición. Operación cancelada para evitar duplicar el registro." };

        var p = new DynamicParameters();
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", codMotivo);
        p.Add("DES_MOTIVO", dto.DES_MOTIVO);
        p.Add("FLG_DEFAULT", dto.FLG_DEFAULT ?? false);
        p.Add("FLG_EXPLICAR", dto.FLG_EXPLICAR ?? false);
        p.Add("FLG_AUTOMATICO", dto.FLG_AUTOMATICO ?? false);
        p.Add("COD_MOTIVO_SUNAT", CodigoSunat(dto.COD_MOTIVO_SUNAT));
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("IMG_PICTURE", dto.IMG_PICTURE, System.Data.DbType.Binary);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Motivos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Motivos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Motivos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Va contra el SP _UPDATE, que ya no calcula códigos: exige el
    /// COD_MOTIVO existente y actualiza esa fila.
    ///
    /// Hasta la separación de INSERT/UPDATE esto pasaba por el mismo SP de alta, que decidía
    /// según la bandera EsNuevo. Cuando el _INSERT dejó de mirarla, editar un motivo creaba uno
    /// nuevo en vez de modificarlo — el código es autogenerado, así que ni siquiera chocaba
    /// contra la clave: simplemente se duplicaba.
    ///
    /// La auditoría viaja como @COD_USUARIO_REGISTRO / @COD_ESTACION_REGISTRO aunque termine en
    /// las columnas de ACTUALIZACION: el generador hace ese mapeo dentro del SP para que el
    /// contrato de parámetros sea el mismo en el alta y en la modificación. Son once, con
    /// @COD_MOTIVO_SUNAT, que los dos SP piden sin valor por omisión.</summary>
    public async Task<WriteResult> UpdateAsync(MotivosUpdateDto dto, string codUsuario, string codEstacion)
    {
        dto.TrimStrings();
        using var conn = _db.CreateConnection();

        // Sin código no hay fila que modificar. Antes esto caía en el alta y duplicaba.
        if (!dto.COD_MOTIVO.HasValue)
            return new WriteResult { Estado = "ERROR", AffectedRows = 0,
                Mensaje = "No se recibió COD_MOTIVO para la edición. Operación cancelada." };

        var p = new DynamicParameters();
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("DES_MOTIVO", dto.DES_MOTIVO);
        p.Add("FLG_DEFAULT", dto.FLG_DEFAULT ?? false);
        p.Add("FLG_EXPLICAR", dto.FLG_EXPLICAR ?? false);
        p.Add("FLG_AUTOMATICO", dto.FLG_AUTOMATICO ?? false);
        p.Add("COD_MOTIVO_SUNAT", CodigoSunat(dto.COD_MOTIVO_SUNAT));
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("IMG_PICTURE", dto.IMG_PICTURE, System.Data.DbType.Binary);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Motivos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Motivos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Motivos.Update, p)
        };
    }
}

    /// <summary>El código SUNAT es optativo: vacío o en blanco viaja como NULL, no como cadena
    /// vacía, para que "sin código" sea uno solo en la tabla.</summary>
    private static string? CodigoSunat(string? valor)
        => string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();

    public async Task<WriteResult> DeleteAsync(int COD_TIPO_MOTIVO, int COD_MOTIVO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Motivos.Delete, new { COD_TIPO_MOTIVO = COD_TIPO_MOTIVO, COD_MOTIVO = COD_MOTIVO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Motivos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Motivos.Delete, new { COD_TIPO_MOTIVO = COD_TIPO_MOTIVO, COD_MOTIVO = COD_MOTIVO })
        };
    }
}

}