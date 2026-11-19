using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposValorCalculoRepository
{
    Task<IEnumerable<TiposValorCalculo>> ShowallAsync();
    Task<TiposValorCalculo?> ShowbyidAsync(string COD_TIPO_VALOR_CALCULO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposValorCalculo>> SearchAsync(TiposValorCalculoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposValorCalculoInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposValorCalculoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_VALOR_CALCULO);
}

public interface ITiposValorCalculoService
{
    Task<ApiResponse<IEnumerable<TiposValorCalculo>>> GetAllAsync();
    Task<ApiResponse<TiposValorCalculo>> GetByIdAsync(string COD_TIPO_VALOR_CALCULO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposValorCalculo>>> SearchAsync(TiposValorCalculoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposValorCalculoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposValorCalculoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_VALOR_CALCULO);
}