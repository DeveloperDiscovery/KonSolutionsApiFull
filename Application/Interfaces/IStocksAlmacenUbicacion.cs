using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IStocksAlmacenUbicacionRepository
{
    Task<IEnumerable<StocksAlmacenUbicacion>> ShowallAsync();
    Task<StocksAlmacenUbicacion?> ShowbyidAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN, string COD_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<StocksAlmacenUbicacionDetalle>> SearchDetailAsync(StocksAlmacenUbicacionSearchDetailDto dto);
    Task<StocksAlmacenUbicacionCombos> LoadCombosAsync();
    Task<StocksAlmacenUbicacionEtiqueta?> EtiquetaAsync(string codStickerRfid);
    Task<IEnumerable<StocksAlmacenUbicacionKardex>> KardexAsync(StocksAlmacenUbicacionKardexDto dto);
    Task<IEnumerable<StocksAlmacenUbicacion>> SearchAsync(StocksAlmacenUbicacionSearchDto dto);
    Task<WriteResult> InsertAsync(StocksAlmacenUbicacionInsertDto dto);
    Task<WriteResult> UpdateAsync(StocksAlmacenUbicacionUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN, string COD_PRODUCTO);
}

public interface IStocksAlmacenUbicacionService
{
    Task<ApiResponse<IEnumerable<StocksAlmacenUbicacion>>> GetAllAsync();
    Task<ApiResponse<StocksAlmacenUbicacion>> GetByIdAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN, string COD_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<StocksAlmacenUbicacionDetalle>>> SearchDetailAsync(StocksAlmacenUbicacionSearchDetailDto dto);
    Task<ApiResponse<StocksAlmacenUbicacionCombos>> LoadCombosAsync();
    Task<ApiResponse<StocksAlmacenUbicacionEtiqueta>> EtiquetaAsync(StocksAlmacenUbicacionEtiquetaDto dto);
    Task<ApiResponse<IEnumerable<StocksAlmacenUbicacionKardex>>> KardexAsync(StocksAlmacenUbicacionKardexDto dto);
    Task<ApiResponse<IEnumerable<StocksAlmacenUbicacion>>> SearchAsync(StocksAlmacenUbicacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(StocksAlmacenUbicacionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(StocksAlmacenUbicacionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN, string COD_PRODUCTO);
}