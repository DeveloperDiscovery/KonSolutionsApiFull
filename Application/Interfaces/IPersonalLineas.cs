using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

/// <summary>Personal por categoria ocupacional y armado de lineas (scripts SQL 124 y 125).</summary>
public interface IPersonalLineasRepository
{
    Task<List<PersonalOperacionItem>> PersonalListarAsync(string? buscar);
    Task<PersonalOperacionItem?> PersonalAsync(int codEntidad);
    Task PersonalGuardarAsync(PersonalOperacionGuardarDto dto, bool nuevo, string codUsuario, string codEstacion);
    Task PersonalEliminarAsync(int codEntidad);
    Task<List<PersonaDisponible>> PersonalDisponiblesAsync();
    Task<List<PersonalResumenItem>> PersonalResumenAsync();
    Task<Dictionary<string, List<CatalogoOperacionItem>>> PersonalCombosAsync();

    Task<List<Armado>> ArmadosAsync(string? buscar);
    Task<Armado?> ArmadoAsync(string cod);
    Task ArmadoGuardarAsync(ArmadoGuardarDto dto, bool nuevo, string codUsuario, string codEstacion);
    Task ArmadoEliminarAsync(string cod);
    Task<string> ArmadoSiguienteCodigoAsync();
    Task<List<ArmadoPuesto>> ArmadoPuestosAsync(string cod);
    Task<List<ArmadoHoja>> ArmadoHojasAsync(string cod);
    Task<List<ArmadoAviso>> ArmadoAvisosAsync(string cod);
    Task<List<ArmadoOperariosCategoria>> ArmadoOperariosPorCategoriaAsync(string cod);
    Task ArmadoPuestosGuardarAsync(ArmadoPuestosGuardarDto dto, string codUsuario, string codEstacion);
    Task<ArmadoSugerido> ArmadoSugerirAsync(string cod, string codUsuario, string codEstacion);
    Task<Dictionary<string, List<CatalogoOperacionItem>>> ArmadoCombosAsync();

    // O7
    Task<List<ArmadoPersonaAsignada>> ArmadoPersonalAsignadoAsync(string cod);
    Task<List<ArmadoPersonaDisponible>> ArmadoPersonalDisponibleAsync(string cod);
    Task<List<ArmadoCobertura>> ArmadoCoberturaAsync(string cod);
    Task ArmadoPersonalGuardarAsync(ArmadoPersonalGuardarDto dto, string codUsuario, string codEstacion);
    Task<ArmadoPersonalSugerido> ArmadoPersonalSugerirAsync(string cod, string codUsuario, string codEstacion);
    Task<ArmadoComparacionResultado> ArmadoCompararAsync(string cod);
}

public interface IPersonalLineasService
{
    Task<ApiResponse<List<PersonalOperacionItem>>> PersonalListarAsync(string? buscar);
    Task<ApiResponse<PersonalOperacionItem>> PersonalAsync(int codEntidad);
    Task<ApiResponse<bool>> PersonalGuardarAsync(PersonalOperacionGuardarDto dto, bool nuevo, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> PersonalEliminarAsync(int codEntidad);
    Task<ApiResponse<List<PersonaDisponible>>> PersonalDisponiblesAsync();
    Task<ApiResponse<List<PersonalResumenItem>>> PersonalResumenAsync();
    Task<ApiResponse<Dictionary<string, List<CatalogoOperacionItem>>>> PersonalCombosAsync();

    Task<ApiResponse<List<Armado>>> ArmadosAsync(string? buscar);
    Task<ApiResponse<Armado>> ArmadoAsync(string cod);
    Task<ApiResponse<bool>> ArmadoGuardarAsync(ArmadoGuardarDto dto, bool nuevo, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> ArmadoEliminarAsync(string cod);
    Task<ApiResponse<string>> ArmadoSiguienteCodigoAsync();
    Task<ApiResponse<List<ArmadoPuesto>>> ArmadoPuestosAsync(string cod);
    Task<ApiResponse<List<ArmadoHoja>>> ArmadoHojasAsync(string cod);
    Task<ApiResponse<List<ArmadoAviso>>> ArmadoAvisosAsync(string cod);
    Task<ApiResponse<List<ArmadoOperariosCategoria>>> ArmadoOperariosPorCategoriaAsync(string cod);
    Task<ApiResponse<bool>> ArmadoPuestosGuardarAsync(ArmadoPuestosGuardarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<ArmadoSugerido>> ArmadoSugerirAsync(string cod, string codUsuario, string codEstacion);
    Task<ApiResponse<Dictionary<string, List<CatalogoOperacionItem>>>> ArmadoCombosAsync();

    // O7
    Task<ApiResponse<List<ArmadoPersonaAsignada>>> ArmadoPersonalAsignadoAsync(string cod);
    Task<ApiResponse<List<ArmadoPersonaDisponible>>> ArmadoPersonalDisponibleAsync(string cod);
    Task<ApiResponse<List<ArmadoCobertura>>> ArmadoCoberturaAsync(string cod);
    Task<ApiResponse<bool>> ArmadoPersonalGuardarAsync(ArmadoPersonalGuardarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<ArmadoPersonalSugerido>> ArmadoPersonalSugerirAsync(string cod, string codUsuario, string codEstacion);
    Task<ApiResponse<ArmadoComparacionResultado>> ArmadoCompararAsync(string cod);
}

/// <summary>Migracion asistida de las operaciones de las rutas existentes al catalogo (script SQL 128).</summary>
public interface IOperacionesMigracionRepository
{
    Task<MigracionResumen> ResumenAsync();
    Task<List<MigracionGrupo>> PropuestaAsync(string? ruta);
    Task<MigracionResultado> AplicarAsync(MigracionAplicarDto dto, string codUsuario, string codEstacion);
    Task<MigracionDeshacerResultado> DeshacerAsync(string? ruta, string codUsuario, string codEstacion);
}

public interface IOperacionesMigracionService
{
    Task<ApiResponse<MigracionResumen>> ResumenAsync();
    Task<ApiResponse<List<MigracionGrupo>>> PropuestaAsync(string? ruta);
    Task<ApiResponse<MigracionResultado>> AplicarAsync(MigracionAplicarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<MigracionDeshacerResultado>> DeshacerAsync(string? ruta, string codUsuario, string codEstacion);
}

