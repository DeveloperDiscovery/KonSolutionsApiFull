using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ConfiguracionEmisionElectronicaRepository : IConfiguracionEmisionElectronicaRepository
{
    private readonly IDbConnectionFactory _db;
    public ConfiguracionEmisionElectronicaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<ConfiguracionEmisionElectronicaRegistro?> ShowbyidAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ConfiguracionEmisionElectronicaRegistro>(
            SP.SCHEMAEstandar.ConfiguracionEmisionElectronica.Showbyid, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> UpdateAsync(ConfiguracionEmisionElectronicaRegistro n, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var sp = SP.SCHEMAEstandar.ConfiguracionEmisionElectronica.Update;
        var p = new DynamicParameters();
        p.Add("COD_AMBIENTE", n.COD_AMBIENTE);
        p.Add("FLG_ACTIVA", n.FLG_ACTIVA);
        p.Add("DES_URL_SEGURIDAD", n.DES_URL_SEGURIDAD);
        p.Add("DES_URL_API", n.DES_URL_API);
        p.Add("DES_CLIENT_ID", n.DES_CLIENT_ID);
        p.Add("DES_USUARIO_SOL", n.DES_USUARIO_SOL);
        // Un binario nulo hay que declararlo: sin DbType, el nulo viaja como otro tipo y el SP lo rechaza.
        p.Add("ENC_CLIENT_SECRET", n.ENC_CLIENT_SECRET, DbType.Binary, size: 1024);
        p.Add("ENC_CLAVE_SOL", n.ENC_CLAVE_SOL, DbType.Binary, size: 1024);
        p.Add("NOM_ARCHIVO_CERTIFICADO", n.NOM_ARCHIVO_CERTIFICADO);
        p.Add("ENC_CERTIFICADO", n.ENC_CERTIFICADO, DbType.Binary, size: -1);
        p.Add("ENC_CLAVE_CERTIFICADO", n.ENC_CLAVE_CERTIFICADO, DbType.Binary, size: 1024);
        p.Add("DES_SUJETO_CERTIFICADO", n.DES_SUJETO_CERTIFICADO);
        p.Add("DES_HUELLA_CERTIFICADO", n.DES_HUELLA_CERTIFICADO);
        p.Add("FEC_VENCIMIENTO_CERTIFICADO", n.FEC_VENCIMIENTO_CERTIFICADO, DbType.DateTime);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(sp, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            // SIN SqlSentence: acá viajan secretos cifrados y el certificado, y esa sentencia se
            // devuelve al cliente y se escribe en el log de errores.
            return new WriteResult { Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number, ErrorProcedure = sp };
        }
    }
}
