using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Zonificacion;

namespace KonSolutionsApi.Application.Interfaces;

public interface IZonificacionRepository
{
    Task<IEnumerable<Zonificacion>> ShowallAsync(string COD_SUCURSAL);
    Task<Zonificacion?> ShowbyidAsync(string COD_SUCURSAL, string COD_ZONIFICACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_SUCURSAL);
    Task<IEnumerable<Zonificacion>> SearchAsync(ZonificacionSearchDto dto);
    Task<WriteResult> InsertAsync(ZonificacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_SUCURSAL, string COD_ZONIFICACION);
    Task<ZonificacionLoadCombosResult> LoadcombosAsync(string codSucursal, int codTipoEstado);
}

public interface IZonificacionService
{
    Task<ApiResponse<IEnumerable<Zonificacion>>> GetAllAsync(string COD_SUCURSAL);
    Task<ApiResponse<Zonificacion>> GetByIdAsync(string COD_SUCURSAL, string COD_ZONIFICACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_SUCURSAL);
    Task<ApiResponse<IEnumerable<Zonificacion>>> SearchAsync(ZonificacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ZonificacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_SUCURSAL, string COD_ZONIFICACION);
    Task<ApiResponse<ZonificacionLoadCombosResult>> LoadcombosAsync(string codSucursal, int codTipoEstado);
}