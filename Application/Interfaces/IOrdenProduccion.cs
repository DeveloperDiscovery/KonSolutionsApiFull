using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IOrdenProduccionRepository
{
    Task<IEnumerable<OrdenProduccion>> ShowallAsync();
    Task<OrdenProduccion?> ShowbyidAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ComboboxItem>> ShowcomboxPorProcesoAsync(string codProceso);
    Task<IEnumerable<OrdenProduccion>> SearchAsync(OrdenProduccionSearchDto dto);
    Task<WriteResult> InsertAsync(OrdenProduccionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(OrdenProduccionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION);
}

public interface IOrdenProduccionService
{
    Task<ApiResponse<IEnumerable<OrdenProduccion>>> GetAllAsync();
    Task<ApiResponse<OrdenProduccion>> GetByIdAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxPorProcesoAsync(string codProceso);
    Task<ApiResponse<IEnumerable<OrdenProduccion>>> SearchAsync(OrdenProduccionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(OrdenProduccionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(OrdenProduccionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION);
}