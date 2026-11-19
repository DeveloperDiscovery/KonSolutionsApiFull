using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEncuestasPersonalRespuestasRepository
{
    Task<IEnumerable<EncuestasPersonalRespuestas>> ShowallAsync();
    Task<EncuestasPersonalRespuestas?> ShowbyidAsync(int NUM_ENCUESTA, decimal COD_ENTIDAD_PERSONAL, int NUM_PREGUNTA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EncuestasPersonalRespuestas>> SearchAsync(EncuestasPersonalRespuestasSearchDto dto);
    Task<WriteResult> InsertAsync(EncuestasPersonalRespuestasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EncuestasPersonalRespuestasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_ENCUESTA, decimal COD_ENTIDAD_PERSONAL, int NUM_PREGUNTA);
}

public interface IEncuestasPersonalRespuestasService
{
    Task<ApiResponse<IEnumerable<EncuestasPersonalRespuestas>>> GetAllAsync();
    Task<ApiResponse<EncuestasPersonalRespuestas>> GetByIdAsync(int NUM_ENCUESTA, decimal COD_ENTIDAD_PERSONAL, int NUM_PREGUNTA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EncuestasPersonalRespuestas>>> SearchAsync(EncuestasPersonalRespuestasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EncuestasPersonalRespuestasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EncuestasPersonalRespuestasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_ENCUESTA, decimal COD_ENTIDAD_PERSONAL, int NUM_PREGUNTA);
}