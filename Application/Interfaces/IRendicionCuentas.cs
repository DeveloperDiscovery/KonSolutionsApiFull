using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRendicionCuentasRepository
{
    Task<IEnumerable<RendicionCuentas>> ShowallAsync();
    Task<RendicionCuentas?> ShowbyidAsync(int NUM_RENDICION_CUENTAS);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<RendicionCuentas>> SearchAsync(RendicionCuentasSearchDto dto);
    Task<WriteResult> InsertAsync(RendicionCuentasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(RendicionCuentasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_RENDICION_CUENTAS);
}

public interface IRendicionCuentasService
{
    Task<ApiResponse<IEnumerable<RendicionCuentas>>> GetAllAsync();
    Task<ApiResponse<RendicionCuentas>> GetByIdAsync(int NUM_RENDICION_CUENTAS);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<RendicionCuentas>>> SearchAsync(RendicionCuentasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RendicionCuentasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(RendicionCuentasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_RENDICION_CUENTAS);
}