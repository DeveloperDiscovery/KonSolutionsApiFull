using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesProceduresRepository : IEntidadesProceduresRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesProceduresRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<string>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<string>(SP.SCHEMAEntorno.EntidadesProcedures.Showall,
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>El SP devuelve b.name (parámetro) y c.name (tipo) SIN alias — dos columnas
    /// llamadas igual en el mismo resultset. Dapper QueryAsync&lt;T&gt; no distingue columnas
    /// repetidas por nombre al mapear a un POCO (la segunda pisa a la primera en silencio,
    /// mismo problema documentado en InventariosAlmacenesPuntoTomaRepository con
    /// DES_OBSERVACION). Se resuelve leyendo por ORDINAL: el SELECT fija el orden
    /// (0=nombre parámetro, 1=system_type_id, 2=nombre tipo), así que no hace falta
    /// distinguir por nombre — alcanza con la posición.</summary>
    public async Task<IEnumerable<EntidadProcedureParametro>> ParametersShowallAsync(string name)
    {
        using var conn = _db.CreateConnection();
        using var reader = await conn.ExecuteReaderAsync(SP.SCHEMAEntorno.EntidadesProcedures.ParametersShowall,
            new { NAME = name }, commandType: CommandType.StoredProcedure);

        var lista = new List<EntidadProcedureParametro>();
        while (reader.Read())
        {
            lista.Add(new EntidadProcedureParametro
            {
                ParameterName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                SystemTypeId = reader.IsDBNull(1) ? 0 : Convert.ToInt32(reader.GetValue(1)),
                TypeName = reader.IsDBNull(2) ? "" : reader.GetString(2)
            });
        }
        return lista;
    }
}
