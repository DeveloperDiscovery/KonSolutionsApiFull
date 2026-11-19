using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPedidosComercialesEspecificacionesTecnicasBaseRepository
{
    Task<IEnumerable<PedidosComercialesEspecificacionesTecnicasBase>> ShowallAsync();
    Task<PedidosComercialesEspecificacionesTecnicasBase?> ShowbyidAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_ENTIDAD, string COD_GRUPO_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PedidosComercialesEspecificacionesTecnicasBase>> SearchAsync(PedidosComercialesEspecificacionesTecnicasBaseSearchDto dto);
    Task<WriteResult> InsertAsync(PedidosComercialesEspecificacionesTecnicasBaseInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_ENTIDAD, string COD_GRUPO_PRODUCTO);
}

public interface IPedidosComercialesEspecificacionesTecnicasBaseService
{
    Task<ApiResponse<IEnumerable<PedidosComercialesEspecificacionesTecnicasBase>>> GetAllAsync();
    Task<ApiResponse<PedidosComercialesEspecificacionesTecnicasBase>> GetByIdAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_ENTIDAD, string COD_GRUPO_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PedidosComercialesEspecificacionesTecnicasBase>>> SearchAsync(PedidosComercialesEspecificacionesTecnicasBaseSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PedidosComercialesEspecificacionesTecnicasBaseInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_ENTIDAD, string COD_GRUPO_PRODUCTO);
}