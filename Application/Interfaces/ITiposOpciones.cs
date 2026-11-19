using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposOpcionesRepository
{
    Task<IEnumerable<TiposOpciones>> ShowallAsync();
    Task<TiposOpciones?> ShowbyidAsync(string COD_TIPO_OPCION_APLICACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposOpciones>> SearchAsync(TiposOpcionesSearchDto dto);
    Task<WriteResult> InsertAsync(TiposOpcionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposOpcionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_OPCION_APLICACION);
}

public interface ITiposOpcionesService
{
    Task<ApiResponse<IEnumerable<TiposOpciones>>> GetAllAsync();
    Task<ApiResponse<TiposOpciones>> GetByIdAsync(string COD_TIPO_OPCION_APLICACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposOpciones>>> SearchAsync(TiposOpcionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposOpcionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposOpcionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_OPCION_APLICACION);
}