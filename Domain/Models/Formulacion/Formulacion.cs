namespace KonSolutionsApi.Domain.Models.Formulacion;

/// <summary>Campos de auditoria que devuelven los SP del constructor de formulas (script SQL 145). El panel de auditoria de la web los lee.</summary>
public abstract class FormulacionAuditable
{
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

/// <summary>Categoria de la paleta (CONSM, CONST, OPEMT, ...): trae su color y sus banderas.</summary>
public class FormulacionComponente : FormulacionAuditable
{
    public string COD_COMPONENTE_FORMULACION { get; set; } = string.Empty;
    public string? DES_COMPONENTE_FORMULACION { get; set; }
    public bool? FLG_ELEMENTOS { get; set; }
    public bool? FLG_HABILITADO { get; set; }
    public bool? FLG_ESPECIFICACIONES_CONSUMO { get; set; }
    public bool? FLG_ESPECIFICACIONES { get; set; }
    public bool? FLG_CONSUMO { get; set; }
    public bool? FLG_CONSUMO_SUPERIOR { get; set; }
    public bool? FLG_CONSUMO_INFERIOR { get; set; }
    public bool? FLG_ESPECIFICACIONES_PIVOT { get; set; }
    public string? DES_NAME_TABLE { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_FORECOLOR { get; set; }
    public byte[]? IMG_PICTURE { get; set; }
    public int CAN_ELEMENTOS { get; set; }
}

public class FormulacionComponenteGuardarDto
{
    public string COD_COMPONENTE_FORMULACION { get; set; } = string.Empty;
    public string DES_COMPONENTE_FORMULACION { get; set; } = string.Empty;
    public bool? FLG_ELEMENTOS { get; set; } = true;
    public bool? FLG_HABILITADO { get; set; } = true;
    public bool? FLG_ESPECIFICACIONES_CONSUMO { get; set; }
    public bool? FLG_ESPECIFICACIONES { get; set; }
    public bool? FLG_CONSUMO { get; set; }
    public bool? FLG_CONSUMO_SUPERIOR { get; set; }
    public bool? FLG_CONSUMO_INFERIOR { get; set; }
    public bool? FLG_ESPECIFICACIONES_PIVOT { get; set; }
    public string? DES_NAME_TABLE { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_FORECOLOR { get; set; }
    /// <summary>Si no llega, la base deja la imagen que ya tiene.</summary>
    public byte[]? IMG_PICTURE { get; set; }
}

/// <summary>Ficha de un componente (un valor, un operador, una funcion...). COD_SINTAXIS es su rol en la formula: VAL, OPE, ABR, CIE, CAS, FIN, THE, ELS, SEP o NOT.</summary>
public class FormulacionElemento : FormulacionAuditable
{
    public string COD_COMPONENTE_FORMULACION { get; set; } = string.Empty;
    public string? DES_COMPONENTE_FORMULACION { get; set; }
    public string COD_ELEMENTO_FORMULACION { get; set; } = string.Empty;
    /// <summary>Fragmento tecnico que se guarda en la formula (por ejemplo CAN_CONSUMO o SUMA().</summary>
    public string? DES_ELEMENTO_FORMULACION { get; set; }
    /// <summary>Lo que ve el usuario en la ficha.</summary>
    public string? COD_ALIAS_FORMULACION { get; set; }
    public string? COD_TIPO_ESPECIFICACION_TECNICA { get; set; }
    public string? DES_TIPO_ESPECIFICACION_TECNICA { get; set; }
    public bool? FLG_NO_ES_RANGO { get; set; }
    public bool? FLG_HABILITADO { get; set; }
    public byte[]? IMG_PICTURE { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_FORECOLOR { get; set; }
    public string? COD_SINTAXIS { get; set; }
    /// <summary>Cuantas fichas de formulas armadas lo usan (solo viene al consultar uno).</summary>
    public int? CAN_USOS { get; set; }
}

public class FormulacionElementoGuardarDto
{
    public string COD_COMPONENTE_FORMULACION { get; set; } = string.Empty;
    /// <summary>Al crear puede venir vacio: la base asigna el siguiente codigo del componente.</summary>
    public string? COD_ELEMENTO_FORMULACION { get; set; }
    public string DES_ELEMENTO_FORMULACION { get; set; } = string.Empty;
    public string COD_ALIAS_FORMULACION { get; set; } = string.Empty;
    public string? COD_TIPO_ESPECIFICACION_TECNICA { get; set; }
    public bool? FLG_NO_ES_RANGO { get; set; }
    public bool? FLG_HABILITADO { get; set; } = true;
    public byte[]? IMG_PICTURE { get; set; }
}

/// <summary>Cabecera de una formula de consumo.</summary>
public class FormulaCabecera : FormulacionAuditable
{
    public int NUM_FORMULACION { get; set; }
    public string? DES_FORMULACION { get; set; }
    public string? COD_GRUPO_PRODUCTO_CONSUMO { get; set; }
    public string? COD_FAMILIA_PRODUCTO_CONSUMO { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO_CONSUMO { get; set; }
    public string? DES_SUB_FAMILIA_CONSUMO { get; set; }
    public string? COD_GRUPO_PRODUCTO { get; set; }
    public string? COD_FAMILIA_PRODUCTO { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO { get; set; }
    public string? DES_SUB_FAMILIA_PRODUCTO { get; set; }
    /// <summary>Texto tecnico, armado por la base con los fragmentos de las fichas.</summary>
    public string? DES_FORMULA { get; set; }
    /// <summary>Texto que se muestra, armado por la base con los alias de las fichas.</summary>
    public string? DES_ALIAS { get; set; }
    public short? COD_TIPO_ESTADO { get; set; }
    public short? COD_ESTADO { get; set; }
    public string? DES_ESTADO { get; set; }
    public string? DES_ESTADO_BACKCOLOR { get; set; }
    public string? DES_ESTADO_FORECOLOR { get; set; }
    public int CAN_FICHAS { get; set; }
}

public class FormulaFicha
{
    public int NUM_FORMULACION { get; set; }
    public short NUM_SECUENCIA { get; set; }
    public string COD_COMPONENTE_FORMULACION { get; set; } = string.Empty;
    public string? DES_COMPONENTE_FORMULACION { get; set; }
    public string COD_ELEMENTO_FORMULACION { get; set; } = string.Empty;
    public string? DES_ELEMENTO_FORMULACION { get; set; }
    public string? COD_ALIAS_FORMULACION { get; set; }
    /// <summary>Fragmento tecnico (o el valor, si es una constante).</summary>
    public string? DES_FORMULA { get; set; }
    public string? DES_ALIAS { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_FORECOLOR { get; set; }
    public string? COD_SINTAXIS { get; set; }
    /// <summary>true: la ficha es una especificacion tecnica (su texto va en DES_FORMULA).</summary>
    public bool? FLG_ESPECIFICACION { get; set; }
}

/// <summary>Una formula con sus fichas en orden.</summary>
public class FormulaDetalle
{
    public FormulaCabecera Cabecera { get; set; } = new();
    public List<FormulaFicha> Fichas { get; set; } = new();
}

/// <summary>Una ficha al grabar o validar. DES_FORMULA solo se usa en las constantes (CONST): es su valor.</summary>
public class FormulaFichaDto
{
    public string COD_COMPONENTE_FORMULACION { get; set; } = string.Empty;
    public string COD_ELEMENTO_FORMULACION { get; set; } = string.Empty;
    public string? DES_FORMULA { get; set; }
    /// <summary>Solo en las fichas de especificaciones tecnicas (el elemento puede ir vacio: la base elige el portador).</summary>
    public string? COD_ESPECIFICACION_TECNICA { get; set; }
}

public class FormulaGuardarDto
{
    /// <summary>Vacio o 0 = formula nueva.</summary>
    public int? NUM_FORMULACION { get; set; }
    public string DES_FORMULACION { get; set; } = string.Empty;
    public string? COD_GRUPO_PRODUCTO_CONSUMO { get; set; }
    public string? COD_FAMILIA_PRODUCTO_CONSUMO { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO_CONSUMO { get; set; }
    public string? COD_GRUPO_PRODUCTO { get; set; }
    public string? COD_FAMILIA_PRODUCTO { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO { get; set; }
    /// <summary>0 EN DESARROLLO, 1 ACTIVA, 2 INACTIVA. Vacio = EN DESARROLLO. Modificar una formula ya grabada con el mismo estado la devuelve a EN DESARROLLO.</summary>
    public short? COD_ESTADO { get; set; }
    public List<FormulaFichaDto> Fichas { get; set; } = new();
}

public class FormulaValidarDto
{
    public List<FormulaFichaDto> Fichas { get; set; } = new();
}

/// <summary>Un problema de la forma de la formula. NUM_POSICION es la ficha (empieza en 1); viene vacio si el problema es de toda la formula.</summary>
public class FormulaError
{
    public int NUM { get; set; }
    public int? NUM_POSICION { get; set; }
    public string DES_ERROR { get; set; } = string.Empty;
}

public class FormulaValidacion
{
    public bool ES_VALIDA => Errores.Count == 0;
    public List<FormulaError> Errores { get; set; } = new();
}

/// <summary>Fila de la paleta: un componente habilitado con una de sus fichas habilitadas (COD_ELEMENTO_FORMULACION viene vacio si el componente no tiene ninguna).</summary>
public class PaletaFicha
{
    public string COD_COMPONENTE_FORMULACION { get; set; } = string.Empty;
    public string? DES_COMPONENTE_FORMULACION { get; set; }
    public bool? FLG_ESPECIFICACIONES_CONSUMO { get; set; }
    public bool? FLG_ESPECIFICACIONES { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_FORECOLOR { get; set; }
    public byte[]? IMG_COMPONENTE { get; set; }
    public string? COD_ELEMENTO_FORMULACION { get; set; }
    public string? DES_ELEMENTO_FORMULACION { get; set; }
    public string? COD_ALIAS_FORMULACION { get; set; }
    public string? COD_TIPO_ESPECIFICACION_TECNICA { get; set; }
    public bool? FLG_NO_ES_RANGO { get; set; }
    public byte[]? IMG_ELEMENTO { get; set; }
    public string? COD_SINTAXIS { get; set; }
    public string? COD_ESPECIFICACION_TECNICA { get; set; }
    public string? DES_ESPECIFICACION_TECNICA_SINGULAR { get; set; }
}

public class PaletaConsultaDto
{
    public string? COD_GRUPO_PRODUCTO_CONSUMO { get; set; }
    public string? COD_FAMILIA_PRODUCTO_CONSUMO { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO_CONSUMO { get; set; }
    public string? COD_GRUPO_PRODUCTO { get; set; }
    public string? COD_FAMILIA_PRODUCTO { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO { get; set; }
}
