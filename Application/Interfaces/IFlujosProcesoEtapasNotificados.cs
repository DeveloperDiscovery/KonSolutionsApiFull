using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IFlujosProcesoEtapasNotificadosRepository
{
    Task<IEnumerable<FlujosProcesoEtapasNotificados>> ShowallAsync();
    Task<FlujosProcesoEtapasNotificados?> ShowbyidAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA, decimal COD_ENTIDAD_NOTIFICAR);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<FlujosProcesoEtapasNotificados>> SearchAsync(FlujosProcesoEtapasNotificadosSearchDto dto);
    Task<WriteResult> InsertAsync(FlujosProcesoEtapasNotificadosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(FlujosProcesoEtapasNotificadosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA, decimal COD_ENTIDAD_NOTIFICAR);
}

public interface IFlujosProcesoEtapasNotificadosService
{
    Task<ApiResponse<IEnumerable<FlujosProcesoEtapasNotificados>>> GetAllAsync();
    Task<ApiResponse<FlujosProcesoEtapasNotificados>> GetByIdAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA, decimal COD_ENTIDAD_NOTIFICAR);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<FlujosProcesoEtapasNotificados>>> SearchAsync(FlujosProcesoEtapasNotificadosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(FlujosProcesoEtapasNotificadosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(FlujosProcesoEtapasNotificadosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA, decimal COD_ENTIDAD_NOTIFICAR);
}