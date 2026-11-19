using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProyectosGastosAdicionalesRepository
{
    Task<IEnumerable<ProyectosGastosAdicionales>> ShowallAsync();
    Task<ProyectosGastosAdicionales?> ShowbyidAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProyectosGastosAdicionales>> SearchAsync(ProyectosGastosAdicionalesSearchDto dto);
    Task<WriteResult> InsertAsync(ProyectosGastosAdicionalesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProyectosGastosAdicionalesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
}

public interface IProyectosGastosAdicionalesService
{
    Task<ApiResponse<IEnumerable<ProyectosGastosAdicionales>>> GetAllAsync();
    Task<ApiResponse<ProyectosGastosAdicionales>> GetByIdAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProyectosGastosAdicionales>>> SearchAsync(ProyectosGastosAdicionalesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProyectosGastosAdicionalesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProyectosGastosAdicionalesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROYECTO, int NUM_SECUENCIA);
}