using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosRolesRepository
{
    Task<IEnumerable<ProyectosRoles>> ShowallAsync();
    Task<ProyectosRoles?> ShowbyidAsync(string COD_ROL_TAREA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ComboboxItem>> CategoriasComboAsync();
    Task<IEnumerable<ProyectosRoles>> SearchAsync(ProyectosRolesSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosRolesInsertDto dto);
    Task<WriteResult> UpdateAsync(ProyectosRolesUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_ROL_TAREA);
}

public interface IProyectosRolesService
{
    Task<ApiResponse<IEnumerable<ProyectosRoles>>> GetAllAsync();
    Task<ApiResponse<ProyectosRoles>> GetByIdAsync(string COD_ROL_TAREA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetCategoriasComboAsync();
    Task<ApiResponse<IEnumerable<ProyectosRoles>>> SearchAsync(ProyectosRolesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosRolesInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosRolesUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_TAREA);
}