using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesDireccionesRepository : IEntidadesDireccionesRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesDireccionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EntidadesDirecciones>> ShowallAsync(decimal COD_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesDirecciones>(SP.SCHEMAComercial.EntidadesDirecciones.Showall, new { COD_ENTIDAD = COD_ENTIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesDirecciones?> ShowbyidAsync(decimal COD_ENTIDAD, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesDirecciones>(SP.SCHEMAComercial.EntidadesDirecciones.Showbyid, new { COD_ENTIDAD = COD_ENTIDAD, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.EntidadesDirecciones.Showcombox);
    }

    public async Task<IEnumerable<EntidadesDirecciones>> SearchAsync(EntidadesDireccionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesDirecciones>(SP.SCHEMAComercial.EntidadesDirecciones.Search, new { COD_ENTIDAD = dto.COD_ENTIDAD, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesDireccionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_DIRECCION", dto.COD_TIPO_DIRECCION);
        p.Add("COD_UBIGEO", dto.COD_UBIGEO);
        p.Add("COD_TIPO_ZONA", dto.COD_TIPO_ZONA);
        p.Add("DES_NOMBRE_ZONA", dto.DES_NOMBRE_ZONA);
        p.Add("COD_TIPO_VIA", dto.COD_TIPO_VIA);
        p.Add("DES_NOMBRE_VIA", dto.DES_NOMBRE_VIA);
        p.Add("DES_NUMERACION", dto.DES_NUMERACION);
        p.Add("DES_KILOMETRO", dto.DES_KILOMETRO);
        p.Add("DES_MANZANA", dto.DES_MANZANA);
        p.Add("DES_LOTE", dto.DES_LOTE);
        p.Add("DES_INTERIOR", dto.DES_INTERIOR);
        p.Add("DES_DIRECCION", dto.DES_DIRECCION);
        p.Add("DES_REFERENCIA", dto.DES_REFERENCIA);
        p.Add("VAL_LATITUD", dto.VAL_LATITUD);
        p.Add("VAL_LONGITUD", dto.VAL_LONGITUD);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesDirecciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesDirecciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesDirecciones.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EntidadesDireccionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_DIRECCION", dto.COD_TIPO_DIRECCION);
        p.Add("COD_UBIGEO", dto.COD_UBIGEO);
        p.Add("COD_TIPO_ZONA", dto.COD_TIPO_ZONA);
        p.Add("DES_NOMBRE_ZONA", dto.DES_NOMBRE_ZONA);
        p.Add("COD_TIPO_VIA", dto.COD_TIPO_VIA);
        p.Add("DES_NOMBRE_VIA", dto.DES_NOMBRE_VIA);
        p.Add("DES_NUMERACION", dto.DES_NUMERACION);
        p.Add("DES_KILOMETRO", dto.DES_KILOMETRO);
        p.Add("DES_MANZANA", dto.DES_MANZANA);
        p.Add("DES_LOTE", dto.DES_LOTE);
        p.Add("DES_INTERIOR", dto.DES_INTERIOR);
        p.Add("DES_DIRECCION", dto.DES_DIRECCION);
        p.Add("DES_REFERENCIA", dto.DES_REFERENCIA);
        p.Add("VAL_LATITUD", dto.VAL_LATITUD);
        p.Add("VAL_LONGITUD", dto.VAL_LONGITUD);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesDirecciones.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesDirecciones.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesDirecciones.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesDirecciones.Delete, new { COD_ENTIDAD = COD_ENTIDAD, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesDirecciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesDirecciones.Delete, new { COD_ENTIDAD = COD_ENTIDAD, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

    /// <summary>PROC_COMERCIAL_ENTIDADES_DIRECCIONES_LOADCOMBOS: TiposDireccion + TiposZona
    /// (Rrhh) + TiposVia (ya venían con Task.WhenAll) + Estado (COD_ESTADO/DES_ESTADO/colores)
    /// en un solo viaje. Si codTipoEstado &lt;= 0, se resuelve el de
    /// COMERCIAL.ENTIDADES_DIRECCIONES automáticamente (mismo mecanismo que
    /// EstadosPorTablaController), para que el cliente no tenga que saberlo.</summary>
    public async Task<EntidadesDireccionesLoadCombosResult> LoadcombosAsync(int codTipoEstado)
    {
        using var conn = _db.CreateConnection();

        if (codTipoEstado <= 0)
        {
            var tabla = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
                "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SHOWBYID",
                new { DES_TABLA = "COMERCIAL.ENTIDADES_DIRECCIONES" },
                commandType: CommandType.StoredProcedure);
            codTipoEstado = tabla?.COD_TIPO_ESTADO ?? 0;
        }

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAComercial.EntidadesDirecciones.Loadcombos,
            new { COD_TIPO_ESTADO = codTipoEstado },
            commandType: CommandType.StoredProcedure);

        var tiposDireccion = MapCombo(await multi.ReadAsync<dynamic>());
        var tiposZona       = MapCombo(await multi.ReadAsync<dynamic>());
        var tiposVia         = MapCombo(await multi.ReadAsync<dynamic>());
        var estadosFilas     = await multi.ReadAsync<dynamic>();

        var estados = MapEstados(estadosFilas);

        return new EntidadesDireccionesLoadCombosResult
        {
            CodTipoEstado = codTipoEstado,
            TiposDireccion = tiposDireccion,
            TiposZona = tiposZona,
            TiposVia = tiposVia,
            Estados = estados
        };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem detectando la columna de código
    /// (primera COD_*) y de descripción (primera DES_*/NOMBRE_*) — nunca busca columnas
    /// literales "Id"/"Texto".</summary>
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

    private static List<EstadoItem> MapEstados(IEnumerable<dynamic> filas) => filas.Select(f =>
    {
        var d = (IDictionary<string, object>)f;
        var codStr = d.TryGetValue("COD_ESTADO", out var id) ? id?.ToString() ?? "" : "";
        return new EstadoItem
        {
            CodEstado = int.TryParse(codStr, out var n) ? n : 0,
            DesEstado = d.TryGetValue("DES_ESTADO", out var tx) ? tx?.ToString() ?? "" : "",
            BackColor = d.TryGetValue("DES_BACKCOLOR", out var bc) ? bc?.ToString() ?? "" : "",
            ForeColor = d.TryGetValue("DES_FORECOLOR", out var fc) ? fc?.ToString() ?? "" : "",
            EsDefault = d.TryGetValue("FLG_DEFAULT", out var df) && df is not null && Convert.ToBoolean(df)
        };
    }).ToList();

    private class TablaTipoEstado { public int COD_TIPO_ESTADO { get; set; } }

}