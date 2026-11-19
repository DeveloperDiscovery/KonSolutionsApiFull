using System.Data;
using System.Text.Json;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Especificaciones tecnicas de una version de diseno (script SQL 113). Las validaciones salen como SqlException
/// 52401 y 52470 a 52476 y las atiende el servicio.</summary>
public class DisenosEspecificacionesRepository : IDisenosEspecificacionesRepository
{
    private const string P = "PROC_INGENIERIA_DISENOS_VERSION_ESPECIFICACIONES_";
    private readonly IDbConnectionFactory _db;
    public DisenosEspecificacionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<DisenosEspecificacionesPanel> PanelAsync(string codDiseno, int numVersion)
    {
        using var conn = _db.CreateConnection();
        using var g = await conn.QueryMultipleAsync(P + "PANEL", new { COD_DISENO = codDiseno, NUM_VERSION = numVersion }, commandType: CommandType.StoredProcedure);
        var cabecera = await g.ReadFirstAsync<DisenosEspecificacionesCabecera>();
        var filas = (await g.ReadAsync<DisenosEspecificacionesFila>()).ToList();
        return new DisenosEspecificacionesPanel { Cabecera = cabecera, Filas = filas };
    }

    public async Task<DisenosEspecificacionesResultado> GuardarAsync(DisenosEspecificacionesGuardarDto dto, string codUsuario, string codEstacion)
    {
        // El procedimiento recibe las filas como JSON y las graba juntas (una sola transaccion). Las fechas y horas van como texto con formato fijo
        // (yyyy-MM-dd y HH:mm:ss) y los numeros como numeros JSON: nada depende de la cultura del servidor.
        var json = JsonSerializer.Serialize((dto.Filas ?? new()).Select(f => new
        {
            f.COD_ESPECIFICACION_TECNICA,
            f.COD_ITEM_TECNICO,
            f.VAL_ESPECIFICACION_TENICA,
            f.VAL_MINIMO,
            f.VAL_MAXIMO,
            FEC_ESPECIFICACION_TENICA = f.FEC_ESPECIFICACION_TENICA?.ToString("yyyy-MM-dd"),
            HOR_ESPECIFICACION_TENICA = f.HOR_ESPECIFICACION_TENICA?.ToString(@"hh\:mm\:ss"),
            f.LOG_ESPECIFICACION_TENICA,
            f.STR_ESPECIFICACION_TENICA
        }));
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<DisenosEspecificacionesResultado>(P + "GUARDAR",
            new { dto.COD_DISENO, dto.NUM_VERSION, FILAS = json, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: CommandType.StoredProcedure);
    }
}
