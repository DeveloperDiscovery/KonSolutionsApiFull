using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class GuiasRemisionSeriesRepository : IGuiasRemisionSeriesRepository
{
    private readonly IDbConnectionFactory _db;
    public GuiasRemisionSeriesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<GuiaRemisionSerie>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GuiaRemisionSerie>(SP.SCHEMALogistica.GuiasRemisionSeries.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<GuiaRemisionSerie?> ShowbyidAsync(string COD_SERIE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GuiaRemisionSerie>(SP.SCHEMALogistica.GuiasRemisionSeries.Showbyid, new { COD_SERIE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.GuiasRemisionSeries.Showcombox);
    }

    public async Task<WriteResult> InsertAsync(GuiaRemisionSerieInsertDto dto, string codUsuario, string codEstacion)
        => await Grabar(SP.SCHEMALogistica.GuiasRemisionSeries.Insert, dto.COD_SERIE, dto.DES_SERIE, dto.COD_SUCURSAL, dto.NUM_ULTIMO_CORRELATIVO, dto.FLG_ACTIVA, codUsuario, codEstacion);

    public async Task<WriteResult> UpdateAsync(GuiaRemisionSerieUpdateDto dto, string codUsuario, string codEstacion)
        => await Grabar(SP.SCHEMALogistica.GuiasRemisionSeries.Update, dto.COD_SERIE, dto.DES_SERIE, dto.COD_SUCURSAL, dto.NUM_ULTIMO_CORRELATIVO, dto.FLG_ACTIVA, codUsuario, codEstacion);

    /// <summary>Alta y modificación tienen el mismo contrato de parámetros.</summary>
    private async Task<WriteResult> Grabar(string sp, string codSerie, string? desSerie, string? codSucursal, int ultimo, bool activa, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SERIE", codSerie);
        p.Add("DES_SERIE", desSerie);
        p.Add("COD_SUCURSAL", string.IsNullOrWhiteSpace(codSucursal) ? null : codSucursal);
        p.Add("NUM_ULTIMO_CORRELATIVO", ultimo);
        p.Add("FLG_ACTIVA", activa);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(sp, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = sp, SqlSentence = SqlSentenceHelper.Build(sp, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string COD_SERIE)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.GuiasRemisionSeries.Delete, new { COD_SERIE }, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.GuiasRemisionSeries.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.GuiasRemisionSeries.Delete, new { COD_SERIE })
            };
        }
    }
}
