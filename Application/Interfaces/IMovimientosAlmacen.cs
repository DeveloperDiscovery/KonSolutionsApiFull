using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMovimientosAlmacenRepository
{
    Task<IEnumerable<MovimientosAlmacen>> ShowallAsync(string codUsuario);
    Task<MovimientosAlmacen?> ShowbyidAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MovimientosAlmacen>> SearchAsync(MovimientosAlmacenSearchDto dto, string codUsuario);
    Task<WriteResult> InsertAsync(MovimientosAlmacenInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(MovimientosAlmacenUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<MovimientosAlmacenLoadCombosResult> LoadcombosAsync(string? codClaseMovimiento);
    Task<MovimientosAlmacenAgregarResult> AgregarDesdeAsync(string sp, decimal numMovimiento, string nombreClave, int valorClave, string? secuencias, string codUsuario, string codEstacion);
    Task<MovimientosAlmacenNumeroImpresionResult> NumeroImpresionAsync(decimal numMovimiento, string codUsuario, string codEstacion);
    Task TransferirAsync(decimal numMovimiento, string codUsuario, string codEstacion);
    Task<IEnumerable<MovimientoDevolucionGuia>> DevolucionGuiasDisponiblesAsync(decimal numMovimiento);
    Task<IEnumerable<MovimientoDevolucionGuia>> DevolucionGuiasAsync(decimal numMovimiento);
    Task<IEnumerable<MovimientoDevolucionLinea>> DevolucionLineasAsync(decimal numMovimiento, string? origenes);
    Task<MovimientosAlmacenAgregarResult> DevolucionAdicionarAsync(decimal numMovimiento, string xml, string codUsuario, string codEstacion);
    Task<int> DevolucionQuitarGuiaAsync(decimal numMovimiento, int numMovimientoOrigen);
}

public interface IMovimientosAlmacenService
{
    Task<ApiResponse<IEnumerable<MovimientosAlmacen>>> GetAllAsync(string codUsuario);
    Task<ApiResponse<MovimientosAlmacen>> GetByIdAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MovimientosAlmacen>>> SearchAsync(MovimientosAlmacenSearchDto dto, string codUsuario);
    Task<ApiResponse<WriteResult>> InsertAsync(MovimientosAlmacenInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(MovimientosAlmacenUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<MovimientosAlmacenLoadCombosResult>> GetLoadcombosAsync(string? codClaseMovimiento);
    Task<ApiResponse<MovimientosAlmacenAgregarResult>> AgregarDesdeReservaAsync(decimal num, MovimientosAlmacenAgregarDto dto, string u, string e);
    Task<ApiResponse<MovimientosAlmacenAgregarResult>> AgregarDesdeRequerimientoAsync(decimal num, MovimientosAlmacenAgregarDto dto, string u, string e);
    Task<ApiResponse<MovimientosAlmacenAgregarResult>> AgregarDesdePedidoComercialAsync(decimal num, MovimientosAlmacenAgregarDto dto, string u, string e);
    Task<ApiResponse<MovimientosAlmacenNumeroImpresionResult>> NumeroImpresionAsync(decimal num, string u, string e);
    Task<ApiResponse<bool>> TransferirAsync(decimal num, string u, string e);
    Task<ApiResponse<IEnumerable<MovimientoDevolucionGuia>>> DevolucionGuiasDisponiblesAsync(decimal num);
    Task<ApiResponse<IEnumerable<MovimientoDevolucionGuia>>> DevolucionGuiasAsync(decimal num);
    Task<ApiResponse<IEnumerable<MovimientoDevolucionLinea>>> DevolucionLineasAsync(decimal num, string? origenes);
    Task<ApiResponse<MovimientosAlmacenAgregarResult>> DevolucionAdicionarAsync(decimal num, MovimientoDevolucionAdicionarDto dto, string u, string e);
    Task<ApiResponse<int>> DevolucionQuitarGuiaAsync(decimal num, int origen);
}