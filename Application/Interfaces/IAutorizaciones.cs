using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAutorizacionesRepository
{
    Task<IEnumerable<Autorizaciones>> ShowallAsync();
    Task<Autorizaciones?> ShowbyidAsync(int NUM_AUTORIZACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Autorizaciones>> SearchAsync(AutorizacionesSearchDto dto);
    Task<WriteResult> InsertAsync(AutorizacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(AutorizacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_AUTORIZACION);
}

public interface IAutorizacionesService
{
    Task<ApiResponse<IEnumerable<Autorizaciones>>> GetAllAsync();
    Task<ApiResponse<Autorizaciones>> GetByIdAsync(int NUM_AUTORIZACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Autorizaciones>>> SearchAsync(AutorizacionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AutorizacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(AutorizacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_AUTORIZACION);
}