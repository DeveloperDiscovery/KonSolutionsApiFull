using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDisenosVersionSectorComponenteProductoRepository
{
    Task<IEnumerable<DisenosVersionSectorComponenteProducto>> ShowallAsync();
    Task<DisenosVersionSectorComponenteProducto?> ShowbyidAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<DisenosVersionSectorComponenteProducto>> SearchAsync(DisenosVersionSectorComponenteProductoSearchDto dto);
    Task<WriteResult> InsertAsync(DisenosVersionSectorComponenteProductoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(DisenosVersionSectorComponenteProductoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
}

public interface IDisenosVersionSectorComponenteProductoService
{
    Task<ApiResponse<IEnumerable<DisenosVersionSectorComponenteProducto>>> GetAllAsync();
    Task<ApiResponse<DisenosVersionSectorComponenteProducto>> GetByIdAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<DisenosVersionSectorComponenteProducto>>> SearchAsync(DisenosVersionSectorComponenteProductoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(DisenosVersionSectorComponenteProductoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(DisenosVersionSectorComponenteProductoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
}