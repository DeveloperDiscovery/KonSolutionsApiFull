using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IUsuariosTiposMovimientoDirectAccessRepository
{
    Task<IEnumerable<UsuariosTiposMovimientoDirectAccess>> ShowallAsync(string codUsuario, string codClaseMovimiento);
    Task<UsuariosTiposMovimientoDirectAccess?> ShowbyidAsync(string codUsuario, string codClaseMovimiento, string codTipoMovimiento);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codUsuario, string codClaseMovimiento);
    Task<IEnumerable<UsuariosTiposMovimientoDirectAccess>> SearchAsync(string codUsuario, string codClaseMovimiento, UsuariosTiposMovimientoDirectAccessSearchDto dto);
    Task<WriteResult> InsertAsync(UsuariosTiposMovimientoDirectAccessInsertDto dto);
    Task<WriteResult> DeleteAsync(string codUsuario, string codClaseMovimiento, string codTipoMovimiento);
}

public interface IUsuariosTiposMovimientoDirectAccessService
{
    Task<ApiResponse<IEnumerable<UsuariosTiposMovimientoDirectAccess>>> GetAllAsync(string codUsuario, string codClaseMovimiento);
    Task<ApiResponse<UsuariosTiposMovimientoDirectAccess>> GetByIdAsync(string codUsuario, string codClaseMovimiento, string codTipoMovimiento);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codUsuario, string codClaseMovimiento);
    Task<ApiResponse<IEnumerable<UsuariosTiposMovimientoDirectAccess>>> SearchAsync(string codUsuario, string codClaseMovimiento, UsuariosTiposMovimientoDirectAccessSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(UsuariosTiposMovimientoDirectAccessInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codUsuario, string codClaseMovimiento, string codTipoMovimiento);
}
