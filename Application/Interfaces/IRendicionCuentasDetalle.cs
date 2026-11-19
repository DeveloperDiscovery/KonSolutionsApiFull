using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRendicionCuentasDetalleRepository
{
    Task<IEnumerable<RendicionCuentasDetalle>> ShowallAsync();
    Task<RendicionCuentasDetalle?> ShowbyidAsync(int NUM_RENDICION_CUENTAS, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<RendicionCuentasDetalle>> SearchAsync(RendicionCuentasDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(RendicionCuentasDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(RendicionCuentasDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_RENDICION_CUENTAS, int NUM_SECUENCIA);
}

public interface IRendicionCuentasDetalleService
{
    Task<ApiResponse<IEnumerable<RendicionCuentasDetalle>>> GetAllAsync();
    Task<ApiResponse<RendicionCuentasDetalle>> GetByIdAsync(int NUM_RENDICION_CUENTAS, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<RendicionCuentasDetalle>>> SearchAsync(RendicionCuentasDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RendicionCuentasDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(RendicionCuentasDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_RENDICION_CUENTAS, int NUM_SECUENCIA);
}