using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ILotesControlDetalleRepository
{
    Task<IEnumerable<LotesControlDetalle>> ShowallAsync(string NUM_LOTE_CONTROL);
    Task<LotesControlDetalle?> ShowbyidAsync(string NUM_LOTE_CONTROL, string COD_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<LotesControlDetalle>> SearchAsync(LotesControlDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(LotesControlDetalleInsertDto dto);
    Task<WriteResult> UpdateAsync(LotesControlDetalleUpdateDto dto);
    Task<WriteResult> DeleteAsync(string NUM_LOTE_CONTROL, string COD_PRODUCTO);
}

public interface ILotesControlDetalleService
{
    Task<ApiResponse<IEnumerable<LotesControlDetalle>>> GetAllAsync(string NUM_LOTE_CONTROL);
    Task<ApiResponse<LotesControlDetalle>> GetByIdAsync(string NUM_LOTE_CONTROL, string COD_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<LotesControlDetalle>>> SearchAsync(LotesControlDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(LotesControlDetalleInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(LotesControlDetalleUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string NUM_LOTE_CONTROL, string COD_PRODUCTO);
}