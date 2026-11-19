using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITransportesEspecificacionesTecnicasRepository
{
    Task<IEnumerable<TransportesEspecificacionesTecnicas>> ShowallAsync(string COD_TRANSPORTE);
    Task<TransportesEspecificacionesTecnicas?> ShowbyidAsync(string COD_TRANSPORTE, string COD_ESPECIFICACION_TECNICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_TRANSPORTE);
    Task<IEnumerable<TransportesEspecificacionesTecnicas>> SearchAsync(TransportesEspecificacionesTecnicasSearchDto dto);
    Task<WriteResult> InsertAsync(TransportesEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TransportesEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TRANSPORTE, string COD_ESPECIFICACION_TECNICA);
}

public interface ITransportesEspecificacionesTecnicasService
{
    Task<ApiResponse<IEnumerable<TransportesEspecificacionesTecnicas>>> GetAllAsync(string COD_TRANSPORTE);
    Task<ApiResponse<TransportesEspecificacionesTecnicas>> GetByIdAsync(string COD_TRANSPORTE, string COD_ESPECIFICACION_TECNICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_TRANSPORTE);
    Task<ApiResponse<IEnumerable<TransportesEspecificacionesTecnicas>>> SearchAsync(TransportesEspecificacionesTecnicasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TransportesEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TransportesEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TRANSPORTE, string COD_ESPECIFICACION_TECNICA);
}