using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposEspecificacionTecnicaRepository
{
    Task<IEnumerable<TiposEspecificacionTecnica>> ShowallAsync();
    Task<TiposEspecificacionTecnica?> ShowbyidAsync(string COD_TIPO_ESPECIFICACION_TECNICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposEspecificacionTecnica>> SearchAsync(TiposEspecificacionTecnicaSearchDto dto);
    Task<WriteResult> InsertAsync(TiposEspecificacionTecnicaInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposEspecificacionTecnicaUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_ESPECIFICACION_TECNICA);
}

public interface ITiposEspecificacionTecnicaService
{
    Task<ApiResponse<IEnumerable<TiposEspecificacionTecnica>>> GetAllAsync();
    Task<ApiResponse<TiposEspecificacionTecnica>> GetByIdAsync(string COD_TIPO_ESPECIFICACION_TECNICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposEspecificacionTecnica>>> SearchAsync(TiposEspecificacionTecnicaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposEspecificacionTecnicaInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposEspecificacionTecnicaUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_ESPECIFICACION_TECNICA);
}