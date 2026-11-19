using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class LotesControlService : ILotesControlService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(LotesControlService);
    private readonly ILotesControlRepository _repo;
    private readonly ErrorLogService _log;
    public LotesControlService(ILotesControlRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<LotesControl>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<LotesControl>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<LotesControl>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<LotesControl>> GetByIdAsync(string NUM_LOTE_CONTROL)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_LOTE_CONTROL);
              return d is null ? ApiResponse<LotesControl>.Fail("No encontrado.") : ApiResponse<LotesControl>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<LotesControl>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(decimal? codEntidad = null)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(codEntidad)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<LotesControl>>> SearchAsync(LotesControlSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<LotesControl>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<LotesControl>>.Fail(ex.Message); }
    }

    /// <summary>Cuántas veces se reintenta el alta cuando el número calculado ya lo tomó otro.
    /// Tres alcanza de sobra: cada choque exige que dos altas caigan en la misma fracción de
    /// segundo, y el número se recalcula en cada vuelta.</summary>
    private const int IntentosCorrelativo = 3;

    /// <summary>Distingue el choque de correlativo de cualquier otro fallo del alta. El
    /// procedimiento responde con el mensaje de "registro existente" del sistema, que está en
    /// español y es configurable, así que se busca la raíz de la palabra en vez del texto
    /// completo.</summary>
    private static bool EsClaveRepetida(Exception ex)
        => (ex.Message ?? "").Contains("exist", StringComparison.OrdinalIgnoreCase);

    public async Task<ApiResponse<WriteResult>> InsertAsync(LotesControlInsertDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            // El número lo asigna el sistema. Si el llamador manda uno, se respeta: sirve para
            // migraciones y para cargar lotes que ya vienen numerados desde afuera.
            var automatico = string.IsNullOrWhiteSpace(dto.NUM_LOTE_CONTROL);
            if (!automatico)
            {
                var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
                return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado.");
            }

            for (var intento = 1; ; intento++)
            {
                dto.NUM_LOTE_CONTROL = await _repo.SiguienteNumeroAsync();
                try
                {
                    var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
                    // El número generado viaja de vuelta: la pantalla lo necesita para saber
                    // qué lote acaba de crear.
                    if (!r.IsError) r.Codigo = dto.NUM_LOTE_CONTROL;
                    return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado.");
                }
                catch (Exception ex) when (intento < IntentosCorrelativo && EsClaveRepetida(ex))
                {
                    // Otro tomó el mismo número entre el cálculo y el alta. Se recalcula y se
                    // vuelve a intentar; el procedimiento avisa con RAISERROR, así que el
                    // choque llega como excepción y no como resultado con error.
                }
            }
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(LotesControlUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string NUM_LOTE_CONTROL)
    {
        try { var r = await _repo.DeleteAsync(NUM_LOTE_CONTROL);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}