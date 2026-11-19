using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICostosProcesoRepository
{
    Task<IEnumerable<CostosProceso>> ShowallAsync();
    Task<CostosProceso?> ShowbyidAsync(int NUM_PERIODO_CONTABLE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CostosProceso>> SearchAsync(CostosProcesoSearchDto dto);
    Task<WriteResult> InsertAsync(CostosProcesoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CostosProcesoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE);
}

public interface ICostosProcesoService
{
    Task<ApiResponse<IEnumerable<CostosProceso>>> GetAllAsync();
    Task<ApiResponse<CostosProceso>> GetByIdAsync(int NUM_PERIODO_CONTABLE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CostosProceso>>> SearchAsync(CostosProcesoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CostosProcesoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CostosProcesoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PERIODO_CONTABLE);
}