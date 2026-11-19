using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IIncotermsFasesRepository
{
    Task<IEnumerable<IncotermsFases>> ShowallAsync(string codIncoterm);
    Task<IncotermsFases?> ShowbyidAsync(string COD_INCOTERM, string COD_FASE_INCOTERM);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<IncotermsFases>> SearchAsync(IncotermsFasesSearchDto dto);
    Task<WriteResult> InsertAsync(IncotermsFasesInsertDto dto);
    Task<WriteResult> UpdateAsync(IncotermsFasesUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_INCOTERM, string COD_FASE_INCOTERM);
}

public interface IIncotermsFasesService
{
    Task<ApiResponse<IEnumerable<IncotermsFases>>> GetAllAsync(string codIncoterm);
    Task<ApiResponse<IncotermsFases>> GetByIdAsync(string COD_INCOTERM, string COD_FASE_INCOTERM);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<IncotermsFases>>> SearchAsync(IncotermsFasesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(IncotermsFasesInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(IncotermsFasesUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_INCOTERM, string COD_FASE_INCOTERM);
}