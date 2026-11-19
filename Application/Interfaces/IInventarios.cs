using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IInventariosRepository
{
    Task<IEnumerable<Inventarios>> ShowallAsync();
    Task<Inventarios?> ShowbyidAsync(short NUM_INVENTARIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Inventarios>> SearchAsync(InventariosSearchDto dto);
    Task<WriteResult> InsertAsync(InventariosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(InventariosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(short NUM_INVENTARIO);

    // ── Procesos de inventario ──────────────────────────────────────────────
    // Ninguno de estos SP devuelve resultset: sólo escriben. Se los ejecuta igual
    // que el resto para reusar la traducción de SqlException a WriteResult, que es
    // lo que le da al usuario el mensaje real del RAISERROR.
    Task<WriteResult> RequerimientoMakeAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion);
    Task<WriteResult> RequerimientoRevierteAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion);
    Task<WriteResult> ImagenAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion);
    Task<WriteResult> CerrarAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion);
    Task<WriteResult> AbrirAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion);
}

public interface IInventariosService
{
    Task<ApiResponse<IEnumerable<Inventarios>>> GetAllAsync();
    Task<ApiResponse<Inventarios>> GetByIdAsync(short NUM_INVENTARIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Inventarios>>> SearchAsync(InventariosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(InventariosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(InventariosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(short NUM_INVENTARIO);

    // ── Procesos de inventario ──────────────────────────────────────────────
    Task<ApiResponse<WriteResult>> RequerimientoMakeAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> RequerimientoRevierteAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> ImagenAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> CerrarAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> AbrirAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion);
}
