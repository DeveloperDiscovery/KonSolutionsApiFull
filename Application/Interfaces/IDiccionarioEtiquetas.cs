using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDiccionarioEtiquetasRepository
{
    Task<IEnumerable<DiccionarioEtiquetas>> ShowallAsync();
    Task<DiccionarioEtiquetas?> ShowbyidAsync(string codEtiqueta);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<WriteResult> InsertAsync(DiccionarioEtiquetasInsertDto dto);
    Task<WriteResult> DeleteAsync(string codEtiqueta);
}

public interface IDiccionarioEtiquetasService
{
    Task<ApiResponse<IEnumerable<DiccionarioEtiquetas>>> GetAllAsync();
    Task<ApiResponse<DiccionarioEtiquetas>> GetByIdAsync(string codEtiqueta);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<WriteResult>> InsertAsync(DiccionarioEtiquetasInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codEtiqueta);
}
