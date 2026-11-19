using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPedidosComercialesCronogramaEntregasRepository
{
    /// <summary>Cronograma COMPLETO del pedido, de todas sus líneas, en una sola consulta.
    /// El _SHOWALL de la tabla exige @NUM_SECUENCIA y devuelve una línea por vez: para pintar
    /// la pestaña de una orden de diez líneas harían falta diez llamadas.</summary>
    Task<IEnumerable<PedidoComercialCronogramaEntrega>> ShowallPedidoAsync(string codTipoOperacion, string codClaseOperacion, decimal numPedidoComercial);

    Task<WriteResult> InsertAsync(PedidoComercialCronogramaEntregaDto dto);
    Task<WriteResult> UpdateAsync(PedidoComercialCronogramaEntregaDto dto);
    Task<WriteResult> DeleteAsync(string codTipoOperacion, string codClaseOperacion, decimal numPedidoComercial, int numSecuencia, DateTime fecEntrega);
}

public interface IPedidosComercialesCronogramaEntregasService
{
    Task<ApiResponse<IEnumerable<PedidoComercialCronogramaEntrega>>> GetPorPedidoAsync(string codTipoOperacion, string codClaseOperacion, decimal numPedidoComercial);
    Task<ApiResponse<WriteResult>> InsertAsync(PedidoComercialCronogramaEntregaDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(PedidoComercialCronogramaEntregaDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codTipoOperacion, string codClaseOperacion, decimal numPedidoComercial, int numSecuencia, DateTime fecEntrega);
}
