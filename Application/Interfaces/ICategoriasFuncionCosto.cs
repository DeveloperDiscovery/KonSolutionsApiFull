using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICategoriasFuncionCostoRepository
{
    Task<IEnumerable<CategoriasFuncionCosto>> ShowallAsync();
    Task<CategoriasFuncionCosto?> ShowbyidAsync(string COD_CATEGORIA_FUNCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CategoriasFuncionCosto>> SearchAsync(CategoriasFuncionCostoSearchDto dto);
    Task<WriteResult> InsertAsync(CategoriasFuncionCostoInsertDto dto);
    Task<WriteResult> UpdateAsync(CategoriasFuncionCostoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_CATEGORIA_FUNCION);
}

public interface ICategoriasFuncionCostoService
{
    Task<ApiResponse<IEnumerable<CategoriasFuncionCosto>>> GetAllAsync();
    Task<ApiResponse<CategoriasFuncionCosto>> GetByIdAsync(string COD_CATEGORIA_FUNCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CategoriasFuncionCosto>>> SearchAsync(CategoriasFuncionCostoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CategoriasFuncionCostoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(CategoriasFuncionCostoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CATEGORIA_FUNCION);
}