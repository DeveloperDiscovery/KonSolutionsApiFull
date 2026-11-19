using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposContratoRepository
{
    Task<IEnumerable<TiposContrato>> ShowallAsync();
    Task<TiposContrato?> ShowbyidAsync(string COD_TIPO_CONTRATO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposContrato>> SearchAsync(TiposContratoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposContratoInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposContratoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_CONTRATO);
}

public interface ITiposContratoService
{
    Task<ApiResponse<IEnumerable<TiposContrato>>> GetAllAsync();
    Task<ApiResponse<TiposContrato>> GetByIdAsync(string COD_TIPO_CONTRATO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposContrato>>> SearchAsync(TiposContratoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposContratoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposContratoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_CONTRATO);
}