using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISexosRepository
{
    Task<IEnumerable<Sexos>> ShowallAsync();
    Task<Sexos?> ShowbyidAsync(string COD_SEXO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Sexos>> SearchAsync(SexosSearchDto dto);
    Task<WriteResult> InsertAsync(SexosInsertDto dto);
    Task<WriteResult> UpdateAsync(SexosUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_SEXO);
}

public interface ISexosService
{
    Task<ApiResponse<IEnumerable<Sexos>>> GetAllAsync();
    Task<ApiResponse<Sexos>> GetByIdAsync(string COD_SEXO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Sexos>>> SearchAsync(SexosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SexosInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(SexosUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_SEXO);
}