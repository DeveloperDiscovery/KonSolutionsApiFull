using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class ProductosEspecificacionesTecnicasService : IProductosEspecificacionesTecnicasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProductosEspecificacionesTecnicasService);
    private readonly IProductosEspecificacionesTecnicasRepository _repo;
    private readonly ErrorLogService _log;
    public ProductosEspecificacionesTecnicasService(IProductosEspecificacionesTecnicasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ProductosEspecificacionesTecnicas>>> GetAllAsync(string codProducto)
    {
        try { var d = (await _repo.ShowallAsync(codProducto)).ToList();
              return ApiResponse<IEnumerable<ProductosEspecificacionesTecnicas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ProductosEspecificacionesTecnicas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ProductosEspecificacionesTecnicas>> GetByIdAsync(string COD_PRODUCTO, string COD_ESPECIFICACION_TECNICA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_PRODUCTO, COD_ESPECIFICACION_TECNICA);
              return d is null ? ApiResponse<ProductosEspecificacionesTecnicas>.Fail("No encontrado.") : ApiResponse<ProductosEspecificacionesTecnicas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ProductosEspecificacionesTecnicas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ProductosEspecificacionesTecnicas>>> SearchAsync(ProductosEspecificacionesTecnicasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ProductosEspecificacionesTecnicas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ProductosEspecificacionesTecnicas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ProductosEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PRODUCTO, string COD_ESPECIFICACION_TECNICA)
    {
        try { var r = await _repo.DeleteAsync(COD_PRODUCTO, COD_ESPECIFICACION_TECNICA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
