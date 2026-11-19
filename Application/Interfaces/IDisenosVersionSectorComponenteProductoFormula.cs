using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDisenosVersionSectorComponenteProductoFormulaRepository
{
    Task<IEnumerable<DisenosVersionSectorComponenteProductoFormula>> ShowallAsync();
    Task<DisenosVersionSectorComponenteProductoFormula?> ShowbyidAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<DisenosVersionSectorComponenteProductoFormula>> SearchAsync(DisenosVersionSectorComponenteProductoFormulaSearchDto dto);
    Task<WriteResult> InsertAsync(DisenosVersionSectorComponenteProductoFormulaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(DisenosVersionSectorComponenteProductoFormulaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
}

public interface IDisenosVersionSectorComponenteProductoFormulaService
{
    Task<ApiResponse<IEnumerable<DisenosVersionSectorComponenteProductoFormula>>> GetAllAsync();
    Task<ApiResponse<DisenosVersionSectorComponenteProductoFormula>> GetByIdAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<DisenosVersionSectorComponenteProductoFormula>>> SearchAsync(DisenosVersionSectorComponenteProductoFormulaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(DisenosVersionSectorComponenteProductoFormulaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(DisenosVersionSectorComponenteProductoFormulaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO);
}