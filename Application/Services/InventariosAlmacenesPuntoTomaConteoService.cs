using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class InventariosAlmacenesPuntoTomaConteoService : IInventariosAlmacenesPuntoTomaConteoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(InventariosAlmacenesPuntoTomaConteoService);
    private readonly IInventariosAlmacenesPuntoTomaConteoRepository _repo;
    private readonly ErrorLogService _log;
    public InventariosAlmacenesPuntoTomaConteoService(IInventariosAlmacenesPuntoTomaConteoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaConteo>>> GetAllAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario)
    {
        try { var d = (await _repo.ShowallAsync(numInventario, codAlmacen, numPuntoTomaInventario)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaConteo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaConteo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<InventariosAlmacenesPuntoTomaConteo>> GetByIdAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, int numSecuencia)
    {
        try { var d = await _repo.ShowbyidAsync(numInventario, codAlmacen, numPuntoTomaInventario, numSecuencia);
              return d is null ? ApiResponse<InventariosAlmacenesPuntoTomaConteo>.Fail("No encontrado.") : ApiResponse<InventariosAlmacenesPuntoTomaConteo>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<InventariosAlmacenesPuntoTomaConteo>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaConteo>>> GetComboboxAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario)
    {
        try { var d = (await _repo.ShowcomboxAsync(numInventario, codAlmacen, numPuntoTomaInventario)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaConteo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaConteo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaConteo>>> SearchAsync(InventariosAlmacenesPuntoTomaConteoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaConteo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<InventariosAlmacenesPuntoTomaConteo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(InventariosAlmacenesPuntoTomaConteoInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(InventariosAlmacenesPuntoTomaConteoUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int numInventario, string codAlmacen, byte numPuntoTomaInventario, int numSecuencia)
    {
        try { var r = await _repo.DeleteAsync(numInventario, codAlmacen, numPuntoTomaInventario, numSecuencia);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<InventariosAlmacenesPuntoTomaReadRfidCodebarResult>> ReadRfidCodebarAsync(string? codStickerUbicacion, string? codStickerRfid)
    {
        try { var d = await _repo.ReadRfidCodebarAsync(codStickerUbicacion, codStickerRfid);
              return d is null ? ApiResponse<InventariosAlmacenesPuntoTomaReadRfidCodebarResult>.Fail("No se pudo resolver la lectura.") : ApiResponse<InventariosAlmacenesPuntoTomaReadRfidCodebarResult>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ReadRfidCodebarAsync));
              return ApiResponse<InventariosAlmacenesPuntoTomaReadRfidCodebarResult>.Fail(ex.Message); }
    }
}
