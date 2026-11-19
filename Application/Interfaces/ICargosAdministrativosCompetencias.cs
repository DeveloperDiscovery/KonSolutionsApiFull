using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICargosAdministrativosCompetenciasRepository
{
    Task<IEnumerable<CargosAdministrativosCompetencias>> ShowallAsync();
    Task<CargosAdministrativosCompetencias?> ShowbyidAsync(int COD_CARGO, string COD_COMPETENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CargosAdministrativosCompetencias>> SearchAsync(CargosAdministrativosCompetenciasSearchDto dto);
    Task<WriteResult> InsertAsync(CargosAdministrativosCompetenciasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CargosAdministrativosCompetenciasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_CARGO, string COD_COMPETENCIA);
}

public interface ICargosAdministrativosCompetenciasService
{
    Task<ApiResponse<IEnumerable<CargosAdministrativosCompetencias>>> GetAllAsync();
    Task<ApiResponse<CargosAdministrativosCompetencias>> GetByIdAsync(int COD_CARGO, string COD_COMPETENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CargosAdministrativosCompetencias>>> SearchAsync(CargosAdministrativosCompetenciasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CargosAdministrativosCompetenciasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CargosAdministrativosCompetenciasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_CARGO, string COD_COMPETENCIA);
}