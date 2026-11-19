using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAlmacenesRepository
{
    Task<IEnumerable<Almacenes>> ShowallAsync();
    Task<Almacenes?> ShowbyidAsync(string COD_ALMACEN);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Almacenes>> SearchAsync(AlmacenesSearchDto dto);
    Task<WriteResult> InsertAsync(AlmacenesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ALMACEN);
    Task<AlmacenesLoadCombosResult> LoadcombosAsync();
}

public interface IAlmacenesService
{
    Task<ApiResponse<IEnumerable<Almacenes>>> GetAllAsync();
    Task<ApiResponse<Almacenes>> GetByIdAsync(string COD_ALMACEN);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Almacenes>>> SearchAsync(AlmacenesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AlmacenesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ALMACEN);
    Task<ApiResponse<AlmacenesLoadCombosResult>> LoadcombosAsync();
}