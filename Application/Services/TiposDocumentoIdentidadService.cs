using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class TiposDocumentoIdentidadService : ITiposDocumentoIdentidadService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposDocumentoIdentidadService);
    private readonly ITiposDocumentoIdentidadRepository _repo;
    private readonly ErrorLogService _log;
    public TiposDocumentoIdentidadService(ITiposDocumentoIdentidadRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposDocumentoIdentidad>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<TiposDocumentoIdentidad>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposDocumentoIdentidad>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposDocumentoIdentidad>> GetByIdAsync(int COD_TIPO_DOCUMENTO_IDENTIDAD)
    {
        try { var d = await _repo.ShowbyidAsync(COD_TIPO_DOCUMENTO_IDENTIDAD);
              return d is null ? ApiResponse<TiposDocumentoIdentidad>.Fail("No encontrado.") : ApiResponse<TiposDocumentoIdentidad>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposDocumentoIdentidad>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposDocumentoIdentidad>>> SearchAsync(TiposDocumentoIdentidadSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposDocumentoIdentidad>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposDocumentoIdentidad>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposDocumentoIdentidadInsertDto dto)
    {
        try { var r = await _repo.InsertAsync(dto);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(int id, TiposDocumentoIdentidadInsertDto dto)
    {
        // Va por el SP _UPDATE. Antes iba por el _INSERT, que modifica si la clave ya existe, pero esa
        // modificación no grababa el código SUNAT: el cambio se perdía sin aviso.
        try { var r = await _repo.UpdateAsync(new TiposDocumentoIdentidadUpdateDto
              {
                  COD_TIPO_DOCUMENTO_IDENTIDAD       = id,
                  DES_TIPO_DOCUMENTO_IDENTIDAD       = dto.DES_TIPO_DOCUMENTO_IDENTIDAD,
                  COD_TIPO_DOCUMENTO_IDENTIDAD_SUNAT = dto.COD_TIPO_DOCUMENTO_IDENTIDAD_SUNAT,
                  VAL_LONGITUD                       = dto.VAL_LONGITUD,
                  IMG_ICONO                          = dto.IMG_ICONO
              });
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro actualizado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int COD_TIPO_DOCUMENTO_IDENTIDAD)
    {
        try { var r = await _repo.DeleteAsync(COD_TIPO_DOCUMENTO_IDENTIDAD);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}