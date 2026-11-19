using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface IHorariosDetalleRepository
{
    Task<IEnumerable<HorariosDetalle>> ShowallAsync();
    Task<HorariosDetalle?> ShowbyidAsync(string COD_HORARIO, int NUM_DIA_SEMANA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<HorariosDetalle>> SearchAsync(HorariosDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(HorariosDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(HorariosDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_HORARIO, int NUM_DIA_SEMANA);
}

public interface IHorariosDetalleService
{
    Task<ApiResponse<IEnumerable<HorariosDetalle>>> GetAllAsync();
    Task<ApiResponse<HorariosDetalle>> GetByIdAsync(string COD_HORARIO, int NUM_DIA_SEMANA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<HorariosDetalle>>> SearchAsync(HorariosDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(HorariosDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(HorariosDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_HORARIO, int NUM_DIA_SEMANA);
}