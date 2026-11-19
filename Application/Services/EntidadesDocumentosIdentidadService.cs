using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class EntidadesDocumentosIdentidadService : IEntidadesDocumentosIdentidadService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesDocumentosIdentidadService);
    private readonly IEntidadesDocumentosIdentidadRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesDocumentosIdentidadService(IEntidadesDocumentosIdentidadRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadesDocumentosIdentidad>>> GetAllAsync(decimal COD_ENTIDAD)
    {
        try { var d = (await _repo.ShowallAsync(COD_ENTIDAD)).ToList();
              return ApiResponse<IEnumerable<EntidadesDocumentosIdentidad>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EntidadesDocumentosIdentidad>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesDocumentosIdentidad>> GetByIdAsync(decimal COD_ENTIDAD, int COD_TIPO_DOCUMENTO_IDENTIDAD)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ENTIDAD, COD_TIPO_DOCUMENTO_IDENTIDAD);
              return d is null ? ApiResponse<EntidadesDocumentosIdentidad>.Fail("No encontrado.") : ApiResponse<EntidadesDocumentosIdentidad>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EntidadesDocumentosIdentidad>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadesDocumentosIdentidad>>> SearchAsync(EntidadesDocumentosIdentidadSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EntidadesDocumentosIdentidad>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EntidadesDocumentosIdentidad>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EntidadesDocumentosIdentidadInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesDocumentosIdentidadUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, int COD_TIPO_DOCUMENTO_IDENTIDAD)
    {
        try { var r = await _repo.DeleteAsync(COD_ENTIDAD, COD_TIPO_DOCUMENTO_IDENTIDAD);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}