using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPlaneamientoConfiguracionRepository
{
    Task<IEnumerable<PlaneamientoConfiguracion>> ShowallAsync();
    Task<PlaneamientoConfiguracion?> ShowbyidAsync(int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PlaneamientoConfiguracion>> SearchAsync(PlaneamientoConfiguracionSearchDto dto);
    Task<WriteResult> InsertAsync(PlaneamientoConfiguracionInsertDto dto);
    Task<WriteResult> UpdateAsync(PlaneamientoConfiguracionUpdateDto dto);
    Task<WriteResult> DeleteAsync(int NUM_SECUENCIA);
}

public interface IPlaneamientoConfiguracionService
{
    Task<ApiResponse<IEnumerable<PlaneamientoConfiguracion>>> GetAllAsync();
    Task<ApiResponse<PlaneamientoConfiguracion>> GetByIdAsync(int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PlaneamientoConfiguracion>>> SearchAsync(PlaneamientoConfiguracionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PlaneamientoConfiguracionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(PlaneamientoConfiguracionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SECUENCIA);
}