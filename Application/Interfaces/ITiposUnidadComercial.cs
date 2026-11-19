using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposUnidadComercialRepository
{
    Task<IEnumerable<TiposUnidadComercial>> ShowallAsync();
    Task<TiposUnidadComercial?> ShowbyidAsync(string COD_TIPO_UNIDAD_COMERCIAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposUnidadComercial>> SearchAsync(TiposUnidadComercialSearchDto dto);
    Task<WriteResult> InsertAsync(TiposUnidadComercialInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposUnidadComercialUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_UNIDAD_COMERCIAL);
}

public interface ITiposUnidadComercialService
{
    Task<ApiResponse<IEnumerable<TiposUnidadComercial>>> GetAllAsync();
    Task<ApiResponse<TiposUnidadComercial>> GetByIdAsync(string COD_TIPO_UNIDAD_COMERCIAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposUnidadComercial>>> SearchAsync(TiposUnidadComercialSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposUnidadComercialInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposUnidadComercialUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_UNIDAD_COMERCIAL);
}