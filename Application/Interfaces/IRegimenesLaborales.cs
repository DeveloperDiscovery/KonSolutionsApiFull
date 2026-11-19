using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRegimenesLaboralesRepository
{
    Task<IEnumerable<RegimenesLaborales>> ShowallAsync();
    Task<RegimenesLaborales?> ShowbyidAsync(string COD_REGIMEN_LABORAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<RegimenesLaborales>> SearchAsync(RegimenesLaboralesSearchDto dto);
    Task<WriteResult> InsertAsync(RegimenesLaboralesInsertDto dto);
    Task<WriteResult> UpdateAsync(RegimenesLaboralesUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_REGIMEN_LABORAL);
}

public interface IRegimenesLaboralesService
{
    Task<ApiResponse<IEnumerable<RegimenesLaborales>>> GetAllAsync();
    Task<ApiResponse<RegimenesLaborales>> GetByIdAsync(string COD_REGIMEN_LABORAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<RegimenesLaborales>>> SearchAsync(RegimenesLaboralesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RegimenesLaboralesInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(RegimenesLaboralesUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_REGIMEN_LABORAL);
}