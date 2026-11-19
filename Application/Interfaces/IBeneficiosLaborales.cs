using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface IBeneficiosLaboralesRepository
{
    Task<IEnumerable<BeneficiosLaborales>> ShowallAsync();
    Task<BeneficiosLaborales?> ShowbyidAsync(string COD_BENEFICIO_LABORAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<BeneficiosLaborales>> SearchAsync(BeneficiosLaboralesSearchDto dto);
    Task<WriteResult> InsertAsync(BeneficiosLaboralesInsertDto dto);
    Task<WriteResult> UpdateAsync(BeneficiosLaboralesUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_BENEFICIO_LABORAL);
}

public interface IBeneficiosLaboralesService
{
    Task<ApiResponse<IEnumerable<BeneficiosLaborales>>> GetAllAsync();
    Task<ApiResponse<BeneficiosLaborales>> GetByIdAsync(string COD_BENEFICIO_LABORAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<BeneficiosLaborales>>> SearchAsync(BeneficiosLaboralesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(BeneficiosLaboralesInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(BeneficiosLaboralesUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_BENEFICIO_LABORAL);
}