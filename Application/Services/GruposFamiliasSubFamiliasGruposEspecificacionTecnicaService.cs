using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class GruposFamiliasSubFamiliasGruposEspecificacionTecnicaService : IGruposFamiliasSubFamiliasGruposEspecificacionTecnicaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(GruposFamiliasSubFamiliasGruposEspecificacionTecnicaService);
    private readonly IGruposFamiliasSubFamiliasGruposEspecificacionTecnicaRepository _repo;
    private readonly ErrorLogService _log;
    public GruposFamiliasSubFamiliasGruposEspecificacionTecnicaService(IGruposFamiliasSubFamiliasGruposEspecificacionTecnicaRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>> GetByIdAsync(int COD_GRUPO_ESPECIFICACION_TECNICA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_GRUPO_ESPECIFICACION_TECNICA);
              return d is null ? ApiResponse<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>.Fail("No encontrado.") : ApiResponse<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>>> SearchAsync(GruposFamiliasSubFamiliasGruposEspecificacionTecnicaSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(GruposFamiliasSubFamiliasGruposEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int COD_GRUPO_ESPECIFICACION_TECNICA)
    {
        try { var r = await _repo.DeleteAsync(COD_GRUPO_ESPECIFICACION_TECNICA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}