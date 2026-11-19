using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

/// <summary>Capa completa por consistencia arquitectónica; el frontend solo usa GetAllAsync
/// (grilla de solo lectura) — la tabla la puebla un proceso todavía no implementado.</summary>
public interface IInventariosAlmacenesImagenRepository
{
    Task<IEnumerable<InventariosAlmacenesImagen>> ShowallAsync(int numInventario, string codAlmacen);
    Task<InventariosAlmacenesImagen?> ShowbyidAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen, string codProducto, string numLoteControl, string codProceso, int numOrdenProduccion, int numOrdenProduccionItem, int numOrdenProduccionUnidad);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(int numInventario, string codAlmacen);
    Task<IEnumerable<InventariosAlmacenesImagen>> SearchAsync(InventariosAlmacenesImagenSearchDto dto);
    Task<WriteResult> InsertAsync(InventariosAlmacenesImagenInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(InventariosAlmacenesImagenUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen, string codProducto, string numLoteControl, string codProceso, int numOrdenProduccion, int numOrdenProduccionItem, int numOrdenProduccionUnidad);
}

public interface IInventariosAlmacenesImagenService
{
    Task<ApiResponse<IEnumerable<InventariosAlmacenesImagen>>> GetAllAsync(int numInventario, string codAlmacen);
    Task<ApiResponse<InventariosAlmacenesImagen>> GetByIdAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen, string codProducto, string numLoteControl, string codProceso, int numOrdenProduccion, int numOrdenProduccionItem, int numOrdenProduccionUnidad);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(int numInventario, string codAlmacen);
    Task<ApiResponse<IEnumerable<InventariosAlmacenesImagen>>> SearchAsync(InventariosAlmacenesImagenSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(InventariosAlmacenesImagenInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(InventariosAlmacenesImagenUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen, string codProducto, string numLoteControl, string codProceso, int numOrdenProduccion, int numOrdenProduccionItem, int numOrdenProduccionUnidad);
}
