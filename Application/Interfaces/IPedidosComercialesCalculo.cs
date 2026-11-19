using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPedidosComercialesCalculoRepository
{
    /// <summary>El SHOWALL es por cabecera: el SP recibe clase, tipo y número de pedido.
    /// No existe una lectura "de todo" — cada pedido tiene sus propios cálculos.</summary>
    Task<IEnumerable<PedidosComercialesCalculo>> ShowallAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<PedidosComercialesCalculo?> ShowbyidAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_VALOR_CALCULO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PedidosComercialesCalculo>> SearchAsync(PedidosComercialesCalculoSearchDto dto);
    Task<WriteResult> InsertAsync(PedidosComercialesCalculoInsertDto dto);
    Task<WriteResult> UpdateAsync(PedidosComercialesCalculoUpdateDto dto);
    Task<WriteResult> DeleteAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_VALOR_CALCULO);
    Task<WriteResult> CalcularAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<WriteResult> SincronizarAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, string codUsuarioRegistro, string codEstacionRegistro);
    Task<WriteResult> RevertirAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, string codUsuarioRegistro, string codEstacionRegistro);
    Task<WriteResult> RevertirProformaAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, string codUsuarioRegistro, string codEstacionRegistro);
}

public interface IPedidosComercialesCalculoService
{
    Task<ApiResponse<IEnumerable<PedidosComercialesCalculo>>> GetAllAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<ApiResponse<PedidosComercialesCalculo>> GetByIdAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_VALOR_CALCULO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PedidosComercialesCalculo>>> SearchAsync(PedidosComercialesCalculoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PedidosComercialesCalculoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(PedidosComercialesCalculoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_VALOR_CALCULO);
    Task<ApiResponse<WriteResult>> CalcularAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial);
    Task<ApiResponse<WriteResult>> SincronizarAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, string codUsuarioRegistro, string codEstacionRegistro);
    Task<ApiResponse<WriteResult>> RevertirAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, string codUsuarioRegistro, string codEstacionRegistro);
    Task<ApiResponse<WriteResult>> RevertirProformaAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, string codUsuarioRegistro, string codEstacionRegistro);
}