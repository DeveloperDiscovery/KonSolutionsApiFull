using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Administracion;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposCargoAdministrativoRepository
{
    Task<IEnumerable<TiposCargoAdministrativo>> ShowallAsync();
    Task<TiposCargoAdministrativo?> ShowbyidAsync(int COD_TIPO_CARGO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposCargoAdministrativo>> SearchAsync(TiposCargoAdministrativoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposCargoAdministrativoInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposCargoAdministrativoUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_TIPO_CARGO);
}

public interface ITiposCargoAdministrativoService
{
    Task<ApiResponse<IEnumerable<TiposCargoAdministrativo>>> GetAllAsync();
    Task<ApiResponse<TiposCargoAdministrativo>> GetByIdAsync(int COD_TIPO_CARGO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposCargoAdministrativo>>> SearchAsync(TiposCargoAdministrativoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposCargoAdministrativoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposCargoAdministrativoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_TIPO_CARGO);
}