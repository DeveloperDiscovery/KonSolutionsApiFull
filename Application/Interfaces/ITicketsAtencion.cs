using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Soporte;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITicketsAtencionRepository
{
    Task<IEnumerable<TicketsAtencion>> ShowallAsync();
    Task<TicketsAtencion?> ShowbyidAsync(int NUM_TICKET);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TicketsAtencion>> SearchAsync(TicketsAtencionSearchDto dto);
    Task<WriteResult> InsertAsync(TicketsAtencionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TicketsAtencionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_TICKET);
}

public interface ITicketsAtencionService
{
    Task<ApiResponse<IEnumerable<TicketsAtencion>>> GetAllAsync();
    Task<ApiResponse<TicketsAtencion>> GetByIdAsync(int NUM_TICKET);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TicketsAtencion>>> SearchAsync(TicketsAtencionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TicketsAtencionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TicketsAtencionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_TICKET);
}