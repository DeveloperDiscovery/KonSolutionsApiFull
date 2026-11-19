using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IGruposFamiliasSubFamiliasEspecificacionTecnicaRepository
{
    Task<IEnumerable<GruposFamiliasSubFamiliasEspecificacionTecnica>> SearchAsync(GruposFamiliasSubFamiliasEspecificacionTecnicaSearchDto dto);
    Task<IEnumerable<GruposFamiliasSubFamiliasEspecificacionTecnicaTreeList>> TreelistAsync(GruposFamiliasSubFamiliasEspecificacionTecnicaSearchDto dto);
    Task<GruposFamiliasSubFamiliasEspecificacionTecnica?> ShowbyidAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO, string COD_ESPECIFICACION_TECNICA);
    Task<WriteResult> InsertAsync(GruposFamiliasSubFamiliasEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO, string COD_ESPECIFICACION_TECNICA);
    Task<GruposFamiliasSubFamiliasEspecificacionTecnicaLoadCombosResult> LoadcombosAsync(int codTipoEstado);
}

public interface IGruposFamiliasSubFamiliasEspecificacionTecnicaService
{
    Task<ApiResponse<IEnumerable<GruposFamiliasSubFamiliasEspecificacionTecnica>>> SearchAsync(GruposFamiliasSubFamiliasEspecificacionTecnicaSearchDto dto);
    Task<ApiResponse<IEnumerable<GruposFamiliasSubFamiliasEspecificacionTecnicaTreeList>>> GetTreelistAsync(GruposFamiliasSubFamiliasEspecificacionTecnicaSearchDto dto);
    Task<ApiResponse<GruposFamiliasSubFamiliasEspecificacionTecnica>> GetByIdAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO, string COD_ESPECIFICACION_TECNICA);
    Task<ApiResponse<WriteResult>> InsertAsync(GruposFamiliasSubFamiliasEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO, string COD_ESPECIFICACION_TECNICA);
    Task<ApiResponse<GruposFamiliasSubFamiliasEspecificacionTecnicaCopiarResultado>> CopiarAsync(GruposFamiliasSubFamiliasEspecificacionTecnicaCopiarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<GruposFamiliasSubFamiliasEspecificacionTecnicaLoadCombosResult>> LoadcombosAsync(int codTipoEstado);
}
