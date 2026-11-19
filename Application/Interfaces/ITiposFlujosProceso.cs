using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposFlujosProcesoRepository
{
    Task<IEnumerable<TiposFlujosProceso>> ShowallAsync();
    Task<TiposFlujosProceso?> ShowbyidAsync(string COD_TIPO_FLUJO_PROCESO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposFlujosProceso>> SearchAsync(TiposFlujosProcesoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposFlujosProcesoInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposFlujosProcesoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_FLUJO_PROCESO);
}

public interface ITiposFlujosProcesoService
{
    Task<ApiResponse<IEnumerable<TiposFlujosProceso>>> GetAllAsync();
    Task<ApiResponse<TiposFlujosProceso>> GetByIdAsync(string COD_TIPO_FLUJO_PROCESO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposFlujosProceso>>> SearchAsync(TiposFlujosProcesoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposFlujosProcesoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposFlujosProcesoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_FLUJO_PROCESO);
}