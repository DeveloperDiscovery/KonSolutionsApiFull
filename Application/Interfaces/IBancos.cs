using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IBancosRepository
{
    Task<IEnumerable<Bancos>> ShowallAsync();
    Task<Bancos?> ShowbyidAsync(string COD_BANCO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Bancos>> SearchAsync(BancosSearchDto dto);
    Task<WriteResult> InsertAsync(BancosInsertDto dto);
    Task<WriteResult> UpdateAsync(BancosUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_BANCO);
}

public interface IBancosService
{
    Task<ApiResponse<IEnumerable<Bancos>>> GetAllAsync();
    Task<ApiResponse<Bancos>> GetByIdAsync(string COD_BANCO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Bancos>>> SearchAsync(BancosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(BancosInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(BancosUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_BANCO);
}