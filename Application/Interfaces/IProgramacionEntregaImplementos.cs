using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProgramacionEntregaImplementosRepository
{
    Task<IEnumerable<ProgramacionEntregaImplementos>> ShowallAsync();
    Task<ProgramacionEntregaImplementos?> ShowbyidAsync(int NUM_ENTREGA_IMPLEMENTOS);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProgramacionEntregaImplementos>> SearchAsync(ProgramacionEntregaImplementosSearchDto dto);
    Task<WriteResult> InsertAsync(ProgramacionEntregaImplementosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProgramacionEntregaImplementosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_ENTREGA_IMPLEMENTOS);
}

public interface IProgramacionEntregaImplementosService
{
    Task<ApiResponse<IEnumerable<ProgramacionEntregaImplementos>>> GetAllAsync();
    Task<ApiResponse<ProgramacionEntregaImplementos>> GetByIdAsync(int NUM_ENTREGA_IMPLEMENTOS);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProgramacionEntregaImplementos>>> SearchAsync(ProgramacionEntregaImplementosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProgramacionEntregaImplementosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProgramacionEntregaImplementosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_ENTREGA_IMPLEMENTOS);
}