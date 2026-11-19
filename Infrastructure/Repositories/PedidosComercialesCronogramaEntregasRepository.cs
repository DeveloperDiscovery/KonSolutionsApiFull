using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PedidosComercialesCronogramaEntregasRepository : IPedidosComercialesCronogramaEntregasRepository
{
    private readonly IDbConnectionFactory _db;
    public PedidosComercialesCronogramaEntregasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PedidoComercialCronogramaEntrega>> ShowallPedidoAsync(
        string codTipoOperacion, string codClaseOperacion, decimal numPedidoComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidoComercialCronogramaEntrega>(
            SP.SCHEMALogistica.PedidosComercialesCronogramaEntregas.ShowallPedido,
            new
            {
                COD_TIPO_OPERACION   = codTipoOperacion,
                COD_CLASE_OPERACION  = codClaseOperacion,
                NUM_PEDIDO_COMERCIAL = numPedidoComercial
            },
            commandType: CommandType.StoredProcedure);
    }

    public Task<WriteResult> InsertAsync(PedidoComercialCronogramaEntregaDto dto)
        => EjecutarAsync(SP.SCHEMALogistica.PedidosComercialesCronogramaEntregas.Insert, Params(dto));

    public Task<WriteResult> UpdateAsync(PedidoComercialCronogramaEntregaDto dto)
        => EjecutarAsync(SP.SCHEMALogistica.PedidosComercialesCronogramaEntregas.Update, Params(dto));

    public Task<WriteResult> DeleteAsync(string codTipoOperacion, string codClaseOperacion,
                                         decimal numPedidoComercial, int numSecuencia, DateTime fecEntrega)
    {
        var p = new DynamicParameters();
        p.Add("COD_TIPO_OPERACION", codTipoOperacion);
        p.Add("COD_CLASE_OPERACION", codClaseOperacion);
        p.Add("NUM_PEDIDO_COMERCIAL", numPedidoComercial);
        p.Add("NUM_SECUENCIA", numSecuencia);
        p.Add("FEC_ENTREGA", fecEntrega.Date, DbType.Date);
        return EjecutarAsync(SP.SCHEMALogistica.PedidosComercialesCronogramaEntregas.Delete, p);
    }

    /// <summary>FEC_ENTREGA forma parte de la CLAVE y en la tabla es "date", sin hora. Se manda
    /// con DbType.Date y .Date explícito: si viajara con hora, un mismo día podría no coincidir
    /// con la fila existente y el UPDATE no encontraría nada.</summary>
    private static DynamicParameters Params(PedidoComercialCronogramaEntregaDto d)
    {
        var p = new DynamicParameters();
        p.Add("COD_TIPO_OPERACION", d.COD_TIPO_OPERACION);
        p.Add("COD_CLASE_OPERACION", d.COD_CLASE_OPERACION);
        p.Add("NUM_PEDIDO_COMERCIAL", d.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA", d.NUM_SECUENCIA);
        p.Add("FEC_ENTREGA", d.FEC_ENTREGA.Date, DbType.Date);
        p.Add("FEC_ENTREGA_REALIZADA", d.FEC_ENTREGA_REALIZADA?.Date, DbType.Date);
        p.Add("NUM_MOVIMIENTO_ALMACEN", d.NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_SECUENCIA_MOVIMIENTO", d.NUM_SECUENCIA_MOVIMIENTO);
        p.Add("CAN_PRODUCTO_ENTREGAR", d.CAN_PRODUCTO_ENTREGAR);
        p.Add("CAN_PRODUCTO_ENTREGADO", d.CAN_PRODUCTO_ENTREGADO);
        return p;
    }

    /// <summary>Los tres SP de escritura no devuelven resultset: se usa ExecuteAsync y el
    /// WriteResult se arma acá. La traducción de SqlException es lo que hace que el mensaje
    /// del RAISERROR —incluido el de "supera la cantidad solicitada"— llegue a la pantalla.</summary>
    private async Task<WriteResult> EjecutarAsync(string sp, DynamicParameters p)
    {
        using var conn = _db.CreateConnection();
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
