using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IGruposProductoRepository
{
    Task<IEnumerable<GruposProducto>> ShowallAsync();
    Task<GruposProducto?> ShowbyidAsync(string COD_GRUPO_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<GruposProducto>> SearchAsync(GruposProductoSearchDto dto);
    Task<WriteResult> InsertAsync(GruposProductoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_GRUPO_PRODUCTO);
    Task<GruposProductoLoadCombosResult> LoadcombosAsync();
}

public interface IGruposProductoService
{
    Task<ApiResponse<IEnumerable<GruposProducto>>> GetAllAsync();
    Task<ApiResponse<GruposProducto>> GetByIdAsync(string COD_GRUPO_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<GruposProducto>>> SearchAsync(GruposProductoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(GruposProductoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_GRUPO_PRODUCTO);
    Task<ApiResponse<GruposProductoLoadCombosResult>> GetLoadcombosAsync();
}