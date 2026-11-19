using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRegistroProduccionRepository
{
    Task<IEnumerable<RegistroProduccion>> ShowallAsync();
    Task<RegistroProduccion?> ShowbyidAsync(int NUM_REGISTRO_PRODUCCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<RegistroProduccion>> SearchAsync(RegistroProduccionSearchDto dto);
    Task<WriteResult> InsertAsync(RegistroProduccionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(RegistroProduccionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_REGISTRO_PRODUCCION);
}

public interface IRegistroProduccionService
{
    Task<ApiResponse<IEnumerable<RegistroProduccion>>> GetAllAsync();
    Task<ApiResponse<RegistroProduccion>> GetByIdAsync(int NUM_REGISTRO_PRODUCCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<RegistroProduccion>>> SearchAsync(RegistroProduccionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RegistroProduccionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(RegistroProduccionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_REGISTRO_PRODUCCION);
}