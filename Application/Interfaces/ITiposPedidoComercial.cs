using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposPedidoComercialRepository
{
    Task<IEnumerable<TiposPedidoComercial>> ShowallAsync();
    Task<TiposPedidoComercial?> ShowbyidAsync(int COD_TIPO_PEDIDO_COMERCIAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposPedidoComercial>> SearchAsync(TiposPedidoComercialSearchDto dto);
    Task<WriteResult> InsertAsync(TiposPedidoComercialInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposPedidoComercialUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_TIPO_PEDIDO_COMERCIAL);
    Task<TiposPedidoComercialLoadCombosResult> LoadcombosAsync();
}

public interface ITiposPedidoComercialService
{
    Task<ApiResponse<IEnumerable<TiposPedidoComercial>>> GetAllAsync();
    Task<ApiResponse<TiposPedidoComercial>> GetByIdAsync(int COD_TIPO_PEDIDO_COMERCIAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposPedidoComercial>>> SearchAsync(TiposPedidoComercialSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposPedidoComercialInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposPedidoComercialUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_TIPO_PEDIDO_COMERCIAL);
    Task<ApiResponse<TiposPedidoComercialLoadCombosResult>> LoadcombosAsync();
}