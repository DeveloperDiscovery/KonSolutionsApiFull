using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProcesosLineasRepository
{
    Task<IEnumerable<ProcesosLineas>> ShowallAsync();
    Task<ProcesosLineas?> ShowbyidAsync(string COD_PROCESO, string COD_LINEA_PRODUCCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProcesosLineas>> SearchAsync(ProcesosLineasSearchDto dto);
    Task<WriteResult> InsertAsync(ProcesosLineasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProcesosLineasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PROCESO, string COD_LINEA_PRODUCCION);
}

public interface IProcesosLineasService
{
    Task<ApiResponse<IEnumerable<ProcesosLineas>>> GetAllAsync();
    Task<ApiResponse<ProcesosLineas>> GetByIdAsync(string COD_PROCESO, string COD_LINEA_PRODUCCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProcesosLineas>>> SearchAsync(ProcesosLineasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProcesosLineasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProcesosLineasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PROCESO, string COD_LINEA_PRODUCCION);
}