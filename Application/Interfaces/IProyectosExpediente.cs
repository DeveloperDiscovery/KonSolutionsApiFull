using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosExpedienteRepository
{
    Task<ExpedienteResumen?> ResumenAsync(int num);
    Task<ExpedientePlanificacion> PlanificacionAsync(int num);
    Task<IEnumerable<ExpedienteRecurso>> RecursosAsync(int num);
    Task<ExpedienteCostos> CostosAsync(int num);
    Task<ExpedienteRiesgos> RiesgosAsync(int num);
    Task<ExpedienteDocumentos> DocumentosAsync(int num);
    Task<ExpedienteMateriales> MaterialesAsync(int num);
    Task<IEnumerable<ExpedienteTipoEntidad>> TiposEntidadAsync(int num);
    Task GuardarTiposEntidadAsync(int num, IEnumerable<string> tipos, string codUsuario, string codEstacion);
    Task<IEnumerable<ComboboxItem>> EntidadesElegiblesAsync(int num);
    Task RecalcularAsync(int num, string codUsuario, string codEstacion, DateTime? fecReal = null);
    Task RecursoCostoGuardarAsync(int num, ExpedienteRecursoCostoDto dto, string codUsuario, string codEstacion);
    Task<IEnumerable<decimal>> EntidadesPropiasAsync();
    Task<IEnumerable<ExpedienteTipoPropio>> TiposPropiosAsync();
    Task TiposPropiosGuardarAsync(IEnumerable<string> tipos, string codUsuario, string codEstacion);
    Task QuitarRecursoAsync(int num, int codTarea, decimal codEntidad, string codRol);
    Task<IEnumerable<ExpedienteMovido>> MoverAsync(int num, ExpedienteMoverDto dto, string codUsuario, string codEstacion);
    Task<ExpedienteAvanceResult?> AvanceAsync(int num, int codTarea, ExpedienteAvanceDto dto, string codUsuario, string codEstacion);
    Task ConfirmarAsync(int num, int codTarea, ExpedienteConfirmarDto dto, string codUsuario, string codEstacion);
    Task<ExpedienteEstadoResult?> EstadoAplicarAsync(string nivel, int num, int cod, ExpedienteEstadoDto dto, string codUsuario, string codEstacion);
    Task<IEnumerable<ExpedienteEvento>> EventosAsync(int num);
    Task<IEnumerable<ExpedienteCompromiso>> CompromisosAsync(int num);
    Task<IEnumerable<ExpedienteCompromisoPersona>> CompromisoPersonasAsync(int num, int secuencia);
    Task<IEnumerable<ExpedienteCompromisoDecision>> CompromisoDecisionesAsync(int num, int secuencia);
    Task<IEnumerable<ComboboxItem>> CompromisoTiposAsync();
    Task<int> CompromisoGuardarAsync(int num, ExpedienteCompromisoDto dto, string codUsuario, string codEstacion);
    Task<string?> CompromisoDecidirAsync(int num, int secuencia, ExpedienteCompromisoDecidirDto dto, string codUsuario, string codEstacion);
    Task CompromisoQuitarAsync(int num, int secuencia);
    Task<IEnumerable<ExpedienteEstadoIcono>> EstadosIconosAsync(int num);
    Task<IEnumerable<ExpedienteMemoria>> MemoriasAsync(int num);
    Task<IEnumerable<ExpedienteOrganigramaNodo>> OrganigramaAsync(int num);
    Task<ExpedienteCargoArea?> CargoAreaAsync(string codEntidad);
    Task<IEnumerable<ExpedienteEstadoHistorial>> EstadosHistorialAsync(int num);
    Task<int> EventoGuardarAsync(int num, ExpedienteEventoDto dto, bool esNuevo, string codUsuario, string codEstacion);
    Task EventoQuitarAsync(int num, int secuencia);
    Task<int> AsignarGastoAsync(int num, ExpedienteGastoDto dto, string codUsuario, string codEstacion);
    Task<ExpedienteArchivo?> ArchivoAsync(int num, int secuencia);
    Task<int> ArchivoRegistrarAsync(int num, int? codFase, int? codTarea, string path, string codUsuario, string codEstacion);
    Task ArchivoQuitarAsync(int num, int secuencia);
    Task<IEnumerable<PortafolioProyecto>> PortafolioAsync(string? estado, string? tipo, string? search);
    Task<IEnumerable<MiTarea>> MisTareasAsync(decimal codEntidad, bool soloAbiertas);
}

