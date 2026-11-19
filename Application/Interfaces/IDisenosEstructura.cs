using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDisenosEstructuraRepository
{
    Task<IEnumerable<DisenosEstructuraNodo>> EstructuraAsync(string codDiseno, int numVersion, int maxNivel);
    Task SectorGuardarAsync(DisenosEstructuraSectorDto dto, string codUsuario, string codEstacion);
    Task SectorEliminarAsync(string codDiseno, int numVersion, string codSector);
    Task ComponenteAgregarAsync(DisenosEstructuraComponenteDto dto, string codUsuario, string codEstacion);
    Task ComponenteQuitarAsync(DisenosEstructuraComponenteDto dto);
    Task ComponenteCambiarAsync(DisenosEstructuraComponenteCambiarDto dto, string codUsuario, string codEstacion);
    Task<int> InsumoGuardarAsync(DisenosEstructuraInsumoDto dto, string codUsuario, string codEstacion);
    Task InsumoEliminarAsync(DisenosEstructuraInsumoClaveDto dto);
    Task BloquearAsync(string codDiseno, int numVersion, string codUsuario);
    Task DesbloquearAsync(string codDiseno, int numVersion, string codUsuario);
    Task<DisenosVersionCopiarResultado> CopiarVersionAsync(DisenosVersionCopiarDto dto, string codUsuario, string codEstacion);
    Task<DisenosSegmentoCopiarResultado> CopiarSegmentoAsync(DisenosSegmentoCopiarDto dto, string codUsuario, string codEstacion);
    Task<DisenosRutaCatalogos> RutaCatalogosAsync();
    Task<string> RutaCrearAsync(DisenosRutaCrearDto dto, string codUsuario, string codEstacion);
    Task RutaAsignarAsync(DisenosRutaAsignarDto dto, string codUsuario, string codEstacion);
    Task<DisenosRutaResultado> RutaCopiarAsync(DisenosRutaCopiarDto dto, string codUsuario, string codEstacion);
}

public interface IDisenosEstructuraService
{
    Task<ApiResponse<IEnumerable<DisenosEstructuraNodo>>> EstructuraAsync(string codDiseno, int numVersion, int maxNivel);
    Task<ApiResponse<bool>> SectorGuardarAsync(DisenosEstructuraSectorDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> SectorEliminarAsync(string codDiseno, int numVersion, string codSector);
    Task<ApiResponse<bool>> ComponenteAgregarAsync(DisenosEstructuraComponenteDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> ComponenteQuitarAsync(DisenosEstructuraComponenteDto dto);
    Task<ApiResponse<bool>> ComponenteCambiarAsync(DisenosEstructuraComponenteCambiarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<DisenosEstructuraInsumoResultado>> InsumoGuardarAsync(DisenosEstructuraInsumoDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> InsumoEliminarAsync(DisenosEstructuraInsumoClaveDto dto);
    Task<ApiResponse<bool>> BloquearAsync(string codDiseno, int numVersion, string codUsuario);
    Task<ApiResponse<bool>> DesbloquearAsync(string codDiseno, int numVersion, string codUsuario);
    Task<ApiResponse<DisenosVersionCopiarResultado>> CopiarVersionAsync(DisenosVersionCopiarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<DisenosSegmentoCopiarResultado>> CopiarSegmentoAsync(DisenosSegmentoCopiarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<DisenosRutaCatalogos>> RutaCatalogosAsync();
    Task<ApiResponse<DisenosRutaResultado>> RutaCrearAsync(DisenosRutaCrearDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> RutaAsignarAsync(DisenosRutaAsignarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<DisenosRutaResultado>> RutaCopiarAsync(DisenosRutaCopiarDto dto, string codUsuario, string codEstacion);
}
