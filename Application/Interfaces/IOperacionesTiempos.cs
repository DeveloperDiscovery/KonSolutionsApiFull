using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

/// <summary>Operaciones, tiempos estandar y rutas con tiempos (scripts SQL 119 a 122).</summary>
public interface IOperacionesTiemposRepository
{
    // O1: catalogos (categorias, maquinas, metodos, distribuciones)
    Task<List<CatalogoOperacionItem>> CatalogoListarAsync(string catalogo, bool soloActivos);
    Task CatalogoGuardarAsync(string catalogo, CatalogoOperacionItem item, bool nuevo, string codUsuario, string codEstacion);
    Task CatalogoEliminarAsync(string catalogo, string cod);
    /// <summary>Los catalogos activos que necesitan las pantallas (categorias, maquinas, metodos, distribuciones, tipos de operacion, procesos, subprocesos, unidades de tiempo).</summary>
    Task<Dictionary<string, List<CatalogoOperacionItem>>> CombosAsync();

    // O2: operaciones y composicion
    Task<List<Operacion>> OperacionesAsync();
    Task<Operacion?> OperacionAsync(string cod);
    Task OperacionGuardarAsync(OperacionGuardarDto dto, bool nuevo, string codUsuario, string codEstacion);
    Task OperacionEliminarAsync(string cod);
    Task<string> SiguienteCodigoAsync();
    Task<List<OperacionComponente>> ComponentesAsync(string codPadre);
    Task ComponentesGuardarAsync(OperacionComponentesGuardarDto dto, string codUsuario, string codEstacion);
    Task<List<OperacionArbolNodo>> ArbolAsync(string cod, DateTime? fecha);
    Task<List<OperacionUsadaEn>> UsadaEnAsync(string cod);
    Task<List<OperacionUsadaEnRuta>> UsadaEnRutasAsync(string cod);

    // O3: tiempos y estudios
    Task<List<OperacionTiempo>> TiemposAsync(string cod);
    Task<OperacionTiempoResultado> TiempoGuardarAsync(OperacionTiempoGuardarDto dto, string codUsuario, string codEstacion);
    Task TiempoAprobarAsync(OperacionTiempoClaveDto dto, string codUsuario, string codEstacion);
    Task TiempoEliminarAsync(OperacionTiempoClaveDto dto);
    Task<List<EstudioTiempo>> EstudiosAsync(string cod);
    Task<List<EstudioTiempoElemento>> EstudioElementosAsync(int num);
    Task<EstudioTiempoResultado> EstudioGuardarAsync(EstudioTiempoGuardarDto dto, string codUsuario, string codEstacion);
    Task EstudioEliminarAsync(int num);
    Task<List<SamPorCategoria>> SamPorCategoriaOperacionAsync(string cod, DateTime? fecha);
    Task<List<SamPorClase>> SamPorClaseOperacionAsync(string cod, DateTime? fecha);

    // O4: rutas con tiempos
    Task<List<RutaTiempoResumen>> RutasAsync();
    Task<List<RutaNodo>> RutaArbolAsync(string ruta, DateTime? fecha);
    Task<int> RutaProcesoAgregarAsync(RutaProcesoAgregarDto dto, string codUsuario, string codEstacion);
    Task<int> RutaSubprocesoAgregarAsync(RutaSubprocesoAgregarDto dto, string codUsuario, string codEstacion);
    Task<RutaOperacionAgregarResultado> RutaOperacionAgregarAsync(RutaOperacionAgregarDto dto, string codUsuario, string codEstacion);
    Task<int> RutaQuitarAsync(RutaFilaClaveDto dto);
    Task RutaTiempoPropioAsync(RutaFilaClaveDto dto, string codUsuario, string codEstacion);
    Task RutaOperacionEditarAsync(RutaOperacionEditarDto dto, string codUsuario, string codEstacion);
    Task<bool> RutaMoverAsync(RutaMoverDto dto);
    Task<int> RutaRecalcularAsync(RutaRecalcularDto dto, string codUsuario, string codEstacion);
    Task<List<SamPorCategoria>> RutaSamPorCategoriaAsync(string ruta);
    Task<List<SamPorClase>> RutaSamPorClaseAsync(string ruta);
    Task<List<RutaPrecedencia>> PrecedenciasAsync(string ruta);
    Task<int> PrecedenciasGenerarAsync(RutaPrecedenciasGenerarDto dto, string codUsuario, string codEstacion);
    Task PrecedenciasGuardarAsync(RutaPrecedenciasGuardarDto dto, string codUsuario, string codEstacion);
}

