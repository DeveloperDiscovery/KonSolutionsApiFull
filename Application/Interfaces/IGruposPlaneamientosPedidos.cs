using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IGruposPlaneamientosPedidosRepository
{
    Task<IEnumerable<GruposPlaneamientosPedidos>> ShowallAsync();
    Task<GruposPlaneamientosPedidos?> ShowbyidAsync(int COD_GRUPO_PLANEAMIENTO, int NUM_PEDIDO_COMERCIAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<GruposPlaneamientosPedidos>> SearchAsync(GruposPlaneamientosPedidosSearchDto dto);
    Task<WriteResult> InsertAsync(GruposPlaneamientosPedidosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_GRUPO_PLANEAMIENTO, int NUM_PEDIDO_COMERCIAL);
}

public interface IGruposPlaneamientosPedidosService
{
    Task<ApiResponse<IEnumerable<GruposPlaneamientosPedidos>>> GetAllAsync();
    Task<ApiResponse<GruposPlaneamientosPedidos>> GetByIdAsync(int COD_GRUPO_PLANEAMIENTO, int NUM_PEDIDO_COMERCIAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<GruposPlaneamientosPedidos>>> SearchAsync(GruposPlaneamientosPedidosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(GruposPlaneamientosPedidosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_GRUPO_PLANEAMIENTO, int NUM_PEDIDO_COMERCIAL);
}