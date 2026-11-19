using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProduccionRutasRepository
{
    Task<IEnumerable<ProduccionRutas>> ShowallAsync();
    Task<ProduccionRutas?> ShowbyidAsync(int COD_RUTA_PRODUCCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProduccionRutas>> SearchAsync(ProduccionRutasSearchDto dto);
    Task<WriteResult> InsertAsync(ProduccionRutasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_RUTA_PRODUCCION);
}

public interface IProduccionRutasService
{
    Task<ApiResponse<IEnumerable<ProduccionRutas>>> GetAllAsync();
    Task<ApiResponse<ProduccionRutas>> GetByIdAsync(int COD_RUTA_PRODUCCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProduccionRutas>>> SearchAsync(ProduccionRutasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProduccionRutasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_RUTA_PRODUCCION);
}