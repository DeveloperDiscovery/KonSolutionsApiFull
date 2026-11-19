using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class StocksAlmacenUbicacionService : IStocksAlmacenUbicacionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(StocksAlmacenUbicacionService);
    private readonly IStocksAlmacenUbicacionRepository _repo;
    private readonly ErrorLogService _log;
    public StocksAlmacenUbicacionService(IStocksAlmacenUbicacionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<StocksAlmacenUbicacion>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<StocksAlmacenUbicacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<StocksAlmacenUbicacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<StocksAlmacenUbicacion>> GetByIdAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN, string COD_PRODUCTO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ALMACEN, COD_UBICACION_ALMACEN, COD_PRODUCTO);
              return d is null ? ApiResponse<StocksAlmacenUbicacion>.Fail("No encontrado.") : ApiResponse<StocksAlmacenUbicacion>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<StocksAlmacenUbicacion>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<StocksAlmacenUbicacionCombos>> LoadCombosAsync()
    {
        try { return ApiResponse<StocksAlmacenUbicacionCombos>.Ok(await _repo.LoadCombosAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadCombosAsync));
              return ApiResponse<StocksAlmacenUbicacionCombos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<StocksAlmacenUbicacionDetalle>>> SearchDetailAsync(StocksAlmacenUbicacionSearchDetailDto dto)
    {
        try { var d = (await _repo.SearchDetailAsync(dto)).ToList();
              return ApiResponse<IEnumerable<StocksAlmacenUbicacionDetalle>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchDetailAsync));
              return ApiResponse<IEnumerable<StocksAlmacenUbicacionDetalle>>.Fail(ex.Message); }
    }

    /// <summary>Una etiqueta leída en la consulta de existencias. Si el procedimiento la rechaza (no trae producto, el producto no
    /// existe, orden/ítem/unidad que no son números) la respuesta es un error con SU mensaje, que es para el usuario y por eso no
    /// va al log de errores.</summary>
    public async Task<ApiResponse<StocksAlmacenUbicacionEtiqueta>> EtiquetaAsync(StocksAlmacenUbicacionEtiquetaDto dto)
    {
        try
        {
            var d = await _repo.EtiquetaAsync(dto.COD_STICKER_RFID);
            return d is null ? ApiResponse<StocksAlmacenUbicacionEtiqueta>.Fail("No se pudo leer la etiqueta.")
                             : ApiResponse<StocksAlmacenUbicacionEtiqueta>.Ok(d);
        }
        catch (Microsoft.Data.SqlClient.SqlException ex) when (ex.Number is >= 50000)
        { return ApiResponse<StocksAlmacenUbicacionEtiqueta>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(EtiquetaAsync));
              return ApiResponse<StocksAlmacenUbicacionEtiqueta>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<StocksAlmacenUbicacionKardex>>> KardexAsync(StocksAlmacenUbicacionKardexDto dto)
    {
        try { var d = (await _repo.KardexAsync(dto)).ToList();
              return ApiResponse<IEnumerable<StocksAlmacenUbicacionKardex>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(KardexAsync));
              return ApiResponse<IEnumerable<StocksAlmacenUbicacionKardex>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<StocksAlmacenUbicacion>>> SearchAsync(StocksAlmacenUbicacionSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<StocksAlmacenUbicacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<StocksAlmacenUbicacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(StocksAlmacenUbicacionInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(StocksAlmacenUbicacionUpdateDto dto)
    {
        try { var r = await _repo.UpdateAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN, string COD_PRODUCTO)
    {
        try { var r = await _repo.DeleteAsync(COD_ALMACEN, COD_UBICACION_ALMACEN, COD_PRODUCTO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}