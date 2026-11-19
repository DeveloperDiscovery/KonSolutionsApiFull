using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMantenimientoIndustrialRepository
{
    Task<IEnumerable<MantenimientoIndustrial>> ShowallAsync();
    Task<MantenimientoIndustrial?> ShowbyidAsync(int NUM_MANTENIMIENTO_INDUSTRIAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MantenimientoIndustrial>> SearchAsync(MantenimientoIndustrialSearchDto dto);
    Task<WriteResult> InsertAsync(MantenimientoIndustrialInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(MantenimientoIndustrialUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL);
}

public interface IMantenimientoIndustrialService
{
    Task<ApiResponse<IEnumerable<MantenimientoIndustrial>>> GetAllAsync();
    Task<ApiResponse<MantenimientoIndustrial>> GetByIdAsync(int NUM_MANTENIMIENTO_INDUSTRIAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MantenimientoIndustrial>>> SearchAsync(MantenimientoIndustrialSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MantenimientoIndustrialInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(MantenimientoIndustrialUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL);
}