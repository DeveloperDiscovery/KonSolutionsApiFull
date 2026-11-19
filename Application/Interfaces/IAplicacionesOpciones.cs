using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAplicacionesOpcionesRepository
{
    Task<IEnumerable<AplicacionesOpciones>> ShowallAsync();
    Task<AplicacionesOpciones?> ShowbyidAsync(string COD_APLICACION, string COD_OPCION_APLICACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<AplicacionesOpciones>> SearchAsync(AplicacionesOpcionesSearchDto dto);
    Task<WriteResult> InsertAsync(AplicacionesOpcionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(AplicacionesOpcionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_APLICACION, string COD_OPCION_APLICACION);
    Task<WriteResult> MoveAsync(AplicacionesOpcionesMoveDto dto, string codUsuario, string codEstacion);
    Task<AplicacionesOpcionesLoadCombosResult> LoadcombosAsync(int codTipoEstado);
}

public interface IAplicacionesOpcionesService
{
    Task<ApiResponse<IEnumerable<AplicacionesOpciones>>> GetAllAsync();
    Task<ApiResponse<AplicacionesOpciones>> GetByIdAsync(string COD_APLICACION, string COD_OPCION_APLICACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<AplicacionesOpciones>>> SearchAsync(AplicacionesOpcionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AplicacionesOpcionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(AplicacionesOpcionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_APLICACION, string COD_OPCION_APLICACION);
    Task<ApiResponse<WriteResult>> MoveAsync(AplicacionesOpcionesMoveDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<AplicacionesOpcionesLoadCombosResult>> LoadcombosAsync(int codTipoEstado);
}