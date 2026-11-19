using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

/// <summary>Lo que el servicio de XML recibidos necesita de Microsoft 365 (Microsoft Graph). Separado en una interfaz para poder probar el servicio con un buzón de mentira.</summary>
public interface IGraphCorreo
{
    bool Configurado { get; }
    string? Buzon { get; }
    /// <summary>El id de la carpeta del buzón a partir de su ruta («Bandeja de entrada/Comprobantes»), o null si no existe.</summary>
    Task<string?> ResolverCarpetaAsync(string ruta, CancellationToken ct);
    Task<IReadOnlyList<CorreoResumen>> ListarCorreosAsync(string carpetaId, int max, CancellationToken ct);
    Task<IReadOnlyList<AdjuntoResumen>> ListarAdjuntosAsync(string correoId, CancellationToken ct);
    Task<byte[]> DescargarAdjuntoAsync(string correoId, string adjuntoId, CancellationToken ct);
    Task MoverAsync(string correoId, string carpetaDestinoId, CancellationToken ct);
}

public interface IXmlRecibidosRepository
{
    Task<ConfiguracionGeneral?> ConfigAsync();
    /// <summary>Carga el XML (PROC_XML_RECIBIDOS_CARGAR). Si el XML se rechaza (vacío, mal formado, de otro RUC, emitido por la empresa...) lanza SqlException con un número 50700 a 50734.</summary>
    Task<(string Clase, decimal Numero, bool Nuevo)> CargarAsync(string xml, string nombreArchivo, string codUsuario, string codEstacion);
    Task AsignarPdfAsync(string clase, decimal numero, string nombrePdf);
}

public interface IXmlRecibidosService
{
    /// <summary>Una corrida: lee la carpeta de recepción del buzón, baja los XML y PDF (desempaquetando los ZIP), carga los XML y mueve cada correo a procesados o a errores.
    /// Programada: si la carga automática está apagada no hace nada. Manual (ignorarActivo): corre aunque esté apagada.</summary>
    Task<XmlRecibidosCiclo> EjecutarAsync(bool ignorarActivo, CancellationToken ct);
    Task<XmlRecibidosEstadoDto> EstadoAsync();
}
