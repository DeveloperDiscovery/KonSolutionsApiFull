using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRegistroProduccionTiposOperacionRepository
{
    Task<IEnumerable<RegistroProduccionTiposOperacion>> ShowallAsync();
    Task<RegistroProduccionTiposOperacion?> ShowbyidAsync(int NUM_REGISTRO_PRODUCCION, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<RegistroProduccionTiposOperacion>> SearchAsync(RegistroProduccionTiposOperacionSearchDto dto);
    Task<WriteResult> InsertAsync(RegistroProduccionTiposOperacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(RegistroProduccionTiposOperacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_REGISTRO_PRODUCCION, int NUM_SECUENCIA);
}

public interface IRegistroProduccionTiposOperacionService
{
    Task<ApiResponse<IEnumerable<RegistroProduccionTiposOperacion>>> GetAllAsync();
    Task<ApiResponse<RegistroProduccionTiposOperacion>> GetByIdAsync(int NUM_REGISTRO_PRODUCCION, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<RegistroProduccionTiposOperacion>>> SearchAsync(RegistroProduccionTiposOperacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RegistroProduccionTiposOperacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(RegistroProduccionTiposOperacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_REGISTRO_PRODUCCION, int NUM_SECUENCIA);
}