namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>Nodo del treeview de opciones de aplicaciones.</summary>
public class NodoOpcion
{
    public string  COD_PARENT              { get; set; } = "";
    public string  COD_CHILDREN            { get; set; } = "";
    public string  DES_OPCION              { get; set; } = "";
    public string  COD_OPCION              { get; set; } = "";
    public string  COD_APLICACION_ORIGINAL { get; set; } = "";
    public string  COD_OPCION_ORIGINAL     { get; set; } = "";
    public string  COD_BOTON_ORIGINAL      { get; set; } = "";
    public string  DES_PAGINA              { get; set; } = "";
    public string  COD_TIPO                    { get; set; } = "";
    public byte[]? IMG_ICONO                   { get; set; }
    public int?    COD_ESTADO                  { get; set; }
    public string  DES_APLICACION              { get; set; } = "";
    public string  DES_OPCION_APLICACION       { get; set; } = "";
    public string  DES_OPCION_APLICACION_PADRE  { get; set; } = "";
    public string  COD_OPCION_APLICACION_PADRE  { get; set; } = "";
    public bool?   FLG_EXIGE_PASSWORD           { get; set; }
    public bool?   FLG_TECLADO_VIRTUAL          { get; set; }
    public bool?   FLG_MULTI_LOAD               { get; set; }
}
