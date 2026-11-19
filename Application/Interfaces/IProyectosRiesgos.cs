using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosRiesgosRepository
{
    Task<IEnumerable<ProyectosRiesgos>> ShowallAsync();
    Task<ProyectosRiesgos?> ShowbyidAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosRiesgos>> SearchAsync(ProyectosRiesgosSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosRiesgosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProyectosRiesgosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
}

public interface IProyectosRiesgosService
{
    Task<ApiResponse<IEnumerable<ProyectosRiesgos>>> GetAllAsync();
    Task<ApiResponse<ProyectosRiesgos>> GetByIdAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosRiesgos>>> SearchAsync(ProyectosRiesgosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosRiesgosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosRiesgosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
}