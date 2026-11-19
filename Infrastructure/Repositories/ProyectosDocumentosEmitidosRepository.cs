using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosDocumentosEmitidosRepository : IProyectosDocumentosEmitidosRepository
{
    private const string PRE = "PROC_PROYECTOS_DOCUMENTOS_EMITIDOS_";
    private readonly IDbConnectionFactory _db;
    public ProyectosDocumentosEmitidosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DocumentoEmitido>> ListarAsync(int num)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DocumentoEmitido>(PRE + "SHOWALL", new { NUM_PROYECTO = num }, commandType: CommandType.StoredProcedure);
    }

    public async Task<DocumentoEmitido?> ObtenerAsync(int num, int secuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DocumentoEmitido>(PRE + "SHOWBYID", new { NUM_PROYECTO = num, NUM_SECUENCIA = secuencia }, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> NuevoAsync(int num, DocumentoEmitidoNuevoDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<int>(PRE + "NUEVO", new { NUM_PROYECTO = num, COD_PLANTILLA_DOCUMENTO = dto.CodPlantilla, DES_TITULO = dto.DesTitulo, COD_FASE = dto.CodFase, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task GuardarAsync(int num, int secuencia, DocumentoEmitidoGuardarDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(PRE + "GUARDAR", new { NUM_PROYECTO = num, NUM_SECUENCIA = secuencia, DES_TITULO = dto.DesTitulo, COD_FASE = dto.CodFase, DES_CONTENIDO = dto.DesContenido, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> EnviarAsync(int num, int secuencia, DocumentoEmitidoEnviarDto dto, int? secArchivo, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<int>(PRE + "ENVIAR", new
        {
            NUM_PROYECTO = num, NUM_SECUENCIA = secuencia, COD_TIPO_DOCUMENTO_COMPROMISO = string.IsNullOrWhiteSpace(dto.CodTipoDocumento) ? null : dto.CodTipoDocumento, NUM_SECUENCIA_ARCHIVO = secArchivo,
            RESPONSABLES = string.Join(",", dto.Responsables.Distinct()), APROBADORES = string.Join(",", dto.Aprobadores.Distinct()), COD_USUARIO = codUsuario, COD_ESTACION = codEstacion
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task EmitirAsync(int num, int secuencia, int secArchivo, decimal? entidadEmisor, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(PRE + "EMITIR", new { NUM_PROYECTO = num, NUM_SECUENCIA = secuencia, NUM_SECUENCIA_ARCHIVO = secArchivo, COD_ENTIDAD_EMISOR = entidadEmisor, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> NuevaVersionAsync(int num, int secuencia, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<int>(PRE + "NUEVA_VERSION", new { NUM_PROYECTO = num, NUM_SECUENCIA = secuencia, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task AnularAsync(int num, int secuencia, string motivo, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(PRE + "ANULAR", new { NUM_PROYECTO = num, NUM_SECUENCIA = secuencia, DES_MOTIVO = motivo, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task EliminarAsync(int num, int secuencia)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(PRE + "DELETE", new { NUM_PROYECTO = num, NUM_SECUENCIA = secuencia }, commandType: CommandType.StoredProcedure);
    }

    public async Task<string?> TextoResueltoAsync(int num, string contenido)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<string?>("PROC_PROYECTOS_DOCUMENTO_TEXTO_RESUELTO", new { NUM_PROYECTO = num, DES_CONTENIDO = contenido }, commandType: CommandType.StoredProcedure);
    }
}
