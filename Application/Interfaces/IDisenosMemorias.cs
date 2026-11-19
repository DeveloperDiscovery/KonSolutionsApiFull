using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDisenosMemoriasRepository
{
    Task<IEnumerable<DisenosMemorias>> ShowallAsync(string codDiseno);
    Task<DisenosMemorias?> ShowbyidAsync(string codDiseno, short numSecuencia);
    Task<IEnumerable<DisenosMemorias>> SearchAsync(DisenosMemoriasSearchDto dto);
    Task<WriteResult> InsertAsync(DisenosMemoriasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string codDiseno, short numSecuencia);
}

public interface IDisenosMemoriasService
{
    Task<ApiResponse<IEnumerable<DisenosMemorias>>> GetAllAsync(string codDiseno);
    Task<ApiResponse<DisenosMemorias>> GetByIdAsync(string codDiseno, short numSecuencia);
    Task<ApiResponse<IEnumerable<DisenosMemorias>>> SearchAsync(DisenosMemoriasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(DisenosMemoriasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codDiseno, short numSecuencia);
}

public interface IDisenosVersionesMemoriasRepository
{
    Task<IEnumerable<DisenosVersionesMemorias>> ShowallAsync(string codDiseno, short numVersion);
    Task<DisenosVersionesMemorias?> ShowbyidAsync(string codDiseno, short numVersion, short numSecuencia);
    Task<IEnumerable<DisenosVersionesMemorias>> SearchAsync(DisenosVersionesMemoriasSearchDto dto);
    Task<WriteResult> InsertAsync(DisenosVersionesMemoriasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string codDiseno, short numVersion, short numSecuencia);
}

public interface IDisenosVersionesMemoriasService
{
    Task<ApiResponse<IEnumerable<DisenosVersionesMemorias>>> GetAllAsync(string codDiseno, short numVersion);
    Task<ApiResponse<DisenosVersionesMemorias>> GetByIdAsync(string codDiseno, short numVersion, short numSecuencia);
    Task<ApiResponse<IEnumerable<DisenosVersionesMemorias>>> SearchAsync(DisenosVersionesMemoriasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(DisenosVersionesMemoriasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codDiseno, short numVersion, short numSecuencia);
}
