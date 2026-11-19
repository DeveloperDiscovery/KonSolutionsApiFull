using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

// Todas las lecturas van filtradas por @COD_ALMACEN: los SP SHOWALL/SEARCH/SHOWCOMBOX
// lo reciben como parámetro obligatorio porque la tabla es hija de LOGISTICA.ALMACENES.
public interface IAlmacenesUbicacionesAlmacenRepository
{
    Task<IEnumerable<AlmacenesUbicacionesAlmacen>> ShowallAsync(string COD_ALMACEN);
    Task<AlmacenesUbicacionesAlmacen?> ShowbyidAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_ALMACEN);
    Task<IEnumerable<AlmacenesUbicacionesAlmacen>> SearchAsync(AlmacenesUbicacionesAlmacenSearchDto dto);
    Task<WriteResult> InsertAsync(AlmacenesUbicacionesAlmacenInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN);
}

public interface IAlmacenesUbicacionesAlmacenService
{
    Task<ApiResponse<IEnumerable<AlmacenesUbicacionesAlmacen>>> GetAllAsync(string COD_ALMACEN);
    Task<ApiResponse<AlmacenesUbicacionesAlmacen>> GetByIdAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_ALMACEN);
    Task<ApiResponse<IEnumerable<AlmacenesUbicacionesAlmacen>>> SearchAsync(AlmacenesUbicacionesAlmacenSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AlmacenesUbicacionesAlmacenInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN);
}
