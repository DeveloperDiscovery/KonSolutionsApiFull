using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Administracion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AreasRepository : IAreasRepository
{
    private readonly IDbConnectionFactory _db;
    public AreasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Areas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Areas>(SP.SCHEMAAdministracion.Areas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Areas?> ShowbyidAsync(int COD_AREA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Areas>(SP.SCHEMAAdministracion.Areas.Showbyid, new { COD_AREA = COD_AREA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAAdministracion.Areas.Showcombox);
    }

    public async Task<IEnumerable<Areas>> SearchAsync(AreasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Areas>(SP.SCHEMAAdministracion.Areas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AreasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("DES_AREA", dto.DES_AREA);
        p.Add("COD_AREA_PADRE", dto.COD_AREA_PADRE);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_CENTRO_COSTO", dto.COD_CENTRO_COSTO);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAdministracion.Areas.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAAdministracion.Areas.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAdministracion.Areas.Insert, p)
            };
        }
    }

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(AreasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("DES_AREA", dto.DES_AREA);
        p.Add("COD_AREA_PADRE", dto.COD_AREA_PADRE);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_CENTRO_COSTO", dto.COD_CENTRO_COSTO);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAdministracion.Areas.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAAdministracion.Areas.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAdministracion.Areas.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(int COD_AREA)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_AREA", COD_AREA);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAdministracion.Areas.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAAdministracion.Areas.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAdministracion.Areas.Delete, p)
            };
        }
    }

    /// <summary>PROC_ADMINISTRACION_AREAS_LOADCOMBOS: sucursales + centros de costo (ya vienen
    /// aliados Id/Texto) y estados (COD_ESTADO/DES_ESTADO/colores) en un solo viaje.
    /// Si codTipoEstado &lt;= 0, se resuelve el de ADMINISTRACION.AREAS automáticamente
    /// (mismo mecanismo que EstadosPorTablaController), para que el cliente no tenga que saberlo.</summary>
    public async Task<AreasLoadCombosResult> LoadcombosAsync(int codTipoEstado)
    {
        using var conn = _db.CreateConnection();

        if (codTipoEstado <= 0)
        {
            var tabla = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
                "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SHOWBYID",
                new { DES_TABLA = "ADMINISTRACION.AREAS" },
                commandType: CommandType.StoredProcedure);
            codTipoEstado = tabla?.COD_TIPO_ESTADO ?? 0;
        }

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAAdministracion.Areas.Loadcombos,
            new { COD_TIPO_ESTADO = codTipoEstado },
            commandType: CommandType.StoredProcedure);

        // PROC_ESTANDAR_SUCURSALES_SHOWCOMBOX y PROC_CONTABILIDAD_CENTROS_COSTO_SHOWCOMBOX no
        // devuelven columnas literales "Id"/"Texto" (por eso el combo llegaba con filas vacías
        // al mapear directo a ComboboxItem) — se detecta la columna real por convención
        // COD_*/DES_* (la usada en todo el resto del sistema) en vez de asumir un nombre fijo.
        var sucursales   = MapCombo(await multi.ReadAsync<dynamic>());
        var centrosCosto = MapCombo(await multi.ReadAsync<dynamic>());
        var estadosFilas = await multi.ReadAsync<dynamic>();

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

        return new AreasLoadCombosResult { CodTipoEstado = codTipoEstado, Sucursales = sucursales, CentrosCosto = centrosCosto, Estados = estados };
    }

    /// <summary>PROC_ADMINISTRACION_AREAS_CARGOS_TREEVIEW: cargos activos del área — usado
    /// para poblar el combo "Cargo" al asignar cargos a un tipo de solicitud comercial.</summary>
    public async Task<IEnumerable<ComboboxItem>> GetCargosByAreaAsync(int codArea)
    {
        using var conn = _db.CreateConnection();
        var filas = await conn.QueryAsync<dynamic>(
            SP.SCHEMAAdministracion.Areas.TreeviewCargos,
            new { COD_AREA = codArea },
            commandType: CommandType.StoredProcedure);
        return MapCombo(filas);
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem detectando la columna de código (primera
    /// que empiece con "COD" o se llame "Id") y la de descripción (primera "DES"/"NOMBRE" o "Texto").</summary>
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

    private class TablaTipoEstado { public int COD_TIPO_ESTADO { get; set; } }

}