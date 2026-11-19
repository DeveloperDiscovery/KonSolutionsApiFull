using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad INVENTARIOS_ALMACENES - tipos equivalentes a la BD.</summary>
public class InventariosAlmacenes
{
    public int? NUM_INVENTARIO { get; set; }
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string? DES_ALMACEN { get; set; }
    public bool? FLG_TOTAL { get; set; }
    public bool? FLG_INMOVILIZACION { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    // Faltaban en esta entidad (solo estaban en los DTOs de Insert/Update): Dapper mapea la
    // fila del SP por nombre de propiedad, así que sin estas 5 el valor de la BD se
    // descartaba en silencio y GET/GetById siempre devolvían null para las fechas, aunque
    // el registro sí las tuviera cargadas.
    public DateTime? FEC_INICIO_PROGRAMADA { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADA { get; set; }
    public DateTime? FEC_INICIO_EFECTUADA { get; set; }
    public DateTime? FEC_FINAL_EFECTUADA { get; set; }
    public DateTime? FEC_CORTE { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_INVENTARIO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class InventariosAlmacenesInsertDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public bool? FLG_TOTAL { get; set; }
    public bool? FLG_INMOVILIZACION { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public DateTime? FEC_INICIO_PROGRAMADA { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADA { get; set; }
    public DateTime? FEC_INICIO_EFECTUADA { get; set; }
    public DateTime? FEC_FINAL_EFECTUADA { get; set; }
    public DateTime? FEC_CORTE { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class InventariosAlmacenesUpdateDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public bool? FLG_TOTAL { get; set; }
    public bool? FLG_INMOVILIZACION { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public DateTime? FEC_INICIO_PROGRAMADA { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADA { get; set; }
    public DateTime? FEC_INICIO_EFECTUADA { get; set; }
    public DateTime? FEC_FINAL_EFECTUADA { get; set; }
    public DateTime? FEC_CORTE { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class InventariosAlmacenesSearchDto
{
    public string? Search { get; set; }
}