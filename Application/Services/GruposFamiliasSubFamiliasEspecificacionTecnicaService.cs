using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class GruposFamiliasSubFamiliasEspecificacionTecnicaService : IGruposFamiliasSubFamiliasEspecificacionTecnicaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(GruposFamiliasSubFamiliasEspecificacionTecnicaService);
    private readonly IGruposFamiliasSubFamiliasEspecificacionTecnicaRepository _repo;
    private readonly ErrorLogService _log;
    public GruposFamiliasSubFamiliasEspecificacionTecnicaService(IGruposFamiliasSubFamiliasEspecificacionTecnicaRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<GruposFamiliasSubFamiliasEspecificacionTecnica>>> SearchAsync(GruposFamiliasSubFamiliasEspecificacionTecnicaSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<GruposFamiliasSubFamiliasEspecificacionTecnica>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<GruposFamiliasSubFamiliasEspecificacionTecnica>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<GruposFamiliasSubFamiliasEspecificacionTecnicaTreeList>>> GetTreelistAsync(GruposFamiliasSubFamiliasEspecificacionTecnicaSearchDto dto)
    {
        try { var d = (await _repo.TreelistAsync(dto)).ToList();
              return ApiResponse<IEnumerable<GruposFamiliasSubFamiliasEspecificacionTecnicaTreeList>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetTreelistAsync));
              return ApiResponse<IEnumerable<GruposFamiliasSubFamiliasEspecificacionTecnicaTreeList>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GruposFamiliasSubFamiliasEspecificacionTecnica>> GetByIdAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO, string COD_ESPECIFICACION_TECNICA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO, COD_ESPECIFICACION_TECNICA);
              return d is null ? ApiResponse<GruposFamiliasSubFamiliasEspecificacionTecnica>.Fail("No encontrado.") : ApiResponse<GruposFamiliasSubFamiliasEspecificacionTecnica>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<GruposFamiliasSubFamiliasEspecificacionTecnica>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(GruposFamiliasSubFamiliasEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Especificación Técnica asignada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO, string COD_ESPECIFICACION_TECNICA)
    {
        try { var r = await _repo.DeleteAsync(COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO, COD_ESPECIFICACION_TECNICA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Asignación quitada."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GruposFamiliasSubFamiliasEspecificacionTecnicaLoadCombosResult>> LoadcombosAsync(int codTipoEstado)
    {
        try { return ApiResponse<GruposFamiliasSubFamiliasEspecificacionTecnicaLoadCombosResult>.Ok(await _repo.LoadcombosAsync(codTipoEstado)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadcombosAsync));
              return ApiResponse<GruposFamiliasSubFamiliasEspecificacionTecnicaLoadCombosResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GruposFamiliasSubFamiliasEspecificacionTecnicaCopiarResultado>> CopiarAsync(GruposFamiliasSubFamiliasEspecificacionTecnicaCopiarDto dto, string codUsuario, string codEstacion)
    {
        var resultado = new GruposFamiliasSubFamiliasEspecificacionTecnicaCopiarResultado();
        try
        {
            var origen = (await _repo.SearchAsync(new GruposFamiliasSubFamiliasEspecificacionTecnicaSearchDto
            {
                COD_GRUPO_PRODUCTO = dto.COD_GRUPO_PRODUCTO,
                COD_FAMILIA_PRODUCTO = dto.COD_FAMILIA_PRODUCTO,
                COD_SUB_FAMILIA_PRODUCTO = dto.COD_SUB_FAMILIA_PRODUCTO
            })).ToList();

            foreach (var destino in dto.Destinos)
            {
                // Evita copiarse a sí mismo si el origen queda incluido por error entre los destinos.
                if (destino.COD_GRUPO_PRODUCTO == dto.COD_GRUPO_PRODUCTO
                    && destino.COD_FAMILIA_PRODUCTO == dto.COD_FAMILIA_PRODUCTO
                    && destino.COD_SUB_FAMILIA_PRODUCTO == dto.COD_SUB_FAMILIA_PRODUCTO)
                    continue;

                foreach (var espec in origen)
                {
                    var r = await _repo.InsertAsync(new GruposFamiliasSubFamiliasEspecificacionTecnicaInsertDto
                    {
                        COD_GRUPO_PRODUCTO = destino.COD_GRUPO_PRODUCTO,
                        COD_FAMILIA_PRODUCTO = destino.COD_FAMILIA_PRODUCTO,
                        COD_SUB_FAMILIA_PRODUCTO = destino.COD_SUB_FAMILIA_PRODUCTO,
                        COD_ESPECIFICACION_TECNICA = espec.COD_ESPECIFICACION_TECNICA,
                        VAL_ESPECIFICACION_TENICA = espec.VAL_ESPECIFICACION_TENICA,
                        VAL_MINIMO = espec.VAL_MINIMO,
                        VAL_MAXIMO = espec.VAL_MAXIMO,
                        NUM_ORDEN_PRESENTACION = espec.NUM_ORDEN_PRESENTACION,
                        FLG_INCLUIR_DESCRIPCION = espec.FLG_INCLUIR_DESCRIPCION,
                        FLG_REGISTRO_COMERCIAL = espec.FLG_REGISTRO_COMERCIAL,
                        FLG_FICHA_TECNICA = espec.FLG_FICHA_TECNICA,
                        FLG_CONTROL_CALIDAD = espec.FLG_CONTROL_CALIDAD,
                        COD_TIPO_REGISTRO_DATOS = espec.COD_TIPO_REGISTRO_DATOS,
                        COD_GRUPO_ESPECIFICACION_TECNICA = espec.COD_GRUPO_ESPECIFICACION_TECNICA,
                        COD_TIPO_ESTADO = espec.COD_TIPO_ESTADO,
                        COD_ESTADO = espec.COD_ESTADO
                    }, codUsuario, codEstacion);

                    if (r.IsError)
                    {
                        resultado.TotalErrores++;
                        resultado.Detalles.Add($"{destino.COD_SUB_FAMILIA_PRODUCTO} / {espec.DES_ESPECIFICACION_TECNICA}: {r.Mensaje}");
                    }
                    else
                    {
                        resultado.TotalCopiados++;
                    }
                }
            }
            return ApiResponse<GruposFamiliasSubFamiliasEspecificacionTecnicaCopiarResultado>.Ok(resultado,
                resultado.TotalErrores == 0 ? "Copia realizada." : $"Copia realizada con {resultado.TotalErrores} error(es).");
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(CopiarAsync));
            return ApiResponse<GruposFamiliasSubFamiliasEspecificacionTecnicaCopiarResultado>.Fail(ex.Message);
        }
    }
}
