using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMonedasTipoCambioRepository
{
    Task<IEnumerable<MonedasTipoCambio>> ShowallAsync(string? codMoneda = null);
    Task<TipoCambioResultado?> ObtenerAsync(string codMoneda, DateTime? fecTipoCambio);
    Task<MonedasTipoCambio?> ShowbyidAsync(string COD_MONEDA, DateTime FEC_TIPO_CAMBIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MonedasTipoCambio>> SearchAsync(MonedasTipoCambioSearchDto dto);
    Task<WriteResult> InsertAsync(MonedasTipoCambioInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(MonedasTipoCambioUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_MONEDA, DateTime FEC_TIPO_CAMBIO);
}

public interface IMonedasTipoCambioService
{
    Task<ApiResponse<IEnumerable<MonedasTipoCambio>>> GetAllAsync(string? codMoneda = null);
    Task<ApiResponse<MonedasTipoCambio>> GetByIdAsync(string COD_MONEDA, DateTime FEC_TIPO_CAMBIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MonedasTipoCambio>>> SearchAsync(MonedasTipoCambioSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MonedasTipoCambioInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(MonedasTipoCambioUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_MONEDA, DateTime FEC_TIPO_CAMBIO);
    /// <summary>Guarda el tipo de cambio SUNAT (siempre USD) ya descargado/parseado por el
    /// controller (vía snippet configurado en Externo → Snippets) como FAC_SUNAT_COMPRA/
    /// FAC_SUNAT_VENTA del día indicado, sin tocar los valores Comercial/Paralelo que ya
    /// existieran para esa fecha.</summary>
    Task<ApiResponse<MonedasTipoCambio>> GuardarSunatAsync(DateTime fecha, decimal compra, decimal venta, string codUsuario, string codEstacion);
    Task<ApiResponse<TipoCambioResultado>> ObtenerAsync(string codMoneda, DateTime? fecTipoCambio);
}