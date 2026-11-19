using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class InventariosService : IInventariosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(InventariosService);
    private readonly IInventariosRepository _repo;
    private readonly ErrorLogService _log;
    public InventariosService(IInventariosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<Inventarios>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<Inventarios>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<Inventarios>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<Inventarios>> GetByIdAsync(short NUM_INVENTARIO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_INVENTARIO);
              return d is null ? ApiResponse<Inventarios>.Fail("No encontrado.") : ApiResponse<Inventarios>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<Inventarios>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<Inventarios>>> SearchAsync(InventariosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<Inventarios>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<Inventarios>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(InventariosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(InventariosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro actualizado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(short NUM_INVENTARIO)
    {
        try { var r = await _repo.DeleteAsync(NUM_INVENTARIO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }


    // ── Procesos de inventario ──────────────────────────────────────────────
    // Mismo patrón que Insert/Update/Delete: FromWriteResult traduce el WriteResult
    // del repositorio a la respuesta de la API, incluido el mensaje de error del SP.

    public async Task<ApiResponse<WriteResult>> RequerimientoMakeAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.RequerimientoMakeAsync(NUM_INVENTARIO, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Requerimiento generado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(RequerimientoMakeAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> RequerimientoRevierteAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.RequerimientoRevierteAsync(NUM_INVENTARIO, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Requerimiento revertido."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(RequerimientoRevierteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> ImagenAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.ImagenAsync(NUM_INVENTARIO, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Imagen de inventario generada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ImagenAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> CerrarAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.CerrarAsync(NUM_INVENTARIO, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Inventario cerrado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(CerrarAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> AbrirAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.AbrirAsync(NUM_INVENTARIO, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Inventario abierto."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(AbrirAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
