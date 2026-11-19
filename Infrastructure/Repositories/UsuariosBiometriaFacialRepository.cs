using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>
/// Acceso a SEGURIDAD.USUARIOS_BIOMETRIA_FACIAL.
///
/// Antes esto eran archivos JSON en {ContentRoot}/face-data. Que ahora esté en la base
/// no es un detalle de implementación: los archivos no entraban en el backup de la
/// base, no se replicaban con ella, no tenían auditoría de quién dio de alta el rostro
/// y se perdían al reinstalar el sitio en otro servidor.
/// </summary>
public class UsuariosBiometriaFacialRepository : IUsuariosBiometriaFacialRepository
{
    private readonly IDbConnectionFactory _db;
    public UsuariosBiometriaFacialRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<UsuariosBiometriaFacial>> ShowallAsync(string codUsuario)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosBiometriaFacial>(
            SP.SCHEMASeguridad.UsuariosBiometriaFacial.Showall,
            new { COD_USUARIO = codUsuario },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<UsuariosBiometriaFacialEstado> EstadoAsync(string codUsuario)
    {
        using var conn = _db.CreateConnection();
        var r = await conn.QueryFirstOrDefaultAsync<UsuariosBiometriaFacialEstado>(
            SP.SCHEMASeguridad.UsuariosBiometriaFacial.Estado,
            new { COD_USUARIO = codUsuario },
            commandType: CommandType.StoredProcedure);

        // El SP siempre devuelve una fila; el respaldo cubre el caso de que alguien lo
        // reemplace mañana por uno que no lo haga. "No registrado" y "no respondió" tienen
        // que llegar iguales a quien llama: sin rostro, no hay verificación.
        return r ?? new UsuariosBiometriaFacialEstado { COD_USUARIO = codUsuario };
    }

    public async Task<IEnumerable<UsuariosBiometriaFacialEstado>> EstadosAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosBiometriaFacialEstado>(
            SP.SCHEMASeguridad.UsuariosBiometriaFacial.Estados,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> ReemplazarAsync(string codUsuario, IReadOnlyList<double[]> descriptores,
                                           string codUsuarioRegistro, string codEstacion)
    {
        // Se convierte TODO antes de abrir la transacción: si una muestra viene con un
        // largo equivocado, conviene enterarse sin haber borrado todavía las que el
        // usuario ya tenía.
        var bytes = descriptores.Select(DescriptorFacial.ABytes).ToList();

        using var conn = _db.CreateConnection();
        using var tx   = conn.BeginTransaction();
        try
        {
            await conn.ExecuteAsync(
                SP.SCHEMASeguridad.UsuariosBiometriaFacial.Delete,
                new { COD_USUARIO = codUsuario, NUM_MUESTRA = (int?)null },
                tx, commandType: CommandType.StoredProcedure);

            foreach (var b in bytes)
            {
                var p = new DynamicParameters();
                p.Add("COD_USUARIO",           codUsuario);
                // DbType explícito: sin esto Dapper manda el byte[] como VARBINARY(MAX) y el
                // SP —que declara VARBINARY(1024)— lo rechaza por truncamiento.
                p.Add("VAL_DESCRIPTOR",        b, DbType.Binary, size: DescriptorFacial.Bytes);
                p.Add("COD_USUARIO_REGISTRO",  codUsuarioRegistro);
                p.Add("COD_ESTACION_REGISTRO", codEstacion);

                await conn.ExecuteAsync(
                    SP.SCHEMASeguridad.UsuariosBiometriaFacial.Insert, p,
                    tx, commandType: CommandType.StoredProcedure);
            }

            tx.Commit();
            return bytes.Count;
        }
        catch
        {
            // Rollback defensivo: si el error vino de la base, la transacción puede estar ya
            // abortada y el Rollback tira su propia excepción, tapando la de verdad.
            try { tx.Rollback(); } catch { /* la que importa es la de afuera */ }
            throw;
        }
    }

    public async Task<int> DeleteAsync(string codUsuario, int? numMuestra = null)
    {
        using var conn = _db.CreateConnection();
        return await conn.ExecuteAsync(
            SP.SCHEMASeguridad.UsuariosBiometriaFacial.Delete,
            new { COD_USUARIO = codUsuario, NUM_MUESTRA = numMuestra },
            commandType: CommandType.StoredProcedure);
    }
}
