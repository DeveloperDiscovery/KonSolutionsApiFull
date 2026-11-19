using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesRepository : IEntidadesRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Entidades>> ShowallAsync(string? desTiposEntidad = null)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Entidades>(SP.SCHEMAEstandar.Entidades.Showall,
            new { DES_TIPOS_ENTIDAD = desTiposEntidad }, commandType: CommandType.StoredProcedure);
    }

    public async Task<Entidades?> ShowbyidAsync(decimal COD_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Entidades>(SP.SCHEMAEstandar.Entidades.Showbyid, new { COD_ENTIDAD = COD_ENTIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        var filas = await conn.QueryAsync<dynamic>(SP.SCHEMAEstandar.Entidades.Showcombox, commandType: CommandType.StoredProcedure);
        return filas.Select(f =>
        {
            var d = (IDictionary<string, object>)f;
            var id = new[] { "Id", "COD_ENTIDAD" }
                .Select(c => d.TryGetValue(c, out var v) ? v?.ToString() : null)
                .FirstOrDefault(v => !string.IsNullOrWhiteSpace(v)) ?? "";
            var tx = new[] { "Texto", "DES_NOMBRE_COMPLETO", "DES_COMERCIAL", "DES_NOMBRE", "DES_ENTIDAD" }
                .Select(c => d.TryGetValue(c, out var v) ? v?.ToString() : null)
                .FirstOrDefault(v => !string.IsNullOrWhiteSpace(v)) ?? "";
            return new ComboboxItem { Id = id, Texto = tx };
        }).ToList();
    }

    public async Task<IEnumerable<Entidades>> SearchAsync(EntidadesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Entidades>(SP.SCHEMAEstandar.Entidades.Search,
            new { SEARCH = dto.Search, DES_TIPOS_ENTIDAD = dto.DesTiposEntidad }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_TIPO_PERSONA", dto.COD_TIPO_PERSONA);
        p.Add("DES_PATERNO", dto.DES_PATERNO);
        p.Add("DES_MATERNO", dto.DES_MATERNO);
        p.Add("DES_NOMBRE", dto.DES_NOMBRE);
        p.Add("DES_NOMBRE2", dto.DES_NOMBRE2);
        p.Add("DES_NOMBRE_COMPLETO", dto.DES_NOMBRE_COMPLETO);
        p.Add("DES_COMERCIAL", dto.DES_COMERCIAL);
        p.Add("DES_PREFIJO_USUARIO_COMERCIAL", dto.DES_PREFIJO_USUARIO_COMERCIAL);
        p.Add("COD_ENTIDAD_DEPENDENCIA", dto.COD_ENTIDAD_DEPENDENCIA);
        p.Add("COD_TIPO_DOCUMENTO_IDENTIDAD", dto.COD_TIPO_DOCUMENTO_IDENTIDAD);
        p.Add("NUM_DOCUMENTO_IDENTIDAD", dto.NUM_DOCUMENTO_IDENTIDAD);
        p.Add("FLG_DOMICILIADO", dto.FLG_DOMICILIADO ?? false);
        p.Add("COD_PAIS", dto.COD_PAIS);
        p.Add("NUM_DUNS", dto.NUM_DUNS);
        p.Add("COD_USUARIO_RELACIONADO", dto.COD_USUARIO_RELACIONADO);
        p.Add("IMG_LOGO", dto.IMG_LOGO, System.Data.DbType.Binary);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD ?? "");
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Entidades.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Entidades.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Entidades.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Entidades.Delete, new { COD_ENTIDAD = COD_ENTIDAD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Entidades.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Entidades.Delete, new { COD_ENTIDAD = COD_ENTIDAD })
        };
    }
}

    /// <summary>PROC_ESTANDAR_ENTIDADES_PARADEPENDER devuelve COD_ENTIDAD/DES_ENTIDAD/
    /// DES_PREFIJO (no Id/Texto) — se mapea acá igual que ShowcomboxAsync.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowparadependerAsync()
    {
        using var conn = _db.CreateConnection();
        var filas = await conn.QueryAsync(SP.SCHEMAEstandar.Entidades.Paradepender, commandType: CommandType.StoredProcedure);
        return filas.Select(f =>
        {
            var d = (IDictionary<string, object>)f;
            var id = d.TryGetValue("COD_ENTIDAD", out var v) ? v?.ToString() : null;
            var tx = d.TryGetValue("DES_ENTIDAD", out var t) ? t?.ToString() : null;
            return new ComboboxItem { Id = id ?? "", Texto = tx ?? "" };
        }).ToList();
    }

    public async Task<IEnumerable<EntidadImagen>> ShowimagesAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadImagen>(SP.SCHEMAEstandar.Entidades.Showimages, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Entidades>> ShowallPorTipoAsync(string codTipoEntidad)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Entidades>(SP.SCHEMAEstandar.Entidades.ShowallPorTipo,
            new { COD_TIPO_ENTIDAD = codTipoEntidad }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Entidades>> ShowallPorTipoDependienteAsync(string codTipoEntidad, decimal codEntidadDependencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Entidades>(SP.SCHEMAEstandar.Entidades.ShowallPorTipoDependiente,
            new { COD_TIPO_ENTIDAD = codTipoEntidad, COD_ENTIDAD_DEPENDENCIA = codEntidadDependencia },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Entidades>> SearchPorTipoAsync(string codTipoEntidad, string search)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Entidades>(SP.SCHEMAEstandar.Entidades.SearchPorTipo,
            new { COD_TIPO_ENTIDAD = codTipoEntidad, SEARCH = search }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>PROC_ESTANDAR_ENTIDADES_LOADCOMBOS: tipos de persona, tipos de documento de
    /// identidad, países, estados (COD_ESTADO/DES_ESTADO/colores) y tipos de medio de
    /// contacto/dirección en un solo viaje. Si codTipoEstado &lt;= 0, se resuelve el de
    /// ESTANDAR.ENTIDADES automáticamente (mismo mecanismo que AreasRepository.LoadcombosAsync).</summary>
    public async Task<EntidadesLoadCombosResult> LoadcombosAsync(int codTipoEstado)
    {
        using var conn = _db.CreateConnection();

        if (codTipoEstado <= 0)
        {
            var tabla = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
                "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SHOWBYID",
                new { DES_TABLA = "ESTANDAR.ENTIDADES" },
                commandType: CommandType.StoredProcedure);
            codTipoEstado = tabla?.COD_TIPO_ESTADO ?? 0;
        }

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAEstandar.Entidades.Loadcombos,
            new { COD_TIPO_ESTADO = codTipoEstado },
            commandType: CommandType.StoredProcedure);

        var tiposPersonas          = MapCombo(await multi.ReadAsync<dynamic>());
        var tiposDocumentoIdentidad = MapCombo(await multi.ReadAsync<dynamic>());
        var paises                  = MapCombo(await multi.ReadAsync<dynamic>());
        var estadosFilas            = await multi.ReadAsync<dynamic>();
        var tiposMedioContacto      = MapCombo(await multi.ReadAsync<dynamic>());
        var tiposDireccion          = MapCombo(await multi.ReadAsync<dynamic>());

        var estados = estadosFilas.Select(f =>
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

        return new EntidadesLoadCombosResult
        {
            CodTipoEstado = codTipoEstado,
            TiposPersonas = tiposPersonas,
            TiposDocumentoIdentidad = tiposDocumentoIdentidad,
            Paises = paises,
            Estados = estados,
            TiposMedioContacto = tiposMedioContacto,
            TiposDireccion = tiposDireccion
        };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem detectando la columna de código
    /// (primera que empiece con "COD") y la de descripción (primera "DES"/"NOMBRE").</summary>
    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas)
    {
        var resultado = new List<ComboboxItem>();
        foreach (var fila in filas)
        {
            var d = (IDictionary<string, object>)fila;
            var colId = d.Keys.FirstOrDefault(k => k.StartsWith("COD", StringComparison.OrdinalIgnoreCase));
            var colTexto = d.Keys.FirstOrDefault(k => k.StartsWith("DES", StringComparison.OrdinalIgnoreCase) || k.StartsWith("NOMBRE", StringComparison.OrdinalIgnoreCase));
            resultado.Add(new ComboboxItem
            {
                Id = colId is not null && d[colId] is not null ? d[colId].ToString() ?? "" : "",
                Texto = colTexto is not null && d[colTexto] is not null ? d[colTexto].ToString() ?? "" : ""
            });
        }
        return resultado;
    }

    private class TablaTipoEstado { public int COD_TIPO_ESTADO { get; set; } }

}
