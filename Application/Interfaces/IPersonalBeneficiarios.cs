using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalBeneficiariosRepository
{
    Task<IEnumerable<PersonalBeneficiarios>> ShowallAsync();
    Task<PersonalBeneficiarios?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_BENEFECIARIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalBeneficiarios>> SearchAsync(PersonalBeneficiariosSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalBeneficiariosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalBeneficiariosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_BENEFECIARIO);
}

public interface IPersonalBeneficiariosService
{
    Task<ApiResponse<IEnumerable<PersonalBeneficiarios>>> GetAllAsync();
    Task<ApiResponse<PersonalBeneficiarios>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_BENEFECIARIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalBeneficiarios>>> SearchAsync(PersonalBeneficiariosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalBeneficiariosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalBeneficiariosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_ENTIDAD_BENEFECIARIO);
}