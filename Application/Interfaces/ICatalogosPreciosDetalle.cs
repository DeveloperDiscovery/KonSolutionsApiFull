using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICatalogosPreciosDetalleRepository
{
    Task<IEnumerable<CatalogosPreciosDetalle>> ShowallAsync();
    Task<CatalogosPreciosDetalle?> ShowbyidAsync(int NUM_CATALOGO_PRECIOS, string COD_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CatalogosPreciosDetalle>> SearchAsync(CatalogosPreciosDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(CatalogosPreciosDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CatalogosPreciosDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_CATALOGO_PRECIOS, string COD_PRODUCTO);
}

public interface ICatalogosPreciosDetalleService
{
    Task<ApiResponse<IEnumerable<CatalogosPreciosDetalle>>> GetAllAsync();
    Task<ApiResponse<CatalogosPreciosDetalle>> GetByIdAsync(int NUM_CATALOGO_PRECIOS, string COD_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CatalogosPreciosDetalle>>> SearchAsync(CatalogosPreciosDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CatalogosPreciosDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CatalogosPreciosDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_CATALOGO_PRECIOS, string COD_PRODUCTO);
}