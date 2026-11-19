using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICajasSaldosRepository
{
    Task<IEnumerable<CajasSaldos>> ShowallAsync();
    Task<CajasSaldos?> ShowbyidAsync(string COD_CAJA, DateTime FEC_CIERRE, string COD_TURNO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CajasSaldos>> SearchAsync(CajasSaldosSearchDto dto);
    Task<WriteResult> InsertAsync(CajasSaldosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CajasSaldosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CAJA, DateTime FEC_CIERRE, string COD_TURNO);
}

public interface ICajasSaldosService
{
    Task<ApiResponse<IEnumerable<CajasSaldos>>> GetAllAsync();
    Task<ApiResponse<CajasSaldos>> GetByIdAsync(string COD_CAJA, DateTime FEC_CIERRE, string COD_TURNO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CajasSaldos>>> SearchAsync(CajasSaldosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CajasSaldosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CajasSaldosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CAJA, DateTime FEC_CIERRE, string COD_TURNO);
}