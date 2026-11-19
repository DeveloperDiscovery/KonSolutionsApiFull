using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Activos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IActivosRepository
{
    Task<IEnumerable<Activos>> ShowallAsync();
    Task<Activos?> ShowbyidAsync(string COD_ACTIVO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Activos>> SearchAsync(ActivosSearchDto dto);
    Task<WriteResult> InsertAsync(ActivosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ActivosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ACTIVO);
}

public interface IActivosService
{
    Task<ApiResponse<IEnumerable<Activos>>> GetAllAsync();
    Task<ApiResponse<Activos>> GetByIdAsync(string COD_ACTIVO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Activos>>> SearchAsync(ActivosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ActivosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ActivosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ACTIVO);
}