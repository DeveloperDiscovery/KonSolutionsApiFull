using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProduccionRutasOperacionesEstructuraRepository
{
    Task<IEnumerable<ProduccionRutasOperacionesEstructura>> ShowallAsync();
    Task<ProduccionRutasOperacionesEstructura?> ShowbyidAsync(int COD_RUTA_PRODUCCION, int NUM_ESTRUCTURA_RUTA_PROCESO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProduccionRutasOperacionesEstructura>> SearchAsync(ProduccionRutasOperacionesEstructuraSearchDto dto);
    Task<WriteResult> InsertAsync(ProduccionRutasOperacionesEstructuraInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProduccionRutasOperacionesEstructuraUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_RUTA_PRODUCCION, int NUM_ESTRUCTURA_RUTA_PROCESO);
}

public interface IProduccionRutasOperacionesEstructuraService
{
    Task<ApiResponse<IEnumerable<ProduccionRutasOperacionesEstructura>>> GetAllAsync();
    Task<ApiResponse<ProduccionRutasOperacionesEstructura>> GetByIdAsync(int COD_RUTA_PRODUCCION, int NUM_ESTRUCTURA_RUTA_PROCESO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProduccionRutasOperacionesEstructura>>> SearchAsync(ProduccionRutasOperacionesEstructuraSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProduccionRutasOperacionesEstructuraInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProduccionRutasOperacionesEstructuraUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_RUTA_PRODUCCION, int NUM_ESTRUCTURA_RUTA_PROCESO);
}