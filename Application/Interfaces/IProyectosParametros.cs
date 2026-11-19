using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosParametrosRepository
{
    Task<ProyectosParametros> ShowAsync();
    Task<WriteResult> GuardarAsync(ProyectosParametrosGuardarDto dto, string codUsuario, string codEstacion);
}

public interface IProyectosParametrosService
{
    Task<ApiResponse<ProyectosParametros>> GetAsync();
    Task<ApiResponse<WriteResult>> GuardarAsync(ProyectosParametrosGuardarDto dto, string codUsuario, string codEstacion);
}
