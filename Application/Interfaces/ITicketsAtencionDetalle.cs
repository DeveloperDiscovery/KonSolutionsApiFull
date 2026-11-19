using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Soporte;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITicketsAtencionDetalleRepository
{
    Task<IEnumerable<TicketsAtencionDetalle>> ShowallAsync();
    Task<TicketsAtencionDetalle?> ShowbyidAsync(int NUM_TICKET, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TicketsAtencionDetalle>> SearchAsync(TicketsAtencionDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(TicketsAtencionDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TicketsAtencionDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_TICKET, int NUM_SECUENCIA);
}

public interface ITicketsAtencionDetalleService
{
    Task<ApiResponse<IEnumerable<TicketsAtencionDetalle>>> GetAllAsync();
    Task<ApiResponse<TicketsAtencionDetalle>> GetByIdAsync(int NUM_TICKET, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TicketsAtencionDetalle>>> SearchAsync(TicketsAtencionDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TicketsAtencionDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TicketsAtencionDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_TICKET, int NUM_SECUENCIA);
}