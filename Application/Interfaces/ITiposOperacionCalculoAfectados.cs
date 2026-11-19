using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposOperacionCalculoAfectadosRepository
{
    Task<IEnumerable<TiposOperacionCalculoAfectados>> ShowallAsync(string codClaseOperacion, string codTipoOperacion, string codTipoValorCalculo);
    Task<TiposOperacionCalculoAfectados?> ShowbyidAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO, string COD_TIPO_VALOR_CALCULO_AFECTADO);
    Task<IEnumerable<TiposOperacionCalculoAfectados>> SearchAsync(string codClaseOperacion, string codTipoOperacion, string codTipoValorCalculo, TiposOperacionCalculoAfectadosSearchDto dto);
    Task<WriteResult> InsertAsync(TiposOperacionCalculoAfectadosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO, string COD_TIPO_VALOR_CALCULO_AFECTADO);
}

public interface ITiposOperacionCalculoAfectadosService
{
    Task<ApiResponse<IEnumerable<TiposOperacionCalculoAfectados>>> GetAllAsync(string codClaseOperacion, string codTipoOperacion, string codTipoValorCalculo);
    Task<ApiResponse<TiposOperacionCalculoAfectados>> GetByIdAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO, string COD_TIPO_VALOR_CALCULO_AFECTADO);
    Task<ApiResponse<IEnumerable<TiposOperacionCalculoAfectados>>> SearchAsync(string codClaseOperacion, string codTipoOperacion, string codTipoValorCalculo, TiposOperacionCalculoAfectadosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposOperacionCalculoAfectadosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO, string COD_TIPO_VALOR_CALCULO_AFECTADO);
}
