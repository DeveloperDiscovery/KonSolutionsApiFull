using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Interfaces;

public interface IOrdenProduccionUnidadEspecificacionesRepository
{
    Task<IEnumerable<OrdenProduccionUnidadEspecificaciones>> ShowallAsync();
    Task<OrdenProduccionUnidadEspecificaciones?> ShowbyidAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<OrdenProduccionUnidadEspecificaciones>> SearchAsync(OrdenProduccionUnidadEspecificacionesSearchDto dto);
    Task<WriteResult> InsertAsync(OrdenProduccionUnidadEspecificacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(OrdenProduccionUnidadEspecificacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
}

public interface IOrdenProduccionUnidadEspecificacionesService
{
    Task<ApiResponse<IEnumerable<OrdenProduccionUnidadEspecificaciones>>> GetAllAsync();
    Task<ApiResponse<OrdenProduccionUnidadEspecificaciones>> GetByIdAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<OrdenProduccionUnidadEspecificaciones>>> SearchAsync(OrdenProduccionUnidadEspecificacionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(OrdenProduccionUnidadEspecificacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(OrdenProduccionUnidadEspecificacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
}