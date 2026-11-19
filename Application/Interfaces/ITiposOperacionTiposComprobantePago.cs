using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposOperacionTiposComprobantePagoRepository
{
    Task<IEnumerable<TiposOperacionTiposComprobantePago>> ShowallAsync(string codClaseOperacion, string codTipoOperacion);
    Task<TiposOperacionTiposComprobantePago?> ShowbyidAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_COMPROBANTE_PAGO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposOperacionTiposComprobantePago>> SearchAsync(string codClaseOperacion, string codTipoOperacion, TiposOperacionTiposComprobantePagoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposOperacionTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_COMPROBANTE_PAGO);
}

public interface ITiposOperacionTiposComprobantePagoService
{
    Task<ApiResponse<IEnumerable<TiposOperacionTiposComprobantePago>>> GetAllAsync(string codClaseOperacion, string codTipoOperacion);
    Task<ApiResponse<TiposOperacionTiposComprobantePago>> GetByIdAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_COMPROBANTE_PAGO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposOperacionTiposComprobantePago>>> SearchAsync(string codClaseOperacion, string codTipoOperacion, TiposOperacionTiposComprobantePagoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposOperacionTiposComprobantePagoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_COMPROBANTE_PAGO);
}