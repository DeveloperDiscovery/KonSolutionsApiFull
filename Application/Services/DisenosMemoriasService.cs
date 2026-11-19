using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class DisenosMemoriasService : IDisenosMemoriasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(DisenosMemoriasService);
    private readonly IDisenosMemoriasRepository _repo;
    private readonly ErrorLogService _log;
    public DisenosMemoriasService(IDisenosMemoriasRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<DisenosMemorias>>> GetAllAsync(string codDiseno)
    {
        try { var d = (await _repo.ShowallAsync(codDiseno)).ToList();
              return ApiResponse<IEnumerable<DisenosMemorias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<DisenosMemorias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<DisenosMemorias>> GetByIdAsync(string codDiseno, short numSecuencia)
    {
        try { var d = await _repo.ShowbyidAsync(codDiseno, numSecuencia);
              return d is null ? ApiResponse<DisenosMemorias>.Fail("No encontrado.") : ApiResponse<DisenosMemorias>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<DisenosMemorias>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<DisenosMemorias>>> SearchAsync(DisenosMemoriasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<DisenosMemorias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<DisenosMemorias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(DisenosMemoriasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Memoria guardada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codDiseno, short numSecuencia)
    {
        try { var r = await _repo.DeleteAsync(codDiseno, numSecuencia);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Memoria eliminada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}

public class DisenosVersionesMemoriasService : IDisenosVersionesMemoriasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(DisenosVersionesMemoriasService);
    private readonly IDisenosVersionesMemoriasRepository _repo;
    private readonly ErrorLogService _log;
    public DisenosVersionesMemoriasService(IDisenosVersionesMemoriasRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<DisenosVersionesMemorias>>> GetAllAsync(string codDiseno, short numVersion)
    {
        try { var d = (await _repo.ShowallAsync(codDiseno, numVersion)).ToList();
              return ApiResponse<IEnumerable<DisenosVersionesMemorias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<DisenosVersionesMemorias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<DisenosVersionesMemorias>> GetByIdAsync(string codDiseno, short numVersion, short numSecuencia)
    {
        try { var d = await _repo.ShowbyidAsync(codDiseno, numVersion, numSecuencia);
              return d is null ? ApiResponse<DisenosVersionesMemorias>.Fail("No encontrado.") : ApiResponse<DisenosVersionesMemorias>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<DisenosVersionesMemorias>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<DisenosVersionesMemorias>>> SearchAsync(DisenosVersionesMemoriasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<DisenosVersionesMemorias>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<DisenosVersionesMemorias>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(DisenosVersionesMemoriasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Memoria guardada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codDiseno, short numVersion, short numSecuencia)
    {
        try { var r = await _repo.DeleteAsync(codDiseno, numVersion, numSecuencia);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Memoria eliminada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
