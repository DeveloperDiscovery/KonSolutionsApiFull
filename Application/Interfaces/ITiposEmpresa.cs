using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposEmpresaRepository
{
    Task<IEnumerable<TiposEmpresa>> ShowallAsync();
    Task<TiposEmpresa?> ShowbyidAsync(string COD_TIPO_EMPRESA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string? search = null);
    Task<IEnumerable<TiposEmpresa>> SearchAsync(TiposEmpresaSearchDto dto);
    Task<WriteResult> InsertAsync(TiposEmpresaInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposEmpresaUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_EMPRESA);
}

public interface ITiposEmpresaService
{
    Task<ApiResponse<IEnumerable<TiposEmpresa>>> GetAllAsync();
    Task<ApiResponse<TiposEmpresa>> GetByIdAsync(string COD_TIPO_EMPRESA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string? search = null);
    Task<ApiResponse<IEnumerable<TiposEmpresa>>> SearchAsync(TiposEmpresaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposEmpresaInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposEmpresaUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_EMPRESA);
}