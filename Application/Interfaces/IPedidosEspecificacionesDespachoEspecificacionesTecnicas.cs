using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPedidosEspecificacionesDespachoEspecificacionesTecnicasRepository
{
    Task<IEnumerable<PedidosEspecificacionesDespachoEspecificacionesTecnicas>> ShowallAsync();
    Task<PedidosEspecificacionesDespachoEspecificacionesTecnicas?> ShowbyidAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PedidosEspecificacionesDespachoEspecificacionesTecnicas>> SearchAsync(PedidosEspecificacionesDespachoEspecificacionesTecnicasSearchDto dto);
    Task<WriteResult> InsertAsync(PedidosEspecificacionesDespachoEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PedidosEspecificacionesDespachoEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
}

public interface IPedidosEspecificacionesDespachoEspecificacionesTecnicasService
{
    Task<ApiResponse<IEnumerable<PedidosEspecificacionesDespachoEspecificacionesTecnicas>>> GetAllAsync();
    Task<ApiResponse<PedidosEspecificacionesDespachoEspecificacionesTecnicas>> GetByIdAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PedidosEspecificacionesDespachoEspecificacionesTecnicas>>> SearchAsync(PedidosEspecificacionesDespachoEspecificacionesTecnicasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PedidosEspecificacionesDespachoEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PedidosEspecificacionesDespachoEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
}