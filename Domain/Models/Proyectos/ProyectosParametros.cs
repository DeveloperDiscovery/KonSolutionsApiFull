namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Parámetros del módulo de Proyectos (una sola fila). La ruta es una clave de ENTORNO.PATHFILES.</summary>
public class ProyectosParametros
{
    public string? COD_KEY_PATHFILE_PROYECTOS { get; set; }
    /// <summary>Carpeta a la que apunta la clave (solo lectura).</summary>
    public string? DES_PATH_FILE { get; set; }
    /// <summary>Tipo de valor de cálculo del comprobante que cuenta como costo (sin impuestos). Por omisión IMTOT.</summary>
    public string? COD_TIPO_VALOR_CALCULO_COSTO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

public class ProyectosParametrosGuardarDto
{
    public string? COD_KEY_PATHFILE_PROYECTOS { get; set; }
    public string? COD_TIPO_VALOR_CALCULO_COSTO { get; set; }
}
