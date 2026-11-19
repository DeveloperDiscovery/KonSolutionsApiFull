namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Fila sugerida para asignar como usuario del sistema, a partir de una
/// ESTANDAR.ENTIDADES sin usuario relacionado todavía (COD_USUARIO_RELACIONADO vacío).
/// Alimenta tanto PROC_ESTANDAR_ENTIDADES_USUARIOS_COMERCIALES (perfil "Usuario Comercial")
/// como PROC_ESTANDAR_ENTIDADES_DEPENDIENTES (perfil "Usuario de Dependencia Comercial") —
/// esta última agrega DES_ENTIDAD_DEPENDENCIA, ausente en la primera.</summary>
public class EntidadUsuarioSugerido
{
    public decimal? COD_ENTIDAD { get; set; }
    public string DES_ENTIDAD { get; set; } = string.Empty;
    public string? DES_ENTIDAD_DEPENDENCIA { get; set; }
    public int? COD_TIPO_DOCUMENTO_IDENTIDAD { get; set; }
    public string NUM_DOCUMENTO_IDENTIDAD { get; set; } = string.Empty;
    public string? DES_PREFIJO { get; set; }
    public string COD_USUARIO_SUGERIDO { get; set; } = string.Empty;
}
