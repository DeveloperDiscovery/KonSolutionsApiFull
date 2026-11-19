using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEstadosRepository
{
    Task<IEnumerable<Estados>> ShowallAsync();
    Task<IEnumerable<Estados>> ShowByTipoAsync(int codTipoEstado);
    Task<WriteResult> ImportFromEstandarAsync(int codTipoEstado, int codEstadoEstandar, string codUsuario, string codEstacion);
    Task<Estados?> ShowbyidAsync(int COD_TIPO_ESTADO, int COD_ESTADO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Estados>> SearchAsync(EstadosSearchDto dto);
    Task<WriteResult> InsertAsync(EstadosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int COD_TIPO_ESTADO, int COD_ESTADO);
}

public interface IEstadosService
{
    Task<ApiResponse<IEnumerable<Estados>>> GetAllAsync();
    Task<ApiResponse<IEnumerable<Estados>>> GetByTipoAsync(int codTipoEstado);
    Task<ApiResponse<WriteResult>> ImportFromEstandarAsync(int codTipoEstado, int codEstadoEstandar, string codUsuario, string codEstacion);
    Task<ApiResponse<Estados>> GetByIdAsync(int COD_TIPO_ESTADO, int COD_ESTADO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Estados>>> SearchAsync(EstadosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EstadosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_TIPO_ESTADO, int COD_ESTADO);
}