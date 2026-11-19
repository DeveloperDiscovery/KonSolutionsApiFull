using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class PedidosComercialesComparativoService : IPedidosComercialesComparativoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PedidosComercialesComparativoService);
    private readonly IPedidosComercialesComparativoRepository _repo;
    private readonly ErrorLogService _log;
    public PedidosComercialesComparativoService(IPedidosComercialesComparativoRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<RequerimientoConPedidos>>> GetRequerimientosConPedidosAsync()
    {
        try { var d = (await _repo.RequerimientosConPedidosAsync()).ToList();
              return ApiResponse<IEnumerable<RequerimientoConPedidos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetRequerimientosConPedidosAsync));
              return ApiResponse<IEnumerable<RequerimientoConPedidos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ComparativoResultado>> GetComparativoAsync(string codClaseOperacion, string codMoneda, string requerimientos)
    {
        try
        {
            // Sin requerimientos no hay comparativo: se devuelve vacío en vez de ir a la
            // base a preguntar por nada.
            if (string.IsNullOrWhiteSpace(requerimientos))
                return ApiResponse<ComparativoResultado>.Ok(new ComparativoResultado());

            var d = await _repo.ComparativoAsync(codClaseOperacion, codMoneda, requerimientos);
            return ApiResponse<ComparativoResultado>.Ok(d, total: d.Lineas.Count);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(GetComparativoAsync));
            return ApiResponse<ComparativoResultado>.Fail(ex.Message);
        }
    }
}
