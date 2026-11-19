using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPersonalCompetenciasRepository
{
    Task<IEnumerable<PersonalCompetencias>> ShowallAsync();
    Task<PersonalCompetencias?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, string COD_COMPETENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PersonalCompetencias>> SearchAsync(PersonalCompetenciasSearchDto dto);
    Task<WriteResult> InsertAsync(PersonalCompetenciasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PersonalCompetenciasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, string COD_COMPETENCIA);
}

public interface IPersonalCompetenciasService
{
    Task<ApiResponse<IEnumerable<PersonalCompetencias>>> GetAllAsync();
    Task<ApiResponse<PersonalCompetencias>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, string COD_COMPETENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PersonalCompetencias>>> SearchAsync(PersonalCompetenciasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PersonalCompetenciasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PersonalCompetenciasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, string COD_COMPETENCIA);
}