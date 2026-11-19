using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICategoriasComisionHistoricoRepository
{
    Task<IEnumerable<CategoriasComisionHistorico>> ShowallAsync();
    Task<CategoriasComisionHistorico?> ShowbyidAsync(string COD_CATEGORIA_COMISION, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CategoriasComisionHistorico>> SearchAsync(CategoriasComisionHistoricoSearchDto dto);
    Task<WriteResult> InsertAsync(CategoriasComisionHistoricoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CategoriasComisionHistoricoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CATEGORIA_COMISION, int NUM_SECUENCIA);
}

public interface ICategoriasComisionHistoricoService
{
    Task<ApiResponse<IEnumerable<CategoriasComisionHistorico>>> GetAllAsync();
    Task<ApiResponse<CategoriasComisionHistorico>> GetByIdAsync(string COD_CATEGORIA_COMISION, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CategoriasComisionHistorico>>> SearchAsync(CategoriasComisionHistoricoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CategoriasComisionHistoricoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CategoriasComisionHistoricoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CATEGORIA_COMISION, int NUM_SECUENCIA);
}