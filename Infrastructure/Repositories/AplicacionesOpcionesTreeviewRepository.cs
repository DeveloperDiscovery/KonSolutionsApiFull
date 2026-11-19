using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Repositorio del treeview de opciones de aplicaciones.</summary>
public class AplicacionesOpcionesTreeviewRepository : IAplicacionesOpcionesTreeviewRepository
{
    private readonly IDbConnectionFactory _db;
    public AplicacionesOpcionesTreeviewRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<NodoOpcion>> GetTreeviewAsync()
    {
        using var conn = _db.CreateConnection();

        // Query dinámico para evitar problemas con el tipo IMAGE de SQL Server en el mapeo tipado.
        var rows = await conn.QueryAsync(
            SP.SCHEMASeguridad.AplicacionesOpcionesTreeview.Showall,
            commandType: CommandType.StoredProcedure,
            commandTimeout: 120);

        return rows.Select(r =>
        {
            var d = (IDictionary<string, object>)r;

            static string Str(IDictionary<string, object> d, string key)
                => d.TryGetValue(key, out var v) && v is string s ? s : "";

            // COD_ESTADO puede llegar sin alias (clave "") si el SP no usa AS COD_ESTADO.
            static int? IntFallback(IDictionary<string, object> d, string key)
            {
                if (d.TryGetValue(key, out var v) && v is not null && v is not DBNull)
                    return Convert.ToInt32(v);
                // Fallback: columna sin nombre — el SP usa C.COD_ESTADO sin alias
                if (d.TryGetValue("", out var u) && u is not null && u is not DBNull)
                    return Convert.ToInt32(u);
                return null;
            }

            static bool? BoolOrNull(IDictionary<string, object> d, string key)
                => d.TryGetValue(key, out var v) && v is not null && v is not DBNull ? Convert.ToBoolean(v) : (bool?)null;

            return new NodoOpcion
            {
                COD_PARENT                  = Str(d, "COD_PARENT"),
                COD_CHILDREN                = Str(d, "COD_CHILDREN"),
                DES_OPCION                  = Str(d, "DES_OPCION"),
                COD_OPCION                  = Str(d, "COD_OPCION"),
                COD_APLICACION_ORIGINAL     = Str(d, "COD_APLICACION_ORIGINAL"),
                COD_OPCION_ORIGINAL         = Str(d, "COD_OPCION_ORIGINAL"),
                COD_BOTON_ORIGINAL          = Str(d, "COD_BOTON_ORIGINAL"),
                DES_PAGINA                  = Str(d, "DES_PAGINA"),
                COD_TIPO                    = Str(d, "COD_TIPO"),
                DES_APLICACION              = Str(d, "DES_APLICACION"),
                DES_OPCION_APLICACION       = Str(d, "DES_OPCION_APLICACION"),
                DES_OPCION_APLICACION_PADRE = Str(d, "DES_OPCION_APLICACION_PADRE"),
                COD_OPCION_APLICACION_PADRE = Str(d, "COD_OPCION_APLICACION_PADRE"),
                COD_ESTADO                  = IntFallback(d, "COD_ESTADO"),
                FLG_EXIGE_PASSWORD          = BoolOrNull(d, "FLG_EXIGE_PASSWORD"),
                FLG_TECLADO_VIRTUAL         = BoolOrNull(d, "FLG_TECLADO_VIRTUAL"),
                // Si el SP del treeview todavía no trae la columna, queda null y el
                // menú la completa desde el maestro plano (ver MenuService).
                FLG_MULTI_LOAD              = BoolOrNull(d, "FLG_MULTI_LOAD"),
                IMG_ICONO                   = null
            };
        });
    }

    public async Task<IEnumerable<NodoOpcionLogo>> GetLogosAsync()
    {
        using var conn = _db.CreateConnection();

        // Mismo motivo que arriba: query dinámico para el tipo IMAGE de SQL Server.
        var rows = await conn.QueryAsync(
            SP.SCHEMASeguridad.AplicacionesOpcionesTreeview.ShowLogos,
            commandType: CommandType.StoredProcedure,
            commandTimeout: 120);

        return rows.Select(r =>
        {
            var d = (IDictionary<string, object>)r;
            static string Str(IDictionary<string, object> d, string key)
                => d.TryGetValue(key, out var v) && v is string s ? s : "";
            return new NodoOpcionLogo
            {
                COD_PARENT   = Str(d, "COD_PARENT"),
                COD_CHILDREN = Str(d, "COD_CHILDREN"),
                IMG_ICONO    = d.TryGetValue("IMG_ICONO", out var img) && img is byte[] b ? b : null
            };
        });
    }
}
