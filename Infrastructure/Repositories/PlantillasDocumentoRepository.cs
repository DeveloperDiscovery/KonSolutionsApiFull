using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PlantillasDocumentoRepository : IPlantillasDocumentoRepository
{
    private const string PRE = "PROC_PROYECTOS_PLANTILLAS_DOCUMENTO_";
    private readonly IDbConnectionFactory _db;
    public PlantillasDocumentoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PlantillaDocumento>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PlantillaDocumento>(PRE + "SHOWALL", commandType: CommandType.StoredProcedure);
    }

    public async Task<PlantillaDocumento?> ShowbyidAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PlantillaDocumento>(PRE + "SHOWBYID", new { COD_PLANTILLA_DOCUMENTO = cod }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<PlantillaDocumento>> SearchAsync(PlantillaDocumentoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PlantillaDocumento>(PRE + "SEARCH", new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ComboAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, PRE + "SHOWCOMBOX");
    }

    private async Task<WriteResult> Escribir(string sp, DynamicParameters p)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(sp, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult { Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number, ErrorProcedure = sp, SqlSentence = SqlSentenceHelper.Build(sp, p) };
        }
    }

    private static DynamicParameters Parametros(PlantillaDocumentoDto dto)
    {
        var p = new DynamicParameters();
        p.Add("COD_PLANTILLA_DOCUMENTO", dto.COD_PLANTILLA_DOCUMENTO); p.Add("DES_PLANTILLA_DOCUMENTO", dto.DES_PLANTILLA_DOCUMENTO);
        p.Add("COD_TIPO_DOCUMENTO_COMPROMISO", string.IsNullOrWhiteSpace(dto.COD_TIPO_DOCUMENTO_COMPROMISO) ? null : dto.COD_TIPO_DOCUMENTO_COMPROMISO);
        p.Add("NUM_DISENO", dto.NUM_DISENO); p.Add("DES_CONTENIDO", dto.DES_CONTENIDO);
        p.Add("FLG_REQUIERE_APROBACION", dto.FLG_REQUIERE_APROBACION); p.Add("FLG_HABILITADO", dto.FLG_HABILITADO); p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        return p;
    }

    public Task<WriteResult> InsertAsync(PlantillaDocumentoDto dto, string codUsuario, string codEstacion)
    {
        var p = Parametros(dto);
        p.Add("COD_USUARIO_REGISTRO", codUsuario); p.Add("COD_ESTACION_REGISTRO", codEstacion);
        return Escribir(PRE + "INSERT", p);
    }

    public Task<WriteResult> UpdateAsync(PlantillaDocumentoDto dto, string codUsuario, string codEstacion)
    {
        var p = Parametros(dto);
        p.Add("COD_USUARIO_ACTUALIZACION", codUsuario); p.Add("COD_ESTACION_ACTUALIZACION", codEstacion);
        return Escribir(PRE + "UPDATE", p);
    }

    public Task<WriteResult> DeleteAsync(string cod)
    {
        var p = new DynamicParameters();
        p.Add("COD_PLANTILLA_DOCUMENTO", cod);
        return Escribir(PRE + "DELETE", p);
    }
}
