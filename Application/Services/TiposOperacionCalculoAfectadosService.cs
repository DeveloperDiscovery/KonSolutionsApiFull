using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class TiposOperacionCalculoAfectadosService : ITiposOperacionCalculoAfectadosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposOperacionCalculoAfectadosService);
    private readonly ITiposOperacionCalculoAfectadosRepository _repo;
    private readonly ErrorLogService _log;
    public TiposOperacionCalculoAfectadosService(ITiposOperacionCalculoAfectadosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposOperacionCalculoAfectados>>> GetAllAsync(string codClaseOperacion, string codTipoOperacion, string codTipoValorCalculo)
    {
        try { var d = (await _repo.ShowallAsync(codClaseOperacion, codTipoOperacion, codTipoValorCalculo)).ToList();
              return ApiResponse<IEnumerable<TiposOperacionCalculoAfectados>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposOperacionCalculoAfectados>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposOperacionCalculoAfectados>> GetByIdAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO, string COD_TIPO_VALOR_CALCULO_AFECTADO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_TIPO_VALOR_CALCULO, COD_TIPO_VALOR_CALCULO_AFECTADO);
              return d is null ? ApiResponse<TiposOperacionCalculoAfectados>.Fail("No encontrado.") : ApiResponse<TiposOperacionCalculoAfectados>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposOperacionCalculoAfectados>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposOperacionCalculoAfectados>>> SearchAsync(string codClaseOperacion, string codTipoOperacion, string codTipoValorCalculo, TiposOperacionCalculoAfectadosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(codClaseOperacion, codTipoOperacion, codTipoValorCalculo, dto)).ToList();
              return ApiResponse<IEnumerable<TiposOperacionCalculoAfectados>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposOperacionCalculoAfectados>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposOperacionCalculoAfectadosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO, string COD_TIPO_VALOR_CALCULO_AFECTADO)
    {
        try { var r = await _repo.DeleteAsync(COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_TIPO_VALOR_CALCULO, COD_TIPO_VALOR_CALCULO_AFECTADO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
