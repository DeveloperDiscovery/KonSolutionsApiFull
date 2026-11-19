using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposCuentaContableRepository
{
    Task<IEnumerable<TiposCuentaContable>> ShowallAsync();
    Task<TiposCuentaContable?> ShowbyidAsync(string COD_TIPO_CUENTA_CONTABLE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposCuentaContable>> SearchAsync(TiposCuentaContableSearchDto dto);
    Task<WriteResult> InsertAsync(TiposCuentaContableInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposCuentaContableUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_CUENTA_CONTABLE);
}

public interface ITiposCuentaContableService
{
    Task<ApiResponse<IEnumerable<TiposCuentaContable>>> GetAllAsync();
    Task<ApiResponse<TiposCuentaContable>> GetByIdAsync(string COD_TIPO_CUENTA_CONTABLE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposCuentaContable>>> SearchAsync(TiposCuentaContableSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposCuentaContableInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposCuentaContableUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_CUENTA_CONTABLE);
}