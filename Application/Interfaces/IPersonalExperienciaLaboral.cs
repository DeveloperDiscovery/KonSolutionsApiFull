using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalExperienciaLaboralRepository
{
    Task<IEnumerable<PersonalExperienciaLaboral>> ShowallAsync();
    Task<PersonalExperienciaLaboral?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalExperienciaLaboral>> SearchAsync(PersonalExperienciaLaboralSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalExperienciaLaboralInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalExperienciaLaboralUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
}

public interface IPersonalExperienciaLaboralService
{
    Task<ApiResponse<IEnumerable<PersonalExperienciaLaboral>>> GetAllAsync();
    Task<ApiResponse<PersonalExperienciaLaboral>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalExperienciaLaboral>>> SearchAsync(PersonalExperienciaLaboralSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalExperienciaLaboralInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalExperienciaLaboralUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
}