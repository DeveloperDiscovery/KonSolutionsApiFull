using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Copia de estados (script SQL 100). Los dos procedimientos devuelven el detalle de lo que hicieron (o harian, con SIMULAR).
/// Los errores de validacion salen como SqlException (52201 a 52208) y los atiende el servicio.</summary>
public class CopiaEstadosRepository : ICopiaEstadosRepository
{
    private const string ProcConfig = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_COPIA";
    private const string ProcTipo = "PROC_ESTANDAR_ESTADOS_COPIA_TIPO";
    private readonly IDbConnectionFactory _db;
    public CopiaEstadosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CopiaConfiguracionEstadosFila>> CopiarConfiguracionAsync(CopiaConfiguracionEstadosDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CopiaConfiguracionEstadosFila>(ProcConfig, new
        {
            COD_APLICACION_ORIGEN = dto.COD_APLICACION_ORIGEN, COD_OPCION_ORIGEN = dto.COD_OPCION_ORIGEN, COD_TIPO_ESTADO_ORIGEN = dto.COD_TIPO_ESTADO_ORIGEN,
            COD_APLICACION_DESTINO = dto.COD_APLICACION_DESTINO, COD_OPCION_DESTINO = dto.COD_OPCION_DESTINO, COD_TIPO_ESTADO_DESTINO = dto.COD_TIPO_ESTADO_DESTINO,
            REEMPLAZAR = dto.REEMPLAZAR, SIMULAR = dto.SIMULAR, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<CopiaEstadosTipoFila>> CopiarEstadosTipoAsync(CopiaEstadosTipoDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CopiaEstadosTipoFila>(ProcTipo, new
        {
            COD_TIPO_ESTADO_ORIGEN = dto.COD_TIPO_ESTADO_ORIGEN, COD_TIPO_ESTADO_DESTINO = dto.COD_TIPO_ESTADO_DESTINO,
            ELIMINAR_ANTERIORES = dto.ELIMINAR_ANTERIORES, SIMULAR = dto.SIMULAR, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion
        }, commandType: CommandType.StoredProcedure);
    }
}
