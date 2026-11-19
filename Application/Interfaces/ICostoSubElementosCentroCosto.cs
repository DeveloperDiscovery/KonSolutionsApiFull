using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICostoSubElementosCentroCostoRepository
{
    Task<IEnumerable<CostoSubElementosCentroCosto>> ShowallAsync();
    Task<CostoSubElementosCentroCosto?> ShowbyidAsync(int NUM_PERIODO_CONTABLE, string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CostoSubElementosCentroCosto>> SearchAsync(CostoSubElementosCentroCostoSearchDto dto);
    Task<WriteResult> InsertAsync(CostoSubElementosCentroCostoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CostoSubElementosCentroCostoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO);
}

public interface ICostoSubElementosCentroCostoService
{
    Task<ApiResponse<IEnumerable<CostoSubElementosCentroCosto>>> GetAllAsync();
    Task<ApiResponse<CostoSubElementosCentroCosto>> GetByIdAsync(int NUM_PERIODO_CONTABLE, string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CostoSubElementosCentroCosto>>> SearchAsync(CostoSubElementosCentroCostoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CostoSubElementosCentroCostoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CostoSubElementosCentroCostoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_ELEMENTO_COSTO, string COD_SUB_ELEMENTO_COSTO);
}