using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMantenimientoIndustrialDetalleEspecificacionesTecnicasRepository
{
    Task<IEnumerable<MantenimientoIndustrialDetalleEspecificacionesTecnicas>> ShowallAsync();
    Task<MantenimientoIndustrialDetalleEspecificacionesTecnicas?> ShowbyidAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MantenimientoIndustrialDetalleEspecificacionesTecnicas>> SearchAsync(MantenimientoIndustrialDetalleEspecificacionesTecnicasSearchDto dto);
    Task<WriteResult> InsertAsync(MantenimientoIndustrialDetalleEspecificacionesTecnicasInsertDto dto);
    Task<WriteResult> UpdateAsync(MantenimientoIndustrialDetalleEspecificacionesTecnicasUpdateDto dto);
    Task<WriteResult> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
}

public interface IMantenimientoIndustrialDetalleEspecificacionesTecnicasService
{
    Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEspecificacionesTecnicas>>> GetAllAsync();
    Task<ApiResponse<MantenimientoIndustrialDetalleEspecificacionesTecnicas>> GetByIdAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MantenimientoIndustrialDetalleEspecificacionesTecnicas>>> SearchAsync(MantenimientoIndustrialDetalleEspecificacionesTecnicasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MantenimientoIndustrialDetalleEspecificacionesTecnicasInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(MantenimientoIndustrialDetalleEspecificacionesTecnicasUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA);
}