using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposEntidadesRepository
{
    Task<IEnumerable<TiposEntidades>> ShowallAsync();
    Task<TiposEntidades?> ShowbyidAsync(string COD_TIPO_ENTIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposEntidades>> SearchAsync(TiposEntidadesSearchDto dto);
    Task<WriteResult> InsertAsync(TiposEntidadesInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposEntidadesUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_ENTIDAD);
}

public interface ITiposEntidadesService
{
    Task<ApiResponse<IEnumerable<TiposEntidades>>> GetAllAsync();
    Task<ApiResponse<TiposEntidades>> GetByIdAsync(string COD_TIPO_ENTIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposEntidades>>> SearchAsync(TiposEntidadesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposEntidadesInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposEntidadesUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_ENTIDAD);
}