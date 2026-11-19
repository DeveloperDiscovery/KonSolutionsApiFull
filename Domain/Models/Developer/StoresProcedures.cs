using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Developer;

/// <summary>Entidad STORES_PROCEDURES - tipos equivalentes a la BD.</summary>
public class StoresProcedures
{
    public string NAME_SCHEMA { get; set; } = string.Empty;
    public string NAME_TABLA { get; set; } = string.Empty;
    public string NAME_STORE_PROCEDURE { get; set; } = string.Empty;
    public string DESCRIPCION_ERROR { get; set; } = string.Empty;
}

public class StoresProceduresInsertDto
{
}

public class StoresProceduresUpdateDto
{
}

public class StoresProceduresSearchDto
{
    public string? Search { get; set; }
}