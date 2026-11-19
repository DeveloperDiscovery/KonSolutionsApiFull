using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposConceptoRemunerativoRepository
{
    Task<IEnumerable<TiposConceptoRemunerativo>> ShowallAsync();
    Task<TiposConceptoRemunerativo?> ShowbyidAsync(string COD_TIPO_REMUNERATIVO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposConceptoRemunerativo>> SearchAsync(TiposConceptoRemunerativoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposConceptoRemunerativoInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposConceptoRemunerativoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_REMUNERATIVO);
}

public interface ITiposConceptoRemunerativoService
{
    Task<ApiResponse<IEnumerable<TiposConceptoRemunerativo>>> GetAllAsync();
    Task<ApiResponse<TiposConceptoRemunerativo>> GetByIdAsync(string COD_TIPO_REMUNERATIVO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposConceptoRemunerativo>>> SearchAsync(TiposConceptoRemunerativoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposConceptoRemunerativoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposConceptoRemunerativoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_REMUNERATIVO);
}