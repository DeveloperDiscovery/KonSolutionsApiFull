using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposCuentaCorrienteRepository
{
    Task<IEnumerable<TiposCuentaCorriente>> ShowallAsync();
    Task<TiposCuentaCorriente?> ShowbyidAsync(string COD_TIPO_CUENTA_CORRIENTE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposCuentaCorriente>> SearchAsync(TiposCuentaCorrienteSearchDto dto);
    Task<WriteResult> InsertAsync(TiposCuentaCorrienteInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposCuentaCorrienteUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_CUENTA_CORRIENTE);
}

public interface ITiposCuentaCorrienteService
{
    Task<ApiResponse<IEnumerable<TiposCuentaCorriente>>> GetAllAsync();
    Task<ApiResponse<TiposCuentaCorriente>> GetByIdAsync(string COD_TIPO_CUENTA_CORRIENTE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposCuentaCorriente>>> SearchAsync(TiposCuentaCorrienteSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposCuentaCorrienteInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposCuentaCorrienteUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_CUENTA_CORRIENTE);
}