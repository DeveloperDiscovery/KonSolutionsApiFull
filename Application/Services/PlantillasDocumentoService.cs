using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Services;

public class PlantillasDocumentoService : IPlantillasDocumentoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PlantillasDocumentoService);
    private readonly IPlantillasDocumentoRepository _repo;
    private readonly ErrorLogService _log;
    public PlantillasDocumentoService(IPlantillasDocumentoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    private async Task<ApiResponse<T>> Corre<T>(Func<Task<ApiResponse<T>>> f, string metodo)
    {
        try { return await f(); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    public Task<ApiResponse<IEnumerable<PlantillaDocumento>>> GetAllAsync() => Corre(async () =>
    { var d = (await _repo.ShowallAsync()).ToList(); return ApiResponse<IEnumerable<PlantillaDocumento>>.Ok(d, total: d.Count); }, nameof(GetAllAsync));

    public Task<ApiResponse<PlantillaDocumento>> GetByIdAsync(string cod) => Corre(async () =>
    { var d = await _repo.ShowbyidAsync(cod); return d is null ? ApiResponse<PlantillaDocumento>.Fail("No encontrada.") : ApiResponse<PlantillaDocumento>.Ok(d); }, nameof(GetByIdAsync));

    public Task<ApiResponse<IEnumerable<PlantillaDocumento>>> SearchAsync(PlantillaDocumentoSearchDto dto) => Corre(async () =>
    { var d = (await _repo.SearchAsync(dto)).ToList(); return ApiResponse<IEnumerable<PlantillaDocumento>>.Ok(d, total: d.Count); }, nameof(SearchAsync));

    public Task<ApiResponse<IEnumerable<ComboboxItem>>> ComboAsync() => Corre(async () =>
    { var d = (await _repo.ComboAsync()).ToList(); return ApiResponse<IEnumerable<ComboboxItem>>.Ok(d, total: d.Count); }, nameof(ComboAsync));

    private static ApiResponse<WriteResult> De(WriteResult r) => r.Estado == "ERROR" ? ApiResponse<WriteResult>.Fail(r.Mensaje ?? "Error") : ApiResponse<WriteResult>.Ok(r);

    public Task<ApiResponse<WriteResult>> InsertAsync(PlantillaDocumentoDto dto, string codUsuario, string codEstacion) => Corre(async () => De(await _repo.InsertAsync(dto, codUsuario, codEstacion)), nameof(InsertAsync));
    public Task<ApiResponse<WriteResult>> UpdateAsync(PlantillaDocumentoDto dto, string codUsuario, string codEstacion) => Corre(async () => De(await _repo.UpdateAsync(dto, codUsuario, codEstacion)), nameof(UpdateAsync));
    public Task<ApiResponse<WriteResult>> DeleteAsync(string cod) => Corre(async () => De(await _repo.DeleteAsync(cod)), nameof(DeleteAsync));
}
