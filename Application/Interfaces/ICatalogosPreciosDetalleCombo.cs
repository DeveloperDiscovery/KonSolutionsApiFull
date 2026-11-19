using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICatalogosPreciosDetalleComboRepository
{
    Task<IEnumerable<CatalogosPreciosDetalleCombo>> ShowallAsync();
    Task<CatalogosPreciosDetalleCombo?> ShowbyidAsync(int NUM_CATALOGO_PRECIOS, string COD_PRODUCTO, string COD_PRODUCTO_COMBO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CatalogosPreciosDetalleCombo>> SearchAsync(CatalogosPreciosDetalleComboSearchDto dto);
    Task<WriteResult> InsertAsync(CatalogosPreciosDetalleComboInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CatalogosPreciosDetalleComboUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_CATALOGO_PRECIOS, string COD_PRODUCTO, string COD_PRODUCTO_COMBO);
}

public interface ICatalogosPreciosDetalleComboService
{
    Task<ApiResponse<IEnumerable<CatalogosPreciosDetalleCombo>>> GetAllAsync();
    Task<ApiResponse<CatalogosPreciosDetalleCombo>> GetByIdAsync(int NUM_CATALOGO_PRECIOS, string COD_PRODUCTO, string COD_PRODUCTO_COMBO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CatalogosPreciosDetalleCombo>>> SearchAsync(CatalogosPreciosDetalleComboSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CatalogosPreciosDetalleComboInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CatalogosPreciosDetalleComboUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_CATALOGO_PRECIOS, string COD_PRODUCTO, string COD_PRODUCTO_COMBO);
}