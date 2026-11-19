using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IGruposRequerimientoPedidosRepository
{
    Task<IEnumerable<GruposRequerimientoPedidos>> ShowallAsync();
    Task<GruposRequerimientoPedidos?> ShowbyidAsync(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO, int NUM_PEDIDO_COMERCIAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<GruposRequerimientoPedidos>> SearchAsync(GruposRequerimientoPedidosSearchDto dto);
    Task<WriteResult> InsertAsync(GruposRequerimientoPedidosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(GruposRequerimientoPedidosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO, int NUM_PEDIDO_COMERCIAL);
}

public interface IGruposRequerimientoPedidosService
{
    Task<ApiResponse<IEnumerable<GruposRequerimientoPedidos>>> GetAllAsync();
    Task<ApiResponse<GruposRequerimientoPedidos>> GetByIdAsync(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO, int NUM_PEDIDO_COMERCIAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<GruposRequerimientoPedidos>>> SearchAsync(GruposRequerimientoPedidosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(GruposRequerimientoPedidosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(GruposRequerimientoPedidosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO, int NUM_PEDIDO_COMERCIAL);
}