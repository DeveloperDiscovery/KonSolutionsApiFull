using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICategoriasComisionRepository
{
    Task<IEnumerable<CategoriasComision>> ShowallAsync();
    Task<CategoriasComision?> ShowbyidAsync(string COD_CATEGORIA_COMISION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CategoriasComision>> SearchAsync(CategoriasComisionSearchDto dto);
    Task<WriteResult> InsertAsync(CategoriasComisionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CategoriasComisionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CATEGORIA_COMISION);
}

public interface ICategoriasComisionService
{
    Task<ApiResponse<IEnumerable<CategoriasComision>>> GetAllAsync();
    Task<ApiResponse<CategoriasComision>> GetByIdAsync(string COD_CATEGORIA_COMISION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CategoriasComision>>> SearchAsync(CategoriasComisionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CategoriasComisionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CategoriasComisionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CATEGORIA_COMISION);
}