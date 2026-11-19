using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
namespace KonSolutionsApi.Application.Interfaces;

public interface ITablasTiposEstadoRepository
{
    Task<IEnumerable<TablasTiposEstado>> ShowallAsync();
    Task<TablasTiposEstado?> ShowbyidAsync(string DES_TABLA);
    Task<IEnumerable<TablasTiposEstado>> SearchAsync(TablasTiposEstadoSearchDto dto);
    Task<WriteResult> InsertAsync(TablasTiposEstadoInsertDto dto);
    Task<WriteResult> DeleteAsync(string DES_TABLA);
    Task<IEnumerable<TablaDisponible>> TablasDisponiblesAsync();
}

public interface ITablasTiposEstadoService
{
    Task<ApiResponse<IEnumerable<TablasTiposEstado>>> GetAllAsync();
    Task<ApiResponse<TablasTiposEstado>> GetByIdAsync(string DES_TABLA);
    Task<ApiResponse<IEnumerable<TablasTiposEstado>>> SearchAsync(TablasTiposEstadoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TablasTiposEstadoInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string DES_TABLA);
    Task<ApiResponse<IEnumerable<TablaDisponible>>> TablasDisponiblesAsync();
}
