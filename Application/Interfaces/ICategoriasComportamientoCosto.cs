using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICategoriasComportamientoCostoRepository
{
    Task<IEnumerable<CategoriasComportamientoCosto>> ShowallAsync();
    Task<CategoriasComportamientoCosto?> ShowbyidAsync(string COD_CATEGORIA_COMPORTAMIENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CategoriasComportamientoCosto>> SearchAsync(CategoriasComportamientoCostoSearchDto dto);
    Task<WriteResult> InsertAsync(CategoriasComportamientoCostoInsertDto dto);
    Task<WriteResult> UpdateAsync(CategoriasComportamientoCostoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_CATEGORIA_COMPORTAMIENTO);
}

public interface ICategoriasComportamientoCostoService
{
    Task<ApiResponse<IEnumerable<CategoriasComportamientoCosto>>> GetAllAsync();
    Task<ApiResponse<CategoriasComportamientoCosto>> GetByIdAsync(string COD_CATEGORIA_COMPORTAMIENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CategoriasComportamientoCosto>>> SearchAsync(CategoriasComportamientoCostoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CategoriasComportamientoCostoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(CategoriasComportamientoCostoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CATEGORIA_COMPORTAMIENTO);
}