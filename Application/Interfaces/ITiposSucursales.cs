using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposSucursalesRepository
{
    Task<IEnumerable<TiposSucursales>> ShowallAsync();
    Task<TiposSucursales?> ShowbyidAsync(string COD_TIPO_SUCURSAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposSucursales>> SearchAsync(TiposSucursalesSearchDto dto);
    Task<WriteResult> InsertAsync(TiposSucursalesInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposSucursalesUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_SUCURSAL);
}

public interface ITiposSucursalesService
{
    Task<ApiResponse<IEnumerable<TiposSucursales>>> GetAllAsync();
    Task<ApiResponse<TiposSucursales>> GetByIdAsync(string COD_TIPO_SUCURSAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposSucursales>>> SearchAsync(TiposSucursalesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposSucursalesInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposSucursalesUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_SUCURSAL);
}