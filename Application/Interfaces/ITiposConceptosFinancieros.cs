using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposConceptosFinancierosRepository
{
    Task<IEnumerable<TiposConceptosFinancieros>> ShowallAsync();
    Task<TiposConceptosFinancieros?> ShowbyidAsync(string COD_TIPO_CONCEPTO_FINANCIERO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposConceptosFinancieros>> SearchAsync(TiposConceptosFinancierosSearchDto dto);
    Task<WriteResult> InsertAsync(TiposConceptosFinancierosInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposConceptosFinancierosUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_CONCEPTO_FINANCIERO);
}

public interface ITiposConceptosFinancierosService
{
    Task<ApiResponse<IEnumerable<TiposConceptosFinancieros>>> GetAllAsync();
    Task<ApiResponse<TiposConceptosFinancieros>> GetByIdAsync(string COD_TIPO_CONCEPTO_FINANCIERO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposConceptosFinancieros>>> SearchAsync(TiposConceptosFinancierosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposConceptosFinancierosInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposConceptosFinancierosUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_CONCEPTO_FINANCIERO);
}