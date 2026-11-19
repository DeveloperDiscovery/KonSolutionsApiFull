using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISucursalesRepository
{
    Task<IEnumerable<Sucursales>> ShowallAsync();
    Task<Sucursales?> ShowbyidAsync(string COD_SUCURSAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Sucursales>> SearchAsync(SucursalesSearchDto dto);
    Task<WriteResult> InsertAsync(SucursalesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(SucursalesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_SUCURSAL);
    Task<SucursalesLoadCombosResult> LoadcombosAsync();
}

public interface ISucursalesService
{
    Task<ApiResponse<IEnumerable<Sucursales>>> GetAllAsync();
    Task<ApiResponse<Sucursales>> GetByIdAsync(string COD_SUCURSAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Sucursales>>> SearchAsync(SucursalesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SucursalesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(SucursalesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_SUCURSAL);
    Task<ApiResponse<SucursalesLoadCombosResult>> GetLoadcombosAsync();
}