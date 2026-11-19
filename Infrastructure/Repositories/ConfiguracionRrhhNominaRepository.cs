using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ConfiguracionRrhhNominaRepository : IConfiguracionRrhhNominaRepository
{
    private readonly IDbConnectionFactory _db;
    public ConfiguracionRrhhNominaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ConfiguracionRrhhNomina>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConfiguracionRrhhNomina>(SP.SCHEMARrhh.ConfiguracionRrhhNomina.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ConfiguracionRrhhNomina?> ShowbyidAsync(int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ConfiguracionRrhhNomina>(SP.SCHEMARrhh.ConfiguracionRrhhNomina.Showbyid, new { NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.ConfiguracionRrhhNomina.Showcombox);
    }

    public async Task<IEnumerable<ConfiguracionRrhhNomina>> SearchAsync(ConfiguracionRrhhNominaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConfiguracionRrhhNomina>(SP.SCHEMARrhh.ConfiguracionRrhhNomina.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ConfiguracionRrhhNominaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_CONCEPTO_REMUNERATIVO_VACACIONES", dto.COD_CONCEPTO_REMUNERATIVO_VACACIONES);
        p.Add("COD_CONCEPTO_REMUNERATIVO_GRATIFICACIONES", dto.COD_CONCEPTO_REMUNERATIVO_GRATIFICACIONES);
        p.Add("COD_CONCEPTO_REMUNERATIVO_CTS", dto.COD_CONCEPTO_REMUNERATIVO_CTS);
        p.Add("COD_CONCEPTO_REMUNERATIVO_UTILIDADADES", dto.COD_CONCEPTO_REMUNERATIVO_UTILIDADADES);
        p.Add("COD_CONCEPTO_REMUNERATIVO_SOLICITUDES", dto.COD_CONCEPTO_REMUNERATIVO_SOLICITUDES);
        p.Add("COD_TIPO_REMUNERATIVO_SOLICTUDES", dto.COD_TIPO_REMUNERATIVO_SOLICTUDES);
        p.Add("COD_TIPO_ENTIDAD_POSTULANTE", dto.COD_TIPO_ENTIDAD_POSTULANTE);
        p.Add("COD_TIPO_ENTIDAD_ACADEMICA", dto.COD_TIPO_ENTIDAD_ACADEMICA);
        p.Add("COD_REGIMEN_LABORAL", dto.COD_REGIMEN_LABORAL);
        p.Add("CAN_DIAS_ALERTA_VENCIMIENTO_CONTRATOS", dto.CAN_DIAS_ALERTA_VENCIMIENTO_CONTRATOS);
        p.Add("CAN_DIAS_ALERTA_VENCIMIENTO_VACACIONES", dto.CAN_DIAS_ALERTA_VENCIMIENTO_VACACIONES);
        p.Add("COD_GRUPO_PRODUCTO_IMPLEMENTO_LABORAL", dto.COD_GRUPO_PRODUCTO_IMPLEMENTO_LABORAL);
        p.Add("COD_FAMILIA_PRODUCTO_IMPLEMENTO_LABORAL", dto.COD_FAMILIA_PRODUCTO_IMPLEMENTO_LABORAL);
        p.Add("FLG_PREFIJO_ID", dto.FLG_PREFIJO_ID ?? false);
        p.Add("CAN_LONGITUD_ID", dto.CAN_LONGITUD_ID);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ConfiguracionRrhhNomina.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ConfiguracionRrhhNomina.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ConfiguracionRrhhNomina.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ConfiguracionRrhhNominaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_CONCEPTO_REMUNERATIVO_VACACIONES", dto.COD_CONCEPTO_REMUNERATIVO_VACACIONES);
        p.Add("COD_CONCEPTO_REMUNERATIVO_GRATIFICACIONES", dto.COD_CONCEPTO_REMUNERATIVO_GRATIFICACIONES);
        p.Add("COD_CONCEPTO_REMUNERATIVO_CTS", dto.COD_CONCEPTO_REMUNERATIVO_CTS);
        p.Add("COD_CONCEPTO_REMUNERATIVO_UTILIDADADES", dto.COD_CONCEPTO_REMUNERATIVO_UTILIDADADES);
        p.Add("COD_CONCEPTO_REMUNERATIVO_SOLICITUDES", dto.COD_CONCEPTO_REMUNERATIVO_SOLICITUDES);
        p.Add("COD_TIPO_REMUNERATIVO_SOLICTUDES", dto.COD_TIPO_REMUNERATIVO_SOLICTUDES);
        p.Add("COD_TIPO_ENTIDAD_POSTULANTE", dto.COD_TIPO_ENTIDAD_POSTULANTE);
        p.Add("COD_TIPO_ENTIDAD_ACADEMICA", dto.COD_TIPO_ENTIDAD_ACADEMICA);
        p.Add("COD_REGIMEN_LABORAL", dto.COD_REGIMEN_LABORAL);
        p.Add("CAN_DIAS_ALERTA_VENCIMIENTO_CONTRATOS", dto.CAN_DIAS_ALERTA_VENCIMIENTO_CONTRATOS);
        p.Add("CAN_DIAS_ALERTA_VENCIMIENTO_VACACIONES", dto.CAN_DIAS_ALERTA_VENCIMIENTO_VACACIONES);
        p.Add("COD_GRUPO_PRODUCTO_IMPLEMENTO_LABORAL", dto.COD_GRUPO_PRODUCTO_IMPLEMENTO_LABORAL);
        p.Add("COD_FAMILIA_PRODUCTO_IMPLEMENTO_LABORAL", dto.COD_FAMILIA_PRODUCTO_IMPLEMENTO_LABORAL);
        p.Add("FLG_PREFIJO_ID", dto.FLG_PREFIJO_ID ?? false);
        p.Add("CAN_LONGITUD_ID", dto.CAN_LONGITUD_ID);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ConfiguracionRrhhNomina.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ConfiguracionRrhhNomina.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ConfiguracionRrhhNomina.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ConfiguracionRrhhNomina.Delete, new { NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ConfiguracionRrhhNomina.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ConfiguracionRrhhNomina.Delete, new { NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}