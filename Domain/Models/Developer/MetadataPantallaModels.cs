namespace KonSolutionsApi.Domain.Models.Developer;

// ── Request models (from web designer) ────────────────────────────────────────

public class GuardarMetadataRequest
{
    public string CodAplicacion { get; set; } = "";
    public string CodOpcion     { get; set; } = "";
    public List<NivelGuardarDto> Niveles { get; set; } = new();
}

public class NivelGuardarDto
{
    public Guid   Id             { get; set; }
    public Guid?  NivelPadreId   { get; set; }
    public int    Profundidad    { get; set; }
    public string Schema         { get; set; } = "";
    public string NombreTabla    { get; set; } = "";
    public string TituloGrilla   { get; set; } = "";
    public string TipoPantalla   { get; set; } = "GRILLA_FORM";
    public int    FilasPorPagina { get; set; } = 10;

    public List<CampoPKGuardarDto>         CamposPK         { get; set; } = new();
    public List<ColumnaGrillaGuardarDto>   ColumnasGrilla   { get; set; } = new();
    public List<CampoFormularioGuardarDto> CamposFormulario { get; set; } = new();
    public List<RelacionGuardarDto>        Relaciones       { get; set; } = new();
}

public class CampoPKGuardarDto
{
    public string NombreCampo { get; set; } = "";
    public int    Orden       { get; set; }
}

public class ColumnaGrillaGuardarDto
{
    public string  NombreCampo   { get; set; } = "";
    public string  Etiqueta      { get; set; } = "";
    public string? CampoMostrar  { get; set; }
    public int     Orden         { get; set; }
    public bool    EsFiltrable   { get; set; }
    public bool    EsOrdenable   { get; set; }
}

public class CampoFormularioGuardarDto
{
    public string  NombreCampo   { get; set; } = "";
    public string  Etiqueta      { get; set; } = "";
    public string  TipoInput     { get; set; } = "TEXTO";
    public int     Orden         { get; set; }
    public bool    EsObligatorio { get; set; }
    public bool    EsSoloLectura { get; set; }
    public string? TablaCombo    { get; set; }
    public int?    LongitudMax   { get; set; }
}

public class RelacionGuardarDto
{
    public string CampoPadre { get; set; } = "";
    public string CampoHijo  { get; set; } = "";
}

// ── Response models (to web dynamic screen) ───────────────────────────────────

public class PantallaMetadataResponse
{
    public string CodAplicacion { get; set; } = "";
    public string CodOpcion     { get; set; } = "";
    public List<NivelMetadataResponse> Niveles { get; set; } = new();
}

public class NivelMetadataResponse
{
    public int    NumNivel       { get; set; }
    public string Titulo         { get; set; } = "";
    public string Tabla          { get; set; } = "";
    public string Schema         { get; set; } = "";

    public List<ClavePKResponse>         Claves           { get; set; } = new();
    public List<ColumnaGrillaResponse>   ColumnasGrilla   { get; set; } = new();
    public List<CampoFormularioResponse> CamposFormulario { get; set; } = new();
}

public class ClavePKResponse
{
    public string Campo { get; set; } = "";
    public int    Orden { get; set; }
}

public class ColumnaGrillaResponse
{
    public string  Campo         { get; set; } = "";
    public string  Etiqueta      { get; set; } = "";
    public int     Orden         { get; set; }
    public bool    EsFiltrable   { get; set; }
    public bool    EsOrdenable   { get; set; }
    public string? CampoMostrar  { get; set; }
}

public class CampoFormularioResponse
{
    public string  Campo         { get; set; } = "";
    public string  Etiqueta      { get; set; } = "";
    public string  TipoControl   { get; set; } = "TEXTO";
    public int     Orden         { get; set; }
    public bool    EsObligatorio { get; set; }
    public bool    EsSoloLectura { get; set; }
    public string? TablaCombo    { get; set; }
    public int?    LongitudMax   { get; set; }
}
