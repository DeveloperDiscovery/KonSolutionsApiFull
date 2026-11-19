using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalTiposPersonalRepository
{
    Task<IEnumerable<PersonalTiposPersonal>> ShowallAsync();
    Task<PersonalTiposPersonal?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalTiposPersonal>> SearchAsync(PersonalTiposPersonalSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalTiposPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalTiposPersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
}

public interface IPersonalTiposPersonalService
{
    Task<ApiResponse<IEnumerable<PersonalTiposPersonal>>> GetAllAsync();
    Task<ApiResponse<PersonalTiposPersonal>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalTiposPersonal>>> SearchAsync(PersonalTiposPersonalSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalTiposPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalTiposPersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
}