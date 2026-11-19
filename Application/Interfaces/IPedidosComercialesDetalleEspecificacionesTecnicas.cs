using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPedidosComercialesDetalleEspecificacionesTecnicasRepository
{
    Task<IEnumerable<PedidosComercialesDetalleEspecificacionesTecnicas>> ShowallAsync();
    Task<PedidosComercialesDetalleEspecificacionesTecnicas?> ShowbyidAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PedidosComercialesDetalleEspecificacionesTecnicas>> SearchAsync(PedidosComercialesDetalleEspecificacionesTecnicasSearchDto dto);
    Task<WriteResult> InsertAsync(PedidosComercialesDetalleEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PedidosComercialesDetalleEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
}

public interface IPedidosComercialesDetalleEspecificacionesTecnicasService
{
    Task<ApiResponse<IEnumerable<PedidosComercialesDetalleEspecificacionesTecnicas>>> GetAllAsync();
    Task<ApiResponse<PedidosComercialesDetalleEspecificacionesTecnicas>> GetByIdAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PedidosComercialesDetalleEspecificacionesTecnicas>>> SearchAsync(PedidosComercialesDetalleEspecificacionesTecnicasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PedidosComercialesDetalleEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PedidosComercialesDetalleEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
}