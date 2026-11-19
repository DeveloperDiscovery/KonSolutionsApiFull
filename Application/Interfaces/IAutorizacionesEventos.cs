using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAutorizacionesEventosRepository
{
    Task<IEnumerable<AutorizacionesEventos>> ShowallAsync();
    Task<AutorizacionesEventos?> ShowbyidAsync(int NUM_AUTORIZACION, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<AutorizacionesEventos>> SearchAsync(AutorizacionesEventosSearchDto dto);
    Task<WriteResult> InsertAsync(AutorizacionesEventosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_AUTORIZACION, int NUM_SECUENCIA);
}

public interface IAutorizacionesEventosService
{
    Task<ApiResponse<IEnumerable<AutorizacionesEventos>>> GetAllAsync();
    Task<ApiResponse<AutorizacionesEventos>> GetByIdAsync(int NUM_AUTORIZACION, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<AutorizacionesEventos>>> SearchAsync(AutorizacionesEventosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AutorizacionesEventosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_AUTORIZACION, int NUM_SECUENCIA);
}