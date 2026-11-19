using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposSolicitudComercialRepository
{
    Task<IEnumerable<TiposSolicitudComercial>> ShowallAsync();
    Task<TiposSolicitudComercial?> ShowbyidAsync(string codTipoSolicitudComercial);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<WriteResult> InsertAsync(TiposSolicitudComercialInsertDto dto);
    Task<WriteResult> DeleteAsync(string codTipoSolicitudComercial);
}

public interface ITiposSolicitudComercialService
{
    Task<ApiResponse<IEnumerable<TiposSolicitudComercial>>> GetAllAsync();
    Task<ApiResponse<TiposSolicitudComercial>> GetByIdAsync(string codTipoSolicitudComercial);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<WriteResult>> InsertAsync(TiposSolicitudComercialInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codTipoSolicitudComercial);
}
