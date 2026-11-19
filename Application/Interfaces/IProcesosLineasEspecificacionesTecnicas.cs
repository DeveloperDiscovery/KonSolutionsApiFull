using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Activos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProcesosLineasEspecificacionesTecnicasRepository
{
    Task<IEnumerable<ProcesosLineasEspecificacionesTecnicas>> ShowallAsync();
    Task<ProcesosLineasEspecificacionesTecnicas?> ShowbyidAsync(string COD_PROCESO, string COD_LINEA_PRODUCCION, string COD_ESPECIFICACION_TECNICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProcesosLineasEspecificacionesTecnicas>> SearchAsync(ProcesosLineasEspecificacionesTecnicasSearchDto dto);
    Task<WriteResult> InsertAsync(ProcesosLineasEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProcesosLineasEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PROCESO, string COD_LINEA_PRODUCCION, string COD_ESPECIFICACION_TECNICA);
}

public interface IProcesosLineasEspecificacionesTecnicasService
{
    Task<ApiResponse<IEnumerable<ProcesosLineasEspecificacionesTecnicas>>> GetAllAsync();
    Task<ApiResponse<ProcesosLineasEspecificacionesTecnicas>> GetByIdAsync(string COD_PROCESO, string COD_LINEA_PRODUCCION, string COD_ESPECIFICACION_TECNICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProcesosLineasEspecificacionesTecnicas>>> SearchAsync(ProcesosLineasEspecificacionesTecnicasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProcesosLineasEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProcesosLineasEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PROCESO, string COD_LINEA_PRODUCCION, string COD_ESPECIFICACION_TECNICA);
}