using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISucursalesSerieComprobanteRepository
{
    Task<IEnumerable<SucursalesSerieComprobante>> ShowallAsync(string codSucursal, string codTipoComprobantePago);
    Task<SucursalesSerieComprobante?> ShowbyidAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, int NUM_SERIE_COMPROBANTE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codSucursal, string codTipoComprobantePago);
    Task<IEnumerable<SucursalesSerieComprobante>> SearchAsync(string codSucursal, string codTipoComprobantePago, SucursalesSerieComprobanteSearchDto dto);
    Task<WriteResult> InsertAsync(SucursalesSerieComprobanteInsertDto dto);
    Task<WriteResult> UpdateAsync(SucursalesSerieComprobanteUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, int NUM_SERIE_COMPROBANTE);
}

public interface ISucursalesSerieComprobanteService
{
    Task<ApiResponse<IEnumerable<SucursalesSerieComprobante>>> GetAllAsync(string codSucursal, string codTipoComprobantePago);
    Task<ApiResponse<SucursalesSerieComprobante>> GetByIdAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, int NUM_SERIE_COMPROBANTE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codSucursal, string codTipoComprobantePago);
    Task<ApiResponse<IEnumerable<SucursalesSerieComprobante>>> SearchAsync(string codSucursal, string codTipoComprobantePago, SucursalesSerieComprobanteSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SucursalesSerieComprobanteInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(SucursalesSerieComprobanteUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, int NUM_SERIE_COMPROBANTE);
}