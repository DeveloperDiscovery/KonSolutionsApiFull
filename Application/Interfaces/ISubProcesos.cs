using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISubProcesosRepository
{
    Task<IEnumerable<SubProcesos>> ShowallAsync(string COD_PROCESO);
    Task<SubProcesos?> ShowbyidAsync(string COD_PROCESO, string COD_SUB_PROCESO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_PROCESO);
    Task<IEnumerable<SubProcesos>> SearchAsync(SubProcesosSearchDto dto);
    Task<WriteResult> InsertAsync(SubProcesosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(SubProcesosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PROCESO, string COD_SUB_PROCESO);
}

public interface ISubProcesosService
{
    Task<ApiResponse<IEnumerable<SubProcesos>>> GetAllAsync(string COD_PROCESO);
    Task<ApiResponse<SubProcesos>> GetByIdAsync(string COD_PROCESO, string COD_SUB_PROCESO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_PROCESO);
    Task<ApiResponse<IEnumerable<SubProcesos>>> SearchAsync(SubProcesosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SubProcesosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(SubProcesosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PROCESO, string COD_SUB_PROCESO);
}