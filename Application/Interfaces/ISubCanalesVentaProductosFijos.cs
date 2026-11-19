using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISubCanalesVentaProductosFijosRepository
{
    Task<IEnumerable<SubCanalesVentaProductosFijos>> ShowallAsync();
    Task<SubCanalesVentaProductosFijos?> ShowbyidAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SubCanalesVentaProductosFijos>> SearchAsync(SubCanalesVentaProductosFijosSearchDto dto);
    Task<WriteResult> InsertAsync(SubCanalesVentaProductosFijosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(SubCanalesVentaProductosFijosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA);
}

public interface ISubCanalesVentaProductosFijosService
{
    Task<ApiResponse<IEnumerable<SubCanalesVentaProductosFijos>>> GetAllAsync();
    Task<ApiResponse<SubCanalesVentaProductosFijos>> GetByIdAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SubCanalesVentaProductosFijos>>> SearchAsync(SubCanalesVentaProductosFijosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SubCanalesVentaProductosFijosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(SubCanalesVentaProductosFijosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_UNIDAD_NEGOCIO, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA);
}