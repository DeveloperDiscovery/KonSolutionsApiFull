using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposSolicitudComercialAreasCargosRepository : ITiposSolicitudComercialAreasCargosRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposSolicitudComercialAreasCargosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposSolicitudComercialAreasCargos>> ShowallAsync(string codTipoSolicitudComercial, int codArea)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposSolicitudComercialAreasCargos>(
            SP.SCHEMAComercial.TiposSolicitudComercialAreasCargos.Showall,
            new { COD_TIPO_SOLICITUD_COMERICAL = codTipoSolicitudComercial, COD_AREA = codArea },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposSolicitudComercialAreasCargos?> ShowbyidAsync(string codTipoSolicitudComercial, int codArea, int codCargo)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposSolicitudComercialAreasCargos>(
            SP.SCHEMAComercial.TiposSolicitudComercialAreasCargos.Showbyid,
            new { COD_TIPO_SOLICITUD_COMERICAL = codTipoSolicitudComercial, COD_AREA = codArea, COD_CARGO = codCargo },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TiposSolicitudComercialAreasCargos>> SearchAsync(TiposSolicitudComercialAreasCargosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposSolicitudComercialAreasCargos>(
            SP.SCHEMAComercial.TiposSolicitudComercialAreasCargos.Search,
            new { COD_TIPO_SOLICITUD_COMERICAL = dto.COD_TIPO_SOLICITUD_COMERICAL, COD_AREA = dto.COD_AREA, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposSolicitudComercialAreasCargosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_SOLICITUD_COMERICAL", dto.COD_TIPO_SOLICITUD_COMERICAL);
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("NUM_NIVEL_FLUJO", dto.NUM_NIVEL_FLUJO);
        p.Add("FLG_APROBADOR", dto.FLG_APROBADOR ?? false);
        p.Add("FLG_NOTIFICADOR", dto.FLG_NOTIFICADOR ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.TiposSolicitudComercialAreasCargos.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.TiposSolicitudComercialAreasCargos.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposSolicitudComercialAreasCargos.Insert, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string codTipoSolicitudComercial, int codArea, int codCargo)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_SOLICITUD_COMERICAL", codTipoSolicitudComercial);
        p.Add("COD_AREA", codArea);
        p.Add("COD_CARGO", codCargo);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.TiposSolicitudComercialAreasCargos.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.TiposSolicitudComercialAreasCargos.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposSolicitudComercialAreasCargos.Delete, p)
            };
        }
    }

    /// <summary>PROC_COMERCIAL_TIPOS_SOLICITUD_COMERCIAL_AREAS_CARGOS_LOADCOMBOS: Tipos de
    /// Solicitud Comercial + Cargos (universo completo, sin filtrar por área) en un solo viaje
    /// para TiposSolicitudComercialAreasCargos.razor. Sin parámetros: a diferencia de
    /// Áreas/Zonificación/Perfiles, esta pantalla no usa EstadoCombo, así que no hay
    /// COD_TIPO_ESTADO que resolver.</summary>
    public async Task<TiposSolicitudComercialAreasCargosLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAComercial.TiposSolicitudComercialAreasCargos.Loadcombos,
            commandType: CommandType.StoredProcedure);

        var tipos = MapCombo(await multi.ReadAsync<dynamic>());
        var cargos = MapCombo(await multi.ReadAsync<dynamic>());

        return new TiposSolicitudComercialAreasCargosLoadCombosResult { Tipos = tipos, Cargos = cargos };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem por convención COD_*/DES_*/NOMBRE_* — sin
    /// reconocer alias literales "Id"/"Texto" (mismo criterio que ComboboxReader).</summary>
    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas)
    {
        var resultado = new List<ComboboxItem>();
        foreach (var fila in filas)
        {
            var d = (IDictionary<string, object>)fila;
            var colId = d.Keys.FirstOrDefault(k => k.StartsWith("COD_", StringComparison.OrdinalIgnoreCase));
            var colTexto = d.Keys.FirstOrDefault(k => k.StartsWith("DES_", StringComparison.OrdinalIgnoreCase) || k.StartsWith("NOMBRE_", StringComparison.OrdinalIgnoreCase));
            resultado.Add(new ComboboxItem
            {
                Id    = colId is not null ? d[colId]?.ToString() ?? "" : "",
                Texto = colTexto is not null ? d[colTexto]?.ToString() ?? "" : ""
            });
        }
        return resultado;
    }
}
