using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosMemoriaRepository
{
    Task<IEnumerable<ProyectosMemoria>> ShowallAsync();
    Task<ProyectosMemoria?> ShowbyidAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosMemoria>> SearchAsync(ProyectosMemoriaSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosMemoriaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProyectosMemoriaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
}

public interface IProyectosMemoriaService
{
    Task<ApiResponse<IEnumerable<ProyectosMemoria>>> GetAllAsync();
    Task<ApiResponse<ProyectosMemoria>> GetByIdAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosMemoria>>> SearchAsync(ProyectosMemoriaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosMemoriaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosMemoriaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
}