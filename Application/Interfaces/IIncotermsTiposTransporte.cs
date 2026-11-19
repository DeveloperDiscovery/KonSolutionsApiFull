using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IIncotermsTiposTransporteRepository
{
    Task<IEnumerable<IncotermsTiposTransporte>> ShowallAsync();
    Task<IncotermsTiposTransporte?> ShowbyidAsync(string COD_INCOTERM, string COD_TIPO_TRANSPORTE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<IncotermsTiposTransporte>> SearchAsync(IncotermsTiposTransporteSearchDto dto);
    Task<WriteResult> InsertAsync(IncotermsTiposTransporteInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_INCOTERM, string COD_TIPO_TRANSPORTE);
}

public interface IIncotermsTiposTransporteService
{
    Task<ApiResponse<IEnumerable<IncotermsTiposTransporte>>> GetAllAsync();
    Task<ApiResponse<IncotermsTiposTransporte>> GetByIdAsync(string COD_INCOTERM, string COD_TIPO_TRANSPORTE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<IncotermsTiposTransporte>>> SearchAsync(IncotermsTiposTransporteSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(IncotermsTiposTransporteInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_INCOTERM, string COD_TIPO_TRANSPORTE);
}