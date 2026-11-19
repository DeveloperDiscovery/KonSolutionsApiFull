using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEncuestasPreguntasRespuestasRepository
{
    Task<IEnumerable<EncuestasPreguntasRespuestas>> ShowallAsync();
    Task<EncuestasPreguntasRespuestas?> ShowbyidAsync(int NUM_ENCUESTA, int NUM_PREGUNTA, int NUM_RESPUESTA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EncuestasPreguntasRespuestas>> SearchAsync(EncuestasPreguntasRespuestasSearchDto dto);
    Task<WriteResult> InsertAsync(EncuestasPreguntasRespuestasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EncuestasPreguntasRespuestasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_ENCUESTA, int NUM_PREGUNTA, int NUM_RESPUESTA);
}

public interface IEncuestasPreguntasRespuestasService
{
    Task<ApiResponse<IEnumerable<EncuestasPreguntasRespuestas>>> GetAllAsync();
    Task<ApiResponse<EncuestasPreguntasRespuestas>> GetByIdAsync(int NUM_ENCUESTA, int NUM_PREGUNTA, int NUM_RESPUESTA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EncuestasPreguntasRespuestas>>> SearchAsync(EncuestasPreguntasRespuestasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EncuestasPreguntasRespuestasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EncuestasPreguntasRespuestasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_ENCUESTA, int NUM_PREGUNTA, int NUM_RESPUESTA);
}