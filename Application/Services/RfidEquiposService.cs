using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rfid;

namespace KonSolutionsApi.Application.Services;

public class RfidEquiposService : IRfidEquiposService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(RfidEquiposService);
    private readonly IRfidEquiposRepository _repo;
    private readonly ErrorLogService _log;
    public RfidEquiposService(IRfidEquiposRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<RfidEquipo>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<RfidEquipo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<RfidEquipo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RfidEquipo>> GetByIdAsync(string COD_PRODUCTO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_PRODUCTO);
              return d is null ? ApiResponse<RfidEquipo>.Fail("No encontrado: ese producto no es un equipo RFID (revisa Configuración general > RFID).") : ApiResponse<RfidEquipo>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<RfidEquipo>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RfidEquipoCombo>>> ComboAsync()
    {
        try { var d = (await _repo.ShowcomboxAsync()).ToList();
              return ApiResponse<IEnumerable<RfidEquipoCombo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ComboAsync));
              return ApiResponse<IEnumerable<RfidEquipoCombo>>.Fail(ex.Message); }
    }
}
