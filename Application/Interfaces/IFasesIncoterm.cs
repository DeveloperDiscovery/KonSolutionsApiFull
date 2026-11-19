using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IFasesIncotermRepository
{
    Task<IEnumerable<FasesIncoterm>> ShowallAsync();
    Task<FasesIncoterm?> ShowbyidAsync(string COD_FASE_INCOTERM);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<FasesIncoterm>> SearchAsync(FasesIncotermSearchDto dto);
    Task<WriteResult> InsertAsync(FasesIncotermInsertDto dto);
    Task<WriteResult> UpdateAsync(FasesIncotermUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_FASE_INCOTERM);
}

public interface IFasesIncotermService
{
    Task<ApiResponse<IEnumerable<FasesIncoterm>>> GetAllAsync();
    Task<ApiResponse<FasesIncoterm>> GetByIdAsync(string COD_FASE_INCOTERM);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<FasesIncoterm>>> SearchAsync(FasesIncotermSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(FasesIncotermInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(FasesIncotermUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_FASE_INCOTERM);
}