using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Developer;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMensajesRepository
{
    Task<IEnumerable<Mensajes>> ShowallAsync();
    Task<Mensajes?> ShowbyidAsync(string COD_KEY_MENSAJE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Mensajes>> SearchAsync(MensajesSearchDto dto);
    Task<WriteResult> InsertAsync(MensajesInsertDto dto);
    Task<WriteResult> UpdateAsync(MensajesUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_KEY_MENSAJE);
}

public interface IMensajesService
{
    Task<ApiResponse<IEnumerable<Mensajes>>> GetAllAsync();
    Task<ApiResponse<Mensajes>> GetByIdAsync(string COD_KEY_MENSAJE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Mensajes>>> SearchAsync(MensajesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MensajesInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(MensajesUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_KEY_MENSAJE);
}