using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class PedidosComercialesCalculoService : IPedidosComercialesCalculoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PedidosComercialesCalculoService);
    private readonly IPedidosComercialesCalculoRepository _repo;
    private readonly ErrorLogService _log;
    public PedidosComercialesCalculoService(IPedidosComercialesCalculoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<PedidosComercialesCalculo>>> GetAllAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        try { var d = (await _repo.ShowallAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial)).ToList();
              return ApiResponse<IEnumerable<PedidosComercialesCalculo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<PedidosComercialesCalculo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<PedidosComercialesCalculo>> GetByIdAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_VALOR_CALCULO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_PEDIDO_COMERCIAL, COD_TIPO_VALOR_CALCULO);
              return d is null ? ApiResponse<PedidosComercialesCalculo>.Fail("No encontrado.") : ApiResponse<PedidosComercialesCalculo>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<PedidosComercialesCalculo>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<PedidosComercialesCalculo>>> SearchAsync(PedidosComercialesCalculoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<PedidosComercialesCalculo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<PedidosComercialesCalculo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(PedidosComercialesCalculoInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(PedidosComercialesCalculoUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PEDIDO_COMERCIAL, string COD_TIPO_VALOR_CALCULO)
    {
        try { var r = await _repo.DeleteAsync(NUM_PEDIDO_COMERCIAL, COD_TIPO_VALOR_CALCULO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> CalcularAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        try { var r = await _repo.CalcularAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Cálculo actualizado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(CalcularAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> SincronizarAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, string codUsuarioRegistro, string codEstacionRegistro)
    {
        try { var r = await _repo.SincronizarAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial, codUsuarioRegistro, codEstacionRegistro);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Pedido convertido."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SincronizarAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> RevertirAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, string codUsuarioRegistro, string codEstacionRegistro)
    {
        try { var r = await _repo.RevertirAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial, codUsuarioRegistro, codEstacionRegistro);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Orden de compra revertida."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(RevertirAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> RevertirProformaAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial, string codUsuarioRegistro, string codEstacionRegistro)
    {
        try { var r = await _repo.RevertirProformaAsync(codClaseOperacion, codTipoOperacion, numPedidoComercial, codUsuarioRegistro, codEstacionRegistro);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Proforma revertida."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(RevertirProformaAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}