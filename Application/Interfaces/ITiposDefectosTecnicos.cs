using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Controlcalidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposDefectosTecnicosRepository
{
    Task<IEnumerable<TiposDefectosTecnicos>> ShowallAsync(string COD_CLASE_DEFECTO_TECNICO);
    Task<TiposDefectosTecnicos?> ShowbyidAsync(string COD_CLASE_DEFECTO_TECNICO, string COD_TIPO_DEFECTO_TECNICO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposDefectosTecnicos>> SearchAsync(TiposDefectosTecnicosSearchDto dto);
    Task<WriteResult> InsertAsync(TiposDefectosTecnicosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TiposDefectosTecnicosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CLASE_DEFECTO_TECNICO, string COD_TIPO_DEFECTO_TECNICO);
}

public interface ITiposDefectosTecnicosService
{
    Task<ApiResponse<IEnumerable<TiposDefectosTecnicos>>> GetAllAsync(string COD_CLASE_DEFECTO_TECNICO);
    Task<ApiResponse<TiposDefectosTecnicos>> GetByIdAsync(string COD_CLASE_DEFECTO_TECNICO, string COD_TIPO_DEFECTO_TECNICO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposDefectosTecnicos>>> SearchAsync(TiposDefectosTecnicosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposDefectosTecnicosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposDefectosTecnicosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_DEFECTO_TECNICO, string COD_TIPO_DEFECTO_TECNICO);
}