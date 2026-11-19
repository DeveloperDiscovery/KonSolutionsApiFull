using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PerfilesUsuarioRepository : IPerfilesUsuarioRepository
{
    private readonly IDbConnectionFactory _db;
    public PerfilesUsuarioRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PerfilesUsuario>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PerfilesUsuario>(SP.SCHEMASeguridad.PerfilesUsuario.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PerfilesUsuario?> ShowbyidAsync(string COD_PERFIL_USUARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PerfilesUsuario>(SP.SCHEMASeguridad.PerfilesUsuario.Showbyid, new { COD_PERFIL_USUARIO = COD_PERFIL_USUARIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.PerfilesUsuario.Showcombox);
    }

    /// <summary>PROC_SEGURIDAD_PERFILES_USUARIO_LOADCOMBOS: complejidades de contraseña +
    /// estados (COD_ESTADO/DES_ESTADO/colores) en un solo viaje. Si codTipoEstado &lt;= 0, se
    /// resuelve el de SEGURIDAD.PERFILES_USUARIO automáticamente (mismo mecanismo que
    /// EstadosPorTablaController), igual que PROC_ADMINISTRACION_AREAS_LOADCOMBOS.</summary>
    public async Task<PerfilesUsuarioLoadCombosResult> LoadcombosAsync(int codTipoEstado)
    {
        using var conn = _db.CreateConnection();

        if (codTipoEstado <= 0)
        {
            var tabla = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
                "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SHOWBYID",
                new { DES_TABLA = "SEGURIDAD.PERFILES_USUARIO" },
                commandType: CommandType.StoredProcedure);
            codTipoEstado = tabla?.COD_TIPO_ESTADO ?? 0;
        }

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMASeguridad.PerfilesUsuario.Loadcombos,
            new { COD_TIPO_ESTADO = codTipoEstado },
            commandType: CommandType.StoredProcedure);

        var complejidades = MapCombo(await multi.ReadAsync<dynamic>());
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

        return new PerfilesUsuarioLoadCombosResult { CodTipoEstado = codTipoEstado, ComplejidadesPassword = complejidades, Estados = estados };
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

    public async Task<IEnumerable<PerfilesUsuario>> SearchAsync(PerfilesUsuarioSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PerfilesUsuario>(SP.SCHEMASeguridad.PerfilesUsuario.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PerfilesUsuarioInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PERFIL_USUARIO", dto.COD_PERFIL_USUARIO);
        p.Add("DES_PERFIL_USUARIO", dto.DES_PERFIL_USUARIO);
        p.Add("COD_PASSWORD_COMPLEJIDAD", dto.COD_PASSWORD_COMPLEJIDAD);
        p.Add("FLG_DEVELOPER", dto.FLG_DEVELOPER ?? false);
        p.Add("FLG_ADMINISTRADOR", dto.FLG_ADMINISTRADOR ?? false);
        p.Add("FLG_SEGURIDAD", dto.FLG_SEGURIDAD ?? false);
        p.Add("FLG_AUDITORIA", dto.FLG_AUDITORIA ?? false);
        p.Add("FLG_COMERCIAL", dto.FLG_COMERCIAL ?? false);
        p.Add("FLG_DEPENDENCIA_COMERCIAL", dto.FLG_DEPENDENCIA_COMERCIAL ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("IMG_ICONO", dto.IMG_ICONO, DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.PerfilesUsuario.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.PerfilesUsuario.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.PerfilesUsuario.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PerfilesUsuarioUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PERFIL_USUARIO", dto.COD_PERFIL_USUARIO);
        p.Add("DES_PERFIL_USUARIO", dto.DES_PERFIL_USUARIO);
        p.Add("COD_PASSWORD_COMPLEJIDAD", dto.COD_PASSWORD_COMPLEJIDAD);
        p.Add("FLG_DEVELOPER", dto.FLG_DEVELOPER ?? false);
        p.Add("FLG_ADMINISTRADOR", dto.FLG_ADMINISTRADOR ?? false);
        p.Add("FLG_SEGURIDAD", dto.FLG_SEGURIDAD ?? false);
        p.Add("FLG_AUDITORIA", dto.FLG_AUDITORIA ?? false);
        p.Add("FLG_COMERCIAL", dto.FLG_COMERCIAL ?? false);
        p.Add("FLG_DEPENDENCIA_COMERCIAL", dto.FLG_DEPENDENCIA_COMERCIAL ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("IMG_ICONO", dto.IMG_ICONO, DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.PerfilesUsuario.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.PerfilesUsuario.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.PerfilesUsuario.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PERFIL_USUARIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.PerfilesUsuario.Delete, new { COD_PERFIL_USUARIO = COD_PERFIL_USUARIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.PerfilesUsuario.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.PerfilesUsuario.Delete, new { COD_PERFIL_USUARIO = COD_PERFIL_USUARIO })
        };
    }
}

}