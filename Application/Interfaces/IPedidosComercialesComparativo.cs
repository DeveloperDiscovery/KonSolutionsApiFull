using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPedidosComercialesComparativoRepository
{
    Task<IEnumerable<RequerimientoConPedidos>> RequerimientosConPedidosAsync();
    Task<ComparativoResultado> ComparativoAsync(string codClaseOperacion, string codMoneda, string requerimientos);
}

public interface IPedidosComercialesComparativoService
{
    Task<ApiResponse<IEnumerable<RequerimientoConPedidos>>> GetRequerimientosConPedidosAsync();
    Task<ApiResponse<ComparativoResultado>> GetComparativoAsync(string codClaseOperacion, string codMoneda, string requerimientos);
}
