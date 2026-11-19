using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposUnidadMedidaRepository
{
    Task<IEnumerable<TiposUnidadMedida>> ShowallAsync();
    Task<TiposUnidadMedida?> ShowbyidAsync(string COD_TIPO_UNIDAD_MEDIDA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposUnidadMedida>> SearchAsync(TiposUnidadMedidaSearchDto dto);
    Task<WriteResult> InsertAsync(TiposUnidadMedidaInsertDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_UNIDAD_MEDIDA);
    Task<WriteResult> UploadJsonAsync(string jsonArray);
}

public interface ITiposUnidadMedidaService
{
    Task<ApiResponse<IEnumerable<TiposUnidadMedida>>> GetAllAsync();
    Task<ApiResponse<TiposUnidadMedida>> GetByIdAsync(string COD_TIPO_UNIDAD_MEDIDA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposUnidadMedida>>> SearchAsync(TiposUnidadMedidaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposUnidadMedidaInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(string id, TiposUnidadMedidaInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_UNIDAD_MEDIDA);
    Task<ApiResponse<WriteResult>> UploadJsonAsync(string jsonArray);
}