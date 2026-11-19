using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICuentaCorrienteRepository
{
    Task<IEnumerable<CuentaCorriente>> ShowallAsync();
    Task<CuentaCorriente?> ShowbyidAsync(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD, string COD_MONEDA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CuentaCorriente>> SearchAsync(CuentaCorrienteSearchDto dto);
    Task<WriteResult> InsertAsync(CuentaCorrienteInsertDto dto);
    Task<WriteResult> UpdateAsync(CuentaCorrienteUpdateDto dto);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD, string COD_MONEDA);
}

public interface ICuentaCorrienteService
{
    Task<ApiResponse<IEnumerable<CuentaCorriente>>> GetAllAsync();
    Task<ApiResponse<CuentaCorriente>> GetByIdAsync(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD, string COD_MONEDA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CuentaCorriente>>> SearchAsync(CuentaCorrienteSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CuentaCorrienteInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(CuentaCorrienteUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD, string COD_MONEDA);
}