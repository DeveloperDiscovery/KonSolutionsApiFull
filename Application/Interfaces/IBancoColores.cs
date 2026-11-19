using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IBancoColoresRepository
{
    Task<IEnumerable<BancoColores>> ShowallAsync();
    Task<BancoColores?> ShowbyidAsync(int COD_BANCO_COLOR);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<BancoColores>> SearchAsync(BancoColoresSearchDto dto);
    Task<WriteResult> InsertAsync(BancoColoresSaveDto dto);
    Task<WriteResult> DeleteAsync(int COD_BANCO_COLOR);
}

public interface IBancoColoresService
{
    Task<ApiResponse<IEnumerable<BancoColores>>> GetAllAsync();
    Task<ApiResponse<BancoColores>> GetByIdAsync(int COD_BANCO_COLOR);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<BancoColores>>> SearchAsync(BancoColoresSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(BancoColoresSaveDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_BANCO_COLOR);
}
