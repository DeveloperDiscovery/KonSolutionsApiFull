using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>DTO para PROC_ENTORNO_EJECUTAR_SENTENCIAS — ejecuta el DES_COMMANDSQL de una
/// acción de notificación (ENTORNO.NOTIFICACIONES_EJECUCION). El comando ya viene armado
/// y resuelto (con los códigos de la notificación puntual) desde quien configuró la acción
/// — no se concatena nada con datos ingresados por el usuario final en tiempo de ejecución.</summary>
public class EjecutarSentenciaDto
{
    [Required] public string DES_SENTENCIA { get; set; } = string.Empty;
}
