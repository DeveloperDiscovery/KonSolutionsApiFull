using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEstructuraRfidRepository
{
    Task<IEnumerable<EstructuraRfid>> ShowallAsync();
    Task<EstructuraRfid?> ShowbyidAsync(string COD_ESTRUCTURA_RFID);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EstructuraRfid>> SearchAsync(EstructuraRfidSearchDto dto);
    Task<WriteResult> InsertAsync(EstructuraRfidInsertDto dto);
    Task<WriteResult> UpdateAsync(EstructuraRfidUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_ESTRUCTURA_RFID);
}

public interface IEstructuraRfidService
{
    Task<ApiResponse<IEnumerable<EstructuraRfid>>> GetAllAsync();
    Task<ApiResponse<EstructuraRfid>> GetByIdAsync(string COD_ESTRUCTURA_RFID);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EstructuraRfid>>> SearchAsync(EstructuraRfidSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EstructuraRfidInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(EstructuraRfidUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ESTRUCTURA_RFID);
}
