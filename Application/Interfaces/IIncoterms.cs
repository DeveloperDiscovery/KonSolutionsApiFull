using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IIncotermsRepository
{
    Task<IEnumerable<Incoterms>> ShowallAsync();
    Task<Incoterms?> ShowbyidAsync(string COD_INCOTERM);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Incoterms>> SearchAsync(IncotermsSearchDto dto);
    Task<WriteResult> InsertAsync(IncotermsInsertDto dto);
    Task<WriteResult> UpdateAsync(IncotermsUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_INCOTERM);
}

public interface IIncotermsService
{
    Task<ApiResponse<IEnumerable<Incoterms>>> GetAllAsync();
    Task<ApiResponse<Incoterms>> GetByIdAsync(string COD_INCOTERM);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Incoterms>>> SearchAsync(IncotermsSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(IncotermsInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(IncotermsUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_INCOTERM);
}