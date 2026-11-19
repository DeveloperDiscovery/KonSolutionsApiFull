using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IFeriadosRepository
{
    Task<IEnumerable<Feriados>> ShowallAsync();
    Task<Feriados?> ShowbyidAsync(DateTime FEC_FERIADO);
    Task<IEnumerable<Feriados>> SearchAsync(FeriadosSearchDto dto);
    Task<WriteResult> InsertAsync(FeriadosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(FeriadosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(DateTime FEC_FERIADO);
}

public interface IFeriadosService
{
    Task<ApiResponse<IEnumerable<Feriados>>> GetAllAsync();
    Task<ApiResponse<Feriados>> GetByIdAsync(DateTime FEC_FERIADO);
    Task<ApiResponse<IEnumerable<Feriados>>> SearchAsync(FeriadosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(FeriadosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(FeriadosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(DateTime FEC_FERIADO);
}
