using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposPlanillaConceptosRemunerativosRepository
{
    Task<IEnumerable<TiposPlanillaConceptosRemunerativos>> ShowallAsync();
    Task<TiposPlanillaConceptosRemunerativos?> ShowbyidAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposPlanillaConceptosRemunerativos>> SearchAsync(TiposPlanillaConceptosRemunerativosSearchDto dto);
    Task<WriteResult> InsertAsync(TiposPlanillaConceptosRemunerativosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposPlanillaConceptosRemunerativosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO);
}

public interface ITiposPlanillaConceptosRemunerativosService
{
    Task<ApiResponse<IEnumerable<TiposPlanillaConceptosRemunerativos>>> GetAllAsync();
    Task<ApiResponse<TiposPlanillaConceptosRemunerativos>> GetByIdAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposPlanillaConceptosRemunerativos>>> SearchAsync(TiposPlanillaConceptosRemunerativosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposPlanillaConceptosRemunerativosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposPlanillaConceptosRemunerativosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_PLANILLA, int COD_CONCEPTO_REMUNERATIVO);
}