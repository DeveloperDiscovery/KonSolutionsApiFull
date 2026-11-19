using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITrazabilidadRepository
{
    Task<TrazabilidadNodo?> NodoAsync(string tipo, string clave);
    Task<IEnumerable<TrazabilidadNodo>> VecinosAsync(string tipo, string clave, string? sentido);
    Task<TrazabilidadEntidad> EntidadAsync(string tipo, string clave);
    Task<IEnumerable<TrazabilidadNodo>> BuscarAsync(string tipoBusqueda, string numero);
    Task<IEnumerable<TrazabilidadFranja>> FranjasAsync();
}

public interface ITrazabilidadService
{
    Task<ApiResponse<TrazabilidadNodo>> NodoAsync(string? tipo, string? clave);
    Task<ApiResponse<IEnumerable<TrazabilidadNodo>>> VecinosAsync(string? tipo, string? clave, string? sentido);
    Task<ApiResponse<TrazabilidadEntidad>> EntidadAsync(string? tipo, string? clave);
    Task<ApiResponse<IEnumerable<TrazabilidadNodo>>> BuscarAsync(string? tipoBusqueda, string? numero);
    Task<ApiResponse<IEnumerable<TrazabilidadFranja>>> FranjasAsync();
}
