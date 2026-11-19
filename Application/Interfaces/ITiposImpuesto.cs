using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposImpuestoRepository
{
    Task<IEnumerable<TiposImpuesto>> ShowallAsync();
    Task<TiposImpuesto?> ShowbyidAsync(string COD_TIPO_IMPUESTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposImpuesto>> SearchAsync(TiposImpuestoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposImpuestoInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposImpuestoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_IMPUESTO);
}

public interface ITiposImpuestoService
{
    Task<ApiResponse<IEnumerable<TiposImpuesto>>> GetAllAsync();
    Task<ApiResponse<TiposImpuesto>> GetByIdAsync(string COD_TIPO_IMPUESTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposImpuesto>>> SearchAsync(TiposImpuestoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposImpuestoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposImpuestoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_IMPUESTO);
}