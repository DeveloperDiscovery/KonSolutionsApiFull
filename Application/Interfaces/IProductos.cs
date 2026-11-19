using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProductosRepository
{
    Task<IEnumerable<Productos>> ShowallAsync(ProductosSearchDto? filtros = null);
    Task<Productos?> ShowbyidAsync(string COD_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Productos>> SearchAsync(ProductosSearchDto dto);
    Task<WriteResult> InsertAsync(ProductosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProductosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PRODUCTO);
    Task<ProductosLoadCombosResult> LoadcombosAsync();
    Task ActualizarDescripcionDetalleAsync(string COD_PRODUCTO);
    Task<IEnumerable<ProductoEspecificacionArbol>> EspecificacionesArbolAsync(ProductosSearchDto? filtros);
    Task<IEnumerable<Productos>> FiltrarPorEspecificacionesAsync(ProductosSearchDto dto);
}

public interface IProductosService
{
    Task<ApiResponse<IEnumerable<Productos>>> GetAllAsync(ProductosSearchDto? filtros = null);
    Task<ApiResponse<Productos>> GetByIdAsync(string COD_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Productos>>> SearchAsync(ProductosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProductosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProductosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PRODUCTO);
    Task<ApiResponse<ProductosLoadCombosResult>> GetLoadcombosAsync();
    Task<ApiResponse<bool>> ActualizarDescripcionDetalleAsync(string COD_PRODUCTO);
    Task<ApiResponse<IEnumerable<ProductoEspecificacionArbol>>> GetEspecificacionesArbolAsync(ProductosSearchDto? filtros);
    Task<ApiResponse<IEnumerable<Productos>>> FiltrarPorEspecificacionesAsync(ProductosSearchDto dto);
}