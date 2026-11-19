using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposImpuestoValoresRepository
{
    Task<IEnumerable<TiposImpuestoValores>> ShowallAsync(string codTipoImpuesto);
    Task<TiposImpuestoValores?> ShowbyidAsync(string COD_TIPO_IMPUESTO, DateTime FEC_INICIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposImpuestoValores>> SearchAsync(TiposImpuestoValoresSearchDto dto);
    Task<WriteResult> InsertAsync(TiposImpuestoValoresInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposImpuestoValoresUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_IMPUESTO, DateTime FEC_INICIO);
}

public interface ITiposImpuestoValoresService
{
    Task<ApiResponse<IEnumerable<TiposImpuestoValores>>> GetAllAsync(string codTipoImpuesto);
    Task<ApiResponse<TiposImpuestoValores>> GetByIdAsync(string COD_TIPO_IMPUESTO, DateTime FEC_INICIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposImpuestoValores>>> SearchAsync(TiposImpuestoValoresSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposImpuestoValoresInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposImpuestoValoresUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_IMPUESTO, DateTime FEC_INICIO);
}