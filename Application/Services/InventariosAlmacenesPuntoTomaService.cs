using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class InventariosAlmacenesPuntoTomaService : IInventariosAlmacenesPuntoTomaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(InventariosAlmacenesPuntoTomaService);
    private readonly IInventariosAlmacenesPuntoTomaRepository _repo;
    private readonly ErrorLogService _log;
    public InventariosAlmacenesPuntoTomaService(IInventariosAlmacenesPuntoTomaRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoToma>>> GetAllAsync(int numInventario, string codAlmacen)
    {
        try { var d = (await _repo.ShowallAsync(numInventario, codAlmacen)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoToma>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoToma>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<InventariosAlmacenesPuntoToma>> GetByIdAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario)
    {
        try { var d = await _repo.ShowbyidAsync(numInventario, codAlmacen, numPuntoTomaInventario);
              return d is null ? ApiResponse<InventariosAlmacenesPuntoToma>.Fail("No encontrado.") : ApiResponse<InventariosAlmacenesPuntoToma>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<InventariosAlmacenesPuntoToma>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoToma>>> GetComboboxAsync(int numInventario, string codAlmacen)
    {
        try { var d = (await _repo.ShowcomboxAsync(numInventario, codAlmacen)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoToma>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoToma>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoToma>>> SearchAsync(InventariosAlmacenesPuntoTomaSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoToma>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoToma>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(InventariosAlmacenesPuntoTomaInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(InventariosAlmacenesPuntoTomaUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario)
    {
        try { var r = await _repo.DeleteAsync(numInventario, codAlmacen, numPuntoTomaInventario);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
