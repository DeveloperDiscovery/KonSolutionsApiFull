using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class InventariosAlmacenesPuntoTomaEquiposAsignadosService : IInventariosAlmacenesPuntoTomaEquiposAsignadosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(InventariosAlmacenesPuntoTomaEquiposAsignadosService);
    private readonly IInventariosAlmacenesPuntoTomaEquiposAsignadosRepository _repo;
    private readonly ErrorLogService _log;
    public InventariosAlmacenesPuntoTomaEquiposAsignadosService(IInventariosAlmacenesPuntoTomaEquiposAsignadosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>>> GetAllAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario)
    {
        try { var d = (await _repo.ShowallAsync(numInventario, codAlmacen, numPuntoTomaInventario)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<InventariosAlmacenesPuntoTomaEquiposAsignados>> GetByIdAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, string codProductoEquipo)
    {
        try { var d = await _repo.ShowbyidAsync(numInventario, codAlmacen, numPuntoTomaInventario, codProductoEquipo);
              return d is null ? ApiResponse<InventariosAlmacenesPuntoTomaEquiposAsignados>.Fail("No encontrado.") : ApiResponse<InventariosAlmacenesPuntoTomaEquiposAsignados>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<InventariosAlmacenesPuntoTomaEquiposAsignados>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>>> GetComboboxAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario)
    {
        try { var d = (await _repo.ShowcomboxAsync(numInventario, codAlmacen, numPuntoTomaInventario)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>>> SearchAsync(InventariosAlmacenesPuntoTomaEquiposAsignadosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaEquiposAsignados>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(InventariosAlmacenesPuntoTomaEquiposAsignadosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Equipo asignado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(InventariosAlmacenesPuntoTomaEquiposAsignadosUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, string codProductoEquipo)
    {
        try { var r = await _repo.DeleteAsync(numInventario, codAlmacen, numPuntoTomaInventario, codProductoEquipo);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Equipo quitado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
