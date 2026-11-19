using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEstructuraEspecificacionTecnicaRepository
{
    Task<IEnumerable<EstructuraEspecificacionTecnica>> ShowallAsync();
    Task<EstructuraEspecificacionTecnica?> ShowbyidAsync(int NUM_ESTRUCTURA_ESPECIFICACION_TECNICA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EstructuraEspecificacionTecnica>> SearchAsync(EstructuraEspecificacionTecnicaSearchDto dto);
    Task<WriteResult> InsertAsync(EstructuraEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_ESTRUCTURA_ESPECIFICACION_TECNICA);
}

public interface IEstructuraEspecificacionTecnicaService
{
    Task<ApiResponse<IEnumerable<EstructuraEspecificacionTecnica>>> GetAllAsync();
    Task<ApiResponse<EstructuraEspecificacionTecnica>> GetByIdAsync(int NUM_ESTRUCTURA_ESPECIFICACION_TECNICA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EstructuraEspecificacionTecnica>>> SearchAsync(EstructuraEspecificacionTecnicaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EstructuraEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_ESTRUCTURA_ESPECIFICACION_TECNICA);
}