public class ProyectoArchivoContenido
{
    public string NOMBRE { get; set; } = "";
    public byte[] CONTENIDO { get; set; } = Array.Empty<byte>();
}

public interface IProyectosExpedienteService
{
    Task<ApiResponse<ExpedienteResumen>> ResumenAsync(int num);
    Task<ApiResponse<ExpedientePlanificacion>> PlanificacionAsync(int num);
    Task<ApiResponse<IEnumerable<ExpedienteRecurso>>> RecursosAsync(int num);
    Task<ApiResponse<ExpedienteCostos>> CostosAsync(int num);
    Task<ApiResponse<ExpedienteRiesgos>> RiesgosAsync(int num);
    Task<ApiResponse<ExpedienteDocumentos>> DocumentosAsync(int num);
    Task<ApiResponse<ExpedienteMateriales>> MaterialesAsync(int num);
    Task<ApiResponse<IEnumerable<ExpedienteTipoEntidad>>> TiposEntidadAsync(int num);
    Task<ApiResponse<bool>> GuardarTiposEntidadAsync(int num, ExpedienteTiposEntidadDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> EntidadesElegiblesAsync(int num);
    Task<ApiResponse<bool>> RecalcularAsync(int num, string codUsuario, string codEstacion, DateTime? fecReal = null);
    Task<ApiResponse<bool>> RecursoCostoGuardarAsync(int num, ExpedienteRecursoCostoDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<IEnumerable<decimal>>> EntidadesPropiasAsync();
    Task<ApiResponse<IEnumerable<ExpedienteTipoPropio>>> TiposPropiosAsync();
    Task<ApiResponse<bool>> TiposPropiosGuardarAsync(IEnumerable<string> tipos, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> QuitarRecursoAsync(int num, int codTarea, decimal codEntidad, string codRol);
    Task<ApiResponse<IEnumerable<ExpedienteMovido>>> MoverAsync(int num, ExpedienteMoverDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<ExpedienteAvanceResult>> AvanceAsync(int num, int codTarea, ExpedienteAvanceDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> ConfirmarAsync(int num, int codTarea, ExpedienteConfirmarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<ExpedienteEstadoResult>> EstadoAplicarAsync(string nivel, int num, int cod, ExpedienteEstadoDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<IEnumerable<ExpedienteEvento>>> EventosAsync(int num);
    Task<ApiResponse<IEnumerable<ExpedienteCompromiso>>> CompromisosAsync(int num);
    Task<ApiResponse<IEnumerable<ExpedienteCompromisoPersona>>> CompromisoPersonasAsync(int num, int secuencia);
    Task<ApiResponse<IEnumerable<ExpedienteCompromisoDecision>>> CompromisoDecisionesAsync(int num, int secuencia);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> CompromisoTiposAsync();
    Task<ApiResponse<int>> CompromisoGuardarAsync(int num, ExpedienteCompromisoDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<string?>> CompromisoDecidirAsync(int num, int secuencia, ExpedienteCompromisoDecidirDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> CompromisoQuitarAsync(int num, int secuencia);
    Task<ApiResponse<IEnumerable<ExpedienteEstadoIcono>>> EstadosIconosAsync(int num);
    Task<ApiResponse<IEnumerable<ExpedienteMemoria>>> MemoriasAsync(int num);
    Task<ApiResponse<IEnumerable<ExpedienteOrganigramaNodo>>> OrganigramaAsync(int num);
    Task<ApiResponse<ExpedienteCargoArea?>> CargoAreaAsync(string codEntidad);
    Task<ApiResponse<IEnumerable<ExpedienteEstadoHistorial>>> EstadosHistorialAsync(int num);
    Task<ApiResponse<int>> EventoGuardarAsync(int num, ExpedienteEventoDto dto, bool esNuevo, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> EventoQuitarAsync(int num, int secuencia);
    Task<ApiResponse<int>> AsignarGastoAsync(int num, ExpedienteGastoDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<int>> ArchivoSubirAsync(int num, int? codFase, int? codTarea, string nombre, Stream contenido, long largo, string codUsuario, string codEstacion);
    Task<ApiResponse<ProyectoArchivoContenido>> ArchivoLeerAsync(int num, int secuencia);
    Task<ApiResponse<bool>> ArchivoQuitarAsync(int num, int secuencia);
    Task<ApiResponse<IEnumerable<PortafolioProyecto>>> PortafolioAsync(string? estado, string? tipo, string? search);
    Task<ApiResponse<IEnumerable<MiTarea>>> MisTareasAsync(decimal codEntidad, bool soloAbiertas);
}
