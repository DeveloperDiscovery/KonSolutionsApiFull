using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad ENTORNO.TIPOS_LECTURA: cómo se lee un producto (a mano, con código de barras, con RFID). Las tres banderas son
/// independientes: un tipo puede permitir más de una forma de lectura. COD_TIPO_LECTURA es tinyint, la base lo asigna (identity).</summary>
public class TiposLectura
{
    public byte? COD_TIPO_LECTURA { get; set; }
    public string DES_TIPO_LECTURA { get; set; } = string.Empty;
    public bool? FLG_MANUAL { get; set; }
    public bool? FLG_CODEBAR { get; set; }
    public bool? FLG_RFID { get; set; }
}

public class TiposLecturaInsertDto
{
    public byte? COD_TIPO_LECTURA { get; set; }
    // La columna es nvarchar(25): más largo se rechaza aquí con un mensaje claro y no se corta en silencio al grabar.
    [Required, StringLength(25)] public string DES_TIPO_LECTURA { get; set; } = string.Empty;
    public bool? FLG_MANUAL { get; set; }
    public bool? FLG_CODEBAR { get; set; }
    public bool? FLG_RFID { get; set; }
}

public class TiposLecturaUpdateDto
{
    [Required] public byte? COD_TIPO_LECTURA { get; set; }
    [Required, StringLength(25)] public string DES_TIPO_LECTURA { get; set; } = string.Empty;
    public bool? FLG_MANUAL { get; set; }
    public bool? FLG_CODEBAR { get; set; }
    public bool? FLG_RFID { get; set; }
}
