using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICostosEntidadesTarifadoRepository
{
    Task<IEnumerable<CostosEntidadesTarifado>> ShowallAsync();
    Task<CostosEntidadesTarifado?> ShowbyidAsync(int NUM_PERIODO_CONTABLE, string COD_PROCESO, string COD_PRODUCTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CostosEntidadesTarifado>> SearchAsync(CostosEntidadesTarifadoSearchDto dto);
    Task<WriteResult> InsertAsync(CostosEntidadesTarifadoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CostosEntidadesTarifadoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_PROCESO, string COD_PRODUCTO);
}

public interface ICostosEntidadesTarifadoService
{
    Task<ApiResponse<IEnumerable<CostosEntidadesTarifado>>> GetAllAsync();
    Task<ApiResponse<CostosEntidadesTarifado>> GetByIdAsync(int NUM_PERIODO_CONTABLE, string COD_PROCESO, string COD_PRODUCTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CostosEntidadesTarifado>>> SearchAsync(CostosEntidadesTarifadoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CostosEntidadesTarifadoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CostosEntidadesTarifadoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_PROCESO, string COD_PRODUCTO);
}