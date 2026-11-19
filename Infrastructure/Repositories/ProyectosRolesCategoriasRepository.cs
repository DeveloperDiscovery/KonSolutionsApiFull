using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosRolesCategoriasRepository : IProyectosRolesCategoriasRepository
{
    private const string PRE = "PROC_PROYECTOS_PROYECTOS_ROLES_CATEGORIAS_";
    private readonly IDbConnectionFactory _db;
    public ProyectosRolesCategoriasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProyectosRolesCategorias>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosRolesCategorias>(PRE + "SHOWALL", commandType: CommandType.StoredProcedure);
    }

    public async Task<ProyectosRolesCategorias?> ShowbyidAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProyectosRolesCategorias>(PRE + "SHOWBYID", new { COD_CATEGORIA_ROL = cod }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ProyectosRolesCategorias>> SearchAsync(ProyectosRolesCategoriasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosRolesCategorias>(PRE + "SEARCH", new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
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

    public Task<WriteResult> InsertAsync(ProyectosRolesCategoriasInsertDto dto, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("COD_CATEGORIA_ROL", dto.COD_CATEGORIA_ROL); p.Add("DES_CATEGORIA_ROL", dto.DES_CATEGORIA_ROL);
        p.Add("DES_DESCRIPCION", dto.DES_DESCRIPCION); p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario); p.Add("COD_ESTACION_REGISTRO", codEstacion);
        return Escribir(PRE + "INSERT", p);
    }

    public Task<WriteResult> UpdateAsync(ProyectosRolesCategoriasUpdateDto dto)
    {
        var p = new DynamicParameters();
        p.Add("COD_CATEGORIA_ROL", dto.COD_CATEGORIA_ROL); p.Add("DES_CATEGORIA_ROL", dto.DES_CATEGORIA_ROL);
        p.Add("DES_DESCRIPCION", dto.DES_DESCRIPCION); p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        return Escribir(PRE + "UPDATE", p);
    }

    public Task<WriteResult> DeleteAsync(string cod)
    {
        var p = new DynamicParameters();
        p.Add("COD_CATEGORIA_ROL", cod);
        return Escribir(PRE + "DELETE", p);
    }
}
