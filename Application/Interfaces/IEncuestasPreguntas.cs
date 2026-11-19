using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEncuestasPreguntasRepository
{
    Task<IEnumerable<EncuestasPreguntas>> ShowallAsync();
    Task<EncuestasPreguntas?> ShowbyidAsync(int NUM_ENCUESTA, int NUM_PREGUNTA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EncuestasPreguntas>> SearchAsync(EncuestasPreguntasSearchDto dto);
    Task<WriteResult> InsertAsync(EncuestasPreguntasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EncuestasPreguntasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_ENCUESTA, int NUM_PREGUNTA);
}

public interface IEncuestasPreguntasService
{
    Task<ApiResponse<IEnumerable<EncuestasPreguntas>>> GetAllAsync();
    Task<ApiResponse<EncuestasPreguntas>> GetByIdAsync(int NUM_ENCUESTA, int NUM_PREGUNTA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EncuestasPreguntas>>> SearchAsync(EncuestasPreguntasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EncuestasPreguntasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EncuestasPreguntasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_ENCUESTA, int NUM_PREGUNTA);
}