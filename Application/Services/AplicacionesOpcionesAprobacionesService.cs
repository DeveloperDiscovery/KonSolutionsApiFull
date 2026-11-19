using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class AplicacionesOpcionesAprobacionesService : IAplicacionesOpcionesAprobacionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(AplicacionesOpcionesAprobacionesService);
    private readonly IAplicacionesOpcionesAprobacionesRepository _repo;
    private readonly ErrorLogService _log;
    public AplicacionesOpcionesAprobacionesService(IAplicacionesOpcionesAprobacionesRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<AplicacionesOpcionesAprobaciones>>> GetAllAsync(string codAplicacion, string codOpcion)
    {
        try { var d = (await _repo.ShowallAsync(codAplicacion, codOpcion)).ToList();
              return ApiResponse<IEnumerable<AplicacionesOpcionesAprobaciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<AplicacionesOpcionesAprobaciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<AplicacionesOpcionesAprobaciones>> GetByIdAsync(string codAplicacion, string codOpcion, int numSecuencia)
    {
        try { var d = await _repo.ShowbyidAsync(codAplicacion, codOpcion, numSecuencia);
              return d is null ? ApiResponse<AplicacionesOpcionesAprobaciones>.Fail("No encontrado.")
                               : ApiResponse<AplicacionesOpcionesAprobaciones>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<AplicacionesOpcionesAprobaciones>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<AplicacionesOpcionesAprobaciones>>> SearchAsync(AplicacionesOpcionesAprobacionesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<AplicacionesOpcionesAprobaciones>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<AplicacionesOpcionesAprobaciones>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<AplicacionesOpcionesAprobacionesResumen>>> GetResumenAsync()
    {
        try { var d = (await _repo.ResumenAsync()).ToList();
              return ApiResponse<IEnumerable<AplicacionesOpcionesAprobacionesResumen>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetResumenAsync));
              return ApiResponse<IEnumerable<AplicacionesOpcionesAprobacionesResumen>>.Fail(ex.Message); }
    }

    /// <summary>Reglas de la cadena de aprobación. Se validan acá y no sólo en la pantalla:
    /// este servicio también se alcanza desde fuera del navegador, y una regla que sólo vive en
    /// la UI se saltea con una llamada directa.
    ///
    /// La secuencia que se está grabando se excluye de la comparación (en el alta viaje 0, que
    /// no existe como clave), para que modificar un nivel no choque contra sí mismo.</summary>
    private async Task<string?> ValidarAsync(string codAplicacion, string codOpcion, int secuenciaPropia,
        int? codArea, int? codCargo, bool aprobador, bool notificado, bool ultimo, int? orden,
        string? codMoneda, decimal? montoMin, decimal? montoMax)
    {
        if (codArea is null || codCargo is null)
            return "Indique el área y el cargo del nivel.";

        // Tramo de montos. Se admite dejarlo vacío (el nivel aprueba sin tope), pero si hay
        // algún importe la moneda deja de ser opcional: un tope sin moneda no se puede comparar
        // contra el importe del documento.
        var hayImporte = montoMin is not null || montoMax is not null;
        if (hayImporte && string.IsNullOrWhiteSpace(codMoneda))
            return "Indique la moneda del tramo de montos.";

        if (montoMin is not null && montoMin < 0)
            return "El monto mínimo no puede ser negativo.";

        if (montoMin is not null && montoMax is not null && montoMax <= montoMin)
            return "El monto máximo debe ser mayor que el monto mínimo.";

        if (!aprobador && !notificado)
            return "El nivel tiene que aprobar o notificar. Si no hace ninguna de las dos cosas, no cumple ninguna función.";

        if (ultimo && !aprobador)
            return "Sólo un aprobador puede ser el último de la cadena.";

        if (aprobador && orden is null)
            return "Un aprobador necesita orden de aprobación: es el lugar que ocupa en la fila.";

        var actuales = (await _repo.ShowallAsync(codAplicacion, codOpcion))
            .Where(x => (x.NUM_SECUENCIA ?? -1) != secuenciaPropia)
            .ToList();

        if (actuales.Any(x => x.COD_AREA == codArea && x.COD_CARGO == codCargo))
            return "Ese cargo ya está en la cadena de aprobación de esta opción.";

        if (aprobador && orden is not null
            && actuales.Any(x => x.FLG_APROBADOR == true && x.NUM_ORDEN_APROBACION == orden))
            return $"El orden {orden} ya lo ocupa otro aprobador de esta opción.";

        if (ultimo && actuales.Any(x => x.FLG_APROBADOR_ULTIMO == true))
            return "Esta opción ya tiene un último aprobador. Quítaselo al actual antes de marcar otro.";

        return null;
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(AplicacionesOpcionesAprobacionesInsertDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            // 0 nunca es una secuencia real: en el alta no hay fila propia que excluir.
            var error = await ValidarAsync(dto.COD_APLICACION, dto.COD_OPCION_APLICACION, 0,
                dto.COD_AREA, dto.COD_CARGO, dto.FLG_APROBADOR ?? false, dto.FLG_NOTIFICADO ?? false,
                dto.FLG_APROBADOR_ULTIMO ?? false, dto.NUM_ORDEN_APROBACION,
                dto.COD_MONEDA, dto.VAL_MONTO_MINIMO, dto.VAL_MONTO_MAXIMO);
            if (error is not null) return ApiResponse<WriteResult>.Fail(error);

            var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
            return ApiResponse<WriteResult>.FromWriteResult(r, "Nivel agregado.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(AplicacionesOpcionesAprobacionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var error = await ValidarAsync(dto.COD_APLICACION, dto.COD_OPCION_APLICACION, dto.NUM_SECUENCIA,
                dto.COD_AREA, dto.COD_CARGO, dto.FLG_APROBADOR ?? false, dto.FLG_NOTIFICADO ?? false,
                dto.FLG_APROBADOR_ULTIMO ?? false, dto.NUM_ORDEN_APROBACION,
                dto.COD_MONEDA, dto.VAL_MONTO_MINIMO, dto.VAL_MONTO_MAXIMO);
            if (error is not null) return ApiResponse<WriteResult>.Fail(error);

            var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
            return ApiResponse<WriteResult>.FromWriteResult(r, "Nivel actualizado.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codAplicacion, string codOpcion, int numSecuencia)
    {
        try { var r = await _repo.DeleteAsync(codAplicacion, codOpcion, numSecuencia);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Nivel eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
