using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPedidosComercialesEstadosRepository
{
    Task<IEnumerable<PedidosComercialesEstados>> ShowallAsync(string codClaseOperacion, string codTipoOperacion, int numPedidoComercial);
}

public interface IPedidosComercialesEstadosService
{
    Task<ApiResponse<IEnumerable<PedidosComercialesEstados>>> GetAllAsync(string codClaseOperacion, string codTipoOperacion, int numPedidoComercial);
}
