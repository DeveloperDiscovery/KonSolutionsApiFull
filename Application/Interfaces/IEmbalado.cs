using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEmbaladoRepository
{
    Task<IEnumerable<Embalado>> ShowallAsync();
    Task<Embalado?> ShowbyidAsync(int NUM_EMBALADO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Embalado>> SearchAsync(EmbaladoSearchDto dto);
    Task<WriteResult> InsertAsync(EmbaladoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EmbaladoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_EMBALADO);
}

public interface IEmbaladoService
{
    Task<ApiResponse<IEnumerable<Embalado>>> GetAllAsync();
    Task<ApiResponse<Embalado>> GetByIdAsync(int NUM_EMBALADO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Embalado>>> SearchAsync(EmbaladoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EmbaladoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EmbaladoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_EMBALADO);
}