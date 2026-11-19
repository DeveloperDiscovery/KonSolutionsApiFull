using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalAfpConceptosRemunerativosRepository
{
    Task<IEnumerable<PersonalAfpConceptosRemunerativos>> ShowallAsync();
    Task<PersonalAfpConceptosRemunerativos?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA, int COD_CONCEPTO_REMUNERATIVO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalAfpConceptosRemunerativos>> SearchAsync(PersonalAfpConceptosRemunerativosSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalAfpConceptosRemunerativosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalAfpConceptosRemunerativosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA, int COD_CONCEPTO_REMUNERATIVO);
}

public interface IPersonalAfpConceptosRemunerativosService
{
    Task<ApiResponse<IEnumerable<PersonalAfpConceptosRemunerativos>>> GetAllAsync();
    Task<ApiResponse<PersonalAfpConceptosRemunerativos>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA, int COD_CONCEPTO_REMUNERATIVO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalAfpConceptosRemunerativos>>> SearchAsync(PersonalAfpConceptosRemunerativosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalAfpConceptosRemunerativosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalAfpConceptosRemunerativosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA, int COD_CONCEPTO_REMUNERATIVO);
}