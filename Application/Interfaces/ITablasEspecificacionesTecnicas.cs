using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITablasEspecificacionesTecnicasRepository
{
    Task<IEnumerable<TablasEspecificacionesTecnicas>> ShowallAsync();
    Task<TablasEspecificacionesTecnicas?> ShowbyidAsync(string COD_TABLA_ESPECIFICACION_TECNICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TablasEspecificacionesTecnicas>> SearchAsync(TablasEspecificacionesTecnicasSearchDto dto);
    Task<WriteResult> InsertAsync(TablasEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TablasEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TABLA_ESPECIFICACION_TECNICA);
}

public interface ITablasEspecificacionesTecnicasService
{
    Task<ApiResponse<IEnumerable<TablasEspecificacionesTecnicas>>> GetAllAsync();
    Task<ApiResponse<TablasEspecificacionesTecnicas>> GetByIdAsync(string COD_TABLA_ESPECIFICACION_TECNICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TablasEspecificacionesTecnicas>>> SearchAsync(TablasEspecificacionesTecnicasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TablasEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TablasEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TABLA_ESPECIFICACION_TECNICA);
}