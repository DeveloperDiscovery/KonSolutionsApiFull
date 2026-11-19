using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ILotesControlRepository
{
    Task<IEnumerable<LotesControl>> ShowallAsync();
    Task<LotesControl?> ShowbyidAsync(string NUM_LOTE_CONTROL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(decimal? codEntidad = null);
    Task<IEnumerable<LotesControl>> SearchAsync(LotesControlSearchDto dto);
    Task<string> SiguienteNumeroAsync();
    Task<WriteResult> InsertAsync(LotesControlInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(LotesControlUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string NUM_LOTE_CONTROL);
}

public interface ILotesControlService
{
    Task<ApiResponse<IEnumerable<LotesControl>>> GetAllAsync();
    Task<ApiResponse<LotesControl>> GetByIdAsync(string NUM_LOTE_CONTROL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(decimal? codEntidad = null);
    Task<ApiResponse<IEnumerable<LotesControl>>> SearchAsync(LotesControlSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(LotesControlInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(LotesControlUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string NUM_LOTE_CONTROL);
}