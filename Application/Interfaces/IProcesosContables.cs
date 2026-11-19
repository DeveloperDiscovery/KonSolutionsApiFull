using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProcesosContablesRepository
{
    Task<IEnumerable<ProcesosContables>> ShowallAsync();
    Task<ProcesosContables?> ShowbyidAsync(int NUM_PERIODO_CONTABLE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProcesosContables>> SearchAsync(ProcesosContablesSearchDto dto);
    Task<WriteResult> InsertAsync(ProcesosContablesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProcesosContablesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE);
}

public interface IProcesosContablesService
{
    Task<ApiResponse<IEnumerable<ProcesosContables>>> GetAllAsync();
    Task<ApiResponse<ProcesosContables>> GetByIdAsync(int NUM_PERIODO_CONTABLE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProcesosContables>>> SearchAsync(ProcesosContablesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProcesosContablesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProcesosContablesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PERIODO_CONTABLE);
}