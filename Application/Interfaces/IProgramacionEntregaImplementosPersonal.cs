using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProgramacionEntregaImplementosPersonalRepository
{
    Task<IEnumerable<ProgramacionEntregaImplementosPersonal>> ShowallAsync();
    Task<ProgramacionEntregaImplementosPersonal?> ShowbyidAsync(int NUM_ENTREGA_IMPLEMENTOS, decimal COD_ENTIDAD_PERSONAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProgramacionEntregaImplementosPersonal>> SearchAsync(ProgramacionEntregaImplementosPersonalSearchDto dto);
    Task<WriteResult> InsertAsync(ProgramacionEntregaImplementosPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProgramacionEntregaImplementosPersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_ENTREGA_IMPLEMENTOS, decimal COD_ENTIDAD_PERSONAL);
}

public interface IProgramacionEntregaImplementosPersonalService
{
    Task<ApiResponse<IEnumerable<ProgramacionEntregaImplementosPersonal>>> GetAllAsync();
    Task<ApiResponse<ProgramacionEntregaImplementosPersonal>> GetByIdAsync(int NUM_ENTREGA_IMPLEMENTOS, decimal COD_ENTIDAD_PERSONAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProgramacionEntregaImplementosPersonal>>> SearchAsync(ProgramacionEntregaImplementosPersonalSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProgramacionEntregaImplementosPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProgramacionEntregaImplementosPersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_ENTREGA_IMPLEMENTOS, decimal COD_ENTIDAD_PERSONAL);
}