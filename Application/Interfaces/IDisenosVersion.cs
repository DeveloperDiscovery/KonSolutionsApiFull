using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

// Todos los listados llevan codDiseno: los SP de DISENOS_VERSION reciben @COD_DISENO
// como parámetro obligatorio (una versión no existe fuera de su diseño).
public interface IDisenosVersionRepository
{
    Task<IEnumerable<DisenosVersion>> ShowallAsync(string codDiseno);
    Task<DisenosVersion?> ShowbyidAsync(string COD_DISENO, int NUM_VERSION);
    Task<IEnumerable<DisenosVersion>> ShowcomboxAsync(string codDiseno);
    Task<IEnumerable<DisenosVersion>> SearchAsync(string codDiseno, DisenosVersionSearchDto dto);
    Task<WriteResult> InsertAsync(DisenosVersionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_DISENO, int NUM_VERSION);
    Task<DisenosVersionLoadCombosResult> LoadcombosAsync();
}

public interface IDisenosVersionService
{
    Task<ApiResponse<IEnumerable<DisenosVersion>>> GetAllAsync(string codDiseno);
    Task<ApiResponse<DisenosVersion>> GetByIdAsync(string COD_DISENO, int NUM_VERSION);
    Task<ApiResponse<IEnumerable<DisenosVersion>>> GetComboboxAsync(string codDiseno);
    Task<ApiResponse<IEnumerable<DisenosVersion>>> SearchAsync(string codDiseno, DisenosVersionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(DisenosVersionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_DISENO, int NUM_VERSION);
    Task<ApiResponse<DisenosVersionLoadCombosResult>> GetLoadcombosAsync();
}
