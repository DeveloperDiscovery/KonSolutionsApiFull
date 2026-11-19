using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEtiquetasBusquedaAreasRepository
{
    Task<IEnumerable<EtiquetasBusquedaAreas>> ShowallAsync();
    Task<EtiquetasBusquedaAreas?> ShowbyidAsync(string codEtiquetaBusqueda);
    Task<WriteResult> InsertAsync(EtiquetasBusquedaAreasInsertDto dto);
    Task<WriteResult> DeleteAsync(string codEtiquetaBusqueda);
}

public interface IEtiquetasBusquedaAreasService
{
    Task<ApiResponse<IEnumerable<EtiquetasBusquedaAreas>>> GetAllAsync();
    Task<ApiResponse<EtiquetasBusquedaAreas>> GetByIdAsync(string codEtiquetaBusqueda);
    Task<ApiResponse<WriteResult>> InsertAsync(EtiquetasBusquedaAreasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codEtiquetaBusqueda);
}
