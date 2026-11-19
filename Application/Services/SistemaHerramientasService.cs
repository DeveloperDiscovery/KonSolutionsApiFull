using System.Text;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Sistema;

namespace KonSolutionsApi.Application.Services;

public class SistemaHerramientasService : ISistemaHerramientasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(SistemaHerramientasService);
    private const string KeyPathRepositorio = "KEY_PATH_FILE_SISTEMAS_REPOSITORY";

    private readonly ISistemaHerramientasRepository _repo;
    private readonly IPathFilesRepository _pathFiles;
    private readonly ErrorLogService _log;

    public SistemaHerramientasService(ISistemaHerramientasRepository repo, IPathFilesRepository pathFiles, ErrorLogService log)
    {
        _repo = repo;
        _pathFiles = pathFiles;
        _log = log;
    }

    public async Task<ApiResponse<List<string>>> GetTablasAsync()
    {
        try { return ApiResponse<List<string>>.Ok(await _repo.GetTablasAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetTablasAsync));
              return ApiResponse<List<string>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GenerarSpsTextResult>> GenerarSpsTextAsync(GenerarSpsTextRequest dto, string codUsuario, string codEstacion)
    {
        try
        {
            var pathFile = await _pathFiles.ShowbyidAsync(KeyPathRepositorio);
            if (pathFile is null || string.IsNullOrWhiteSpace(pathFile.DES_PATH_FILE))
                return ApiResponse<GenerarSpsTextResult>.Fail(
                    $"No se encontró la ruta configurada con la llave '{KeyPathRepositorio}' en Entorno → Rutas de Archivos.");

            var sps = await _repo.GetSpsPorPrefijoAsync(dto.PrefijoSp.Trim());

            List<ColumnaTabla> columnas = new();
            var tablaEncontrada = false;
            if (!string.IsNullOrWhiteSpace(dto.NombreTabla))
            {
                columnas = await _repo.GetEstructuraTablaAsync(dto.NombreTabla.Trim());
                tablaEncontrada = columnas.Count > 0;
            }

            var contenido = ArmarContenido(dto, sps, columnas);

            Directory.CreateDirectory(pathFile.DES_PATH_FILE);
            var nombreArchivo = $"{dto.PrefijoSp.Trim()}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            var rutaCompleta = Path.Combine(pathFile.DES_PATH_FILE, nombreArchivo);
            await File.WriteAllTextAsync(rutaCompleta, contenido, Encoding.UTF8);

            return ApiResponse<GenerarSpsTextResult>.Ok(new GenerarSpsTextResult
            {
                RutaArchivo = rutaCompleta,
                CantidadSps = sps.Count,
                TablaEncontrada = tablaEncontrada
            }, "Archivo generado correctamente.");
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(GenerarSpsTextAsync));
            return ApiResponse<GenerarSpsTextResult>.Fail(ex.Message);
        }
    }

    private static string ArmarContenido(GenerarSpsTextRequest dto, List<SpEncontrado> sps, List<ColumnaTabla> columnas)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
        sb.AppendLine($"Prefijo SP: {dto.PrefijoSp}");
        if (!string.IsNullOrWhiteSpace(dto.NombreTabla)) sb.AppendLine($"Tabla: {dto.NombreTabla}");
        sb.AppendLine(new string('=', 80));
        sb.AppendLine();

        sb.AppendLine($"STORED PROCEDURES ({sps.Count} encontrados)");
        sb.AppendLine(new string('-', 80));
        if (sps.Count == 0)
        {
            sb.AppendLine("(No se encontraron stored procedures con ese prefijo.)");
        }
        else
        {
            foreach (var sp in sps)
            {
                sb.AppendLine($"-- {sp.Nombre}");
                sb.AppendLine(sp.Definicion ?? "");
                sb.AppendLine();
                sb.AppendLine(new string('-', 80));
            }
        }

        if (!string.IsNullOrWhiteSpace(dto.NombreTabla))
        {
            sb.AppendLine();
            sb.AppendLine($"ESTRUCTURA DE TABLA: {dto.NombreTabla}");
            sb.AppendLine(new string('-', 80));
            if (columnas.Count == 0)
            {
                sb.AppendLine("(No se encontró la tabla.)");
            }
            else
            {
                sb.AppendLine($"Schema: {columnas[0].SchemaName}");
                sb.AppendLine();
                foreach (var c in columnas)
                {
                    var tipo = c.TipoDato;
                    if (c.MaxLength.HasValue) tipo += $"({c.MaxLength})";
                    else if (c.Precision.HasValue) tipo += $"({c.Precision},{c.Scale ?? 0})";
                    var nullable = c.EsNullable ? "NULL" : "NOT NULL";
                    var def = string.IsNullOrWhiteSpace(c.ValorPorDefecto) ? "" : $" DEFAULT {c.ValorPorDefecto}";
                    sb.AppendLine($"{c.OrdinalPosition,3}. {c.NombreColumna,-40} {tipo,-20} {nullable}{def}");
                }
            }
        }

        return sb.ToString();
    }
}
