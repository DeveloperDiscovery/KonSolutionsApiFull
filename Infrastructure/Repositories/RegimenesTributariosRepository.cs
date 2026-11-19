using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Sunat;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RegimenesTributariosRepository : IRegimenesTributariosRepository
{
    private readonly IDbConnectionFactory _db;
    public RegimenesTributariosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RegimenesTributarios>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegimenesTributarios>(SP.SCHEMASunat.RegimenesTributarios.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<RegimenesTributarios?> ShowbyidAsync(int COD_REGIMEN_TRIBUTARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RegimenesTributarios>(SP.SCHEMASunat.RegimenesTributarios.Showbyid, new { COD_REGIMEN_TRIBUTARIO = COD_REGIMEN_TRIBUTARIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASunat.RegimenesTributarios.Showcombox);
    }

    public async Task<IEnumerable<RegimenesTributarios>> SearchAsync(RegimenesTributariosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegimenesTributarios>(SP.SCHEMASunat.RegimenesTributarios.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RegimenesTributariosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_REGIMEN_TRIBUTARIO", dto.COD_REGIMEN_TRIBUTARIO);
        p.Add("DES_REGIMEN_TRIBUTARIO", dto.DES_REGIMEN_TRIBUTARIO);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("FLG_RETENCION_TRIBUTO", dto.FLG_RETENCION_TRIBUTO ?? false);
        p.Add("FLG_DEPOSITO_CUENTA", dto.FLG_DEPOSITO_CUENTA ?? false);
        p.Add("FLG_PERIODO", dto.FLG_PERIODO ?? false);
        p.Add("FLG_INCREMENTA", dto.FLG_INCREMENTA ?? false);
        p.Add("FLG_DEPENDE_PRODUCTO", dto.FLG_DEPENDE_PRODUCTO ?? false);
        p.Add("FLG_RESTRICCION", dto.FLG_RESTRICCION ?? false);
        p.Add("FLG_PROVISION", dto.FLG_PROVISION ?? false);
        p.Add("NUM_PRIORIDAD", dto.NUM_PRIORIDAD);
        p.Add("NUM_DECIMALES_REDONDEO", dto.NUM_DECIMALES_REDONDEO);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("NUM_DIAS", dto.NUM_DIAS);
        p.Add("VAL_MINIMO", dto.VAL_MINIMO);
        p.Add("TAS_REGIMEN_TRIBUTARIO", dto.TAS_REGIMEN_TRIBUTARIO);
        p.Add("COD_TIPO_IMPUESTO", dto.COD_TIPO_IMPUESTO);
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASunat.RegimenesTributarios.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASunat.RegimenesTributarios.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASunat.RegimenesTributarios.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(RegimenesTributariosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_REGIMEN_TRIBUTARIO", dto.COD_REGIMEN_TRIBUTARIO);
        p.Add("DES_REGIMEN_TRIBUTARIO", dto.DES_REGIMEN_TRIBUTARIO);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("FLG_RETENCION_TRIBUTO", dto.FLG_RETENCION_TRIBUTO ?? false);
        p.Add("FLG_DEPOSITO_CUENTA", dto.FLG_DEPOSITO_CUENTA ?? false);
        p.Add("FLG_PERIODO", dto.FLG_PERIODO ?? false);
        p.Add("FLG_INCREMENTA", dto.FLG_INCREMENTA ?? false);
        p.Add("FLG_DEPENDE_PRODUCTO", dto.FLG_DEPENDE_PRODUCTO ?? false);
        p.Add("FLG_RESTRICCION", dto.FLG_RESTRICCION ?? false);
        p.Add("FLG_PROVISION", dto.FLG_PROVISION ?? false);
        p.Add("NUM_PRIORIDAD", dto.NUM_PRIORIDAD);
        p.Add("NUM_DECIMALES_REDONDEO", dto.NUM_DECIMALES_REDONDEO);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("NUM_DIAS", dto.NUM_DIAS);
        p.Add("VAL_MINIMO", dto.VAL_MINIMO);
        p.Add("TAS_REGIMEN_TRIBUTARIO", dto.TAS_REGIMEN_TRIBUTARIO);
        p.Add("COD_TIPO_IMPUESTO", dto.COD_TIPO_IMPUESTO);
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASunat.RegimenesTributarios.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASunat.RegimenesTributarios.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASunat.RegimenesTributarios.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_REGIMEN_TRIBUTARIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASunat.RegimenesTributarios.Delete, new { COD_REGIMEN_TRIBUTARIO = COD_REGIMEN_TRIBUTARIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASunat.RegimenesTributarios.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASunat.RegimenesTributarios.Delete, new { COD_REGIMEN_TRIBUTARIO = COD_REGIMEN_TRIBUTARIO })
        };
    }
}

    /// <summary>PROC_SUNAT_REGIMENES_TRIBUTARIOS_LOADCOMBOS: tipos de comprobante de pago,
    /// monedas, tipos de impuesto y tipos de valor de cálculo en un solo viaje.</summary>
    public async Task<RegimenesTributariosLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMASunat.RegimenesTributarios.Loadcombos,
            commandType: CommandType.StoredProcedure);

        var tiposComprobantePago = MapCombo(await multi.ReadAsync<dynamic>());
        var monedas              = MapCombo(await multi.ReadAsync<dynamic>());
        var tiposImpuesto        = MapCombo(await multi.ReadAsync<dynamic>());
        var tiposValorCalculo    = MapCombo(await multi.ReadAsync<dynamic>());

        return new RegimenesTributariosLoadCombosResult
        {
            TiposComprobantePago = tiposComprobantePago,
            Monedas = monedas,
            TiposImpuesto = tiposImpuesto,
            TiposValorCalculo = tiposValorCalculo
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

}