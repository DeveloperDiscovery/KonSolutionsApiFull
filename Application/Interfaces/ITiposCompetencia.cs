using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposCompetenciaRepository
{
    Task<IEnumerable<TiposCompetencia>> ShowallAsync();
    Task<TiposCompetencia?> ShowbyidAsync(string COD_TIPO_COMPETENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposCompetencia>> SearchAsync(TiposCompetenciaSearchDto dto);
    Task<WriteResult> InsertAsync(TiposCompetenciaInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposCompetenciaUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_COMPETENCIA);
}

public interface ITiposCompetenciaService
{
    Task<ApiResponse<IEnumerable<TiposCompetencia>>> GetAllAsync();
    Task<ApiResponse<TiposCompetencia>> GetByIdAsync(string COD_TIPO_COMPETENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposCompetencia>>> SearchAsync(TiposCompetenciaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposCompetenciaInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposCompetenciaUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_COMPETENCIA);
}