using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposPosRepository
{
    Task<IEnumerable<TiposPos>> ShowallAsync();
    Task<TiposPos?> ShowbyidAsync(string COD_TIPO_POS);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposPos>> SearchAsync(TiposPosSearchDto dto);
    Task<WriteResult> InsertAsync(TiposPosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposPosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_POS);
}

public interface ITiposPosService
{
    Task<ApiResponse<IEnumerable<TiposPos>>> GetAllAsync();
    Task<ApiResponse<TiposPos>> GetByIdAsync(string COD_TIPO_POS);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposPos>>> SearchAsync(TiposPosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposPosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposPosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_POS);
}