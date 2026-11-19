using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEstructuraCodebarRepository
{
    Task<IEnumerable<EstructuraCodebar>> ShowallAsync();
    Task<EstructuraCodebar?> ShowbyidAsync(string COD_ESTRUCTURA_CODEBAR);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<WriteResult> InsertAsync(EstructuraCodebarInsertDto dto);
    Task<WriteResult> UpdateAsync(EstructuraCodebarUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_ESTRUCTURA_CODEBAR);
}

public interface IEstructuraCodebarService
{
    Task<ApiResponse<IEnumerable<EstructuraCodebar>>> GetAllAsync();
    Task<ApiResponse<EstructuraCodebar>> GetByIdAsync(string COD_ESTRUCTURA_CODEBAR);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<WriteResult>> InsertAsync(EstructuraCodebarInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(EstructuraCodebarUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ESTRUCTURA_CODEBAR);
}
