using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMantenimientoIndustrialDetalleEntidadesRepository
{
    Task<IEnumerable<MantenimientoIndustrialDetalleEntidades>> ShowallAsync();
    Task<MantenimientoIndustrialDetalleEntidades?> ShowbyidAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, decimal COD_ENTIDAD_ASIGNADA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MantenimientoIndustrialDetalleEntidades>> SearchAsync(MantenimientoIndustrialDetalleEntidadesSearchDto dto);
    Task<WriteResult> InsertAsync(MantenimientoIndustrialDetalleEntidadesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, decimal COD_ENTIDAD_ASIGNADA);
}

public interface IMantenimientoIndustrialDetalleEntidadesService
{
    Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEntidades>>> GetAllAsync();
    Task<ApiResponse<MantenimientoIndustrialDetalleEntidades>> GetByIdAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, decimal COD_ENTIDAD_ASIGNADA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEntidades>>> SearchAsync(MantenimientoIndustrialDetalleEntidadesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MantenimientoIndustrialDetalleEntidadesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, decimal COD_ENTIDAD_ASIGNADA);
}