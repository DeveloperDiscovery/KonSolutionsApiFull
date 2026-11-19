using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalMarcacionesRepository
{
    Task<IEnumerable<PersonalMarcaciones>> ShowallAsync();
    Task<PersonalMarcaciones?> ShowbyidAsync(decimal NUM_MARCACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalMarcaciones>> SearchAsync(PersonalMarcacionesSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalMarcacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalMarcacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_MARCACION);
}

public interface IPersonalMarcacionesService
{
    Task<ApiResponse<IEnumerable<PersonalMarcaciones>>> GetAllAsync();
    Task<ApiResponse<PersonalMarcaciones>> GetByIdAsync(decimal NUM_MARCACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalMarcaciones>>> SearchAsync(PersonalMarcacionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalMarcacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalMarcacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_MARCACION);
}