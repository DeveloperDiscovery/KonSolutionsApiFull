using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

/// <summary>Gestión genérica de documentos guardados como archivos físicos en una carpeta —
/// reutilizable desde cualquier módulo (Solicitudes Comerciales, Entidades, etc.). La ruta base
/// se resuelve por codKeyPathfile (clave de ENTORNO.PATHFILES); identificador es la subcarpeta
/// (ej. el N° de Solicitud, el código de una Entidad).</summary>
public interface IDocumentosCarpetaService
{
    Task<ApiResponse<IEnumerable<DocumentoCarpetaDto>>> ListAsync(string codKeyPathfile, string identificador);
    Task<ApiResponse<bool>> UploadAsync(string codKeyPathfile, string identificador, IFormFile archivo);
    Task<ApiResponse<bool>> DeleteAsync(string codKeyPathfile, string identificador, string nombreArchivo);
}
