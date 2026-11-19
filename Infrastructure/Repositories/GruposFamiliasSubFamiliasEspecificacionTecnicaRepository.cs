using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class GruposFamiliasSubFamiliasEspecificacionTecnicaRepository : IGruposFamiliasSubFamiliasEspecificacionTecnicaRepository
{
    private readonly IDbConnectionFactory _db;
    public GruposFamiliasSubFamiliasEspecificacionTecnicaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<GruposFamiliasSubFamiliasEspecificacionTecnica>> SearchAsync(GruposFamiliasSubFamiliasEspecificacionTecnicaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposFamiliasSubFamiliasEspecificacionTecnica>(
            SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasEspecificacionTecnica.Showall,
            new { dto.COD_GRUPO_PRODUCTO, dto.COD_FAMILIA_PRODUCTO, dto.COD_SUB_FAMILIA_PRODUCTO },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<GruposFamiliasSubFamiliasEspecificacionTecnicaTreeList>> TreelistAsync(GruposFamiliasSubFamiliasEspecificacionTecnicaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposFamiliasSubFamiliasEspecificacionTecnicaTreeList>(
            SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasEspecificacionTecnica.Treelist,
            new { dto.COD_GRUPO_PRODUCTO, dto.COD_FAMILIA_PRODUCTO, dto.COD_SUB_FAMILIA_PRODUCTO },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<GruposFamiliasSubFamiliasEspecificacionTecnica?> ShowbyidAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO, string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GruposFamiliasSubFamiliasEspecificacionTecnica>(
            SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasEspecificacionTecnica.Showbyid,
            new { COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO, COD_ESPECIFICACION_TECNICA },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(GruposFamiliasSubFamiliasEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_FAMILIA_PRODUCTO", dto.COD_FAMILIA_PRODUCTO);
        p.Add("COD_SUB_FAMILIA_PRODUCTO", dto.COD_SUB_FAMILIA_PRODUCTO);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        // Solo se asigna a nivel de Especificación Técnica completa, nunca a nivel de
        // Ítem puntual — COD_ITEM_TECNICO siempre se envía NULL.
        p.Add("COD_ITEM_TECNICO", (int?)null);
        p.Add("VAL_ESPECIFICACION_TENICA", dto.VAL_ESPECIFICACION_TENICA);
        p.Add("VAL_MINIMO", dto.VAL_MINIMO);
        p.Add("VAL_MAXIMO", dto.VAL_MAXIMO);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("FLG_INCLUIR_DESCRIPCION", dto.FLG_INCLUIR_DESCRIPCION ?? false);
        p.Add("FLG_REGISTRO_COMERCIAL", dto.FLG_REGISTRO_COMERCIAL ?? false);
        p.Add("FLG_FICHA_TECNICA", dto.FLG_FICHA_TECNICA ?? false);
        p.Add("FLG_CONTROL_CALIDAD", dto.FLG_CONTROL_CALIDAD ?? false);
        p.Add("COD_TIPO_REGISTRO_DATOS", dto.COD_TIPO_REGISTRO_DATOS);
        p.Add("COD_GRUPO_ESPECIFICACION_TECNICA", dto.COD_GRUPO_ESPECIFICACION_TECNICA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasEspecificacionTecnica.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasEspecificacionTecnica.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasEspecificacionTecnica.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO, string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasEspecificacionTecnica.Delete,
            new { COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO, COD_ESPECIFICACION_TECNICA },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasEspecificacionTecnica.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasEspecificacionTecnica.Delete, new { COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO, COD_ESPECIFICACION_TECNICA })
        };
    }
}

    /// <summary>PROC_INGENIERIA_GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA_LOADCOMBOS:
    /// Tipos de Registro de Datos + Grupos de Especificación Técnica + estados en un solo
    /// viaje. Si codTipoEstado &lt;= 0, se resuelve el de
    /// INGENIERIA.GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA automáticamente, igual
    /// que PROC_ADMINISTRACION_AREAS_LOADCOMBOS.</summary>
    public async Task<GruposFamiliasSubFamiliasEspecificacionTecnicaLoadCombosResult> LoadcombosAsync(int codTipoEstado)
    {
        using var conn = _db.CreateConnection();

        if (codTipoEstado <= 0)
        {
            var tabla = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
                "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SHOWBYID",
                new { DES_TABLA = "INGENIERIA.GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA" },
                commandType: CommandType.StoredProcedure);
            codTipoEstado = tabla?.COD_TIPO_ESTADO ?? 0;
        }

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasEspecificacionTecnica.Loadcombos,
            new { COD_TIPO_ESTADO = codTipoEstado },
            commandType: CommandType.StoredProcedure);

        var tiposRegistroDatos = MapCombo(await multi.ReadAsync<dynamic>());
        var gruposEspecificacion = MapCombo(await multi.ReadAsync<dynamic>());
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

        return new GruposFamiliasSubFamiliasEspecificacionTecnicaLoadCombosResult
        {
            CodTipoEstado = codTipoEstado,
            TiposRegistroDatos = tiposRegistroDatos,
            GruposEspecificacionTecnica = gruposEspecificacion,
            Estados = estados
        };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem por convención COD_*/DES_*/NOMBRE_* — sin
    /// reconocer alias literales "Id"/"Texto" (mismo criterio que ComboboxReader).</summary>
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
                Id    = colId is not null ? d[colId]?.ToString() ?? "" : "",
                Texto = colTexto is not null ? d[colTexto]?.ToString() ?? "" : ""
            });
        }
        return resultado;
    }

    private class TablaTipoEstado { public int COD_TIPO_ESTADO { get; set; } }
}
