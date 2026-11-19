using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposMenuOpcionesRepository
{
    Task<IEnumerable<TiposMenuOpciones>> ShowallAsync();
    Task<TiposMenuOpciones?> ShowbyidAsync(string COD_TIPO_MENU_OPCIONES);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposMenuOpciones>> SearchAsync(TiposMenuOpcionesSearchDto dto);
    Task<WriteResult> InsertAsync(TiposMenuOpcionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_MENU_OPCIONES);
}

public interface ITiposMenuOpcionesService
{
    Task<ApiResponse<IEnumerable<TiposMenuOpciones>>> GetAllAsync();
    Task<ApiResponse<TiposMenuOpciones>> GetByIdAsync(string COD_TIPO_MENU_OPCIONES);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposMenuOpciones>>> SearchAsync(TiposMenuOpcionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposMenuOpcionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_MENU_OPCIONES);
}
