using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMotivosRepository
{
    Task<IEnumerable<Motivos>> ShowallAsync();
    Task<IEnumerable<Motivos>> ShowByTipoAsync(int codTipoMotivo);
    Task<Motivos?> ShowbyidAsync(int COD_TIPO_MOTIVO, int COD_MOTIVO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Motivos>> SearchAsync(MotivosSearchDto dto);
    Task<WriteResult> InsertAsync(MotivosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(MotivosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_TIPO_MOTIVO, int COD_MOTIVO);
}

public interface IMotivosService
{
    Task<ApiResponse<IEnumerable<Motivos>>> GetAllAsync();
    Task<ApiResponse<IEnumerable<Motivos>>> GetByTipoAsync(int codTipoMotivo);
    Task<ApiResponse<Motivos>> GetByIdAsync(int COD_TIPO_MOTIVO, int COD_MOTIVO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Motivos>>> SearchAsync(MotivosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MotivosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(MotivosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_TIPO_MOTIVO, int COD_MOTIVO);
}