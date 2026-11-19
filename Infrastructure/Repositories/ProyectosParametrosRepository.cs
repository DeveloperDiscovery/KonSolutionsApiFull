using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosParametrosRepository : IProyectosParametrosRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosParametrosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<ProyectosParametros> ShowAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProyectosParametros>(SP.SCHEMAProyectos.ProyectosParametros.Show, commandType: CommandType.StoredProcedure) ?? new ProyectosParametros();
    }

    public async Task<WriteResult> GuardarAsync(ProyectosParametrosGuardarDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_KEY_PATHFILE_PROYECTOS", string.IsNullOrWhiteSpace(dto.COD_KEY_PATHFILE_PROYECTOS) ? null : dto.COD_KEY_PATHFILE_PROYECTOS.Trim());
        p.Add("COD_TIPO_VALOR_CALCULO_COSTO", string.IsNullOrWhiteSpace(dto.COD_TIPO_VALOR_CALCULO_COSTO) ? null : dto.COD_TIPO_VALOR_CALCULO_COSTO.Trim());
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMAProyectos.ProyectosParametros.Guardar, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult { Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number, ErrorProcedure = SP.SCHEMAProyectos.ProyectosParametros.Guardar, SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosParametros.Guardar, p) };
        }
    }
}
