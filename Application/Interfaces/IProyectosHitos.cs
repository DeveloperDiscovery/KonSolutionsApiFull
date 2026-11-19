using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosHitosRepository
{
    Task<IEnumerable<ProyectosHitos>> ShowallAsync();
    Task<ProyectosHitos?> ShowbyidAsync(int NUM_PROYECTO, int COD_HITO_PROYECTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosHitos>> SearchAsync(ProyectosHitosSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosHitosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProyectosHitosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int COD_HITO_PROYECTO);
}

public interface IProyectosHitosService
{
    Task<ApiResponse<IEnumerable<ProyectosHitos>>> GetAllAsync();
    Task<ApiResponse<ProyectosHitos>> GetByIdAsync(int NUM_PROYECTO, int COD_HITO_PROYECTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosHitos>>> SearchAsync(ProyectosHitosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosHitosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosHitosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, int COD_HITO_PROYECTO);
}