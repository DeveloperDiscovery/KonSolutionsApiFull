using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposMantenimientoIndustrialRepository
{
    Task<IEnumerable<TiposMantenimientoIndustrial>> ShowallAsync();
    Task<TiposMantenimientoIndustrial?> ShowbyidAsync(string COD_TIPO_MANTENIMIENTO_INDUSTRIAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposMantenimientoIndustrial>> SearchAsync(TiposMantenimientoIndustrialSearchDto dto);
    Task<WriteResult> InsertAsync(TiposMantenimientoIndustrialInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposMantenimientoIndustrialUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_MANTENIMIENTO_INDUSTRIAL);
}

public interface ITiposMantenimientoIndustrialService
{
    Task<ApiResponse<IEnumerable<TiposMantenimientoIndustrial>>> GetAllAsync();
    Task<ApiResponse<TiposMantenimientoIndustrial>> GetByIdAsync(string COD_TIPO_MANTENIMIENTO_INDUSTRIAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposMantenimientoIndustrial>>> SearchAsync(TiposMantenimientoIndustrialSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposMantenimientoIndustrialInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposMantenimientoIndustrialUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_MANTENIMIENTO_INDUSTRIAL);
}