using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISucursalesTiposComprobantePagoRepository
{
    Task<IEnumerable<SucursalesTiposComprobantePago>> ShowallAsync(string COD_SUCURSAL);
    Task<SucursalesTiposComprobantePago?> ShowbyidAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_SUCURSAL);
    Task<IEnumerable<SucursalesTiposComprobantePago>> SearchAsync(SucursalesTiposComprobantePagoSearchDto dto);
    Task<WriteResult> InsertAsync(SucursalesTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO);
}

public interface ISucursalesTiposComprobantePagoService
{
    Task<ApiResponse<IEnumerable<SucursalesTiposComprobantePago>>> GetAllAsync(string COD_SUCURSAL);
    Task<ApiResponse<SucursalesTiposComprobantePago>> GetByIdAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_SUCURSAL);
    Task<ApiResponse<IEnumerable<SucursalesTiposComprobantePago>>> SearchAsync(SucursalesTiposComprobantePagoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SucursalesTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO);
}