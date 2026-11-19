using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMovimientosContablesRepository
{
    Task<IEnumerable<MovimientosContables>> ShowallAsync();
    Task<MovimientosContables?> ShowbyidAsync(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MovimientosContables>> SearchAsync(MovimientosContablesSearchDto dto);
    Task<WriteResult> InsertAsync(MovimientosContablesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(MovimientosContablesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE);
}

public interface IMovimientosContablesService
{
    Task<ApiResponse<IEnumerable<MovimientosContables>>> GetAllAsync();
    Task<ApiResponse<MovimientosContables>> GetByIdAsync(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MovimientosContables>>> SearchAsync(MovimientosContablesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MovimientosContablesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(MovimientosContablesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE);
}