using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Administracion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AreasCargosEntidadesRepository : IAreasCargosEntidadesRepository
{
    private readonly IDbConnectionFactory _db;
    public AreasCargosEntidadesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<AreasCargosEntidades>> ShowallAsync(int codArea, int codCargo)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AreasCargosEntidades>(
            SP.SCHEMAAdministracion.AreasCargosEntidades.Showall,
            new { COD_AREA = codArea, COD_CARGO = codCargo },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Las personas asignadas a un centro de costo, alcanzadas por su área
    /// (AREAS_CARGOS_ENTIDADES -> AREAS.COD_CENTRO_COSTO). Sirve para que los combos de entidad
    /// requiriente ofrezcan sólo a quien pertenece al centro de costo elegido.
    ///
    /// NO usa ComboboxReader: el lector arma el texto con UNA columna, y acá el texto se compone
    /// de tres (nombre, área y cargo), que es lo que permite distinguir a la persona cuando
    /// aparece en más de un área. Es el mismo motivo por el que RequerimientosStockRepository
    /// tiene su propio MapEntidadCombo.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxPorCentroCostoAsync(string codCentroCosto)
    {
        using var conn = _db.CreateConnection();
        var filas = await conn.QueryAsync<dynamic>(
            SP.SCHEMAAdministracion.AreasCargosEntidades.EntidadesPorCentroCosto,
            new { COD_CENTRO_COSTO = codCentroCosto },
            commandType: CommandType.StoredProcedure);

        var resultado = new List<ComboboxItem>();
        foreach (var fila in filas)
        {
            var d = (IDictionary<string, object>)fila;
            string Col(string c) => d.TryGetValue(c, out var v) ? v?.ToString()?.Trim() ?? "" : "";

            // "NOMBRE — ÁREA / CARGO". El área y el cargo sólo se agregan si vienen: una persona
            // sin cargo cargado tiene que seguir apareciendo, con su nombre a secas, en vez de
            // quedar con guiones colgando.
            var texto = Col("DES_NOMBRE_COMPLETO");
            var area  = Col("DES_AREA");
            var cargo = Col("DES_CARGO");
            var detalle = string.Join(" / ", new[] { area, cargo }.Where(x => !string.IsNullOrWhiteSpace(x)));
            if (!string.IsNullOrWhiteSpace(detalle)) texto = $"{texto} — {detalle}";

            resultado.Add(new ComboboxItem { Id = Col("COD_ENTIDAD"), Texto = texto });
        }
        return resultado;
    }

    public async Task<AreasCargosEntidades?> ShowbyidAsync(int codArea, int codCargo, decimal codEntidad)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AreasCargosEntidades>(
            SP.SCHEMAAdministracion.AreasCargosEntidades.Showbyid,
            new { COD_AREA = codArea, COD_CARGO = codCargo, COD_ENTIDAD = codEntidad },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<AreasCargosEntidades>> SearchAsync(AreasCargosEntidadesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AreasCargosEntidades>(
            SP.SCHEMAAdministracion.AreasCargosEntidades.Search,
            new { COD_AREA = dto.COD_AREA, COD_CARGO = dto.COD_CARGO, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AreasCargosEntidadesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAAdministracion.AreasCargosEntidades.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAAdministracion.AreasCargosEntidades.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAdministracion.AreasCargosEntidades.Insert, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(int codArea, int codCargo, decimal codEntidad)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_AREA", codArea);
        p.Add("COD_CARGO", codCargo);
        p.Add("COD_ENTIDAD", codEntidad);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAAdministracion.AreasCargosEntidades.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAAdministracion.AreasCargosEntidades.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAdministracion.AreasCargosEntidades.Delete, p)
            };
        }
    }

    public async Task<IEnumerable<AreasCargosEntidadesNodo>> GetTreeviewAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AreasCargosEntidadesNodo>(
            SP.SCHEMAAdministracion.AreasCargosEntidades.Treeview,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<EntidadDisponible>> GetEntidadesDisponiblesAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadDisponible>(
            SP.SCHEMAEstandar.Entidades.EntidadesDisponibles,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<AreaCargoEntidadPorOpcion>> GetPorEtiquetaAsync(string codEtiquetaBusqueda)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AreaCargoEntidadPorOpcion>(
            SP.SCHEMAAdministracion.AreasCargosEntidades.PorOpcion,
            new { COD_ETIQUETA_BUSQUEDA = codEtiquetaBusqueda },
            commandType: CommandType.StoredProcedure);
    }
}
