using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalConceptosRemunerativosRepository
{
    Task<IEnumerable<PersonalConceptosRemunerativos>> ShowallAsync();
    Task<PersonalConceptosRemunerativos?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalConceptosRemunerativos>> SearchAsync(PersonalConceptosRemunerativosSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalConceptosRemunerativosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalConceptosRemunerativosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO);
}

public interface IPersonalConceptosRemunerativosService
{
    Task<ApiResponse<IEnumerable<PersonalConceptosRemunerativos>>> GetAllAsync();
    Task<ApiResponse<PersonalConceptosRemunerativos>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalConceptosRemunerativos>>> SearchAsync(PersonalConceptosRemunerativosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalConceptosRemunerativosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalConceptosRemunerativosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO);
}