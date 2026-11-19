using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Services;

public class ProyectosRolesCategoriasService : IProyectosRolesCategoriasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ProyectosRolesCategoriasService);
    private readonly IProyectosRolesCategoriasRepository _repo;
    private readonly ErrorLogService _log;
    public ProyectosRolesCategoriasService(IProyectosRolesCategoriasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    private async Task<ApiResponse<T>> Corre<T>(Func<Task<ApiResponse<T>>> f, string metodo)
    {
        try { return await f(); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    public Task<ApiResponse<IEnumerable<ProyectosRolesCategorias>>> GetAllAsync() => Corre(async () =>
    { var d = (await _repo.ShowallAsync()).ToList(); return ApiResponse<IEnumerable<ProyectosRolesCategorias>>.Ok(d, total: d.Count); }, nameof(GetAllAsync));

    public Task<ApiResponse<ProyectosRolesCategorias>> GetByIdAsync(string cod) => Corre(async () =>
    { var d = await _repo.ShowbyidAsync(cod); return d is null ? ApiResponse<ProyectosRolesCategorias>.Fail("No encontrado.") : ApiResponse<ProyectosRolesCategorias>.Ok(d); }, nameof(GetByIdAsync));

    public Task<ApiResponse<IEnumerable<ProyectosRolesCategorias>>> SearchAsync(ProyectosRolesCategoriasSearchDto dto) => Corre(async () =>
    { var d = (await _repo.SearchAsync(dto)).ToList(); return ApiResponse<IEnumerable<ProyectosRolesCategorias>>.Ok(d, total: d.Count); }, nameof(SearchAsync));

    private static ApiResponse<WriteResult> De(WriteResult r) => r.Estado == "ERROR" ? ApiResponse<WriteResult>.Fail(r.Mensaje ?? "Error") : ApiResponse<WriteResult>.Ok(r);

    public Task<ApiResponse<WriteResult>> InsertAsync(ProyectosRolesCategoriasInsertDto dto, string codUsuario, string codEstacion) => Corre(async () => De(await _repo.InsertAsync(dto, codUsuario, codEstacion)), nameof(InsertAsync));
    public Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosRolesCategoriasUpdateDto dto) => Corre(async () => De(await _repo.UpdateAsync(dto)), nameof(UpdateAsync));
    public Task<ApiResponse<WriteResult>> DeleteAsync(string cod) => Corre(async () => De(await _repo.DeleteAsync(cod)), nameof(DeleteAsync));
}
