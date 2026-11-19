using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISaldosCuentaCorrienteRepository
{
    Task<IEnumerable<SaldosCuentaCorriente>> ShowallAsync();
    Task<SaldosCuentaCorriente?> ShowbyidAsync(int NUM_PERIODO_CONTABLE, decimal COD_ENTIDAD_CONTABLE, string COD_CUENTA_CONTABLE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SaldosCuentaCorriente>> SearchAsync(SaldosCuentaCorrienteSearchDto dto);
    Task<WriteResult> InsertAsync(SaldosCuentaCorrienteInsertDto dto);
    Task<WriteResult> UpdateAsync(SaldosCuentaCorrienteUpdateDto dto);
    Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE, decimal COD_ENTIDAD_CONTABLE, string COD_CUENTA_CONTABLE);
}

public interface ISaldosCuentaCorrienteService
{
    Task<ApiResponse<IEnumerable<SaldosCuentaCorriente>>> GetAllAsync();
    Task<ApiResponse<SaldosCuentaCorriente>> GetByIdAsync(int NUM_PERIODO_CONTABLE, decimal COD_ENTIDAD_CONTABLE, string COD_CUENTA_CONTABLE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SaldosCuentaCorriente>>> SearchAsync(SaldosCuentaCorrienteSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SaldosCuentaCorrienteInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(SaldosCuentaCorrienteUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PERIODO_CONTABLE, decimal COD_ENTIDAD_CONTABLE, string COD_CUENTA_CONTABLE);
}