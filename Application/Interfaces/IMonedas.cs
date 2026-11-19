using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMonedasRepository
{
    Task<IEnumerable<Monedas>> ShowallAsync();
    Task<Monedas?> ShowbyidAsync(string COD_MONEDA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Monedas>> SearchAsync(MonedasSearchDto dto);
    Task<WriteResult> InsertAsync(MonedasInsertDto dto);
    Task<WriteResult> UpdateAsync(string COD_MONEDA, MonedasUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_MONEDA);
}

public interface IMonedasService
{
    Task<ApiResponse<IEnumerable<Monedas>>> GetAllAsync();
    Task<ApiResponse<Monedas>> GetByIdAsync(string COD_MONEDA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Monedas>>> SearchAsync(MonedasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MonedasInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(string COD_MONEDA, MonedasUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_MONEDA);
}