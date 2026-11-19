using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Lo que el servicio de XML recibidos usa de la base: la configuración general y los procedimientos del script 41 (PROC_XML_RECIBIDOS_CARGAR y PROC_XML_RECIBIDOS_PDF_ASIGNAR).
/// Un XML que se rechaza llega como SqlException con un número propio del script (50700 a 50734) y un mensaje claro.</summary>
public class XmlRecibidosRepository : IXmlRecibidosRepository
{
    private readonly IDbConnectionFactory _db;
    public XmlRecibidosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<ConfiguracionGeneral?> ConfigAsync()
    {
        using var conn = _db.CreateConnection();
        var filas = await conn.QueryAsync<ConfiguracionGeneral>(SP.SCHEMAEstandar.ConfiguracionGeneral.Showall, commandType: CommandType.StoredProcedure);
        return filas.FirstOrDefault();
    }

    private sealed class FilaCarga { public string COD_CLASE { get; set; } = ""; public decimal NUM_DOCUMENTO { get; set; } public bool FLG_NUEVO { get; set; } }

    public async Task<(string Clase, decimal Numero, bool Nuevo)> CargarAsync(string xml, string nombreArchivo, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("DES_XML", xml, DbType.String, size: -1);
        p.Add("DES_NOMBRE_ARCHIVO", nombreArchivo.Length > 260 ? nombreArchivo[^260..] : nombreArchivo);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        var f = await conn.QueryFirstAsync<FilaCarga>("PROC_XML_RECIBIDOS_CARGAR", p, commandType: CommandType.StoredProcedure);
        return (f.COD_CLASE.Trim(), f.NUM_DOCUMENTO, f.FLG_NUEVO);
    }

    public async Task AsignarPdfAsync(string clase, decimal numero, string nombrePdf)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_XML_RECIBIDOS_PDF_ASIGNAR", new { COD_CLASE = clase, NUM_DOCUMENTO = numero, DES_NOMBRE_ARCHIVO_PDF = nombrePdf, FLG_REEMPLAZAR = false }, commandType: CommandType.StoredProcedure);
    }
}
