using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesProductosRepository
{
    Task<IEnumerable<EntidadesProductos>> ShowallAsync(decimal? codEntidad = null, string? codProducto = null);
    Task<EntidadesProductos?> ShowbyidAsync(decimal COD_ENTIDAD, string COD_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EntidadesProductos>> SearchAsync(EntidadesProductosSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesProductosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EntidadesProductosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, string COD_PRODUCTO);
}

public interface IEntidadesProductosService
{
    Task<ApiResponse<IEnumerable<EntidadesProductos>>> GetAllAsync(decimal? codEntidad = null, string? codProducto = null);
    Task<ApiResponse<EntidadesProductos>> GetByIdAsync(decimal COD_ENTIDAD, string COD_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EntidadesProductos>>> SearchAsync(EntidadesProductosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesProductosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesProductosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, string COD_PRODUCTO);
}