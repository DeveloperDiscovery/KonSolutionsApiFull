using System.ComponentModel.DataAnnotations;

namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad ENTORNO.ENTIDADES_CONFIGURAR - tipos equivalentes a la BD. Clave compuesta
/// DES_TABLE + DES_COLUMN. El SHOWALL detecta automáticamente (SYS.objects/columns) las
/// columnas COD_ENTIDAD% de toda la base y crea la fila si todavía no existe — DES_TABLE/
/// DES_COLUMN quedan fijados por ese proceso; DES_COMMANDSQL es el único campo que se edita
/// a mano desde la pantalla.</summary>
public class EntidadesConfigurar
{
    public string DES_TABLE { get; set; } = string.Empty;
    public string DES_COLUMN { get; set; } = string.Empty;
    public string? DES_COMMANDSQL { get; set; }
}

public class EntidadesConfigurarInsertDto
{
    [Required] public string DES_TABLE { get; set; } = string.Empty;
    [Required] public string DES_COLUMN { get; set; } = string.Empty;
    public string? DES_COMMANDSQL { get; set; }
}

public class EntidadesConfigurarUpdateDto
{
    [Required] public string DES_TABLE { get; set; } = string.Empty;
    [Required] public string DES_COLUMN { get; set; } = string.Empty;
    public string? DES_COMMANDSQL { get; set; }
}

public class EntidadesConfigurarSearchDto
{
    [Required] public string DES_TABLE { get; set; } = string.Empty;
    public string? Search { get; set; }
}
