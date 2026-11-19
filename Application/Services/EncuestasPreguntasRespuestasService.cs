using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class EncuestasPreguntasRespuestasService : IEncuestasPreguntasRespuestasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EncuestasPreguntasRespuestasService);
    private readonly IEncuestasPreguntasRespuestasRepository _repo;
    private readonly ErrorLogService _log;
    public EncuestasPreguntasRespuestasService(IEncuestasPreguntasRespuestasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EncuestasPreguntasRespuestas>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<EncuestasPreguntasRespuestas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EncuestasPreguntasRespuestas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EncuestasPreguntasRespuestas>> GetByIdAsync(int NUM_ENCUESTA, int NUM_PREGUNTA, int NUM_RESPUESTA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_ENCUESTA, NUM_PREGUNTA, NUM_RESPUESTA);
              return d is null ? ApiResponse<EncuestasPreguntasRespuestas>.Fail("No encontrado.") : ApiResponse<EncuestasPreguntasRespuestas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EncuestasPreguntasRespuestas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EncuestasPreguntasRespuestas>>> SearchAsync(EncuestasPreguntasRespuestasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EncuestasPreguntasRespuestas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EncuestasPreguntasRespuestas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EncuestasPreguntasRespuestasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EncuestasPreguntasRespuestasUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_ENCUESTA, int NUM_PREGUNTA, int NUM_RESPUESTA)
    {
        try { var r = await _repo.DeleteAsync(NUM_ENCUESTA, NUM_PREGUNTA, NUM_RESPUESTA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}