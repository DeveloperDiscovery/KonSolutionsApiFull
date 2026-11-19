using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad TIPOS_DOCUMENTO_IDENTIDAD - tipos equivalentes a la BD.</summary>
public class TiposDocumentoIdentidad
{
    public int? COD_TIPO_DOCUMENTO_IDENTIDAD { get; set; }
    public string DES_TIPO_DOCUMENTO_IDENTIDAD { get; set; } = string.Empty;
    public string COD_TIPO_DOCUMENTO_IDENTIDAD_SUNAT { get; set; } = string.Empty;
    public int? VAL_LONGITUD { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposDocumentoIdentidadInsertDto
{
    public int? COD_TIPO_DOCUMENTO_IDENTIDAD { get; set; }
    [Required] public string DES_TIPO_DOCUMENTO_IDENTIDAD { get; set; } = string.Empty;
    public int? VAL_LONGITUD { get; set; }
    /// <summary>Código del tipo de documento en el catálogo 06 de SUNAT (1 DNI, 6 RUC, 4 carné de extranjería, 7
    /// pasaporte...): uno o dos caracteres, letras o números. Vacío se graba como NULL; el repositorio lo pasa a
    /// mayúsculas.</summary>
    [RegularExpression(@"^[0-9A-Za-z]{1,2}$", ErrorMessage = "El código SUNAT del tipo de documento es de uno o dos caracteres, letras o números (por ejemplo, 1 o 6).")]
    public string? COD_TIPO_DOCUMENTO_IDENTIDAD_SUNAT { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposDocumentoIdentidadUpdateDto
{
    public int? COD_TIPO_DOCUMENTO_IDENTIDAD { get; set; }
    [Required] public string DES_TIPO_DOCUMENTO_IDENTIDAD { get; set; } = string.Empty;
    public int? VAL_LONGITUD { get; set; }
    /// <summary>Código del tipo de documento en el catálogo 06 de SUNAT (1 DNI, 6 RUC, 4 carné de extranjería, 7
    /// pasaporte...): uno o dos caracteres, letras o números. Vacío se graba como NULL; el repositorio lo pasa a
    /// mayúsculas.</summary>
    [RegularExpression(@"^[0-9A-Za-z]{1,2}$", ErrorMessage = "El código SUNAT del tipo de documento es de uno o dos caracteres, letras o números (por ejemplo, 1 o 6).")]
    public string? COD_TIPO_DOCUMENTO_IDENTIDAD_SUNAT { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposDocumentoIdentidadSearchDto
{
    public string? Search { get; set; }
}