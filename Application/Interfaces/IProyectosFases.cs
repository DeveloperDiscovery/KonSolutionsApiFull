using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosFasesRepository
{
    Task<IEnumerable<ProyectosFases>> ShowallAsync();
    Task<ProyectosFases?> ShowbyidAsync(int NUM_PROYECTO, int COD_FASE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosFases>> SearchAsync(ProyectosFasesSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosFasesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProyectosFasesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int COD_FASE);
}

public interface IProyectosFasesService
{
    Task<ApiResponse<IEnumerable<ProyectosFases>>> GetAllAsync();
    Task<ApiResponse<ProyectosFases>> GetByIdAsync(int NUM_PROYECTO, int COD_FASE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosFases>>> SearchAsync(ProyectosFasesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosFasesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosFasesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, int COD_FASE);
}