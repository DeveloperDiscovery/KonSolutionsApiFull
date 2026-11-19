using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IGruposRequerimientoRepository
{
    Task<IEnumerable<GruposRequerimiento>> ShowallAsync();
    Task<GruposRequerimiento?> ShowbyidAsync(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<GruposRequerimiento>> SearchAsync(GruposRequerimientoSearchDto dto);
    Task<WriteResult> InsertAsync(GruposRequerimientoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(GruposRequerimientoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO);
}

public interface IGruposRequerimientoService
{
    Task<ApiResponse<IEnumerable<GruposRequerimiento>>> GetAllAsync();
    Task<ApiResponse<GruposRequerimiento>> GetByIdAsync(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<GruposRequerimiento>>> SearchAsync(GruposRequerimientoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(GruposRequerimientoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(GruposRequerimientoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO);
}