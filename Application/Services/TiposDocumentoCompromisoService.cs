using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Services;

public class TiposDocumentoCompromisoService : ITiposDocumentoCompromisoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposDocumentoCompromisoService);
    private readonly ITiposDocumentoCompromisoRepository _repo;
    private readonly ErrorLogService _log;
    public TiposDocumentoCompromisoService(ITiposDocumentoCompromisoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    private async Task<ApiResponse<T>> Corre<T>(Func<Task<ApiResponse<T>>> f, string metodo)
    {
        try { return await f(); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    public Task<ApiResponse<IEnumerable<TiposDocumentoCompromiso>>> GetAllAsync() => Corre(async () =>
    { var d = (await _repo.ShowallAsync()).ToList(); return ApiResponse<IEnumerable<TiposDocumentoCompromiso>>.Ok(d, total: d.Count); }, nameof(GetAllAsync));

    public Task<ApiResponse<TiposDocumentoCompromiso>> GetByIdAsync(string cod) => Corre(async () =>
    { var d = await _repo.ShowbyidAsync(cod); return d is null ? ApiResponse<TiposDocumentoCompromiso>.Fail("No encontrado.") : ApiResponse<TiposDocumentoCompromiso>.Ok(d); }, nameof(GetByIdAsync));

    public Task<ApiResponse<IEnumerable<TiposDocumentoCompromiso>>> SearchAsync(TiposDocumentoCompromisoSearchDto dto) => Corre(async () =>
    { var d = (await _repo.SearchAsync(dto)).ToList(); return ApiResponse<IEnumerable<TiposDocumentoCompromiso>>.Ok(d, total: d.Count); }, nameof(SearchAsync));

    private static ApiResponse<WriteResult> De(WriteResult r) => r.Estado == "ERROR" ? ApiResponse<WriteResult>.Fail(r.Mensaje ?? "Error") : ApiResponse<WriteResult>.Ok(r);

    public Task<ApiResponse<WriteResult>> InsertAsync(TiposDocumentoCompromisoInsertDto dto, string codUsuario, string codEstacion) => Corre(async () => De(await _repo.InsertAsync(dto, codUsuario, codEstacion)), nameof(InsertAsync));
    public Task<ApiResponse<WriteResult>> UpdateAsync(TiposDocumentoCompromisoUpdateDto dto) => Corre(async () => De(await _repo.UpdateAsync(dto)), nameof(UpdateAsync));
    public Task<ApiResponse<WriteResult>> DeleteAsync(string cod) => Corre(async () => De(await _repo.DeleteAsync(cod)), nameof(DeleteAsync));
}
