using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IComplejidadPasswordRepository
{
    Task<IEnumerable<ComplejidadPassword>> ShowallAsync();
    Task<ComplejidadPassword?> ShowbyidAsync(string COD_PASSWORD_COMPLEJIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ComplejidadPassword>> SearchAsync(ComplejidadPasswordSearchDto dto);
    Task<WriteResult> InsertAsync(ComplejidadPasswordInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ComplejidadPasswordUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PASSWORD_COMPLEJIDAD);
}

public interface IComplejidadPasswordService
{
    Task<ApiResponse<IEnumerable<ComplejidadPassword>>> GetAllAsync();
    Task<ApiResponse<ComplejidadPassword>> GetByIdAsync(string COD_PASSWORD_COMPLEJIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ComplejidadPassword>>> SearchAsync(ComplejidadPasswordSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ComplejidadPasswordInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ComplejidadPasswordUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PASSWORD_COMPLEJIDAD);
}