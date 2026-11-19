using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISucursalesSerieComprobantesAplicadasRepository
{
    Task<IEnumerable<SucursalesSerieComprobantesAplicadas>> ShowallAsync();
    Task<SucursalesSerieComprobantesAplicadas?> ShowbyidAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, string COD_TIPO_COMPROBANTE_PAGO_APLICADO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SucursalesSerieComprobantesAplicadas>> SearchAsync(SucursalesSerieComprobantesAplicadasSearchDto dto);
    Task<WriteResult> InsertAsync(SucursalesSerieComprobantesAplicadasInsertDto dto);
    Task<WriteResult> UpdateAsync(SucursalesSerieComprobantesAplicadasUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, string COD_TIPO_COMPROBANTE_PAGO_APLICADO);
}

public interface ISucursalesSerieComprobantesAplicadasService
{
    Task<ApiResponse<IEnumerable<SucursalesSerieComprobantesAplicadas>>> GetAllAsync();
    Task<ApiResponse<SucursalesSerieComprobantesAplicadas>> GetByIdAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, string COD_TIPO_COMPROBANTE_PAGO_APLICADO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SucursalesSerieComprobantesAplicadas>>> SearchAsync(SucursalesSerieComprobantesAplicadasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SucursalesSerieComprobantesAplicadasInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(SucursalesSerieComprobantesAplicadasUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_SUCURSAL, string COD_TIPO_COMPROBANTE_PAGO, string COD_TIPO_COMPROBANTE_PAGO_APLICADO);
}