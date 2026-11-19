using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMovimientosAlmacenTransporteRepository
{
    Task<IEnumerable<MovimientosAlmacenTransporte>> ShowallAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<MovimientosAlmacenTransporte?> ShowbyidAsync(decimal NUM_MOVIMIENTO_ALMACEN, string COD_TRANSPORTE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MovimientosAlmacenTransporte>> SearchAsync(MovimientosAlmacenTransporteSearchDto dto);
    Task<WriteResult> InsertAsync(MovimientosAlmacenTransporteInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(MovimientosAlmacenTransporteUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, string COD_TRANSPORTE);
}

public interface IMovimientosAlmacenTransporteService
{
    Task<ApiResponse<IEnumerable<MovimientosAlmacenTransporte>>> GetAllAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<MovimientosAlmacenTransporte>> GetByIdAsync(decimal NUM_MOVIMIENTO_ALMACEN, string COD_TRANSPORTE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MovimientosAlmacenTransporte>>> SearchAsync(MovimientosAlmacenTransporteSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MovimientosAlmacenTransporteInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(MovimientosAlmacenTransporteUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN, string COD_TRANSPORTE);
}
