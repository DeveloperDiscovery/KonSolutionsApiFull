using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRankingAcademicoRepository
{
    Task<IEnumerable<RankingAcademico>> ShowallAsync();
    Task<RankingAcademico?> ShowbyidAsync(string COD_RANKING_ACADEMICO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<RankingAcademico>> SearchAsync(RankingAcademicoSearchDto dto);
    Task<WriteResult> InsertAsync(RankingAcademicoInsertDto dto);
    Task<WriteResult> UpdateAsync(RankingAcademicoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_RANKING_ACADEMICO);
}

public interface IRankingAcademicoService
{
    Task<ApiResponse<IEnumerable<RankingAcademico>>> GetAllAsync();
    Task<ApiResponse<RankingAcademico>> GetByIdAsync(string COD_RANKING_ACADEMICO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<RankingAcademico>>> SearchAsync(RankingAcademicoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RankingAcademicoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(RankingAcademicoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_RANKING_ACADEMICO);
}