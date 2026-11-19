using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPedidosComercialesDetalleRepository
{
    Task<IEnumerable<PedidosComercialesDetalle>> ShowallAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<PedidosComercialesDetalle?> ShowbyidAsync(decimal numPedidoComercial, int numSecuencia);
    Task<IEnumerable<PedidosComercialesDetalle>> SearchAsync(PedidosComercialesDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(PedidosComercialesDetalleInsertDto dto);
    Task<WriteResult> UpdateAsync(PedidosComercialesDetalleUpdateDto dto);
    Task<WriteResult> DeleteAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia);
}

public interface IPedidosComercialesDetalleService
{
    Task<ApiResponse<IEnumerable<PedidosComercialesDetalle>>> GetAllAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<ApiResponse<PedidosComercialesDetalle>> GetByIdAsync(decimal numPedidoComercial, int numSecuencia);
    Task<ApiResponse<IEnumerable<PedidosComercialesDetalle>>> SearchAsync(PedidosComercialesDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PedidosComercialesDetalleInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(PedidosComercialesDetalleUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, int numSecuencia);
}
