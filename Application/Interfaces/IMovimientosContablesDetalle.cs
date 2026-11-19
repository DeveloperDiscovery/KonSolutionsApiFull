using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMovimientosContablesDetalleRepository
{
    Task<IEnumerable<MovimientosContablesDetalle>> ShowallAsync();
    Task<MovimientosContablesDetalle?> ShowbyidAsync(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MovimientosContablesDetalle>> SearchAsync(MovimientosContablesDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(MovimientosContablesDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(MovimientosContablesDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE);
}

public interface IMovimientosContablesDetalleService
{
    Task<ApiResponse<IEnumerable<MovimientosContablesDetalle>>> GetAllAsync();
    Task<ApiResponse<MovimientosContablesDetalle>> GetByIdAsync(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MovimientosContablesDetalle>>> SearchAsync(MovimientosContablesDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MovimientosContablesDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(MovimientosContablesDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE);
}