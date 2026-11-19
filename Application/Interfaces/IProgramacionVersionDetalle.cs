using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProgramacionVersionDetalleRepository
{
    Task<IEnumerable<ProgramacionVersionDetalle>> ShowallAsync();
    Task<ProgramacionVersionDetalle?> ShowbyidAsync(int NUM_PROGRAMA, int NUM_VERSION, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProgramacionVersionDetalle>> SearchAsync(ProgramacionVersionDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(ProgramacionVersionDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProgramacionVersionDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROGRAMA, int NUM_VERSION, int NUM_SECUENCIA);
}

public interface IProgramacionVersionDetalleService
{
    Task<ApiResponse<IEnumerable<ProgramacionVersionDetalle>>> GetAllAsync();
    Task<ApiResponse<ProgramacionVersionDetalle>> GetByIdAsync(int NUM_PROGRAMA, int NUM_VERSION, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProgramacionVersionDetalle>>> SearchAsync(ProgramacionVersionDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProgramacionVersionDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProgramacionVersionDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROGRAMA, int NUM_VERSION, int NUM_SECUENCIA);
}