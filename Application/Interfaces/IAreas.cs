using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Administracion;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAreasRepository
{
    Task<IEnumerable<Areas>> ShowallAsync();
    Task<Areas?> ShowbyidAsync(int COD_AREA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Areas>> SearchAsync(AreasSearchDto dto);
    Task<WriteResult> InsertAsync(AreasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(AreasUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_AREA);
    Task<AreasLoadCombosResult> LoadcombosAsync(int codTipoEstado);
    Task<IEnumerable<ComboboxItem>> GetCargosByAreaAsync(int codArea);
}

public interface IAreasService
{
    Task<ApiResponse<IEnumerable<Areas>>> GetAllAsync();
    Task<ApiResponse<Areas>> GetByIdAsync(int COD_AREA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Areas>>> SearchAsync(AreasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AreasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(AreasUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_AREA);
    Task<ApiResponse<AreasLoadCombosResult>> LoadcombosAsync(int codTipoEstado);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetCargosByAreaAsync(int codArea);
}