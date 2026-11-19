using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEncuestasRepository
{
    Task<IEnumerable<Encuestas>> ShowallAsync();
    Task<Encuestas?> ShowbyidAsync(int NUM_ENCUESTA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Encuestas>> SearchAsync(EncuestasSearchDto dto);
    Task<WriteResult> InsertAsync(EncuestasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EncuestasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_ENCUESTA);
}

public interface IEncuestasService
{
    Task<ApiResponse<IEnumerable<Encuestas>>> GetAllAsync();
    Task<ApiResponse<Encuestas>> GetByIdAsync(int NUM_ENCUESTA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Encuestas>>> SearchAsync(EncuestasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EncuestasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EncuestasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_ENCUESTA);
}