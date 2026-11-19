using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalCuentaCorrienteDetalleRepository
{
    Task<IEnumerable<PersonalCuentaCorrienteDetalle>> ShowallAsync();
    Task<PersonalCuentaCorrienteDetalle?> ShowbyidAsync(int NUM_CUENTA_CORRIENTE, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalCuentaCorrienteDetalle>> SearchAsync(PersonalCuentaCorrienteDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalCuentaCorrienteDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalCuentaCorrienteDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_CUENTA_CORRIENTE, int NUM_SECUENCIA);
}

public interface IPersonalCuentaCorrienteDetalleService
{
    Task<ApiResponse<IEnumerable<PersonalCuentaCorrienteDetalle>>> GetAllAsync();
    Task<ApiResponse<PersonalCuentaCorrienteDetalle>> GetByIdAsync(int NUM_CUENTA_CORRIENTE, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalCuentaCorrienteDetalle>>> SearchAsync(PersonalCuentaCorrienteDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalCuentaCorrienteDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalCuentaCorrienteDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_CUENTA_CORRIENTE, int NUM_SECUENCIA);
}