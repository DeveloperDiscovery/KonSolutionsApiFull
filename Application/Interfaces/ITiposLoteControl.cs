using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposLoteControlRepository
{
    Task<IEnumerable<TiposLoteControl>> ShowallAsync();
    Task<TiposLoteControl?> ShowbyidAsync(string COD_TIPO_LOTE_CONTROL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposLoteControl>> SearchAsync(TiposLoteControlSearchDto dto);
    Task<WriteResult> InsertAsync(TiposLoteControlInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposLoteControlUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_LOTE_CONTROL);
}

public interface ITiposLoteControlService
{
    Task<ApiResponse<IEnumerable<TiposLoteControl>>> GetAllAsync();
    Task<ApiResponse<TiposLoteControl>> GetByIdAsync(string COD_TIPO_LOTE_CONTROL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposLoteControl>>> SearchAsync(TiposLoteControlSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposLoteControlInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposLoteControlUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_LOTE_CONTROL);
}