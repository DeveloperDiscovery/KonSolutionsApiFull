using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEstadosEstandarRepository
{
    Task<IEnumerable<EstadosEstandar>> ShowallAsync();
    Task<EstadosEstandar?> ShowbyidAsync(int COD_INDICE, int COD_ESTADO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EstadosEstandar>> SearchAsync(EstadosEstandarSearchDto dto);
    Task<WriteResult> InsertAsync(EstadosEstandarInsertDto dto);
    Task<WriteResult> UpdateAsync(EstadosEstandarUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_INDICE, int COD_ESTADO);
}

public interface IEstadosEstandarService
{
    Task<ApiResponse<IEnumerable<EstadosEstandar>>> GetAllAsync();
    Task<ApiResponse<EstadosEstandar>> GetByIdAsync(int COD_INDICE, int COD_ESTADO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EstadosEstandar>>> SearchAsync(EstadosEstandarSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EstadosEstandarInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(EstadosEstandarUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_INDICE, int COD_ESTADO);
}