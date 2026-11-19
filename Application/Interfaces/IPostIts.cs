using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPostItsRepository
{
    Task<IEnumerable<PostIts>> ShowallAsync();
    Task<PostIts?> ShowbyidAsync(long NUM_POSTIT);
    Task<WriteResult> InsertAsync(PostItsInsertDto dto);
    Task<WriteResult> DeleteAsync(long NUM_POSTIT);
}

public interface IPostItsService
{
    Task<ApiResponse<IEnumerable<PostIts>>> GetAllAsync();
    Task<ApiResponse<PostIts>> GetByIdAsync(long NUM_POSTIT);
    Task<ApiResponse<WriteResult>> InsertAsync(PostItsInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(long NUM_POSTIT);
}
