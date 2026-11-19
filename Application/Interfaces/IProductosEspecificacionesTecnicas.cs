using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProductosEspecificacionesTecnicasRepository
{
    Task<IEnumerable<ProductosEspecificacionesTecnicas>> ShowallAsync(string codProducto);
    Task<ProductosEspecificacionesTecnicas?> ShowbyidAsync(string COD_PRODUCTO, string COD_ESPECIFICACION_TECNICA);
    Task<IEnumerable<ProductosEspecificacionesTecnicas>> SearchAsync(ProductosEspecificacionesTecnicasSearchDto dto);
    Task<WriteResult> InsertAsync(ProductosEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PRODUCTO, string COD_ESPECIFICACION_TECNICA);
}

public interface IProductosEspecificacionesTecnicasService
{
    Task<ApiResponse<IEnumerable<ProductosEspecificacionesTecnicas>>> GetAllAsync(string codProducto);
    Task<ApiResponse<ProductosEspecificacionesTecnicas>> GetByIdAsync(string COD_PRODUCTO, string COD_ESPECIFICACION_TECNICA);
    Task<ApiResponse<IEnumerable<ProductosEspecificacionesTecnicas>>> SearchAsync(ProductosEspecificacionesTecnicasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProductosEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PRODUCTO, string COD_ESPECIFICACION_TECNICA);
}
