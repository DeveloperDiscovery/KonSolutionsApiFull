using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesUsuariosSugeridosRepository
{
    Task<IEnumerable<EntidadUsuarioSugerido>> ComercialesAsync();
    Task<IEnumerable<EntidadUsuarioSugerido>> DependientesAsync();
    Task<IEnumerable<EntidadUsuarioSugerido>> DependientesPorEntidadAsync(decimal codEntidad);
}

public interface IEntidadesUsuariosSugeridosService
{
    Task<ApiResponse<IEnumerable<EntidadUsuarioSugerido>>> GetComercialesAsync();
    Task<ApiResponse<IEnumerable<EntidadUsuarioSugerido>>> GetDependientesAsync();
    Task<ApiResponse<IEnumerable<EntidadUsuarioSugerido>>> GetDependientesPorEntidadAsync(decimal codEntidad);
}
