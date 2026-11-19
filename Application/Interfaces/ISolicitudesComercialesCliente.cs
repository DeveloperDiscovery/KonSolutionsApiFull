using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

/// <summary>Variante de solo lectura de Solicitudes Comerciales, filtrada por
/// COD_ENTIDAD_CLIENTE (PROC_..._CLIENTE_SHOWALL/SEARCH) — pensada para que un cliente
/// vea únicamente sus propias solicitudes. Reutiliza el mismo modelo/DTO que el módulo
/// completo (SolicitudesComerciales), ya que las columnas del resultset son idénticas.</summary>
public interface ISolicitudesComercialesClienteRepository
{
    Task<IEnumerable<SolicitudesComerciales>> ShowallAsync(decimal codEntidadCliente);
    Task<IEnumerable<SolicitudesComerciales>> SearchAsync(decimal codEntidadCliente, string? search);
}

public interface ISolicitudesComercialesClienteService
{
    Task<ApiResponse<IEnumerable<SolicitudesComerciales>>> GetAllAsync(decimal codEntidadCliente);
    Task<ApiResponse<IEnumerable<SolicitudesComerciales>>> SearchAsync(decimal codEntidadCliente, string? search);
}
