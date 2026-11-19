using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAfpsRepository
{
    Task<IEnumerable<Afps>> ShowallAsync();
    Task<Afps?> ShowbyidAsync(string COD_AFP);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Afps>> SearchAsync(AfpsSearchDto dto);
    Task<WriteResult> InsertAsync(AfpsInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(AfpsUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_AFP);
}

public interface IAfpsService
{
    Task<ApiResponse<IEnumerable<Afps>>> GetAllAsync();
    Task<ApiResponse<Afps>> GetByIdAsync(string COD_AFP);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Afps>>> SearchAsync(AfpsSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AfpsInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(AfpsUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_AFP);
}