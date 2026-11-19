using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEjecutarSentenciasRepository
{
    Task ExecuteAsync(string desSentencia);
}

public interface IEjecutarSentenciasService
{
    Task<ApiResponse<bool>> ExecuteAsync(EjecutarSentenciaDto dto);
}
