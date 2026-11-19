using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesProceduresRepository
{
    Task<IEnumerable<string>> ShowallAsync();
    Task<IEnumerable<EntidadProcedureParametro>> ParametersShowallAsync(string name);
}

public interface IEntidadesProceduresService
{
    Task<ApiResponse<IEnumerable<string>>> GetAllAsync();
    Task<ApiResponse<IEnumerable<EntidadProcedureParametro>>> GetParametersAsync(string name);
}
