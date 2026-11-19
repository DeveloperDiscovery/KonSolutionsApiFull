using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposDocumentoCompromisoRepository : ITiposDocumentoCompromisoRepository
{
    private const string PRE = "PROC_PROYECTOS_TIPOS_DOCUMENTO_COMPROMISO_";
    private readonly IDbConnectionFactory _db;
    public TiposDocumentoCompromisoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposDocumentoCompromiso>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDocumentoCompromiso>(PRE + "SHOWALL", commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposDocumentoCompromiso?> ShowbyidAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposDocumentoCompromiso>(PRE + "SHOWBYID", new { COD_TIPO_DOCUMENTO_COMPROMISO = cod }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TiposDocumentoCompromiso>> SearchAsync(TiposDocumentoCompromisoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDocumentoCompromiso>(PRE + "SEARCH", new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
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

    public Task<WriteResult> InsertAsync(TiposDocumentoCompromisoInsertDto dto, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("COD_TIPO_DOCUMENTO_COMPROMISO", dto.COD_TIPO_DOCUMENTO_COMPROMISO); p.Add("DES_TIPO_DOCUMENTO_COMPROMISO", dto.DES_TIPO_DOCUMENTO_COMPROMISO); p.Add("COD_CLASE_COMPROMISO", dto.COD_CLASE_COMPROMISO);
        p.Add("DES_DESCRIPCION", dto.DES_DESCRIPCION); p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario); p.Add("COD_ESTACION_REGISTRO", codEstacion);
        return Escribir(PRE + "INSERT", p);
    }

    public Task<WriteResult> UpdateAsync(TiposDocumentoCompromisoUpdateDto dto)
    {
        var p = new DynamicParameters();
        p.Add("COD_TIPO_DOCUMENTO_COMPROMISO", dto.COD_TIPO_DOCUMENTO_COMPROMISO); p.Add("DES_TIPO_DOCUMENTO_COMPROMISO", dto.DES_TIPO_DOCUMENTO_COMPROMISO); p.Add("COD_CLASE_COMPROMISO", dto.COD_CLASE_COMPROMISO);
        p.Add("DES_DESCRIPCION", dto.DES_DESCRIPCION); p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        return Escribir(PRE + "UPDATE", p);
    }

    public Task<WriteResult> DeleteAsync(string cod)
    {
        var p = new DynamicParameters();
        p.Add("COD_TIPO_DOCUMENTO_COMPROMISO", cod);
        return Escribir(PRE + "DELETE", p);
    }
}
