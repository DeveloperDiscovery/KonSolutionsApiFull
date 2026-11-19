using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICajasRepository
{
    Task<IEnumerable<Cajas>> ShowallAsync();
    Task<Cajas?> ShowbyidAsync(string COD_CAJA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Cajas>> SearchAsync(CajasSearchDto dto);
    Task<WriteResult> InsertAsync(CajasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CajasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CAJA);
}

public interface ICajasService
{
    Task<ApiResponse<IEnumerable<Cajas>>> GetAllAsync();
    Task<ApiResponse<Cajas>> GetByIdAsync(string COD_CAJA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Cajas>>> SearchAsync(CajasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CajasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CajasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CAJA);
}