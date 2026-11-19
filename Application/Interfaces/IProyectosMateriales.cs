using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosMaterialesRepository
{
    Task<IEnumerable<ProyectosMateriales>> ShowallAsync();
    Task<ProyectosMateriales?> ShowbyidAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosMateriales>> SearchAsync(ProyectosMaterialesSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosMaterialesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProyectosMaterialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
}

public interface IProyectosMaterialesService
{
    Task<ApiResponse<IEnumerable<ProyectosMateriales>>> GetAllAsync();
    Task<ApiResponse<ProyectosMateriales>> GetByIdAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosMateriales>>> SearchAsync(ProyectosMaterialesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosMaterialesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosMaterialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
}