namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Matrices de operaciones (script SQL 130): arboles de operaciones modelo, ligados opcionalmente a una sub familia.
/// Se asignan a una version de diseno como COPIA independiente, que se puede editar sin tocar la matriz.</summary>
public class MatrizResumen
{
    public string COD_RUTA_PRODUCCION { get; set; } = "";
    public string? DES_RUTA_PRODUCCION { get; set; }
    public string? COD_TIPO_RUTA_PRODUCCION { get; set; }
    public string? DES_TIPO_RUTA_PRODUCCION { get; set; }
    public string? COD_GRUPO_PRODUCTO { get; set; }
    public string? COD_FAMILIA_PRODUCTO { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO { get; set; }
    public string? DES_SUB_FAMILIA_PRODUCTO { get; set; }
    public int CAN_ELEMENTOS { get; set; }
    public int CAN_HOJAS_CATALOGO { get; set; }
    public int CAN_HOJAS_SIN_TIEMPO { get; set; }
    public decimal? VAL_SAM_TOTAL { get; set; }
    public int CAN_COPIAS { get; set; }
    public int CAN_VERSIONES { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

/// <summary>Una matriz que se puede asignar a una version; FLG_SUGERIDA = es de la sub familia del diseno.</summary>
public class MatrizParaVersion
{
    public string COD_RUTA_PRODUCCION { get; set; } = "";
    public string? DES_RUTA_PRODUCCION { get; set; }
    public string? COD_GRUPO_PRODUCTO { get; set; }
    public string? COD_FAMILIA_PRODUCTO { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO { get; set; }
    public string? DES_SUB_FAMILIA_PRODUCTO { get; set; }
    public int CAN_ELEMENTOS { get; set; }
    public int CAN_HOJAS_CATALOGO { get; set; }
    public int CAN_HOJAS_SIN_TIEMPO { get; set; }
    public decimal? VAL_SAM_TOTAL { get; set; }
    public bool FLG_SUGERIDA { get; set; }
}

/// <summary>Alta y cambio de una matriz. La sub familia son los tres codigos juntos o ninguno.</summary>
public class MatrizGuardarDto
{
    public string? COD_RUTA_PRODUCCION { get; set; }
    public string DES_RUTA { get; set; } = "";
    public string COD_TIPO_RUTA { get; set; } = "PRD";
    public string? COD_GRUPO_PRODUCTO { get; set; }
    public string? COD_FAMILIA_PRODUCTO { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO { get; set; }
}

public class MatrizDuplicarDto
{
    public string COD_RUTA_PRODUCCION { get; set; } = "";
    public string? DES_RUTA { get; set; }
}

/// <summary>Asignar (copiar) una matriz a una version. FEC_TIEMPOS = fecha para tomar la version de tiempo vigente (hoy si no se indica).
/// REEMPLAZAR = la version ya tiene ruta y se acepta cambiarla (la anterior no se borra).</summary>
public class MatrizAsignarDto
{
    public string COD_DISENO { get; set; } = "";
    public int NUM_VERSION { get; set; }
    public string COD_MATRIZ { get; set; } = "";
    public DateTime? FEC_TIEMPOS { get; set; }
    public bool REEMPLAZAR { get; set; }
}

public class MatrizAsignarResultado
{
    public string COD_RUTA_PRODUCCION { get; set; } = "";
    public int ELEMENTOS_COPIADOS { get; set; }
    public int PRECEDENCIAS_COPIADAS { get; set; }
}

/// <summary>Convertir la ruta de una version en una matriz nueva. SIN_SUB_FAMILIA = dejarla sin sub familia;
/// si no se indica ninguna, toma la del diseno.</summary>
public class GuardarComoMatrizDto
{
    public string COD_DISENO { get; set; } = "";
    public int NUM_VERSION { get; set; }
    public string DES_MATRIZ { get; set; } = "";
    public string? COD_GRUPO_PRODUCTO { get; set; }
    public string? COD_FAMILIA_PRODUCTO { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO { get; set; }
    public bool SIN_SUB_FAMILIA { get; set; }
}

/// <summary>Resultado de darle a una version su propia copia de la ruta que compartia con otras (FLG_COPIADA = false: ya no se compartia).</summary>
public class RutaIndependizarResultado
{
    public string COD_RUTA_PRODUCCION { get; set; } = "";
    public int ELEMENTOS_COPIADOS { get; set; }
    public int PRECEDENCIAS_COPIADAS { get; set; }
    public bool FLG_COPIADA { get; set; }
}

/// <summary>Una ruta que usan varias versiones a la vez (el editor de una cambia a las otras).</summary>
public class RutaCompartida
{
    public string COD_RUTA_PRODUCCION { get; set; } = "";
    public string? DES_RUTA_PRODUCCION { get; set; }
    public int CAN_VERSIONES { get; set; }
    public bool FLG_ALGUNA_BLOQUEADA { get; set; }
    public string? DES_VERSIONES { get; set; }
}

/// <summary>La ruta de operaciones de una version de diseno (script 132): de que matriz o ruta es copia, cuantas versiones la comparten y sus totales.</summary>
public class DisenosRutaOperacionesInfo
{
    public string COD_DISENO { get; set; } = "";
    public int NUM_VERSION { get; set; }
    public bool FLG_BLOQUEADO { get; set; }
    public string? COD_RUTA_PRODUCCION { get; set; }
    public string? DES_RUTA_PRODUCCION { get; set; }
    public string? COD_TIPO_RUTA_PRODUCCION { get; set; }
    public int USADA_POR { get; set; }
    public int BLOQUEADAS { get; set; }
    public int ELEMENTOS { get; set; }
    public int CAN_HOJAS_SIN_TIEMPO { get; set; }
    public decimal? VAL_SAM_TOTAL { get; set; }
    public string? COD_RUTA_ORIGEN { get; set; }
    public string? DES_RUTA_ORIGEN { get; set; }
    public bool FLG_ORIGEN_MATRIZ { get; set; }
    public DateTime? FEC_ORIGEN { get; set; }
}
