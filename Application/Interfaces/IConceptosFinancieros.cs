using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IConceptosFinancierosRepository
{
    Task<IEnumerable<ConceptosFinancieros>> ShowallAsync();
    Task<ConceptosFinancieros?> ShowbyidAsync(string COD_TIPO_CONCEPTO_FINANCIERO, string COD_CONCEPTO_FINANCIERO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ConceptosFinancieros>> SearchAsync(ConceptosFinancierosSearchDto dto);
    Task<WriteResult> InsertAsync(ConceptosFinancierosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ConceptosFinancierosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_CONCEPTO_FINANCIERO, string COD_CONCEPTO_FINANCIERO);
}

public interface IConceptosFinancierosService
{
    Task<ApiResponse<IEnumerable<ConceptosFinancieros>>> GetAllAsync();
    Task<ApiResponse<ConceptosFinancieros>> GetByIdAsync(string COD_TIPO_CONCEPTO_FINANCIERO, string COD_CONCEPTO_FINANCIERO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ConceptosFinancieros>>> SearchAsync(ConceptosFinancierosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ConceptosFinancierosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ConceptosFinancierosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_CONCEPTO_FINANCIERO, string COD_CONCEPTO_FINANCIERO);
}