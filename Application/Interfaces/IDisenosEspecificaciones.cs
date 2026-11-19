using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDisenosEspecificacionesRepository
{
    Task<DisenosEspecificacionesPanel> PanelAsync(string codDiseno, int numVersion);
    Task<DisenosEspecificacionesResultado> GuardarAsync(DisenosEspecificacionesGuardarDto dto, string codUsuario, string codEstacion);
}

public interface IDisenosEspecificacionesService
{
    Task<ApiResponse<DisenosEspecificacionesPanel>> PanelAsync(string codDiseno, int numVersion);
    Task<ApiResponse<DisenosEspecificacionesResultado>> GuardarAsync(DisenosEspecificacionesGuardarDto dto, string codUsuario, string codEstacion);
}
