using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

public class ComplejidadPassword
{
    public string COD_PASSWORD_COMPLEJIDAD { get; set; } = string.Empty;
    public string DES_PASSWORD_COMPLEJIDAD { get; set; } = string.Empty;
    public string DES_CARACTERES_LETRAS_MAYUSCULAS { get; set; } = string.Empty;
    public string DES_CARACTERES_LETRAS_MINUSCULAS { get; set; } = string.Empty;
    public string DES_CARACTERES_NUMERICOS { get; set; } = string.Empty;
    public string DES_CARACTERES_ESPECIALES { get; set; } = string.Empty;
    public string CAN_CARACTERES_MINIMO { get; set; } = string.Empty;
    public string CAN_CARACTERES_MAXIMO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class ComplejidadPasswordInsertDto
{
    [Required] public string COD_PASSWORD_COMPLEJIDAD { get; set; } = string.Empty;
    [Required] public string DES_PASSWORD_COMPLEJIDAD { get; set; } = string.Empty;
    // Campos de caracteres: OPCIONALES (la tabla acepta NULL). Sin [Required] para
    // permitir grabar cuando se dejan vacíos.
    public string? DES_CARACTERES_LETRAS_MAYUSCULAS { get; set; }
    public string? DES_CARACTERES_LETRAS_MINUSCULAS { get; set; }
    public string? DES_CARACTERES_NUMERICOS { get; set; }
    public string? DES_CARACTERES_ESPECIALES { get; set; }
    public int? CAN_CARACTERES_MINIMO { get; set; }
    public int? CAN_CARACTERES_MAXIMO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class ComplejidadPasswordUpdateDto
{
    [Required] public string COD_PASSWORD_COMPLEJIDAD { get; set; } = string.Empty;
    [Required] public string DES_PASSWORD_COMPLEJIDAD { get; set; } = string.Empty;
    [Required] public string DES_CARACTERES_LETRAS_MAYUSCULAS { get; set; } = string.Empty;
    [Required] public string DES_CARACTERES_LETRAS_MINUSCULAS { get; set; } = string.Empty;
    [Required] public string DES_CARACTERES_NUMERICOS { get; set; } = string.Empty;
    [Required] public string DES_CARACTERES_ESPECIALES { get; set; } = string.Empty;
    public int? CAN_CARACTERES_MINIMO { get; set; }
    public int? CAN_CARACTERES_MAXIMO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class ComplejidadPasswordSearchDto
{
    public string? Search { get; set; }
}