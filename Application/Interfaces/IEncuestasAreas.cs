using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEncuestasAreasRepository
{
    Task<IEnumerable<EncuestasAreas>> ShowallAsync();
    Task<EncuestasAreas?> ShowbyidAsync(int NUM_ENCUESTA, int COD_AREA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EncuestasAreas>> SearchAsync(EncuestasAreasSearchDto dto);
    Task<WriteResult> InsertAsync(EncuestasAreasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_ENCUESTA, int COD_AREA);
}

public interface IEncuestasAreasService
{
    Task<ApiResponse<IEnumerable<EncuestasAreas>>> GetAllAsync();
    Task<ApiResponse<EncuestasAreas>> GetByIdAsync(int NUM_ENCUESTA, int COD_AREA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EncuestasAreas>>> SearchAsync(EncuestasAreasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EncuestasAreasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_ENCUESTA, int COD_AREA);
}