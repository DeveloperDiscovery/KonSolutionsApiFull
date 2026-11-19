using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TrazabilidadRepository : ITrazabilidadRepository
{
    private readonly IDbConnectionFactory _db;
    public TrazabilidadRepository(IDbConnectionFactory db) => _db = db;

    public async Task<TrazabilidadNodo?> NodoAsync(string tipo, string clave)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TrazabilidadNodo>(
            SP.SCHEMALogistica.Trazabilidad.Nodo,
            new { TIPO_NODO = tipo, CLAVE = clave },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TrazabilidadNodo>> VecinosAsync(string tipo, string clave, string? sentido)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TrazabilidadNodo>(
            SP.SCHEMALogistica.Trazabilidad.Vecinos,
            new { TIPO_NODO = tipo, CLAVE = clave, SENTIDO = string.IsNullOrWhiteSpace(sentido) ? null : sentido },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TrazabilidadNodo>> BuscarAsync(string tipoBusqueda, string numero)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TrazabilidadNodo>(
            SP.SCHEMALogistica.Trazabilidad.Buscar,
            new { TIPO_BUSQUEDA = tipoBusqueda, NUMERO = numero },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TrazabilidadFranja>> FranjasAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TrazabilidadFranja>(
            SP.SCHEMALogistica.Trazabilidad.Franjas, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Dos resultsets, siempre en este orden: los campos (etiqueta / valor) y las líneas.</summary>
    public async Task<TrazabilidadEntidad> EntidadAsync(string tipo, string clave)
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMALogistica.Trazabilidad.Entidad,
            new { TIPO_NODO = tipo, CLAVE = clave },
            commandType: CommandType.StoredProcedure);
        return new TrazabilidadEntidad
        {
            Campos = (await multi.ReadAsync<TrazabilidadCampo>()).ToList(),
            Lineas = (await multi.ReadAsync<TrazabilidadLinea>()).ToList()
        };
    }
}
