using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalAfpRepository
{
    Task<IEnumerable<PersonalAfp>> ShowallAsync();
    Task<PersonalAfp?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalAfp>> SearchAsync(PersonalAfpSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalAfpInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalAfpUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
}

public interface IPersonalAfpService
{
    Task<ApiResponse<IEnumerable<PersonalAfp>>> GetAllAsync();
    Task<ApiResponse<PersonalAfp>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalAfp>>> SearchAsync(PersonalAfpSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalAfpInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalAfpUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
}