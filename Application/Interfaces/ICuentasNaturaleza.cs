using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICuentasNaturalezaRepository
{
    Task<IEnumerable<CuentasNaturaleza>> ShowallAsync();
    Task<CuentasNaturaleza?> ShowbyidAsync(string COD_CUENTA_NATURALEZA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CuentasNaturaleza>> SearchAsync(CuentasNaturalezaSearchDto dto);
    Task<WriteResult> InsertAsync(CuentasNaturalezaInsertDto dto);
    Task<WriteResult> UpdateAsync(CuentasNaturalezaUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_CUENTA_NATURALEZA);
}

public interface ICuentasNaturalezaService
{
    Task<ApiResponse<IEnumerable<CuentasNaturaleza>>> GetAllAsync();
    Task<ApiResponse<CuentasNaturaleza>> GetByIdAsync(string COD_CUENTA_NATURALEZA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CuentasNaturaleza>>> SearchAsync(CuentasNaturalezaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CuentasNaturalezaInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(CuentasNaturalezaUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CUENTA_NATURALEZA);
}