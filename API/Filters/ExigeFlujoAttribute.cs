namespace KonSolutionsApi.API.Filters;

/// <summary>Declara la OPCIÓN DE MENÚ (COD_APLICACION + COD_OPCION_APLICACION) desde la que
/// se ejecuta la escritura, para que FlujoAuthorizationFilter pueda consultar
/// ESTANDAR.OPCIONES_PROCESOS_ESTADOS_FLUJOS_DEPENDENCIA antes de dejarla pasar.
///
/// A PROPÓSITO NO LLEVA LA TABLA (DES_OBJETO): ese dato ya lo declara [ExigePermiso] en el
/// mismo controlador, y duplicarlo aquí sería la misma información escrita dos veces —el día
/// que alguien corrija una y no la otra, quedan dos verdades. El filtro lee [ExigePermiso]
/// para saber la tabla y [ExigeFlujo] para saber la opción de menú.
///
/// Por eso [ExigeFlujo] sin [ExigePermiso] en el mismo controlador no tiene tabla de la que
/// leer el estado: el filtro lo audita como "sin objeto" y deja pasar, igual que
/// PermisoAuthorizationFilter hace cuando falta declarar el objeto — nunca castiga al usuario
/// por algo mal configurado del lado nuestro.
///
/// COD_APLICACION y COD_OPCION_APLICACION son las de la PANTALLA, no las de la tabla, porque
/// el mismo COD_TIPO_ESTADO puede tener flujos DISTINTOS según desde qué opción de menú se
/// lo edite —la clave primaria de OPCIONES_PROCESOS_ESTADOS_FLUJOS_DEPENDENCIA incluye las
/// dos—, así que hay que declararlas explícitas y no se pueden inferir de la tabla sola.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public sealed class ExigeFlujoAttribute : Attribute
{
    public string CodAplicacion { get; }
    public string CodOpcionAplicacion { get; }
    public ExigeFlujoAttribute(string codAplicacion, string codOpcionAplicacion)
    {
        CodAplicacion = codAplicacion;
        CodOpcionAplicacion = codOpcionAplicacion;
    }
}
