using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposUnidadNegocioRepository
{
    Task<IEnumerable<TiposUnidadNegocio>> ShowallAsync();
    Task<TiposUnidadNegocio?> ShowbyidAsync(string COD_TIPO_UNIDAD_NEGOCIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposUnidadNegocio>> SearchAsync(TiposUnidadNegocioSearchDto dto);
    Task<WriteResult> InsertAsync(TiposUnidadNegocioInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposUnidadNegocioUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_UNIDAD_NEGOCIO);
}

public interface ITiposUnidadNegocioService
{
    Task<ApiResponse<IEnumerable<TiposUnidadNegocio>>> GetAllAsync();
    Task<ApiResponse<TiposUnidadNegocio>> GetByIdAsync(string COD_TIPO_UNIDAD_NEGOCIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposUnidadNegocio>>> SearchAsync(TiposUnidadNegocioSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposUnidadNegocioInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposUnidadNegocioUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_UNIDAD_NEGOCIO);
}