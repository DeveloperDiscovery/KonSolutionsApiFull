using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IClasesElementosCostosRepository
{
    Task<IEnumerable<ClasesElementosCostos>> ShowallAsync();
    Task<ClasesElementosCostos?> ShowbyidAsync(string COD_CLASE_COSTO, string COD_ELEMENTO_COSTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ClasesElementosCostos>> SearchAsync(ClasesElementosCostosSearchDto dto);
    Task<WriteResult> InsertAsync(ClasesElementosCostosInsertDto dto);
    Task<WriteResult> UpdateAsync(ClasesElementosCostosUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_CLASE_COSTO, string COD_ELEMENTO_COSTO);
}

public interface IClasesElementosCostosService
{
    Task<ApiResponse<IEnumerable<ClasesElementosCostos>>> GetAllAsync();
    Task<ApiResponse<ClasesElementosCostos>> GetByIdAsync(string COD_CLASE_COSTO, string COD_ELEMENTO_COSTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ClasesElementosCostos>>> SearchAsync(ClasesElementosCostosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ClasesElementosCostosInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ClasesElementosCostosUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_COSTO, string COD_ELEMENTO_COSTO);
}