using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IGruposFamiliasSubFamiliasReglasCodificacionRepository
{
    Task<IEnumerable<GruposFamiliasSubFamiliasReglasCodificacion>> ShowallAsync();
    Task<GruposFamiliasSubFamiliasReglasCodificacion?> ShowbyidAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<GruposFamiliasSubFamiliasReglasCodificacion>> SearchAsync(GruposFamiliasSubFamiliasReglasCodificacionSearchDto dto);
    Task<WriteResult> InsertAsync(GruposFamiliasSubFamiliasReglasCodificacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(GruposFamiliasSubFamiliasReglasCodificacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO);
}

public interface IGruposFamiliasSubFamiliasReglasCodificacionService
{
    Task<ApiResponse<IEnumerable<GruposFamiliasSubFamiliasReglasCodificacion>>> GetAllAsync();
    Task<ApiResponse<GruposFamiliasSubFamiliasReglasCodificacion>> GetByIdAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<GruposFamiliasSubFamiliasReglasCodificacion>>> SearchAsync(GruposFamiliasSubFamiliasReglasCodificacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(GruposFamiliasSubFamiliasReglasCodificacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(GruposFamiliasSubFamiliasReglasCodificacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO);
}