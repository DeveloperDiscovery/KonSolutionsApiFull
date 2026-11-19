using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAplicacionesRepository
{
    Task<IEnumerable<Aplicaciones>> ShowallAsync();
    Task<Aplicaciones?> ShowbyidAsync(string COD_APLICACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Aplicaciones>> SearchAsync(AplicacionesSearchDto dto);
    Task<WriteResult> InsertAsync(AplicacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(AplicacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_APLICACION);
}

public interface IAplicacionesService
{
    Task<ApiResponse<IEnumerable<Aplicaciones>>> GetAllAsync();
    Task<ApiResponse<Aplicaciones>> GetByIdAsync(string COD_APLICACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Aplicaciones>>> SearchAsync(AplicacionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AplicacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(AplicacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_APLICACION);
}