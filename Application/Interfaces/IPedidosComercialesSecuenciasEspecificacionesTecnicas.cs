using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPedidosComercialesSecuenciasEspecificacionesTecnicasRepository
{
    Task<IEnumerable<PedidosComercialesSecuenciasEspecificacionesTecnicas>> ShowallAsync();
    Task<PedidosComercialesSecuenciasEspecificacionesTecnicas?> ShowbyidAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PedidosComercialesSecuenciasEspecificacionesTecnicas>> SearchAsync(PedidosComercialesSecuenciasEspecificacionesTecnicasSearchDto dto);
    Task<WriteResult> InsertAsync(PedidosComercialesSecuenciasEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PedidosComercialesSecuenciasEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
}

public interface IPedidosComercialesSecuenciasEspecificacionesTecnicasService
{
    Task<ApiResponse<IEnumerable<PedidosComercialesSecuenciasEspecificacionesTecnicas>>> GetAllAsync();
    Task<ApiResponse<PedidosComercialesSecuenciasEspecificacionesTecnicas>> GetByIdAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PedidosComercialesSecuenciasEspecificacionesTecnicas>>> SearchAsync(PedidosComercialesSecuenciasEspecificacionesTecnicasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PedidosComercialesSecuenciasEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PedidosComercialesSecuenciasEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
}