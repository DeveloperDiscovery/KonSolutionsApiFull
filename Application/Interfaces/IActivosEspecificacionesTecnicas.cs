using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Activos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IActivosEspecificacionesTecnicasRepository
{
    Task<IEnumerable<ActivosEspecificacionesTecnicas>> ShowallAsync();
    Task<ActivosEspecificacionesTecnicas?> ShowbyidAsync(string COD_ACTIVO, string COD_ESPECIFICACION_TECNICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ActivosEspecificacionesTecnicas>> SearchAsync(ActivosEspecificacionesTecnicasSearchDto dto);
    Task<WriteResult> InsertAsync(ActivosEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ActivosEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ACTIVO, string COD_ESPECIFICACION_TECNICA);
}

public interface IActivosEspecificacionesTecnicasService
{
    Task<ApiResponse<IEnumerable<ActivosEspecificacionesTecnicas>>> GetAllAsync();
    Task<ApiResponse<ActivosEspecificacionesTecnicas>> GetByIdAsync(string COD_ACTIVO, string COD_ESPECIFICACION_TECNICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ActivosEspecificacionesTecnicas>>> SearchAsync(ActivosEspecificacionesTecnicasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ActivosEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ActivosEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ACTIVO, string COD_ESPECIFICACION_TECNICA);
}