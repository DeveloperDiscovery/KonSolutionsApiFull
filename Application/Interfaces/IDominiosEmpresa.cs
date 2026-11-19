using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDominiosEmpresaRepository
{
    Task<IEnumerable<DominiosEmpresa>> ShowallAsync();
    Task<DominiosEmpresa?> ShowbyidAsync(byte COD_DOMINIO_EMPRESA);
    Task<IEnumerable<DominiosEmpresa>> ShowcomboxAsync();
    Task<WriteResult> InsertAsync(DominiosEmpresaInsertDto dto);
    Task<WriteResult> DeleteAsync(byte COD_DOMINIO_EMPRESA);
}

public interface IDominiosEmpresaService
{
    Task<ApiResponse<IEnumerable<DominiosEmpresa>>> GetAllAsync();
    Task<ApiResponse<DominiosEmpresa>> GetByIdAsync(byte COD_DOMINIO_EMPRESA);
    Task<ApiResponse<IEnumerable<DominiosEmpresa>>> GetComboboxAsync();
    Task<ApiResponse<WriteResult>> InsertAsync(DominiosEmpresaInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(byte COD_DOMINIO_EMPRESA);
}
