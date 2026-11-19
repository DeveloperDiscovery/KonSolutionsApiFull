using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICostosProduccionRepository
{
    Task<IEnumerable<CostosProduccion>> ShowallAsync();
    Task<CostosProduccion?> ShowbyidAsync(int NUM_PERIODO_CONTABLE, string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CostosProduccion>> SearchAsync(CostosProduccionSearchDto dto);
    Task<WriteResult> InsertAsync(CostosProduccionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CostosProduccionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO);
}

public interface ICostosProduccionService
{
    Task<ApiResponse<IEnumerable<CostosProduccion>>> GetAllAsync();
    Task<ApiResponse<CostosProduccion>> GetByIdAsync(int NUM_PERIODO_CONTABLE, string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CostosProduccion>>> SearchAsync(CostosProduccionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CostosProduccionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CostosProduccionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO);
}