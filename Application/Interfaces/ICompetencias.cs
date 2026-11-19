using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICompetenciasRepository
{
    Task<IEnumerable<Competencias>> ShowallAsync();
    Task<Competencias?> ShowbyidAsync(string COD_COMPETENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Competencias>> SearchAsync(CompetenciasSearchDto dto);
    Task<WriteResult> InsertAsync(CompetenciasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CompetenciasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_COMPETENCIA);
}

public interface ICompetenciasService
{
    Task<ApiResponse<IEnumerable<Competencias>>> GetAllAsync();
    Task<ApiResponse<Competencias>> GetByIdAsync(string COD_COMPETENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Competencias>>> SearchAsync(CompetenciasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CompetenciasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CompetenciasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_COMPETENCIA);
}