public interface IOperacionesTiemposService
{
    Task<ApiResponse<List<CatalogoOperacionItem>>> CatalogoListarAsync(string catalogo, bool soloActivos);
    Task<ApiResponse<bool>> CatalogoGuardarAsync(string catalogo, CatalogoOperacionItem item, bool nuevo, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> CatalogoEliminarAsync(string catalogo, string cod);
    Task<ApiResponse<Dictionary<string, List<CatalogoOperacionItem>>>> CombosAsync();

    Task<ApiResponse<List<Operacion>>> OperacionesAsync();
    Task<ApiResponse<Operacion>> OperacionAsync(string cod);
    Task<ApiResponse<bool>> OperacionGuardarAsync(OperacionGuardarDto dto, bool nuevo, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> OperacionEliminarAsync(string cod);
    Task<ApiResponse<string>> SiguienteCodigoAsync();
    Task<ApiResponse<List<OperacionComponente>>> ComponentesAsync(string codPadre);
    Task<ApiResponse<bool>> ComponentesGuardarAsync(OperacionComponentesGuardarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<List<OperacionArbolNodo>>> ArbolAsync(string cod, DateTime? fecha);
    Task<ApiResponse<List<OperacionUsadaEn>>> UsadaEnAsync(string cod);
    Task<ApiResponse<List<OperacionUsadaEnRuta>>> UsadaEnRutasAsync(string cod);

    Task<ApiResponse<List<OperacionTiempo>>> TiemposAsync(string cod);
    Task<ApiResponse<OperacionTiempoResultado>> TiempoGuardarAsync(OperacionTiempoGuardarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> TiempoAprobarAsync(OperacionTiempoClaveDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> TiempoEliminarAsync(OperacionTiempoClaveDto dto);
    Task<ApiResponse<List<EstudioTiempo>>> EstudiosAsync(string cod);
    Task<ApiResponse<List<EstudioTiempoElemento>>> EstudioElementosAsync(int num);
    Task<ApiResponse<EstudioTiempoResultado>> EstudioGuardarAsync(EstudioTiempoGuardarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> EstudioEliminarAsync(int num);
    Task<ApiResponse<List<SamPorCategoria>>> SamPorCategoriaOperacionAsync(string cod, DateTime? fecha);
    Task<ApiResponse<List<SamPorClase>>> SamPorClaseOperacionAsync(string cod, DateTime? fecha);

    Task<ApiResponse<List<RutaTiempoResumen>>> RutasAsync();
    Task<ApiResponse<List<RutaNodo>>> RutaArbolAsync(string ruta, DateTime? fecha);
    Task<ApiResponse<int>> RutaProcesoAgregarAsync(RutaProcesoAgregarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<int>> RutaSubprocesoAgregarAsync(RutaSubprocesoAgregarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<RutaOperacionAgregarResultado>> RutaOperacionAgregarAsync(RutaOperacionAgregarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<int>> RutaQuitarAsync(RutaFilaClaveDto dto);
    Task<ApiResponse<bool>> RutaTiempoPropioAsync(RutaFilaClaveDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> RutaOperacionEditarAsync(RutaOperacionEditarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> RutaMoverAsync(RutaMoverDto dto);
    Task<ApiResponse<int>> RutaRecalcularAsync(RutaRecalcularDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<List<SamPorCategoria>>> RutaSamPorCategoriaAsync(string ruta);
    Task<ApiResponse<List<SamPorClase>>> RutaSamPorClaseAsync(string ruta);
    Task<ApiResponse<List<RutaPrecedencia>>> PrecedenciasAsync(string ruta);
    Task<ApiResponse<int>> PrecedenciasGenerarAsync(RutaPrecedenciasGenerarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> PrecedenciasGuardarAsync(RutaPrecedenciasGuardarDto dto, string codUsuario, string codEstacion);
}
