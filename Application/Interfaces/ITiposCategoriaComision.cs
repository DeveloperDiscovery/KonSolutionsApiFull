using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposCategoriaComisionRepository
{
    Task<IEnumerable<TiposCategoriaComision>> ShowallAsync();
    Task<TiposCategoriaComision?> ShowbyidAsync(string COD_TIPO_CATEGORIA_COMISION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposCategoriaComision>> SearchAsync(TiposCategoriaComisionSearchDto dto);
    Task<WriteResult> InsertAsync(TiposCategoriaComisionInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposCategoriaComisionUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_CATEGORIA_COMISION);
}

public interface ITiposCategoriaComisionService
{
    Task<ApiResponse<IEnumerable<TiposCategoriaComision>>> GetAllAsync();
    Task<ApiResponse<TiposCategoriaComision>> GetByIdAsync(string COD_TIPO_CATEGORIA_COMISION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposCategoriaComision>>> SearchAsync(TiposCategoriaComisionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposCategoriaComisionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposCategoriaComisionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_CATEGORIA_COMISION);
}