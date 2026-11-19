using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Sistema;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISistemaHerramientasRepository
{
    Task<List<SpEncontrado>> GetSpsPorPrefijoAsync(string prefijo);
    Task<List<ColumnaTabla>> GetEstructuraTablaAsync(string tabla);
    Task<List<string>> GetTablasAsync();
}

public interface ISistemaHerramientasService
{
    Task<ApiResponse<GenerarSpsTextResult>> GenerarSpsTextAsync(GenerarSpsTextRequest dto, string codUsuario, string codEstacion);
    Task<ApiResponse<List<string>>> GetTablasAsync();
}
