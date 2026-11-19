using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDisenosProductoRepository
{
    Task<IEnumerable<DisenosProducto>> ShowallAsync();
    Task<DisenosProducto?> ShowbyidAsync(string COD_DISENO, string COD_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<DisenosProducto>> SearchAsync(DisenosProductoSearchDto dto);
    Task<WriteResult> InsertAsync(DisenosProductoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(DisenosProductoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_DISENO, string COD_PRODUCTO);
}

public interface IDisenosProductoService
{
    Task<ApiResponse<IEnumerable<DisenosProducto>>> GetAllAsync();
    Task<ApiResponse<DisenosProducto>> GetByIdAsync(string COD_DISENO, string COD_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<DisenosProducto>>> SearchAsync(DisenosProductoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(DisenosProductoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(DisenosProductoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_DISENO, string COD_PRODUCTO);
}