using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAsientosContablesRepository
{
    Task<IEnumerable<AsientosContables>> ShowallAsync();
    Task<AsientosContables?> ShowbyidAsync(string COD_ASIENTO_CONTABLE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<AsientosContables>> SearchAsync(AsientosContablesSearchDto dto);
    Task<WriteResult> InsertAsync(AsientosContablesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(AsientosContablesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ASIENTO_CONTABLE);
}

public interface IAsientosContablesService
{
    Task<ApiResponse<IEnumerable<AsientosContables>>> GetAllAsync();
    Task<ApiResponse<AsientosContables>> GetByIdAsync(string COD_ASIENTO_CONTABLE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<AsientosContables>>> SearchAsync(AsientosContablesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AsientosContablesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(AsientosContablesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ASIENTO_CONTABLE);
}