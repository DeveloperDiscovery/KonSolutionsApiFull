using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposCarreraAcademicaRepository
{
    Task<IEnumerable<TiposCarreraAcademica>> ShowallAsync();
    Task<TiposCarreraAcademica?> ShowbyidAsync(string COD_TIPO_CARRERA_ACADEMICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposCarreraAcademica>> SearchAsync(TiposCarreraAcademicaSearchDto dto);
    Task<WriteResult> InsertAsync(TiposCarreraAcademicaInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposCarreraAcademicaUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_CARRERA_ACADEMICA);
}

public interface ITiposCarreraAcademicaService
{
    Task<ApiResponse<IEnumerable<TiposCarreraAcademica>>> GetAllAsync();
    Task<ApiResponse<TiposCarreraAcademica>> GetByIdAsync(string COD_TIPO_CARRERA_ACADEMICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposCarreraAcademica>>> SearchAsync(TiposCarreraAcademicaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposCarreraAcademicaInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposCarreraAcademicaUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_CARRERA_ACADEMICA);
}