using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface IConceptosRemunerativosRepository
{
    Task<IEnumerable<ConceptosRemunerativos>> ShowallAsync();
    Task<ConceptosRemunerativos?> ShowbyidAsync(int COD_CONCEPTO_REMUNERATIVO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ConceptosRemunerativos>> SearchAsync(ConceptosRemunerativosSearchDto dto);
    Task<WriteResult> InsertAsync(ConceptosRemunerativosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ConceptosRemunerativosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_CONCEPTO_REMUNERATIVO);
}

public interface IConceptosRemunerativosService
{
    Task<ApiResponse<IEnumerable<ConceptosRemunerativos>>> GetAllAsync();
    Task<ApiResponse<ConceptosRemunerativos>> GetByIdAsync(int COD_CONCEPTO_REMUNERATIVO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ConceptosRemunerativos>>> SearchAsync(ConceptosRemunerativosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ConceptosRemunerativosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ConceptosRemunerativosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_CONCEPTO_REMUNERATIVO);
}