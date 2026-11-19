using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesDireccionesRepository
{
    Task<IEnumerable<EntidadesDirecciones>> ShowallAsync(decimal COD_ENTIDAD);
    Task<EntidadesDirecciones?> ShowbyidAsync(decimal COD_ENTIDAD, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EntidadesDirecciones>> SearchAsync(EntidadesDireccionesSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesDireccionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EntidadesDireccionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, int NUM_SECUENCIA);
    Task<EntidadesDireccionesLoadCombosResult> LoadcombosAsync(int codTipoEstado);
}

public interface IEntidadesDireccionesService
{
    Task<ApiResponse<IEnumerable<EntidadesDirecciones>>> GetAllAsync(decimal COD_ENTIDAD);
    Task<ApiResponse<EntidadesDirecciones>> GetByIdAsync(decimal COD_ENTIDAD, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EntidadesDirecciones>>> SearchAsync(EntidadesDireccionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesDireccionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesDireccionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, int NUM_SECUENCIA);
    Task<ApiResponse<EntidadesDireccionesLoadCombosResult>> LoadcombosAsync(int codTipoEstado);
}