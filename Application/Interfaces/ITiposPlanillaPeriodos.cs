using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposPlanillaPeriodosRepository
{
    Task<IEnumerable<TiposPlanillaPeriodos>> ShowallAsync();
    Task<TiposPlanillaPeriodos?> ShowbyidAsync(string COD_TIPO_PLANILLA, string COD_TIPO_PERIODO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposPlanillaPeriodos>> SearchAsync(TiposPlanillaPeriodosSearchDto dto);
    Task<WriteResult> InsertAsync(TiposPlanillaPeriodosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_PLANILLA, string COD_TIPO_PERIODO);
}

public interface ITiposPlanillaPeriodosService
{
    Task<ApiResponse<IEnumerable<TiposPlanillaPeriodos>>> GetAllAsync();
    Task<ApiResponse<TiposPlanillaPeriodos>> GetByIdAsync(string COD_TIPO_PLANILLA, string COD_TIPO_PERIODO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposPlanillaPeriodos>>> SearchAsync(TiposPlanillaPeriodosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposPlanillaPeriodosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_PLANILLA, string COD_TIPO_PERIODO);
}