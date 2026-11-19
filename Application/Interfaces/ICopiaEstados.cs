using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICopiaEstadosRepository
{
    Task<IEnumerable<CopiaConfiguracionEstadosFila>> CopiarConfiguracionAsync(CopiaConfiguracionEstadosDto dto, string codUsuario, string codEstacion);
    Task<IEnumerable<CopiaEstadosTipoFila>> CopiarEstadosTipoAsync(CopiaEstadosTipoDto dto, string codUsuario, string codEstacion);
}

public interface ICopiaEstadosService
{
    Task<ApiResponse<IEnumerable<CopiaConfiguracionEstadosFila>>> CopiarConfiguracionAsync(CopiaConfiguracionEstadosDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<IEnumerable<CopiaEstadosTipoFila>>> CopiarEstadosTipoAsync(CopiaEstadosTipoDto dto, string codUsuario, string codEstacion);
}
