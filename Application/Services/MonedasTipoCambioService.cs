using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class MonedasTipoCambioService : IMonedasTipoCambioService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(MonedasTipoCambioService);
    private readonly IMonedasTipoCambioRepository _repo;
    private readonly ErrorLogService _log;
    public MonedasTipoCambioService(IMonedasTipoCambioRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<MonedasTipoCambio>>> GetAllAsync(string? codMoneda = null)
    {
        try { var d = (await _repo.ShowallAsync(codMoneda)).ToList();
              return ApiResponse<IEnumerable<MonedasTipoCambio>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<MonedasTipoCambio>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<MonedasTipoCambio>> GetByIdAsync(string COD_MONEDA, DateTime FEC_TIPO_CAMBIO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_MONEDA, FEC_TIPO_CAMBIO);
              return d is null ? ApiResponse<MonedasTipoCambio>.Fail("No encontrado.") : ApiResponse<MonedasTipoCambio>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<MonedasTipoCambio>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MonedasTipoCambio>>> SearchAsync(MonedasTipoCambioSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<MonedasTipoCambio>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<MonedasTipoCambio>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(MonedasTipoCambioInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(MonedasTipoCambioUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_MONEDA, DateTime FEC_TIPO_CAMBIO)
    {
        try { var r = await _repo.DeleteAsync(COD_MONEDA, FEC_TIPO_CAMBIO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<MonedasTipoCambio>> GuardarSunatAsync(DateTime fecha, decimal compra, decimal venta, string codUsuario, string codEstacion)
    {
        try
        {
            // El feed de SUNAT es siempre USD/PEN — se conservan los valores Comercial/Paralelo
            // que ya existieran para esa fecha (si los hay), para no pisarlos con NULL al hacer
            // el upsert; solo se actualizan los campos SUNAT.
            const string codMoneda = "USD";
            var existente = await _repo.ShowbyidAsync(codMoneda, fecha);
            var dto = new MonedasTipoCambioInsertDto
            {
                COD_MONEDA = codMoneda,
                FEC_TIPO_CAMBIO = fecha,
                FAC_COMERCIAL_COMPRA = existente?.FAC_COMERCIAL_COMPRA,
                FAC_COMERCIAL_VENTA = existente?.FAC_COMERCIAL_VENTA,
                FAC_SUNAT_COMPRA = compra,
                FAC_SUNAT_VENTA = venta,
                FAC_PARALELO_COMPRA = existente?.FAC_PARALELO_COMPRA,
                FAC_PARALELO_VENTA = existente?.FAC_PARALELO_VENTA,
                COD_USUARIO_REGISTRO = codUsuario,
                COD_ESTACION_REGISTRO = codEstacion
            };
            var w = await _repo.InsertAsync(dto, codUsuario, codEstacion);
            if (w.Estado != "OK") return ApiResponse<MonedasTipoCambio>.Fail(w.Mensaje ?? "No se pudo guardar el tipo de cambio.");

            var guardado = await _repo.ShowbyidAsync(codMoneda, fecha);
            return guardado is null
                ? ApiResponse<MonedasTipoCambio>.Fail("Se guardó pero no se pudo releer el registro.")
                : ApiResponse<MonedasTipoCambio>.Ok(guardado, $"Tipo de cambio SUNAT del {fecha:dd/MM/yyyy} descargado.");
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(GuardarSunatAsync));
            return ApiResponse<MonedasTipoCambio>.Fail($"No se pudo guardar el tipo de cambio de SUNAT: {ex.Message}");
        }
    }

    public async Task<ApiResponse<TipoCambioResultado>> ObtenerAsync(string codMoneda, DateTime? fecTipoCambio)
    {
        try
        {
            var r = await _repo.ObtenerAsync(codMoneda, fecTipoCambio);
            return r is null
                ? ApiResponse<TipoCambioResultado>.Fail("No se pudo resolver el tipo de cambio.")
                : ApiResponse<TipoCambioResultado>.Ok(r);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(ObtenerAsync));
            return ApiResponse<TipoCambioResultado>.Fail(ex.Message);
        }
    }

}
