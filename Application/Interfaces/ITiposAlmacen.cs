using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposAlmacenRepository
{
    Task<IEnumerable<TiposAlmacen>> ShowallAsync();
    Task<TiposAlmacen?> ShowbyidAsync(string COD_TIPO_ALMACEN);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposAlmacen>> SearchAsync(TiposAlmacenSearchDto dto);
    /// <summary>Sólo da de alta. Si la clave ya existe, el SP lo rechaza — antes lo habría
    /// pisado en silencio.</summary>
    Task<WriteResult> InsertAsync(TiposAlmacenInsertDto dto);
    /// <summary>Sólo modifica. Si la clave no existe, el SP lo rechaza en vez de crearla.</summary>
    Task<WriteResult> UpdateAsync(TiposAlmacenUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_ALMACEN);
}

public interface ITiposAlmacenService
{
    Task<ApiResponse<IEnumerable<TiposAlmacen>>> GetAllAsync();
    Task<ApiResponse<TiposAlmacen>> GetByIdAsync(string COD_TIPO_ALMACEN);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposAlmacen>>> SearchAsync(TiposAlmacenSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposAlmacenInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposAlmacenUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_ALMACEN);
}