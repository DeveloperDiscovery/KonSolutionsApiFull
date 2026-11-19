using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ESTANDAR.TIPOS_PERSONAS - tipos equivalentes a la BD.</summary>
public class TiposPersonas
{
    public byte COD_TIPO_PERSONA { get; set; }
    public string DES_TIPO_PERSONA { get; set; } = string.Empty;
}

public class TiposPersonasInsertDto
{
    [Required] public byte COD_TIPO_PERSONA { get; set; }
    [Required] public string DES_TIPO_PERSONA { get; set; } = string.Empty;
}
