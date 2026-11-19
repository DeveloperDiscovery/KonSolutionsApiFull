using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rfid;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Los equipos RFID son productos (ESTANDAR.PRODUCTOS) con su ficha técnica: aquí solo se leen. Qué productos son equipos y qué especificación
/// guarda cada dato lo dice Configuración general &gt; RFID (los resuelve dbo.FNC_RFID_EQUIPOS).</summary>
public class RfidEquiposRepository : IRfidEquiposRepository
{
    private readonly IDbConnectionFactory _db;
    public RfidEquiposRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RfidEquipo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RfidEquipo>(SP.SCHEMARfid.Equipos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<RfidEquipo?> ShowbyidAsync(string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RfidEquipo>(
            SP.SCHEMARfid.Equipos.Showbyid, new { COD_PRODUCTO = COD_PRODUCTO?.Trim() }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RfidEquipoCombo>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RfidEquipoCombo>(SP.SCHEMARfid.Equipos.Showcombox, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> EstacionesComboAsync(string? codGrupoProducto, string? codFamiliaProducto, string? codSubFamiliaProducto)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARfid.Estaciones.Showcombox, new
        {
            COD_GRUPO_PRODUCTO = Nulo(codGrupoProducto), COD_FAMILIA_PRODUCTO = Nulo(codFamiliaProducto), COD_SUB_FAMILIA_PRODUCTO = Nulo(codSubFamiliaProducto)
        });
    }

    private static string? Nulo(string? v) => string.IsNullOrWhiteSpace(v) ? null : v.Trim();
}
