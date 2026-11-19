using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IFamiliasProductoRepository
{
    Task<IEnumerable<FamiliasProducto>> ShowallAsync(string COD_GRUPO_PRODUCTO);
    Task<FamiliasProducto?> ShowbyidAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<FamiliasProducto>> SearchAsync(FamiliasProductoSearchDto dto);
    Task<WriteResult> InsertAsync(FamiliasProductoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO);
    Task<FamiliasProductoLoadCombosResult> LoadcombosAsync();
}

public interface IFamiliasProductoService
{
    Task<ApiResponse<IEnumerable<FamiliasProducto>>> GetAllAsync(string COD_GRUPO_PRODUCTO);
    Task<ApiResponse<FamiliasProducto>> GetByIdAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<FamiliasProducto>>> SearchAsync(FamiliasProductoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(FamiliasProductoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO);
    Task<ApiResponse<FamiliasProductoLoadCombosResult>> GetLoadcombosAsync();
}