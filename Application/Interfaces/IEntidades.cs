using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesRepository
{
    /// <summary>desTiposEntidad opcional: string armado como 'CLI_PRV_' (cada
    /// COD_TIPO_ENTIDAD elegido + guion bajo, concatenados) — lo recibe tal cual
    /// PROC_ESTANDAR_ENTIDADES_SHOWALL en @DES_TIPOS_ENTIDAD para el filtro multi-selección
    /// de chips en la grilla (independiente del CodTipoEntidadFijo de las pantallas
    /// reutilizadas, que usa el _MODULESHOWALL de un solo tipo).</summary>
    Task<IEnumerable<Entidades>> ShowallAsync(string? desTiposEntidad = null);
    Task<Entidades?> ShowbyidAsync(decimal COD_ENTIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Entidades>> SearchAsync(EntidadesSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD);
    /// <summary>PROC_ESTANDAR_ENTIDADES_PARADEPENDER — solo entidades Persona Jurídica
    /// (COD_TIPO_PERSONA = 2), candidatas a ser la "entidad dependencia" de una natural.</summary>
    Task<IEnumerable<ComboboxItem>> ShowparadependerAsync();
    /// <summary>PROC_ESTANDAR_ENTIDADES_SHOWIMAGES — experimento: logos de todas las
    /// entidades, en un endpoint aparte del SHOWALL principal (que no trae IMG_LOGO).</summary>
    Task<IEnumerable<EntidadImagen>> ShowimagesAsync();
    /// <summary>PROC_ESTANDAR_ENTIDADES_TIPOS_ENTIDAD_MODULESHOWALL — solo entidades
    /// asignadas al COD_TIPO_ENTIDAD indicado (pantallas reutilizadas con tipo fijo).</summary>
    Task<IEnumerable<Entidades>> ShowallPorTipoAsync(string codTipoEntidad);
    /// <summary>PROC_ESTANDAR_ENTIDADES_TIPOS_ENTIDAD_MODULESEARCH — igual que
    /// ShowallPorTipoAsync pero con filtro de texto.</summary>
    Task<IEnumerable<Entidades>> SearchPorTipoAsync(string codTipoEntidad, string search);
    /// <summary>PROC_ESTANDAR_ENTIDADES_TIPOS_ENTIDAD_DEPENDIENTES_MODULESHOWALL — entidades
    /// del COD_TIPO_ENTIDAD indicado que además dependen (COD_ENTIDAD_DEPENDENCIA) de la
    /// entidad dada (ej. "Entidad conductora" dependiente de la "Entidad propietaria").</summary>
    Task<IEnumerable<Entidades>> ShowallPorTipoDependienteAsync(string codTipoEntidad, decimal codEntidadDependencia);
    /// <summary>PROC_ESTANDAR_ENTIDADES_LOADCOMBOS — tipos de persona, tipos de documento
    /// de identidad, países, estados, tipos de medio de contacto y tipos de dirección en
    /// un solo viaje para el diálogo de Entidades.</summary>
    Task<EntidadesLoadCombosResult> LoadcombosAsync(int codTipoEstado);
}

public interface IEntidadesService
{
    Task<ApiResponse<IEnumerable<Entidades>>> GetAllAsync(string? desTiposEntidad = null);
    Task<ApiResponse<Entidades>> GetByIdAsync(decimal COD_ENTIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Entidades>>> SearchAsync(EntidadesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetParaDependerAsync();
    Task<ApiResponse<IEnumerable<EntidadImagen>>> GetImagesAsync();
    Task<ApiResponse<IEnumerable<Entidades>>> GetAllPorTipoAsync(string codTipoEntidad);
    Task<ApiResponse<IEnumerable<Entidades>>> SearchPorTipoAsync(string codTipoEntidad, string? search);
    Task<ApiResponse<IEnumerable<Entidades>>> GetAllPorTipoDependienteAsync(string codTipoEntidad, decimal codEntidadDependencia);
    Task<ApiResponse<EntidadesLoadCombosResult>> LoadcombosAsync(int codTipoEstado);
}