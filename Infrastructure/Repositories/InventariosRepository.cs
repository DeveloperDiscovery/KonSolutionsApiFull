using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class InventariosRepository : IInventariosRepository
{
    private readonly IDbConnectionFactory _db;
    public InventariosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Inventarios>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Inventarios>(SP.SCHEMALogistica.Inventarios.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Inventarios?> ShowbyidAsync(short NUM_INVENTARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Inventarios>(SP.SCHEMALogistica.Inventarios.Showbyid, new { NUM_INVENTARIO = NUM_INVENTARIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.Inventarios.Showcombox);
    }

    public async Task<IEnumerable<Inventarios>> SearchAsync(InventariosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Inventarios>(SP.SCHEMALogistica.Inventarios.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(InventariosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        // El SP declara @NUM_INVENTARIO en su firma aunque no lo use en el INSERT (la columna
        // es IDENTITY, el valor real sale de SCOPE_IDENTITY() más abajo en el propio SP) — SQL
        // Server igual exige que se le pase algo porque el parámetro no tiene DEFAULT.
        p.Add("NUM_INVENTARIO", dbType: DbType.Int16, value: null);
        p.Add("DES_INVENTARIO", dto.DES_INVENTARIO);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("COD_TIPO_INVENTARIO", dto.COD_TIPO_INVENTARIO);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("FEC_INICIO_PROGRAMADA", dto.FEC_INICIO_PROGRAMADA);
        p.Add("FEC_FINAL_PROGRAMADA", dto.FEC_FINAL_PROGRAMADA);
        p.Add("FEC_INICIO_EFECTUADA", dto.FEC_INICIO_EFECTUADA);
        p.Add("FEC_FINAL_EFECTUADA", dto.FEC_FINAL_EFECTUADA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        // Se lee como fila suelta y no como WriteResult porque el SP devuelve la clave con su
        // propio nombre —NUM_INVENTARIO— y Dapper no tiene cómo saber que eso es el Codigo.
        // El mismo resultset sirve para el camino del CATCH, que trae ErrorNumber/ErrorMessage.
        // Mismo criterio que RequerimientosStockRepository/ReservasStockRepository.
        var fila = await conn.QueryFirstOrDefaultAsync(SP.SCHEMALogistica.Inventarios.Insert, p, commandType: CommandType.StoredProcedure);
        return LeerAlta(fila);
    }

    /// <summary>Traduce el resultset del alta: o trae la clave que generó la base
    /// (SCOPE_IDENTITY), o trae el error del CATCH.</summary>
    private static WriteResult LeerAlta(dynamic? fila)
    {
        if (fila is null) return new WriteResult { Estado = "OK", AffectedRows = 1 };
        var campos = (IDictionary<string, object?>)fila;

        if (campos.TryGetValue("ErrorNumber", out var num) && num is not null)
            return new WriteResult
            {
                Estado = "ERROR",
                AffectedRows = 0,
                ErrorNumber = Convert.ToInt32(num),
                Mensaje = campos.TryGetValue("ErrorMessage", out var msg) ? msg?.ToString() ?? "" : "",
                ErrorProcedure = campos.TryGetValue("ErrorProcedure", out var sp) ? sp?.ToString() ?? "" : ""
            };

        return new WriteResult
        {
            Estado = "OK",
            AffectedRows = 1,
            Codigo = campos.TryGetValue("NUM_INVENTARIO", out var cod) ? cod?.ToString() ?? "" : ""
        };
    }

    /// <summary>Modificación. Va contra el SP _UPDATE, que actualiza la fila existente por
    /// NUM_INVENTARIO y falla si no existe.</summary>
    public async Task<WriteResult> UpdateAsync(InventariosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("DES_INVENTARIO", dto.DES_INVENTARIO);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("COD_TIPO_INVENTARIO", dto.COD_TIPO_INVENTARIO);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("FEC_INICIO_PROGRAMADA", dto.FEC_INICIO_PROGRAMADA);
        p.Add("FEC_FINAL_PROGRAMADA", dto.FEC_FINAL_PROGRAMADA);
        p.Add("FEC_INICIO_EFECTUADA", dto.FEC_INICIO_EFECTUADA);
        p.Add("FEC_FINAL_EFECTUADA", dto.FEC_FINAL_EFECTUADA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.Inventarios.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.Inventarios.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.Inventarios.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(short NUM_INVENTARIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.Inventarios.Delete, new { NUM_INVENTARIO = NUM_INVENTARIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.Inventarios.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.Inventarios.Delete, new { NUM_INVENTARIO = NUM_INVENTARIO })
        };
    }
}


    // ══════════════════════════════════════════════════════════════════════════
    // PROCESOS DE INVENTARIO (botones 04 a 08 de la opción)
    // --------------------------------------------------------------------------
    // Los cinco comparten forma, así que hay un solo método privado y cada público
    // arma sólo sus parámetros. La razón para no unificarlos del todo es que los
    // nombres de los parámetros NO coinciden entre los SP:
    //
    //    REQUERIMIENTOMAKE / REQUERIMIENTOREVIERTE -> @COD_USUARIO  / @COD_ESTACION
    //    CERRAR / ABRIR                            -> @COD_USUARIO_REGISTRO
    //                                                 @COD_ESTACION_REGISTRO
    //
    // Dapper los manda por nombre exacto: mandar el par equivocado da error 201
    // (parámetro esperado no suministrado). Está verificado contra el texto de los
    // SP, no supuesto.
    //
    // Ninguno devuelve resultset, así que se usa ExecuteAsync y el WriteResult se
    // construye acá: QueryFirstOrDefault sobre un SP sin filas daría null y se
    // perdería el conteo de filas afectadas.
    // ══════════════════════════════════════════════════════════════════════════

    /// <summary>Ejecuta un SP de proceso y traduce la SqlException al WriteResult que la
    /// aplicación sabe mostrar. Sin esto, el mensaje del RAISERROR del SP no llega a la
    /// pantalla.</summary>
    private async Task<WriteResult> EjecutarProcesoAsync(string sp, DynamicParameters p)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var filas = await conn.ExecuteAsync(sp, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = filas };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = sp,
                SqlSentence = SqlSentenceHelper.Build(sp, p)
            };
        }
    }

    /// <summary>Parámetros de los SP que declaran @COD_USUARIO / @COD_ESTACION.</summary>
    private static DynamicParameters ParamsUsuario(short num, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", num);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        return p;
    }

    /// <summary>Parámetros de los SP que declaran @COD_USUARIO_REGISTRO / @COD_ESTACION_REGISTRO.</summary>
    private static DynamicParameters ParamsRegistro(short num, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", num);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        return p;
    }

    /// <summary>Botón 04 · Genera el requerimiento de equipos para el inventario.</summary>
    public Task<WriteResult> RequerimientoMakeAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion)
        => EjecutarProcesoAsync(SP.SCHEMALogistica.Inventarios.RequerimientoMake,
                                ParamsUsuario(NUM_INVENTARIO, codUsuario, codEstacion));

    /// <summary>Botón 05 · Revierte el requerimiento generado.</summary>
    public Task<WriteResult> RequerimientoRevierteAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion)
        => EjecutarProcesoAsync(SP.SCHEMALogistica.Inventarios.RequerimientoRevierte,
                                ParamsUsuario(NUM_INVENTARIO, codUsuario, codEstacion));

    /// <summary>Botón 06 · Genera la imagen del inventario.
    /// OJO: PROC_LOGISTICA_INVENTARIOS_IMAGEN todavía no existe en la base. Este método está
    /// escrito y cableado, pero va a fallar hasta que se cree el SP.</summary>
    public Task<WriteResult> ImagenAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion)
        => EjecutarProcesoAsync(SP.SCHEMALogistica.Inventarios.Imagen,
                                ParamsUsuario(NUM_INVENTARIO, codUsuario, codEstacion));

    /// <summary>Botón 07 · Cierra el inventario (alias de estado 'CLO').</summary>
    public Task<WriteResult> CerrarAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion)
        => EjecutarProcesoAsync(SP.SCHEMALogistica.Inventarios.Cerrar,
                                ParamsRegistro(NUM_INVENTARIO, codUsuario, codEstacion));

    /// <summary>Botón 08 · Reabre el inventario (alias de estado 'OPE').</summary>
    public Task<WriteResult> AbrirAsync(short NUM_INVENTARIO, string codUsuario, string codEstacion)
        => EjecutarProcesoAsync(SP.SCHEMALogistica.Inventarios.Abrir,
                                ParamsRegistro(NUM_INVENTARIO, codUsuario, codEstacion));

}
