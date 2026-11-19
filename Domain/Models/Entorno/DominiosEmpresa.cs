using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad ENTORNO.DOMINIOS_EMPRESA — dominios de correo válidos para la empresa
/// (ej. "empresa.com"), usados al completar el correo institucional de un usuario.
/// Tabla mínima: código autonumérico (tinyint IDENTITY) + descripción, sin estado ni
/// auditoría.</summary>
public class DominiosEmpresa
{
    public byte COD_DOMINIO_EMPRESA { get; set; }
    public string DES_DOMINIO_EMPRESA { get; set; } = string.Empty;
}

/// <summary>DTO de guardado. PROC_ENTORNO_DOMINIOS_EMPRESA_INSERT es upsert: si
/// COD_DOMINIO_EMPRESA viene en 0 se trata como alta (el SP devuelve el
/// SCOPE_IDENTITY() asignado); si viene con un código existente, actualiza esa fila.</summary>
public class DominiosEmpresaInsertDto
{
    public byte COD_DOMINIO_EMPRESA { get; set; }
    [Required] public string DES_DOMINIO_EMPRESA { get; set; } = string.Empty;
}
