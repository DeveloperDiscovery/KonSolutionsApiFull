using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposMotivosRepository
{
    Task<IEnumerable<TiposMotivos>> ShowallAsync();
    Task<TiposMotivos?> ShowbyidAsync(int COD_TIPO_MOTIVO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposMotivos>> SearchAsync(TiposMotivosSearchDto dto);
    Task<WriteResult> InsertAsync(TiposMotivosInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposMotivosUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_TIPO_MOTIVO);
}

public interface ITiposMotivosService
{
    Task<ApiResponse<IEnumerable<TiposMotivos>>> GetAllAsync();
    Task<ApiResponse<TiposMotivos>> GetByIdAsync(int COD_TIPO_MOTIVO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposMotivos>>> SearchAsync(TiposMotivosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposMotivosInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposMotivosUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_TIPO_MOTIVO);
}