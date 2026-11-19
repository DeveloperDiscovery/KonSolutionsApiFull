using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposMovimientoRepository
{
    Task<IEnumerable<TiposMovimiento>> ShowallAsync(string codClaseMovimiento);
    Task<TiposMovimiento?> ShowbyidAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codClaseMovimiento);
    Task<IEnumerable<TiposMovimiento>> SearchAsync(TiposMovimientoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposMovimientoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposMovimientoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO);
    Task<TiposMovimientoLoadCombosResult> LoadcombosAsync(int codTipoEstado);
}

public interface ITiposMovimientoService
{
    Task<ApiResponse<IEnumerable<TiposMovimiento>>> GetAllAsync(string codClaseMovimiento);
    Task<ApiResponse<TiposMovimiento>> GetByIdAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codClaseMovimiento);
    Task<ApiResponse<IEnumerable<TiposMovimiento>>> SearchAsync(TiposMovimientoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposMovimientoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposMovimientoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO);
    Task<ApiResponse<TiposMovimientoLoadCombosResult>> LoadcombosAsync(int codTipoEstado);
}