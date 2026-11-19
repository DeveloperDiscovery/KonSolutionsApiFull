using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPedidosComercialesRepository
{
    Task<IEnumerable<PedidosComerciales>> ShowallAsync(string codClaseOperacion, string codTipoOperacion);
    Task<PedidosComerciales?> ShowbyidAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<IEnumerable<PedidosComerciales>> SearchAsync(PedidosComercialesSearchDto dto);
    Task<WriteResult> InsertAsync(PedidosComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PedidosComercialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<PedidosComercialesLoadCombosResult> LoadcombosAsync(string codClaseOperacion);
    Task<IEnumerable<PedidoComercialAtencion>> AtencionAsync(string codClaseOperacion, string? codTipoOperacion);
}

public interface IPedidosComercialesService
{
    Task<ApiResponse<IEnumerable<PedidosComerciales>>> GetAllAsync(string codClaseOperacion, string codTipoOperacion);
    Task<ApiResponse<PedidosComerciales>> GetByIdAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<ApiResponse<IEnumerable<PedidosComerciales>>> SearchAsync(PedidosComercialesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PedidosComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PedidosComercialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<ApiResponse<PedidosComercialesLoadCombosResult>> LoadcombosAsync(string codClaseOperacion);
    Task<ApiResponse<IEnumerable<PedidoComercialAtencion>>> AtencionAsync(string codClaseOperacion, string? codTipoOperacion);
}
