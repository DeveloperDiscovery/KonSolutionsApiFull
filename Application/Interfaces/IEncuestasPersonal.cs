using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEncuestasPersonalRepository
{
    Task<IEnumerable<EncuestasPersonal>> ShowallAsync();
    Task<EncuestasPersonal?> ShowbyidAsync(int NUM_ENCUESTA, decimal COD_ENTIDAD_PERSONAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EncuestasPersonal>> SearchAsync(EncuestasPersonalSearchDto dto);
    Task<WriteResult> InsertAsync(EncuestasPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EncuestasPersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_ENCUESTA, decimal COD_ENTIDAD_PERSONAL);
}

public interface IEncuestasPersonalService
{
    Task<ApiResponse<IEnumerable<EncuestasPersonal>>> GetAllAsync();
    Task<ApiResponse<EncuestasPersonal>> GetByIdAsync(int NUM_ENCUESTA, decimal COD_ENTIDAD_PERSONAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EncuestasPersonal>>> SearchAsync(EncuestasPersonalSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EncuestasPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EncuestasPersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_ENCUESTA, decimal COD_ENTIDAD_PERSONAL);
}