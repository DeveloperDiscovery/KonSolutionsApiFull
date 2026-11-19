using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesTiposEntidadesRepository
{
    Task<IEnumerable<EntidadesTiposEntidades>> ShowallAsync();
    Task<EntidadesTiposEntidades?> ShowbyidAsync(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EntidadesTiposEntidades>> SearchAsync(EntidadesTiposEntidadesSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesTiposEntidadesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD);
    Task<IEnumerable<EntidadesTiposEntidadesShowselectRow>> ShowselectAsync(decimal COD_ENTIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxidAsync(string codTipoEntidad);
}

public interface IEntidadesTiposEntidadesService
{
    Task<ApiResponse<IEnumerable<EntidadesTiposEntidades>>> GetAllAsync();
    Task<ApiResponse<EntidadesTiposEntidades>> GetByIdAsync(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EntidadesTiposEntidades>>> SearchAsync(EntidadesTiposEntidadesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesTiposEntidadesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD);
    Task<ApiResponse<IEnumerable<EntidadesTiposEntidadesShowselectRow>>> GetShowselectAsync(decimal COD_ENTIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxidAsync(string codTipoEntidad);
}