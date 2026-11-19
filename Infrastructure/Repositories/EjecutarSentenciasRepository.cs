using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EjecutarSentenciasRepository : IEjecutarSentenciasRepository
{
    private readonly IDbConnectionFactory _db;
    public EjecutarSentenciasRepository(IDbConnectionFactory db) => _db = db;

    public async Task ExecuteAsync(string desSentencia)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(SP.SCHEMAEntorno.EjecutarSentencias.Ejecutar,
            new { DES_SENTENCIA = desSentencia }, commandType: CommandType.StoredProcedure);
    }
}
