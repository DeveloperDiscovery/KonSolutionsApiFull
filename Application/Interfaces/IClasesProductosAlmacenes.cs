using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IClasesProductosAlmacenesRepository
{
    Task<IEnumerable<ClasesProductosAlmacenes>> ShowallAsync();
    Task<ClasesProductosAlmacenes?> ShowbyidAsync(string COD_CLASE_PRODUCTO, string COD_ALMACEN);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ClasesProductosAlmacenes>> SearchAsync(ClasesProductosAlmacenesSearchDto dto);
    Task<WriteResult> InsertAsync(ClasesProductosAlmacenesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CLASE_PRODUCTO, string COD_ALMACEN);
}

public interface IClasesProductosAlmacenesService
{
    Task<ApiResponse<IEnumerable<ClasesProductosAlmacenes>>> GetAllAsync();
    Task<ApiResponse<ClasesProductosAlmacenes>> GetByIdAsync(string COD_CLASE_PRODUCTO, string COD_ALMACEN);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ClasesProductosAlmacenes>>> SearchAsync(ClasesProductosAlmacenesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ClasesProductosAlmacenesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_PRODUCTO, string COD_ALMACEN);
}