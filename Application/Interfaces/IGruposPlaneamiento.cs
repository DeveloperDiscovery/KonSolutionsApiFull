using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IGruposPlaneamientoRepository
{
    Task<IEnumerable<GruposPlaneamiento>> ShowallAsync();
    Task<GruposPlaneamiento?> ShowbyidAsync(int COD_GRUPO_PLANEAMIENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<GruposPlaneamiento>> SearchAsync(GruposPlaneamientoSearchDto dto);
    Task<WriteResult> InsertAsync(GruposPlaneamientoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(GruposPlaneamientoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_GRUPO_PLANEAMIENTO);
}

public interface IGruposPlaneamientoService
{
    Task<ApiResponse<IEnumerable<GruposPlaneamiento>>> GetAllAsync();
    Task<ApiResponse<GruposPlaneamiento>> GetByIdAsync(int COD_GRUPO_PLANEAMIENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<GruposPlaneamiento>>> SearchAsync(GruposPlaneamientoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(GruposPlaneamientoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(GruposPlaneamientoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_GRUPO_PLANEAMIENTO);
}