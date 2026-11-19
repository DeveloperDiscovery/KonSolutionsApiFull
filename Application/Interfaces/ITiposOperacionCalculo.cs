using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposOperacionCalculoRepository
{
    Task<IEnumerable<TiposOperacionCalculo>> ShowallAsync(string codClaseOperacion, string codTipoOperacion);
    Task<TiposOperacionCalculo?> ShowbyidAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposOperacionCalculo>> SearchAsync(string codClaseOperacion, string codTipoOperacion, TiposOperacionCalculoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposOperacionCalculoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposOperacionCalculoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO);
    Task<WriteResult> CopyAsync(TiposOperacionCalculoCopyDto dto, string codUsuario, string codEstacion);
}

public interface ITiposOperacionCalculoService
{
    Task<ApiResponse<IEnumerable<TiposOperacionCalculo>>> GetAllAsync(string codClaseOperacion, string codTipoOperacion);
    Task<ApiResponse<TiposOperacionCalculo>> GetByIdAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposOperacionCalculo>>> SearchAsync(string codClaseOperacion, string codTipoOperacion, TiposOperacionCalculoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposOperacionCalculoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposOperacionCalculoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO);
    Task<ApiResponse<WriteResult>> CopyAsync(TiposOperacionCalculoCopyDto dto, string codUsuario, string codEstacion);
}