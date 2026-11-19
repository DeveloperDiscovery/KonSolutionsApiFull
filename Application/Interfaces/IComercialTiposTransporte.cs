using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IComercialTiposTransporteRepository
{
    Task<IEnumerable<ComercialTiposTransporte>> ShowallAsync();
    Task<ComercialTiposTransporte?> ShowbyidAsync(string COD_TIPO_TRANSPORTE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ComercialTiposTransporte>> SearchAsync(ComercialTiposTransporteSearchDto dto);
    Task<WriteResult> InsertAsync(ComercialTiposTransporteInsertDto dto);
    Task<WriteResult> UpdateAsync(ComercialTiposTransporteUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_TRANSPORTE);
}

public interface IComercialTiposTransporteService
{
    Task<ApiResponse<IEnumerable<ComercialTiposTransporte>>> GetAllAsync();
    Task<ApiResponse<ComercialTiposTransporte>> GetByIdAsync(string COD_TIPO_TRANSPORTE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ComercialTiposTransporte>>> SearchAsync(ComercialTiposTransporteSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ComercialTiposTransporteInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ComercialTiposTransporteUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_TRANSPORTE);
}