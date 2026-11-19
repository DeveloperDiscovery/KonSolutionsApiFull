using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPedidosEspecificacionesDespachoRepository
{
    Task<IEnumerable<PedidosEspecificacionesDespacho>> ShowallAsync();
    Task<PedidosEspecificacionesDespacho?> ShowbyidAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PedidosEspecificacionesDespacho>> SearchAsync(PedidosEspecificacionesDespachoSearchDto dto);
    Task<WriteResult> InsertAsync(PedidosEspecificacionesDespachoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
}

public interface IPedidosEspecificacionesDespachoService
{
    Task<ApiResponse<IEnumerable<PedidosEspecificacionesDespacho>>> GetAllAsync();
    Task<ApiResponse<PedidosEspecificacionesDespacho>> GetByIdAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PedidosEspecificacionesDespacho>>> SearchAsync(PedidosEspecificacionesDespachoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PedidosEspecificacionesDespachoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
}