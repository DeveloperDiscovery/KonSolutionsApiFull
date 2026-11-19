using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMantenimientoIndustrialDetalleRepository
{
    Task<IEnumerable<MantenimientoIndustrialDetalle>> ShowallAsync();
    Task<MantenimientoIndustrialDetalle?> ShowbyidAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MantenimientoIndustrialDetalle>> SearchAsync(MantenimientoIndustrialDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(MantenimientoIndustrialDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(MantenimientoIndustrialDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA);
}

public interface IMantenimientoIndustrialDetalleService
{
    Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalle>>> GetAllAsync();
    Task<ApiResponse<MantenimientoIndustrialDetalle>> GetByIdAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalle>>> SearchAsync(MantenimientoIndustrialDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MantenimientoIndustrialDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(MantenimientoIndustrialDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA);
}