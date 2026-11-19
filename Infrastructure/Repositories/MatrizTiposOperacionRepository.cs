using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Matriz de valores de calculo y de tipos de comprobante por tipo de operacion (script SQL 104).
/// Los errores de validacion salen como SqlException (52301 a 52304) y los atiende el servicio.</summary>
public class MatrizTiposOperacionRepository : IMatrizTiposOperacionRepository
{
    private const string ProcCalculo = "PROC_COMERCIAL_TIPOS_OPERACION_MATRIZ_CALCULO";
    private const string ProcCalculoSet = "PROC_COMERCIAL_TIPOS_OPERACION_MATRIZ_CALCULO_SET";
    private const string ProcComprobantes = "PROC_COMERCIAL_TIPOS_OPERACION_MATRIZ_COMPROBANTES";
    private const string ProcComprobanteSet = "PROC_COMERCIAL_TIPOS_OPERACION_MATRIZ_COMPROBANTES_SET";
    private const string ProcFormulas = "PROC_COMERCIAL_TIPOS_OPERACION_FORMULAS";
    private const string ProcFormulaSet = "PROC_COMERCIAL_TIPOS_OPERACION_FORMULA_SET";
    private readonly IDbConnectionFactory _db;
    public MatrizTiposOperacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<MatrizCalculoCelda>> CalculoAsync(string codClaseOperacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MatrizCalculoCelda>(ProcCalculo, new { COD_CLASE_OPERACION = codClaseOperacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<MatrizSetResultado> CalculoSetAsync(MatrizCalculoSetDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<MatrizSetResultado>(ProcCalculoSet, new
        {
            dto.COD_CLASE_OPERACION, dto.COD_TIPO_OPERACION, dto.COD_TIPO_VALOR_CALCULO, dto.ACTIVO, dto.FORZAR,
            COD_USUARIO = codUsuario, COD_ESTACION = codEstacion
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<MatrizComprobanteCelda>> ComprobantesAsync(string codClaseOperacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MatrizComprobanteCelda>(ProcComprobantes, new { COD_CLASE_OPERACION = codClaseOperacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<MatrizSetResultado> ComprobanteSetAsync(MatrizComprobanteSetDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<MatrizSetResultado>(ProcComprobanteSet, new
        {
            dto.COD_CLASE_OPERACION, dto.COD_TIPO_OPERACION, dto.COD_TIPO_COMPROBANTE_PAGO, dto.ACTIVO,
            COD_USUARIO = codUsuario, COD_ESTACION = codEstacion
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FormulaCelda>> FormulasAsync(string codClaseOperacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FormulaCelda>(ProcFormulas, new { COD_CLASE_OPERACION = codClaseOperacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<FormulaSetResultado> FormulaSetAsync(FormulaSetDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<FormulaSetResultado>(ProcFormulaSet, new
        {
            dto.COD_CLASE_OPERACION, dto.COD_TIPO_OPERACION, dto.COD_TIPO_VALOR_CALCULO,
            AFECTADOS = string.Join(",", dto.AFECTADOS ?? new()),
            dto.ACTUALIZAR_PROPIEDADES, dto.NUM_ORDEN_PRESENTACION, dto.FLG_CALCULO_INVERSO, dto.FLG_MOSTRAR_COMPROBANTE, dto.FLG_VALOR_MOSTRAR, dto.FLG_POSITIVO,
            COD_USUARIO = codUsuario, COD_ESTACION = codEstacion
        }, commandType: CommandType.StoredProcedure);
    }
}
