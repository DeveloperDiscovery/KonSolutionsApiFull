using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITitulosAcademicosRepository
{
    Task<IEnumerable<TitulosAcademicos>> ShowallAsync();
    Task<TitulosAcademicos?> ShowbyidAsync(string COD_TITULO_ACADEMICO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TitulosAcademicos>> SearchAsync(TitulosAcademicosSearchDto dto);
    Task<WriteResult> InsertAsync(TitulosAcademicosInsertDto dto);
    Task<WriteResult> UpdateAsync(TitulosAcademicosUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TITULO_ACADEMICO);
}

public interface ITitulosAcademicosService
{
    Task<ApiResponse<IEnumerable<TitulosAcademicos>>> GetAllAsync();
    Task<ApiResponse<TitulosAcademicos>> GetByIdAsync(string COD_TITULO_ACADEMICO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TitulosAcademicos>>> SearchAsync(TitulosAcademicosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TitulosAcademicosInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TitulosAcademicosUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TITULO_ACADEMICO);
}