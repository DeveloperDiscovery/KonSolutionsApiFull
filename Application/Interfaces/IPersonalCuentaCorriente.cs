using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalCuentaCorrienteRepository
{
    Task<IEnumerable<PersonalCuentaCorriente>> ShowallAsync();
    Task<PersonalCuentaCorriente?> ShowbyidAsync(int NUM_CUENTA_CORRIENTE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalCuentaCorriente>> SearchAsync(PersonalCuentaCorrienteSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalCuentaCorrienteInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalCuentaCorrienteUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_CUENTA_CORRIENTE);
}

public interface IPersonalCuentaCorrienteService
{
    Task<ApiResponse<IEnumerable<PersonalCuentaCorriente>>> GetAllAsync();
    Task<ApiResponse<PersonalCuentaCorriente>> GetByIdAsync(int NUM_CUENTA_CORRIENTE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalCuentaCorriente>>> SearchAsync(PersonalCuentaCorrienteSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalCuentaCorrienteInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalCuentaCorrienteUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_CUENTA_CORRIENTE);
}