using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposPlanillaRepository
{
    Task<IEnumerable<TiposPlanilla>> ShowallAsync();
    Task<TiposPlanilla?> ShowbyidAsync(string COD_TIPO_PLANILLA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposPlanilla>> SearchAsync(TiposPlanillaSearchDto dto);
    Task<WriteResult> InsertAsync(TiposPlanillaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposPlanillaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_PLANILLA);
}

public interface ITiposPlanillaService
{
    Task<ApiResponse<IEnumerable<TiposPlanilla>>> GetAllAsync();
    Task<ApiResponse<TiposPlanilla>> GetByIdAsync(string COD_TIPO_PLANILLA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposPlanilla>>> SearchAsync(TiposPlanillaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposPlanillaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposPlanillaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_PLANILLA);
}