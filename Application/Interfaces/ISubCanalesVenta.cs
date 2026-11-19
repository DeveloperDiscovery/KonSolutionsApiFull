using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISubCanalesVentaRepository
{
    Task<IEnumerable<SubCanalesVenta>> ShowallAsync();
    Task<SubCanalesVenta?> ShowbyidAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SubCanalesVenta>> SearchAsync(SubCanalesVentaSearchDto dto);
    Task<WriteResult> InsertAsync(SubCanalesVentaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(SubCanalesVentaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA);
}

public interface ISubCanalesVentaService
{
    Task<ApiResponse<IEnumerable<SubCanalesVenta>>> GetAllAsync();
    Task<ApiResponse<SubCanalesVenta>> GetByIdAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SubCanalesVenta>>> SearchAsync(SubCanalesVentaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SubCanalesVentaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(SubCanalesVentaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA);
}