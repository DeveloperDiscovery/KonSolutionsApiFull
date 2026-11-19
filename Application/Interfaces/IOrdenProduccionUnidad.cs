using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IOrdenProduccionUnidadRepository
{
    Task<IEnumerable<OrdenProduccionUnidad>> ShowallAsync();
    Task<OrdenProduccionUnidad?> ShowbyidAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ComboboxItem>> ShowcomboxPorItemAsync(string codProceso, int numOrdenProduccion, int numOrdenProduccionItem);
    Task<IEnumerable<OrdenProduccionUnidad>> SearchAsync(OrdenProduccionUnidadSearchDto dto);
    Task<WriteResult> InsertAsync(OrdenProduccionUnidadInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(OrdenProduccionUnidadUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
}

public interface IOrdenProduccionUnidadService
{
    Task<ApiResponse<IEnumerable<OrdenProduccionUnidad>>> GetAllAsync();
    Task<ApiResponse<OrdenProduccionUnidad>> GetByIdAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxPorItemAsync(string codProceso, int numOrdenProduccion, int numOrdenProduccionItem);
    Task<ApiResponse<IEnumerable<OrdenProduccionUnidad>>> SearchAsync(OrdenProduccionUnidadSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(OrdenProduccionUnidadInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(OrdenProduccionUnidadUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
}