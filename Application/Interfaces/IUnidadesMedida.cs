using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IUnidadesMedidaRepository
{
    Task<IEnumerable<UnidadesMedida>> ShowallAsync();
    Task<UnidadesMedida?> ShowbyidAsync(string COD_UNIDAD_MEDIDA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<UnidadesMedida>> SearchAsync(UnidadesMedidaSearchDto dto);
    Task<WriteResult> InsertAsync(UnidadesMedidaInsertDto dto);
    Task<WriteResult> DeleteAsync(string COD_UNIDAD_MEDIDA);
    Task<WriteResult> UploadJsonAsync(string jsonArray);
}

public interface IUnidadesMedidaService
{
    Task<ApiResponse<IEnumerable<UnidadesMedida>>> GetAllAsync();
    Task<ApiResponse<UnidadesMedida>> GetByIdAsync(string COD_UNIDAD_MEDIDA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<UnidadesMedida>>> SearchAsync(UnidadesMedidaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(UnidadesMedidaInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(string id, UnidadesMedidaInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_UNIDAD_MEDIDA);
    Task<ApiResponse<WriteResult>> UploadJsonAsync(string jsonArray);
}