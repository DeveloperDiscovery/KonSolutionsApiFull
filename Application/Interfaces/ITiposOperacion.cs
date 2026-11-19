using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposOperacionRepository
{
    Task<IEnumerable<TiposOperacion>> ShowallAsync(string codClaseOperacion);
    Task<TiposOperacion?> ShowbyidAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposOperacion>> SearchAsync(string codClaseOperacion, TiposOperacionSearchDto dto);
    Task<WriteResult> InsertAsync(TiposOperacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposOperacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION);
    /// <summary>PROC_ESTANDAR_TIPOS_OPERACION_LOADCOMBOS — tipos de entidad, tipos de
    /// pedido comercial, tipos de precio, condiciones de pago, clases de operación y
    /// estados en un solo viaje para el diálogo de Tipos de Operación.</summary>
    Task<TiposOperacionLoadCombosResult> LoadcombosAsync(int codTipoEstadoClasesOperacion);
}

public interface ITiposOperacionService
{
    Task<ApiResponse<IEnumerable<TiposOperacion>>> GetAllAsync(string codClaseOperacion);
    Task<ApiResponse<TiposOperacion>> GetByIdAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposOperacion>>> SearchAsync(string codClaseOperacion, TiposOperacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposOperacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposOperacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION);
    Task<ApiResponse<TiposOperacionLoadCombosResult>> LoadcombosAsync(int codTipoEstadoClasesOperacion);
}