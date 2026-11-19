using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class InventariosAlmacenesPuntoTomaRepository : IInventariosAlmacenesPuntoTomaRepository
{
    private readonly IDbConnectionFactory _db;
    public InventariosAlmacenesPuntoTomaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<InventariosAlmacenesPuntoToma>> ShowallAsync(int numInventario, string codAlmacen)
    {
        using var conn = _db.CreateConnection();
        using var reader = await conn.ExecuteReaderAsync(SP.SCHEMALogistica.InventariosAlmacenesPuntoToma.Showall,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen }, commandType: CommandType.StoredProcedure);
        return MapAll(reader);
    }

    public async Task<InventariosAlmacenesPuntoToma?> ShowbyidAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario)
    {
        using var conn = _db.CreateConnection();
        using var reader = await conn.ExecuteReaderAsync(SP.SCHEMALogistica.InventariosAlmacenesPuntoToma.Showbyid,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario },
            commandType: CommandType.StoredProcedure);
        return MapAll(reader).FirstOrDefault();
    }

    /// <summary>Pese al nombre "SHOWCOMBOX", este SP devuelve todas las columnas de la tabla
    /// (igual que SHOWALL), no Id/Texto — probablemente nunca se usó como combo real. Se lee
    /// con el mismo mapeo que ShowallAsync en vez de pasar por ComboboxReader.</summary>
    public async Task<IEnumerable<InventariosAlmacenesPuntoToma>> ShowcomboxAsync(int numInventario, string codAlmacen)
    {
        using var conn = _db.CreateConnection();
        using var reader = await conn.ExecuteReaderAsync(SP.SCHEMALogistica.InventariosAlmacenesPuntoToma.Showcombox,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen }, commandType: CommandType.StoredProcedure);
        return MapAll(reader);
    }

    public async Task<IEnumerable<InventariosAlmacenesPuntoToma>> SearchAsync(InventariosAlmacenesPuntoTomaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        using var reader = await conn.ExecuteReaderAsync(SP.SCHEMALogistica.InventariosAlmacenesPuntoToma.Search,
            new { NUM_INVENTARIO = dto.NUM_INVENTARIO, COD_ALMACEN = dto.COD_ALMACEN, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
        return MapAll(reader);
    }

    public async Task<WriteResult> InsertAsync(InventariosAlmacenesPuntoTomaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("NUM_PUNTO_TOMA_INVENTARIO", dto.NUM_PUNTO_TOMA_INVENTARIO);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("FEC_INICIO_PROGRAMADA", dto.FEC_INICIO_PROGRAMADA);
        p.Add("FEC_FINAL_PROGRAMADA", dto.FEC_FINAL_PROGRAMADA);
        p.Add("FEC_INICIO_EFECTUADA", dto.FEC_INICIO_EFECTUADA);
        p.Add("FEC_FINAL_EFECTUADA", dto.FEC_FINAL_EFECTUADA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("FLG_READER_RFID", dto.FLG_READER_RFID);
        p.Add("FLG_READER_CODEBAR", dto.FLG_READER_CODEBAR);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesPuntoToma.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesPuntoToma.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesPuntoToma.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta — el SP no altera la clave.</summary>
    public async Task<WriteResult> UpdateAsync(InventariosAlmacenesPuntoTomaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("NUM_PUNTO_TOMA_INVENTARIO", dto.NUM_PUNTO_TOMA_INVENTARIO);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("FEC_INICIO_PROGRAMADA", dto.FEC_INICIO_PROGRAMADA);
        p.Add("FEC_FINAL_PROGRAMADA", dto.FEC_FINAL_PROGRAMADA);
        p.Add("FEC_INICIO_EFECTUADA", dto.FEC_INICIO_EFECTUADA);
        p.Add("FEC_FINAL_EFECTUADA", dto.FEC_FINAL_EFECTUADA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("FLG_READER_RFID", dto.FLG_READER_RFID);
        p.Add("FLG_READER_CODEBAR", dto.FLG_READER_CODEBAR);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesPuntoToma.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesPuntoToma.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesPuntoToma.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenesPuntoToma.Delete,
            new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenesPuntoToma.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenesPuntoToma.Delete, new { NUM_INVENTARIO = numInventario, COD_ALMACEN = codAlmacen, NUM_PUNTO_TOMA_INVENTARIO = numPuntoTomaInventario })
        };
    }
}

    /// <summary>Mapeo manual del reader (en vez de Dapper QueryAsync&lt;T&gt; automático) porque
    /// SHOWALL/SHOWBYID/SHOWCOMBOX/SEARCH devuelven DOS columnas "DES_OBSERVACION" (la propia
    /// del punto de toma y la del inventario padre, ver comentario en el Domain Model). Se
    /// resuelve leyendo por ORDINAL, mismo criterio que ya usa
    /// <see cref="ComboboxReader"/> en este proyecto para columnas ambiguas: la PRIMERA
    /// DES_OBSERVACION (columna propia, listada primero por el generador) va a
    /// DES_OBSERVACION, la ÚLTIMA (la del JOIN) va a DES_OBSERVACION_INVENTARIO. El resto de
    /// columnas son únicas y se leen por nombre con GetOrdinal normal.</summary>
    private static List<InventariosAlmacenesPuntoToma> MapAll(IDataReader reader)
    {
        var ordObservacionPropia = -1;
        var ordObservacionInventario = -1;
        for (var i = 0; i < reader.FieldCount; i++)
        {
            if (!string.Equals(reader.GetName(i), "DES_OBSERVACION", StringComparison.OrdinalIgnoreCase)) continue;
            if (ordObservacionPropia < 0) ordObservacionPropia = i;
            ordObservacionInventario = i;
        }

        var lista = new List<InventariosAlmacenesPuntoToma>();
        while (reader.Read())
        {
            lista.Add(new InventariosAlmacenesPuntoToma
            {
                NUM_INVENTARIO = Nullable<int>(reader, "NUM_INVENTARIO"),
                COD_ALMACEN = Texto(reader, "COD_ALMACEN"),
                NUM_PUNTO_TOMA_INVENTARIO = NullableByte(reader, "NUM_PUNTO_TOMA_INVENTARIO"),
                COD_ENTIDAD_RESPONSABLE = Nullable<decimal>(reader, "COD_ENTIDAD_RESPONSABLE"),
                FEC_INICIO_PROGRAMADA = Nullable<DateTime>(reader, "FEC_INICIO_PROGRAMADA"),
                FEC_FINAL_PROGRAMADA = Nullable<DateTime>(reader, "FEC_FINAL_PROGRAMADA"),
                FEC_INICIO_EFECTUADA = Nullable<DateTime>(reader, "FEC_INICIO_EFECTUADA"),
                FEC_FINAL_EFECTUADA = Nullable<DateTime>(reader, "FEC_FINAL_EFECTUADA"),
                DES_OBSERVACION = TextoOrdinal(reader, ordObservacionPropia),
                FLG_READER_RFID = Nullable<bool>(reader, "FLG_READER_RFID"),
                FLG_READER_CODEBAR = Nullable<bool>(reader, "FLG_READER_CODEBAR"),
                COD_TIPO_ESTADO = Nullable<int>(reader, "COD_TIPO_ESTADO"),
                COD_ESTADO = Nullable<int>(reader, "COD_ESTADO"),
                COD_USUARIO_REGISTRO = Texto(reader, "COD_USUARIO_REGISTRO"),
                COD_ESTACION_REGISTRO = Texto(reader, "COD_ESTACION_REGISTRO"),
                FEC_REGISTRO = Nullable<DateTime>(reader, "FEC_REGISTRO"),
                COD_USUARIO_ACTUALIZACION = Texto(reader, "COD_USUARIO_ACTUALIZACION"),
                COD_ESTACION_ACTUALIZACION = Texto(reader, "COD_ESTACION_ACTUALIZACION"),
                FEC_ACTUALIZACION = Nullable<DateTime>(reader, "FEC_ACTUALIZACION"),
                DES_BACKCOLOR = Texto(reader, "DES_BACKCOLOR"),
                DES_ESTADO = Texto(reader, "DES_ESTADO"),
                DES_FORECOLOR = Texto(reader, "DES_FORECOLOR"),
                DES_COMERCIAL = Texto(reader, "DES_COMERCIAL"),
                DES_MATERNO = Texto(reader, "DES_MATERNO"),
                DES_NOMBRE = Texto(reader, "DES_NOMBRE"),
                DES_NOMBRE_COMPLETO = Texto(reader, "DES_NOMBRE_COMPLETO"),
                DES_NOMBRE2 = Texto(reader, "DES_NOMBRE2"),
                DES_PATERNO = Texto(reader, "DES_PATERNO"),
                DES_PREFIJO_USUARIO_COMERCIAL = Texto(reader, "DES_PREFIJO_USUARIO_COMERCIAL"),
                DES_OBSERVACION_INVENTARIO = TextoOrdinal(reader, ordObservacionInventario),
            });
        }
        return lista;
    }

    private static int OrdinalDe(IDataReader reader, string nombre)
    {
        for (var i = 0; i < reader.FieldCount; i++)
            if (string.Equals(reader.GetName(i), nombre, StringComparison.OrdinalIgnoreCase))
                return i;
        return -1;
    }

    private static string? TextoOrdinal(IDataReader reader, int ordinal)
        => ordinal < 0 || reader.IsDBNull(ordinal) ? null : Convert.ToString(reader.GetValue(ordinal));

    private static string Texto(IDataReader reader, string nombre)
    {
        var ord = OrdinalDe(reader, nombre);
        return ord < 0 || reader.IsDBNull(ord) ? string.Empty : Convert.ToString(reader.GetValue(ord)) ?? string.Empty;
    }

    private static T? Nullable<T>(IDataReader reader, string nombre) where T : struct
    {
        var ord = OrdinalDe(reader, nombre);
        if (ord < 0 || reader.IsDBNull(ord)) return null;
        return (T)Convert.ChangeType(reader.GetValue(ord), typeof(T));
    }

    private static byte? NullableByte(IDataReader reader, string nombre)
    {
        var ord = OrdinalDe(reader, nombre);
        if (ord < 0 || reader.IsDBNull(ord)) return null;
        return Convert.ToByte(reader.GetValue(ord));
    }
}
