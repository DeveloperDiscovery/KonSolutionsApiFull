using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Aprobaciones;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAprobacionesDetalleRepository
{
    Task<IEnumerable<AprobacionesDetalle>> ShowallAsync();
    Task<AprobacionesDetalle?> ShowbyidAsync(int NUM_APROBACION, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<AprobacionesDetalle>> SearchAsync(AprobacionesDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(AprobacionesDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(AprobacionesDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_APROBACION, int NUM_SECUENCIA);
}

public interface IAprobacionesDetalleService
{
    Task<ApiResponse<IEnumerable<AprobacionesDetalle>>> GetAllAsync();
    Task<ApiResponse<AprobacionesDetalle>> GetByIdAsync(int NUM_APROBACION, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<AprobacionesDetalle>>> SearchAsync(AprobacionesDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AprobacionesDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(AprobacionesDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_APROBACION, int NUM_SECUENCIA);
}