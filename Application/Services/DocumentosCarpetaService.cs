using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Services;

public class DocumentosCarpetaService : IDocumentosCarpetaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(DocumentosCarpetaService);

    private static readonly HashSet<string> ExtensionesPermitidas =
        new(StringComparer.OrdinalIgnoreCase) { ".pdf", ".jpg", ".jpeg", ".png" };

    private readonly IPathFilesRepository _pathFilesRepo;
    private readonly ErrorLogService _log;

    public DocumentosCarpetaService(IPathFilesRepository pathFilesRepo, ErrorLogService log)
    {
        _pathFilesRepo = pathFilesRepo;
        _log = log;
    }

    private async Task<string> ResolverCarpetaAsync(string codKeyPathfile, string identificador)
    {
        var pf = await _pathFilesRepo.ShowbyidAsync(codKeyPathfile);
        if (pf is null || string.IsNullOrWhiteSpace(pf.DES_PATH_FILE))
            throw new InvalidOperationException(
                $"No se encontró la ruta configurada para la clave '{codKeyPathfile}' en ENTORNO.PATHFILES.");

        // Path.GetFileName evita path traversal si identificador llegara con "..\" o "/".
        var subcarpeta = Path.GetFileName(identificador);
        var carpeta = Path.Combine(pf.DES_PATH_FILE, subcarpeta);
        Directory.CreateDirectory(carpeta);
        return carpeta;
    }

    public async Task<ApiResponse<IEnumerable<DocumentoCarpetaDto>>> ListAsync(string codKeyPathfile, string identificador)
    {
        try
        {
            var carpeta = await ResolverCarpetaAsync(codKeyPathfile, identificador);
            var archivos = Directory.GetFiles(carpeta)
                .Select(f => new FileInfo(f))
                .OrderByDescending(fi => fi.LastWriteTime)
                .Select(fi => new DocumentoCarpetaDto
                {
                    NombreArchivo = fi.Name,
                    TamanoBytes = fi.Length,
                    FechaModificacion = fi.LastWriteTime,
                    Extension = fi.Extension.TrimStart('.').ToUpperInvariant(),
                    RutaCompleta = fi.FullName
                });
            return ApiResponse<IEnumerable<DocumentoCarpetaDto>>.Ok(archivos);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(ListAsync));
            return ApiResponse<IEnumerable<DocumentoCarpetaDto>>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<bool>> UploadAsync(string codKeyPathfile, string identificador, IFormFile archivo)
    {
        try
        {
            if (archivo is null || archivo.Length == 0)
                return ApiResponse<bool>.Fail("No se recibió ningún archivo.");

            var ext = Path.GetExtension(archivo.FileName);
            if (!ExtensionesPermitidas.Contains(ext))
                return ApiResponse<bool>.Fail("Solo se permiten archivos PDF o imágenes (jpg, jpeg, png).");

            var carpeta = await ResolverCarpetaAsync(codKeyPathfile, identificador);
            var nombreSeguro = Path.GetFileName(archivo.FileName);
            var destino = Path.Combine(carpeta, nombreSeguro);

            using (var stream = new FileStream(destino, FileMode.Create))
                await archivo.CopyToAsync(stream);

            return ApiResponse<bool>.Ok(true, "Archivo cargado.");
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(UploadAsync));
            return ApiResponse<bool>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<bool>> DeleteAsync(string codKeyPathfile, string identificador, string nombreArchivo)
    {
        try
        {
            var carpeta = await ResolverCarpetaAsync(codKeyPathfile, identificador);
            var nombreSeguro = Path.GetFileName(nombreArchivo);
            var ruta = Path.Combine(carpeta, nombreSeguro);
            if (File.Exists(ruta)) File.Delete(ruta);
            return ApiResponse<bool>.Ok(true, "Archivo eliminado.");
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
            return ApiResponse<bool>.Fail(ex.Message);
        }
    }
}
