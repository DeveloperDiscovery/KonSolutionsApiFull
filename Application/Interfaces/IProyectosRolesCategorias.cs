using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosRolesCategoriasRepository
{
    Task<IEnumerable<ProyectosRolesCategorias>> ShowallAsync();
    Task<ProyectosRolesCategorias?> ShowbyidAsync(string cod);
    Task<IEnumerable<ProyectosRolesCategorias>> SearchAsync(ProyectosRolesCategoriasSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosRolesCategoriasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProyectosRolesCategoriasUpdateDto dto);
    Task<WriteResult> DeleteAsync(string cod);
}

public interface IProyectosRolesCategoriasService
{
    Task<ApiResponse<IEnumerable<ProyectosRolesCategorias>>> GetAllAsync();
    Task<ApiResponse<ProyectosRolesCategorias>> GetByIdAsync(string cod);
    Task<ApiResponse<IEnumerable<ProyectosRolesCategorias>>> SearchAsync(ProyectosRolesCategoriasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosRolesCategoriasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosRolesCategoriasUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string cod);
}
