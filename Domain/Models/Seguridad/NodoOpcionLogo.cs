namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>PROC_SEGURIDAD_APLICACIONES_OPCINOES_TREVIEWLOGOS — experimento: íconos de
/// TODOS los nodos del árbol (Raíz/Aplicación/Opciones) en un SP aparte del Showall de
/// texto, identificados por el mismo par COD_PARENT/COD_CHILDREN que arma la jerarquía.</summary>
public class NodoOpcionLogo
{
    public string COD_PARENT { get; set; } = "";
    public string COD_CHILDREN { get; set; } = "";
    public byte[]? IMG_ICONO { get; set; }
}
