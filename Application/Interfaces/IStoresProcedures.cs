using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Developer;

namespace KonSolutionsApi.Application.Interfaces;

public interface IStoresProceduresRepository
{
    Task<IEnumerable<StoresProcedures>> ShowallAsync();
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<StoresProcedures>> SearchAsync(StoresProceduresSearchDto dto);
}

public interface IStoresProceduresService
{
    Task<ApiResponse<IEnumerable<StoresProcedures>>> GetAllAsync();
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<StoresProcedures>>> SearchAsync(StoresProceduresSearchDto dto);
}