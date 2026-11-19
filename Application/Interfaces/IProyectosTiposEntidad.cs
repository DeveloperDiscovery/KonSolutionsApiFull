using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosTiposEntidadRepository
{
    Task<IEnumerable<ProyectosTiposEntidad>> ShowallAsync();
    Task<ProyectosTiposEntidad?> ShowbyidAsync(int NUM_PROYECTO, string COD_TIPO_ENTIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosTiposEntidad>> SearchAsync(ProyectosTiposEntidadSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosTiposEntidadInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROYECTO, string COD_TIPO_ENTIDAD);
}

public interface IProyectosTiposEntidadService
{
    Task<ApiResponse<IEnumerable<ProyectosTiposEntidad>>> GetAllAsync();
    Task<ApiResponse<ProyectosTiposEntidad>> GetByIdAsync(int NUM_PROYECTO, string COD_TIPO_ENTIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosTiposEntidad>>> SearchAsync(ProyectosTiposEntidadSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosTiposEntidadInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, string COD_TIPO_ENTIDAD);
}