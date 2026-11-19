using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMantenimientoIndustrialDetalleRequerimientosRepository
{
    Task<IEnumerable<MantenimientoIndustrialDetalleRequerimientos>> ShowallAsync();
    Task<MantenimientoIndustrialDetalleRequerimientos?> ShowbyidAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MantenimientoIndustrialDetalleRequerimientos>> SearchAsync(MantenimientoIndustrialDetalleRequerimientosSearchDto dto);
    Task<WriteResult> InsertAsync(MantenimientoIndustrialDetalleRequerimientosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(MantenimientoIndustrialDetalleRequerimientosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_PRODUCTO);
}

public interface IMantenimientoIndustrialDetalleRequerimientosService
{
    Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalleRequerimientos>>> GetAllAsync();
    Task<ApiResponse<MantenimientoIndustrialDetalleRequerimientos>> GetByIdAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalleRequerimientos>>> SearchAsync(MantenimientoIndustrialDetalleRequerimientosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MantenimientoIndustrialDetalleRequerimientosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(MantenimientoIndustrialDetalleRequerimientosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_PRODUCTO);
}