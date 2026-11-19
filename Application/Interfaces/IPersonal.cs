using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalRepository
{
    Task<IEnumerable<Personal>> ShowallAsync();
    Task<Personal?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Personal>> SearchAsync(PersonalSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL);
}

public interface IPersonalService
{
    Task<ApiResponse<IEnumerable<Personal>>> GetAllAsync();
    Task<ApiResponse<Personal>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Personal>>> SearchAsync(PersonalSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL);
}