using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposProyectoRepository
{
    Task<IEnumerable<TiposProyecto>> ShowallAsync();
    Task<TiposProyecto?> ShowbyidAsync(string COD_TIPO_PROYECTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposProyecto>> SearchAsync(TiposProyectoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposProyectoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposProyectoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_PROYECTO);
}

public interface ITiposProyectoService
{
    Task<ApiResponse<IEnumerable<TiposProyecto>>> GetAllAsync();
    Task<ApiResponse<TiposProyecto>> GetByIdAsync(string COD_TIPO_PROYECTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposProyecto>>> SearchAsync(TiposProyectoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposProyectoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposProyectoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_PROYECTO);
}
