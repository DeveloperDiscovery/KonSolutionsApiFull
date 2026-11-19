using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PedidosComercialesCalculoRepository : IPedidosComercialesCalculoRepository
{
    private readonly IDbConnectionFactory _db;
    public PedidosComercialesCalculoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PedidosComercialesCalculo>> ShowallAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosComercialesCalculo>(
            SP.SCHEMAComercial.PedidosComercialesCalculo.Showall,
            new
            {
                COD_CLASE_OPERACION  = codClaseOperacion,
                COD_TIPO_OPERACION   = codTipoOperacion,
                NUM_PEDIDO_COMERCIAL = numPedidoComercial
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<PedidosComercialesCalculo?> ShowbyidAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_VALOR_CALCULO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PedidosComercialesCalculo>(SP.SCHEMAComercial.PedidosComercialesCalculo.Showbyid, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, COD_TIPO_VALOR_CALCULO = COD_TIPO_VALOR_CALCULO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.PedidosComercialesCalculo.Showcombox);
    }

    public async Task<IEnumerable<PedidosComercialesCalculo>> SearchAsync(PedidosComercialesCalculoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosComercialesCalculo>(SP.SCHEMAComercial.PedidosComercialesCalculo.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PedidosComercialesCalculoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("COD_TIPO_IMPUESTO", dto.COD_TIPO_IMPUESTO);
        p.Add("TAS_IMPUESTO", dto.TAS_IMPUESTO);
        p.Add("VAL_CALCULO", dto.VAL_CALCULO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComercialesCalculo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.PedidosComercialesCalculo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComercialesCalculo.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PedidosComercialesCalculoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("COD_TIPO_IMPUESTO", dto.COD_TIPO_IMPUESTO);
        p.Add("TAS_IMPUESTO", dto.TAS_IMPUESTO);
        p.Add("VAL_CALCULO", dto.VAL_CALCULO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComercialesCalculo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.PedidosComercialesCalculo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComercialesCalculo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_VALOR_CALCULO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComercialesCalculo.Delete, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, COD_TIPO_VALOR_CALCULO = COD_TIPO_VALOR_CALCULO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.PedidosComercialesCalculo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComercialesCalculo.Delete, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, COD_TIPO_VALOR_CALCULO = COD_TIPO_VALOR_CALCULO })
        };
    }
}

    /// <summary>Ejecuta PROC_COMERCIAL_PEDIDOS_COMERCIALES_CALCULA. NO es una lectura: borra
    /// y reescribe PEDIDOS_COMERCIALES_CALCULO y actualiza VAL_TOTAL de la cabecera.
    ///
    /// El SP termina con un SELECT de la tabla de trabajo, que acá no interesa: lo que vale
    /// es el efecto. Se usa ExecuteAsync y ese resultset se descarta. Si el SP falla hace
    /// ROLLBACK y THROW, así que el error llega como SqlException y se devuelve igual que en
    /// el resto del repositorio.</summary>
    public async Task<WriteResult> CalcularAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", codClaseOperacion);
        p.Add("COD_TIPO_OPERACION", codTipoOperacion);
        p.Add("NUM_PEDIDO_COMERCIAL", numPedidoComercial);
        try
        {
            var filas = await conn.ExecuteAsync(SP.SCHEMAComercial.PedidosComercialesCalculo.Calcula, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = filas };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.PedidosComercialesCalculo.Calcula,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComercialesCalculo.Calcula, p)
            };
        }
    }

    /// <summary>Ejecuta PROC_COMERCIAL_PEDIDO_COMERCIAL_SINCRONIZA: convierte el pedido
    /// elegido en el comparativo (ej. una Proforma) en un pedido de la clase/tipo
    /// "sincronizada" configurada en ESTANDAR.TIPOS_OPERACION (ej. Orden de Compra). Escribe
    /// — crea un pedido nuevo y actualiza estados — igual que CalcularAsync arriba.</summary>
    public async Task<WriteResult> SincronizarAsync(string codClaseOperacion, string codTipoOperacion,
        decimal numPedidoComercial, string codUsuarioRegistro, string codEstacionRegistro)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", codClaseOperacion);
        p.Add("COD_TIPO_OPERACION", codTipoOperacion);
        p.Add("NUM_PEDIDO_COMERCIAL", numPedidoComercial);
        p.Add("COD_USUARIO_REGISTRO", codUsuarioRegistro);
        p.Add("COD_ESTACION_REGISTRO", codEstacionRegistro);
        try
        {
            var filas = await conn.ExecuteAsync(SP.SCHEMAComercial.PedidosComercialesCalculo.Sincroniza, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = filas };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.PedidosComercialesCalculo.Sincroniza,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComercialesCalculo.Sincroniza, p)
            };
        }
    }

    /// <summary>Ejecuta PROC_COMERCIAL_PEDIDO_COMERCIAL_REVERTIR sobre la ORDEN DE COMPRA
    /// (el pedido que creó SINCRONIZA): anula la orden y devuelve a su estado anterior la
    /// proforma de origen, las proformas rechazadas y los requerimientos.</summary>
    public Task<WriteResult> RevertirAsync(string codClaseOperacion, string codTipoOperacion,
        decimal numPedidoComercial, string codUsuarioRegistro, string codEstacionRegistro)
        => EjecutarReversionAsync(SP.SCHEMAComercial.PedidosComercialesCalculo.Revertir,
            codClaseOperacion, codTipoOperacion, numPedidoComercial, codUsuarioRegistro, codEstacionRegistro);

    /// <summary>Ejecuta PROC_COMERCIAL_PEDIDO_COMERCIAL_PROFORMA_REVERTIR con la clave de la
    /// PROFORMA: el SP ubica la orden que salió de ella y la revierte.</summary>
    public Task<WriteResult> RevertirProformaAsync(string codClaseOperacion, string codTipoOperacion,
        decimal numPedidoComercial, string codUsuarioRegistro, string codEstacionRegistro)
        => EjecutarReversionAsync(SP.SCHEMAComercial.PedidosComercialesCalculo.ProformaRevertir,
            codClaseOperacion, codTipoOperacion, numPedidoComercial, codUsuarioRegistro, codEstacionRegistro);

    private async Task<WriteResult> EjecutarReversionAsync(string sp, string codClaseOperacion, string codTipoOperacion,
        decimal numPedidoComercial, string codUsuarioRegistro, string codEstacionRegistro)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", codClaseOperacion);
        p.Add("COD_TIPO_OPERACION", codTipoOperacion);
        p.Add("NUM_PEDIDO_COMERCIAL", numPedidoComercial);
        p.Add("COD_USUARIO_REGISTRO", codUsuarioRegistro);
        p.Add("COD_ESTACION_REGISTRO", codEstacionRegistro);
        try
        {
            var filas = await conn.ExecuteAsync(sp, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = filas };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = sp, SqlSentence = SqlSentenceHelper.Build(sp, p)
            };
        }
    }
}
