using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Soporte;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITicketsAtencionDetallePasosRepository
{
    Task<IEnumerable<TicketsAtencionDetallePasos>> ShowallAsync();
    Task<TicketsAtencionDetallePasos?> ShowbyidAsync(int NUM_TICKET, int NUM_SECUENCIA, int NUM_PASO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TicketsAtencionDetallePasos>> SearchAsync(TicketsAtencionDetallePasosSearchDto dto);
    Task<WriteResult> InsertAsync(TicketsAtencionDetallePasosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TicketsAtencionDetallePasosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_TICKET, int NUM_SECUENCIA, int NUM_PASO);
}

public interface ITicketsAtencionDetallePasosService
{
    Task<ApiResponse<IEnumerable<TicketsAtencionDetallePasos>>> GetAllAsync();
    Task<ApiResponse<TicketsAtencionDetallePasos>> GetByIdAsync(int NUM_TICKET, int NUM_SECUENCIA, int NUM_PASO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TicketsAtencionDetallePasos>>> SearchAsync(TicketsAtencionDetallePasosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TicketsAtencionDetallePasosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TicketsAtencionDetallePasosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_TICKET, int NUM_SECUENCIA, int NUM_PASO);
}