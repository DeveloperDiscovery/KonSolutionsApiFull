using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposZonaRepository
{
    Task<IEnumerable<TiposZona>> ShowallAsync();
    Task<TiposZona?> ShowbyidAsync(string COD_TIPO_ZONA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposZona>> SearchAsync(TiposZonaSearchDto dto);
    Task<WriteResult> InsertAsync(TiposZonaInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposZonaUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_ZONA);
}

public interface ITiposZonaService
{
    Task<ApiResponse<IEnumerable<TiposZona>>> GetAllAsync();
    Task<ApiResponse<TiposZona>> GetByIdAsync(string COD_TIPO_ZONA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposZona>>> SearchAsync(TiposZonaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposZonaInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposZonaUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_ZONA);
}