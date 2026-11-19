using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalConceptosRemunerativosHistoricoRepository
{
    Task<IEnumerable<PersonalConceptosRemunerativosHistorico>> ShowallAsync();
    Task<PersonalConceptosRemunerativosHistorico?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalConceptosRemunerativosHistorico>> SearchAsync(PersonalConceptosRemunerativosHistoricoSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalConceptosRemunerativosHistoricoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalConceptosRemunerativosHistoricoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA);
}

public interface IPersonalConceptosRemunerativosHistoricoService
{
    Task<ApiResponse<IEnumerable<PersonalConceptosRemunerativosHistorico>>> GetAllAsync();
    Task<ApiResponse<PersonalConceptosRemunerativosHistorico>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalConceptosRemunerativosHistorico>>> SearchAsync(PersonalConceptosRemunerativosHistoricoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalConceptosRemunerativosHistoricoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalConceptosRemunerativosHistoricoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int COD_CONCEPTO_REMUNERATIVO, int NUM_SECUENCIA);
}