using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Activos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IActivosEntidadesRepository
{
    Task<IEnumerable<ActivosEntidades>> ShowallAsync();
    Task<ActivosEntidades?> ShowbyidAsync(string COD_ACTIVO, decimal COD_ENTIDAD, DateTime FEC_ASIGNACION_INICIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ActivosEntidades>> SearchAsync(ActivosEntidadesSearchDto dto);
    Task<WriteResult> InsertAsync(ActivosEntidadesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ActivosEntidadesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ACTIVO, decimal COD_ENTIDAD, DateTime FEC_ASIGNACION_INICIO);
}

public interface IActivosEntidadesService
{
    Task<ApiResponse<IEnumerable<ActivosEntidades>>> GetAllAsync();
    Task<ApiResponse<ActivosEntidades>> GetByIdAsync(string COD_ACTIVO, decimal COD_ENTIDAD, DateTime FEC_ASIGNACION_INICIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ActivosEntidades>>> SearchAsync(ActivosEntidadesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ActivosEntidadesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ActivosEntidadesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ACTIVO, decimal COD_ENTIDAD, DateTime FEC_ASIGNACION_INICIO);
}