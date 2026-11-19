using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Activos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IActivosValorizacionRepository
{
    Task<IEnumerable<ActivosValorizacion>> ShowallAsync();
    Task<ActivosValorizacion?> ShowbyidAsync(string COD_ACTIVO, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ActivosValorizacion>> SearchAsync(ActivosValorizacionSearchDto dto);
    Task<WriteResult> InsertAsync(ActivosValorizacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ActivosValorizacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ACTIVO, int NUM_SECUENCIA);
}

public interface IActivosValorizacionService
{
    Task<ApiResponse<IEnumerable<ActivosValorizacion>>> GetAllAsync();
    Task<ApiResponse<ActivosValorizacion>> GetByIdAsync(string COD_ACTIVO, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ActivosValorizacion>>> SearchAsync(ActivosValorizacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ActivosValorizacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ActivosValorizacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ACTIVO, int NUM_SECUENCIA);
}