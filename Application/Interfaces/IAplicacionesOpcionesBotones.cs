using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAplicacionesOpcionesBotonesRepository
{
    Task<IEnumerable<AplicacionesOpcionesBotones>> ShowallAsync(string codAplicacion, string codOpcionAplicacion);
    Task<AplicacionesOpcionesBotones?> ShowbyidAsync(string COD_APLICACION, string COD_OPCION_APLICACION, string COD_BOTON_OPCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<AplicacionesOpcionesBotones>> SearchAsync(AplicacionesOpcionesBotonesSearchDto dto);
    Task<WriteResult> InsertAsync(AplicacionesOpcionesBotonesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_APLICACION, string COD_OPCION_APLICACION, string COD_BOTON_OPCION);
    Task<AplicacionesOpcionesBotonesLoadCombosResult> LoadcombosAsync(int codTipoEstado);
}

public interface IAplicacionesOpcionesBotonesService
{
    Task<ApiResponse<IEnumerable<AplicacionesOpcionesBotones>>> GetAllAsync(string codAplicacion, string codOpcionAplicacion);
    Task<ApiResponse<AplicacionesOpcionesBotones>> GetByIdAsync(string COD_APLICACION, string COD_OPCION_APLICACION, string COD_BOTON_OPCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<AplicacionesOpcionesBotones>>> SearchAsync(AplicacionesOpcionesBotonesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AplicacionesOpcionesBotonesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_APLICACION, string COD_OPCION_APLICACION, string COD_BOTON_OPCION);
    Task<ApiResponse<AplicacionesOpcionesBotonesLoadCombosResult>> LoadcombosAsync(int codTipoEstado);
}