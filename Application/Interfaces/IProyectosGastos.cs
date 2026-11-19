using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosGastosRepository
{
    Task<IEnumerable<ProyectosGastos>> ShowallAsync();
    Task<ProyectosGastos?> ShowbyidAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosGastos>> SearchAsync(ProyectosGastosSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosGastosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProyectosGastosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
}

public interface IProyectosGastosService
{
    Task<ApiResponse<IEnumerable<ProyectosGastos>>> GetAllAsync();
    Task<ApiResponse<ProyectosGastos>> GetByIdAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosGastos>>> SearchAsync(ProyectosGastosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosGastosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosGastosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
}