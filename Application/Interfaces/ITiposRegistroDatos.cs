using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposRegistroDatosRepository
{
    Task<IEnumerable<TiposRegistroDatos>> ShowallAsync();
    Task<TiposRegistroDatos?> ShowbyidAsync(string COD_TIPO_REGISTRO_DATOS);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposRegistroDatos>> SearchAsync(TiposRegistroDatosSearchDto dto);
    Task<WriteResult> InsertAsync(TiposRegistroDatosInsertDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_REGISTRO_DATOS);
}

public interface ITiposRegistroDatosService
{
    Task<ApiResponse<IEnumerable<TiposRegistroDatos>>> GetAllAsync();
    Task<ApiResponse<TiposRegistroDatos>> GetByIdAsync(string COD_TIPO_REGISTRO_DATOS);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposRegistroDatos>>> SearchAsync(TiposRegistroDatosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposRegistroDatosInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_REGISTRO_DATOS);
}
