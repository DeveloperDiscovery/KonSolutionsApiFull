using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface ILogisticaTiposTransporteRepository
{
    Task<IEnumerable<LogisticaTiposTransporte>> ShowallAsync();
    Task<LogisticaTiposTransporte?> ShowbyidAsync(string COD_TIPO_TRANSPORTE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<LogisticaTiposTransporte>> SearchAsync(LogisticaTiposTransporteSearchDto dto);
    Task<WriteResult> InsertAsync(LogisticaTiposTransporteInsertDto dto);
    Task<WriteResult> UpdateAsync(LogisticaTiposTransporteUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_TRANSPORTE);
}

public interface ILogisticaTiposTransporteService
{
    Task<ApiResponse<IEnumerable<LogisticaTiposTransporte>>> GetAllAsync();
    Task<ApiResponse<LogisticaTiposTransporte>> GetByIdAsync(string COD_TIPO_TRANSPORTE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<LogisticaTiposTransporte>>> SearchAsync(LogisticaTiposTransporteSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(LogisticaTiposTransporteInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(LogisticaTiposTransporteUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_TRANSPORTE);
}