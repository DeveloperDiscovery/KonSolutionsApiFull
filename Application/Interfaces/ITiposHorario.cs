using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposHorarioRepository
{
    Task<IEnumerable<TiposHorario>> ShowallAsync();
    Task<TiposHorario?> ShowbyidAsync(string COD_TIPO_HORARIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposHorario>> SearchAsync(TiposHorarioSearchDto dto);
    Task<WriteResult> InsertAsync(TiposHorarioInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposHorarioUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_HORARIO);
}

public interface ITiposHorarioService
{
    Task<ApiResponse<IEnumerable<TiposHorario>>> GetAllAsync();
    Task<ApiResponse<TiposHorario>> GetByIdAsync(string COD_TIPO_HORARIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposHorario>>> SearchAsync(TiposHorarioSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposHorarioInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposHorarioUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_HORARIO);
}