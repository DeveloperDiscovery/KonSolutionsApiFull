using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IGruposFamiliasSubFamiliasGruposEspecificacionTecnicaRepository
{
    Task<IEnumerable<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>> ShowallAsync();
    Task<GruposFamiliasSubFamiliasGruposEspecificacionTecnica?> ShowbyidAsync(int COD_GRUPO_ESPECIFICACION_TECNICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>> SearchAsync(GruposFamiliasSubFamiliasGruposEspecificacionTecnicaSearchDto dto);
    Task<WriteResult> InsertAsync(GruposFamiliasSubFamiliasGruposEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_GRUPO_ESPECIFICACION_TECNICA);
}

public interface IGruposFamiliasSubFamiliasGruposEspecificacionTecnicaService
{
    Task<ApiResponse<IEnumerable<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>>> GetAllAsync();
    Task<ApiResponse<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>> GetByIdAsync(int COD_GRUPO_ESPECIFICACION_TECNICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>>> SearchAsync(GruposFamiliasSubFamiliasGruposEspecificacionTecnicaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(GruposFamiliasSubFamiliasGruposEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_GRUPO_ESPECIFICACION_TECNICA);
}