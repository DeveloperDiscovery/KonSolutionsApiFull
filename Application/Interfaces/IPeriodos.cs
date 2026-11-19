using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPeriodosRepository
{
    Task<IEnumerable<Periodos>> ShowallAsync(string codTipoPeriodo);
    Task<Periodos?> ShowbyidAsync(string codTipoPeriodo, int numPeriodo);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codTipoPeriodo);
    Task<IEnumerable<Periodos>> SearchAsync(PeriodosSearchDto dto);
    Task<WriteResult> InsertAsync(PeriodosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PeriodosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string codTipoPeriodo, int numPeriodo);
}

public interface IPeriodosService
{
    Task<ApiResponse<IEnumerable<Periodos>>> GetAllAsync(string codTipoPeriodo);
    Task<ApiResponse<Periodos>> GetByIdAsync(string codTipoPeriodo, int numPeriodo);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codTipoPeriodo);
    Task<ApiResponse<IEnumerable<Periodos>>> SearchAsync(PeriodosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PeriodosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PeriodosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codTipoPeriodo, int numPeriodo);
}