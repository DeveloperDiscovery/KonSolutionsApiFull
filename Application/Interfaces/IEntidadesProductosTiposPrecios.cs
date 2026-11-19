using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesProductosTiposPreciosRepository
{
    Task<IEnumerable<EntidadesProductosTiposPrecios>> ShowallAsync(decimal codEntidad, string codProducto, int? codTipoPrecio = null);
    Task<EntidadesProductosTiposPrecios?> ShowbyidAsync(decimal COD_ENTIDAD, string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EntidadesProductosTiposPrecios>> SearchAsync(EntidadesProductosTiposPreciosSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesProductosTiposPreciosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EntidadesProductosTiposPreciosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA);
}

public interface IEntidadesProductosTiposPreciosService
{
    Task<ApiResponse<IEnumerable<EntidadesProductosTiposPrecios>>> GetAllAsync(decimal codEntidad, string codProducto, int? codTipoPrecio = null);
    Task<ApiResponse<EntidadesProductosTiposPrecios>> GetByIdAsync(decimal COD_ENTIDAD, string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EntidadesProductosTiposPrecios>>> SearchAsync(EntidadesProductosTiposPreciosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesProductosTiposPreciosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesProductosTiposPreciosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA);
}