using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Activos;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposActivoRepository
{
    Task<IEnumerable<TiposActivo>> ShowallAsync();
    Task<TiposActivo?> ShowbyidAsync(string COD_TIPO_ACTIVO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposActivo>> SearchAsync(TiposActivoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposActivoInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposActivoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_ACTIVO);
}

public interface ITiposActivoService
{
    Task<ApiResponse<IEnumerable<TiposActivo>>> GetAllAsync();
    Task<ApiResponse<TiposActivo>> GetByIdAsync(string COD_TIPO_ACTIVO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposActivo>>> SearchAsync(TiposActivoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposActivoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposActivoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_ACTIVO);
}