using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rfid;

namespace KonSolutionsApi.Application.Services;

public class RfidImpresionService : IRfidImpresionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(RfidImpresionService);
    private readonly IRfidImpresionRepository _repo;
    private readonly ErrorLogService _log;
    public RfidImpresionService(IRfidImpresionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<RfidTrabajoImpresion>>> SearchAsync(RfidTrabajoImpresionSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<RfidTrabajoImpresion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<RfidTrabajoImpresion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RfidTrabajoImpresion>> GetByIdAsync(int NUM_TRABAJO)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_TRABAJO);
              return d is null ? ApiResponse<RfidTrabajoImpresion>.Fail("No encontrado.") : ApiResponse<RfidTrabajoImpresion>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<RfidTrabajoImpresion>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RfidTrabajoCreado>> SolicitarAsync(RfidImprimirDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var (r, c) = await _repo.SolicitarAsync(dto, codUsuario, codEstacion);
            if (r.IsError) { WriteResult.OnSpError?.Invoke(r.Mensaje); return ApiResponse<RfidTrabajoCreado>.Fail(r.Mensaje); }
            return c is null ? ApiResponse<RfidTrabajoCreado>.Fail("El procedimiento no devolvió el trabajo creado.")
                             : ApiResponse<RfidTrabajoCreado>.Ok(c, $"Trabajo de impresión {c.NUM_TRABAJO} pedido: {c.CAN_FALTAN} etiquetas por imprimir. Lo atiende el servicio del agente en la planta.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SolicitarAsync));
              return ApiResponse<RfidTrabajoCreado>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RfidTrabajoEstado>> CancelarAsync(int NUM_TRABAJO, string codUsuario)
    {
        try
        {
            var (r, e) = await _repo.CancelarAsync(NUM_TRABAJO, codUsuario);
            if (r.IsError) { WriteResult.OnSpError?.Invoke(r.Mensaje); return ApiResponse<RfidTrabajoEstado>.Fail(r.Mensaje); }
            var estado = e ?? new RfidTrabajoEstado();
            return ApiResponse<RfidTrabajoEstado>.Ok(estado, estado.COD_ESTADO_TRABAJO == "C" ? "Trabajo cancelado." : "Cancelación pedida: el servicio se detiene al terminar la etiqueta que está imprimiendo.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(CancelarAsync));
              return ApiResponse<RfidTrabajoEstado>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RfidTrabajoImpresion?>> ReclamarAsync(RfidTrabajoReclamarDto dto, string codUsuario)
    {
        try
        {
            var (r, t) = await _repo.ReclamarAsync(dto, codUsuario);
            if (r.IsError) { WriteResult.OnSpError?.Invoke(r.Mensaje); return ApiResponse<RfidTrabajoImpresion?>.Fail(r.Mensaje); }
            return ApiResponse<RfidTrabajoImpresion?>.Ok(t, t is null ? "No hay trabajos de impresión para esta impresora." : $"Trabajo {t.NUM_TRABAJO} tomado.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ReclamarAsync));
              return ApiResponse<RfidTrabajoImpresion?>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RfidTrabajoEstado>> ProgresoAsync(int NUM_TRABAJO, RfidTrabajoProgresoDto dto, string codUsuario)
    {
        try
        {
            var (r, e) = await _repo.ProgresoAsync(NUM_TRABAJO, dto, codUsuario);
            if (r.IsError) { WriteResult.OnSpError?.Invoke(r.Mensaje); return ApiResponse<RfidTrabajoEstado>.Fail(r.Mensaje); }
            return ApiResponse<RfidTrabajoEstado>.Ok(e ?? new RfidTrabajoEstado());
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ProgresoAsync));
              return ApiResponse<RfidTrabajoEstado>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> TerminarAsync(int NUM_TRABAJO, RfidTrabajoTerminarDto dto, string codUsuario)
    {
        try { var r = await _repo.TerminarAsync(NUM_TRABAJO, dto, codUsuario);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Trabajo cerrado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(TerminarAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
