using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface INueveBoxDesempenoPotencialRepository
{
    Task<IEnumerable<NueveBoxDesempenoPotencial>> ShowallAsync();
    Task<NueveBoxDesempenoPotencial?> ShowbyidAsync(string COD_DESEMPENO_9BOX, string COD_POTENCIAL_9BOX);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<NueveBoxDesempenoPotencial>> SearchAsync(NueveBoxDesempenoPotencialSearchDto dto);
    Task<WriteResult> InsertAsync(NueveBoxDesempenoPotencialInsertDto dto);
    Task<WriteResult> UpdateAsync(NueveBoxDesempenoPotencialUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_DESEMPENO_9BOX, string COD_POTENCIAL_9BOX);
}

public interface INueveBoxDesempenoPotencialService
{
    Task<ApiResponse<IEnumerable<NueveBoxDesempenoPotencial>>> GetAllAsync();
    Task<ApiResponse<NueveBoxDesempenoPotencial>> GetByIdAsync(string COD_DESEMPENO_9BOX, string COD_POTENCIAL_9BOX);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<NueveBoxDesempenoPotencial>>> SearchAsync(NueveBoxDesempenoPotencialSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(NueveBoxDesempenoPotencialInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(NueveBoxDesempenoPotencialUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_DESEMPENO_9BOX, string COD_POTENCIAL_9BOX);
}