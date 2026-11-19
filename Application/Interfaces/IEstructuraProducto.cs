using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEstructuraProductoRepository
{
    Task<IEnumerable<EstructuraProducto>> ShowallAsync();
    Task<EstructuraProducto?> ShowbyidAsync(string codGrupoProducto);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EstructuraProducto>> SearchAsync(EstructuraProductoSearchDto dto);
    Task<WriteResult> InsertAsync(EstructuraProductoInsertDto dto);
    Task<WriteResult> UpdateAsync(EstructuraProductoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string codGrupoProducto);
}

public interface IEstructuraProductoService
{
    Task<ApiResponse<IEnumerable<EstructuraProducto>>> GetAllAsync();
    Task<ApiResponse<EstructuraProducto>> GetByIdAsync(string codGrupoProducto);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EstructuraProducto>>> SearchAsync(EstructuraProductoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EstructuraProductoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(EstructuraProductoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codGrupoProducto);
}
