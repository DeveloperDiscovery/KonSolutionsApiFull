using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProcesosLineasActivosRepository
{
    Task<IEnumerable<ProcesosLineasActivos>> ShowallAsync();
    Task<ProcesosLineasActivos?> ShowbyidAsync(string COD_PROCESO, string COD_ACTIVO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProcesosLineasActivos>> SearchAsync(ProcesosLineasActivosSearchDto dto);
    Task<WriteResult> InsertAsync(ProcesosLineasActivosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProcesosLineasActivosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PROCESO, string COD_ACTIVO);
}

public interface IProcesosLineasActivosService
{
    Task<ApiResponse<IEnumerable<ProcesosLineasActivos>>> GetAllAsync();
    Task<ApiResponse<ProcesosLineasActivos>> GetByIdAsync(string COD_PROCESO, string COD_ACTIVO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProcesosLineasActivos>>> SearchAsync(ProcesosLineasActivosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProcesosLineasActivosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProcesosLineasActivosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PROCESO, string COD_ACTIVO);
}