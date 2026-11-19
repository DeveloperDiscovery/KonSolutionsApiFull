using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPedidosComercialesSecuenciasRepository
{
    Task<IEnumerable<PedidosComercialesSecuencias>> ShowallAsync();
    Task<PedidosComercialesSecuencias?> ShowbyidAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PedidosComercialesSecuencias>> SearchAsync(PedidosComercialesSecuenciasSearchDto dto);
    Task<WriteResult> InsertAsync(PedidosComercialesSecuenciasInsertDto dto);
    Task<WriteResult> UpdateAsync(PedidosComercialesSecuenciasUpdateDto dto);
    Task<WriteResult> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
}

public interface IPedidosComercialesSecuenciasService
{
    Task<ApiResponse<IEnumerable<PedidosComercialesSecuencias>>> GetAllAsync();
    Task<ApiResponse<PedidosComercialesSecuencias>> GetByIdAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PedidosComercialesSecuencias>>> SearchAsync(PedidosComercialesSecuenciasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PedidosComercialesSecuenciasInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(PedidosComercialesSecuenciasUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA);
}