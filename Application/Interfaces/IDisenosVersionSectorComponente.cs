using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDisenosVersionSectorComponenteRepository
{
    Task<IEnumerable<DisenosVersionSectorComponente>> ShowallAsync();
    Task<DisenosVersionSectorComponente?> ShowbyidAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<DisenosVersionSectorComponente>> SearchAsync(DisenosVersionSectorComponenteSearchDto dto);
    Task<WriteResult> InsertAsync(DisenosVersionSectorComponenteInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(DisenosVersionSectorComponenteUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
}

public interface IDisenosVersionSectorComponenteService
{
    Task<ApiResponse<IEnumerable<DisenosVersionSectorComponente>>> GetAllAsync();
    Task<ApiResponse<DisenosVersionSectorComponente>> GetByIdAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<DisenosVersionSectorComponente>>> SearchAsync(DisenosVersionSectorComponenteSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(DisenosVersionSectorComponenteInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(DisenosVersionSectorComponenteUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
}