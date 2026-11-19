using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AlmacenesUbicacionesAlmacenRepository : IAlmacenesUbicacionesAlmacenRepository
{
    private readonly IDbConnectionFactory _db;
    public AlmacenesUbicacionesAlmacenRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<AlmacenesUbicacionesAlmacen>> ShowallAsync(string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AlmacenesUbicacionesAlmacen>(
            SP.SCHEMALogistica.AlmacenesUbicacionesAlmacen.Showall,
            new { COD_ALMACEN = COD_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<AlmacenesUbicacionesAlmacen?> ShowbyidAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AlmacenesUbicacionesAlmacen>(
            SP.SCHEMALogistica.AlmacenesUbicacionesAlmacen.Showbyid,
            new { COD_ALMACEN = COD_ALMACEN, COD_UBICACION_ALMACEN = COD_UBICACION_ALMACEN },
            commandType: CommandType.StoredProcedure);
    }

    // El SP _SHOWCOMBOX devuelve las columnas de la tabla (COD_*/DES_*), no "Id"/"Texto":
    // la traducción a ComboboxItem se hace acá. Mapearlo directo devolvía items en blanco.
    //
    // Con la columna clave EXPLÍCITA, no "la primera COD_*": la primera columna del SP es
    // COD_ALMACEN, así que cada ubicación llegaba con Id = el código del almacén (00001) y
    // el texto de la ubicación. El combo se veía bien, pero lo que se grababa era el
    // almacén: Confirmación de Recepción lo rechazaba con "la ubicación 00001 no existe en
    // el almacén destino 00001". Mismo arreglo que el combo de almacenes del rol.
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.AlmacenesUbicacionesAlmacen.Showcombox,
            columnaId: "COD_UBICACION_ALMACEN", columnaTexto: "DES_UBICACION_ALMACEN", parametros: new { COD_ALMACEN = COD_ALMACEN });
    }

    public async Task<IEnumerable<AlmacenesUbicacionesAlmacen>> SearchAsync(AlmacenesUbicacionesAlmacenSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AlmacenesUbicacionesAlmacen>(
            SP.SCHEMALogistica.AlmacenesUbicacionesAlmacen.Search,
            new { COD_ALMACEN = dto.COD_ALMACEN, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AlmacenesUbicacionesAlmacenInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_UBICACION_ALMACEN", dto.COD_UBICACION_ALMACEN);
        p.Add("DES_UBICACION_ALMACEN", dto.DES_UBICACION_ALMACEN);
        // Polígono dibujado sobre el plano: viaja tal cual desde la UI y vuelve igual.
        p.Add("DES_POLIGONO", dto.DES_POLIGONO);
        // Por defecto false: una ubicación dada de alta desde el CRUD es manual. El
        // generador de casilleros es el único que la marca como automática.
        p.Add("FLG_CREACION_AUTOMATICA", dto.FLG_CREACION_AUTOMATICA ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMALogistica.AlmacenesUbicacionesAlmacen.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.AlmacenesUbicacionesAlmacen.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.AlmacenesUbicacionesAlmacen.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMALogistica.AlmacenesUbicacionesAlmacen.Delete,
            new { COD_ALMACEN = COD_ALMACEN, COD_UBICACION_ALMACEN = COD_UBICACION_ALMACEN },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.AlmacenesUbicacionesAlmacen.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.AlmacenesUbicacionesAlmacen.Delete, new { COD_ALMACEN = COD_ALMACEN, COD_UBICACION_ALMACEN = COD_UBICACION_ALMACEN })
        };
    }
}
}
