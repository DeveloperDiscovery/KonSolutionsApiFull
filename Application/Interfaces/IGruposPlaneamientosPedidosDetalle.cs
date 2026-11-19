using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IGruposPlaneamientosPedidosDetalleRepository
{
    Task<IEnumerable<GruposPlaneamientosPedidosDetalle>> ShowallAsync();
    Task<GruposPlaneamientosPedidosDetalle?> ShowbyidAsync(int COD_GRUPO_PLANEAMIENTO, int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<GruposPlaneamientosPedidosDetalle>> SearchAsync(GruposPlaneamientosPedidosDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(GruposPlaneamientosPedidosDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(GruposPlaneamientosPedidosDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_GRUPO_PLANEAMIENTO, int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
}

public interface IGruposPlaneamientosPedidosDetalleService
{
    Task<ApiResponse<IEnumerable<GruposPlaneamientosPedidosDetalle>>> GetAllAsync();
    Task<ApiResponse<GruposPlaneamientosPedidosDetalle>> GetByIdAsync(int COD_GRUPO_PLANEAMIENTO, int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<GruposPlaneamientosPedidosDetalle>>> SearchAsync(GruposPlaneamientosPedidosDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(GruposPlaneamientosPedidosDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(GruposPlaneamientosPedidosDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_GRUPO_PLANEAMIENTO, int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
}