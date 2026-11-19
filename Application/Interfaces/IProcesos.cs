using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProcesosRepository
{
    Task<IEnumerable<Procesos>> ShowallAsync();
    Task<Procesos?> ShowbyidAsync(string COD_PROCESO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Procesos>> SearchAsync(ProcesosSearchDto dto);
    Task<WriteResult> InsertAsync(ProcesosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProcesosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PROCESO);
    Task<ProcesosLoadCombosResult> LoadcombosAsync();
}

public interface IProcesosService
{
    Task<ApiResponse<IEnumerable<Procesos>>> GetAllAsync();
    Task<ApiResponse<Procesos>> GetByIdAsync(string COD_PROCESO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Procesos>>> SearchAsync(ProcesosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProcesosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProcesosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PROCESO);
    Task<ApiResponse<ProcesosLoadCombosResult>> LoadcombosAsync();
}