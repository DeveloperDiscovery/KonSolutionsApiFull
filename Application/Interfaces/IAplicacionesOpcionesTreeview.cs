using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

/// <summary>Contrato del servicio de treeview de opciones de aplicaciones.</summary>
public interface IAplicacionesOpcionesTreeviewService
{
    Task<ApiResponse<IEnumerable<NodoOpcion>>> GetTreeviewAsync();
    Task<ApiResponse<IEnumerable<NodoOpcionLogo>>> GetLogosAsync();
}

/// <summary>Contrato del repositorio de treeview de opciones de aplicaciones.</summary>
public interface IAplicacionesOpcionesTreeviewRepository
{
    Task<IEnumerable<NodoOpcion>> GetTreeviewAsync();
    Task<IEnumerable<NodoOpcionLogo>> GetLogosAsync();
}
