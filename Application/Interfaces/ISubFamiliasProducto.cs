using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISubFamiliasProductoRepository
{
    Task<IEnumerable<SubFamiliasProducto>> ShowallAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO);
    Task<SubFamiliasProducto?> ShowbyidAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SubFamiliasProducto>> SearchAsync(SubFamiliasProductoSearchDto dto);
    Task<WriteResult> InsertAsync(SubFamiliasProductoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO);
    Task<SubFamiliasProductoLoadCombosResult> LoadcombosAsync();
}

public interface ISubFamiliasProductoService
{
    Task<ApiResponse<IEnumerable<SubFamiliasProducto>>> GetAllAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO);
    Task<ApiResponse<SubFamiliasProducto>> GetByIdAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SubFamiliasProducto>>> SearchAsync(SubFamiliasProductoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SubFamiliasProductoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO);
    Task<ApiResponse<SubFamiliasProductoLoadCombosResult>> GetLoadcombosAsync();
}