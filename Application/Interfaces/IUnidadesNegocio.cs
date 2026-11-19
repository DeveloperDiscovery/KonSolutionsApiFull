using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IUnidadesNegocioRepository
{
    Task<IEnumerable<UnidadesNegocio>> ShowallAsync();
    Task<UnidadesNegocio?> ShowbyidAsync(int NUM_UNIDAD_NEGOCIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<UnidadesNegocio>> SearchAsync(UnidadesNegocioSearchDto dto);
    Task<WriteResult> InsertAsync(UnidadesNegocioInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(UnidadesNegocioUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_UNIDAD_NEGOCIO);
}

public interface IUnidadesNegocioService
{
    Task<ApiResponse<IEnumerable<UnidadesNegocio>>> GetAllAsync();
    Task<ApiResponse<UnidadesNegocio>> GetByIdAsync(int NUM_UNIDAD_NEGOCIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<UnidadesNegocio>>> SearchAsync(UnidadesNegocioSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(UnidadesNegocioInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(UnidadesNegocioUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_UNIDAD_NEGOCIO);
}