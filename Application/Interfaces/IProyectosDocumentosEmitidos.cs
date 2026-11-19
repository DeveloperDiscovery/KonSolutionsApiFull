using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPlantillasDocumentoRepository
{
    Task<IEnumerable<PlantillaDocumento>> ShowallAsync();
    Task<PlantillaDocumento?> ShowbyidAsync(string cod);
    Task<IEnumerable<PlantillaDocumento>> SearchAsync(PlantillaDocumentoSearchDto dto);
    Task<IEnumerable<ComboboxItem>> ComboAsync();
    Task<WriteResult> InsertAsync(PlantillaDocumentoDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(PlantillaDocumentoDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string cod);
}

public interface IPlantillasDocumentoService
{
    Task<ApiResponse<IEnumerable<PlantillaDocumento>>> GetAllAsync();
    Task<ApiResponse<PlantillaDocumento>> GetByIdAsync(string cod);
    Task<ApiResponse<IEnumerable<PlantillaDocumento>>> SearchAsync(PlantillaDocumentoSearchDto dto);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> ComboAsync();
    Task<ApiResponse<WriteResult>> InsertAsync(PlantillaDocumentoDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(PlantillaDocumentoDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string cod);
}

public interface IProyectosDocumentosEmitidosRepository
{
    Task<IEnumerable<DocumentoEmitido>> ListarAsync(int num);
    Task<DocumentoEmitido?> ObtenerAsync(int num, int secuencia);
    Task<int> NuevoAsync(int num, DocumentoEmitidoNuevoDto dto, string codUsuario, string codEstacion);
    Task GuardarAsync(int num, int secuencia, DocumentoEmitidoGuardarDto dto, string codUsuario, string codEstacion);
    Task<int> EnviarAsync(int num, int secuencia, DocumentoEmitidoEnviarDto dto, int? secArchivo, string codUsuario, string codEstacion);
    Task EmitirAsync(int num, int secuencia, int secArchivo, decimal? entidadEmisor, string codUsuario, string codEstacion);
    Task<int> NuevaVersionAsync(int num, int secuencia, string codUsuario, string codEstacion);
    Task AnularAsync(int num, int secuencia, string motivo, string codUsuario, string codEstacion);
    Task EliminarAsync(int num, int secuencia);
    Task<string?> TextoResueltoAsync(int num, string contenido);
}

public interface IProyectosDocumentosEmitidosService
{
    Task<ApiResponse<IEnumerable<DocumentoEmitido>>> ListarAsync(int num);
    Task<ApiResponse<DocumentoEmitido>> ObtenerAsync(int num, int secuencia);
    Task<ApiResponse<int>> NuevoAsync(int num, DocumentoEmitidoNuevoDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> GuardarAsync(int num, int secuencia, DocumentoEmitidoGuardarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<int>> EnviarAsync(int num, int secuencia, DocumentoEmitidoEnviarDto dto, string? codRol, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> EmitirAsync(int num, int secuencia, DocumentoEmitidoEmitirDto dto, string? codRol, string codUsuario, string codEstacion);
    Task<ApiResponse<int>> NuevaVersionAsync(int num, int secuencia, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> AnularAsync(int num, int secuencia, DocumentoEmitidoAnularDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> EliminarAsync(int num, int secuencia);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> PlantillasAsync();
    Task<ApiResponse<string?>> TextoResueltoAsync(int num, DocumentoEmitidoTextoDto dto);
    /// <summary>El PDF del documento: el congelado si ya se emitio; si no, uno nuevo con el texto actual (vista previa).</summary>
    Task<ApiResponse<ProyectoArchivoContenido>> PdfAsync(int num, int secuencia, string? codRol, string codUsuario, string codEstacion);
}
