using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEspecificacionesTecnicasRepository
{
    Task<IEnumerable<EspecificacionesTecnicas>> ShowallAsync();
    Task<EspecificacionesTecnicas?> ShowbyidAsync(string COD_ESPECIFICACION_TECNICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EspecificacionesTecnicas>> SearchAsync(EspecificacionesTecnicasSearchDto dto);
    Task<WriteResult> InsertAsync(EspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ESPECIFICACION_TECNICA);
    Task<EspecificacionesTecnicasLoadCombosResult> LoadcombosAsync();
}

public interface IEspecificacionesTecnicasService
{
    Task<ApiResponse<IEnumerable<EspecificacionesTecnicas>>> GetAllAsync();
    Task<ApiResponse<EspecificacionesTecnicas>> GetByIdAsync(string COD_ESPECIFICACION_TECNICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EspecificacionesTecnicas>>> SearchAsync(EspecificacionesTecnicasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ESPECIFICACION_TECNICA);
    Task<ApiResponse<EspecificacionesTecnicasLoadCombosResult>> GetLoadcombosAsync();
}
