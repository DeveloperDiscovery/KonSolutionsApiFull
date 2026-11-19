using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class AlmacenesCierreService : IAlmacenesCierreService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(AlmacenesCierreService);
    private readonly IAlmacenesCierreRepository _repo;
    private readonly ErrorLogService _log;
    public AlmacenesCierreService(IAlmacenesCierreRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<AlmacenesCierre>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<AlmacenesCierre>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<AlmacenesCierre>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<AlmacenesCierre>> GetByIdAsync(int NUM_CIERRE_ALMACEN)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_CIERRE_ALMACEN);
              return d is null ? ApiResponse<AlmacenesCierre>.Fail("No encontrado.") : ApiResponse<AlmacenesCierre>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<AlmacenesCierre>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<AlmacenesCierre>>> SearchAsync(AlmacenesCierreSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<AlmacenesCierre>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<AlmacenesCierre>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<AlmacenesCierreConsulta>> ConsultarAsync(string COD_ALMACEN, DateTime? FEC_MOVIMENTO)
    {
        if (string.IsNullOrWhiteSpace(COD_ALMACEN)) return ApiResponse<AlmacenesCierreConsulta>.Fail("Indica el almacén.");
        try { var d = await _repo.ConsultarAsync(COD_ALMACEN, FEC_MOVIMENTO);
              return d is null ? ApiResponse<AlmacenesCierreConsulta>.Fail("No se pudo consultar.") : ApiResponse<AlmacenesCierreConsulta>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ConsultarAsync));
              return ApiResponse<AlmacenesCierreConsulta>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(AlmacenesCierreInsertDto dto, string codUsuario, string codEstacion)
    {
        if (dto.COD_ALMACENES is not { Count: > 0 } || dto.COD_ALMACENES.All(string.IsNullOrWhiteSpace))
            return ApiResponse<WriteResult>.Fail("Elige al menos un almacén para cerrar.");
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, r.AffectedRows > 1 ? $"{r.AffectedRows} almacenes cerrados." : "Almacén cerrado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(AlmacenesCierreUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Cierre modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> ReabrirAsync(int NUM_CIERRE_ALMACEN, AlmacenesCierreReabrirDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.ReabrirAsync(NUM_CIERRE_ALMACEN, dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Almacén reabierto."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ReabrirAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_CIERRE_ALMACEN)
    {
        try { var r = await _repo.DeleteAsync(NUM_CIERRE_ALMACEN);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Cierre eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
