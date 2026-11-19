using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposMedioContactoRepository
{
    Task<IEnumerable<TiposMedioContacto>> ShowallAsync();
    Task<TiposMedioContacto?> ShowbyidAsync(int COD_TIPO_MEDIO_CONTACTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposMedioContacto>> SearchAsync(TiposMedioContactoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposMedioContactoInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposMedioContactoUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_TIPO_MEDIO_CONTACTO);
}

public interface ITiposMedioContactoService
{
    Task<ApiResponse<IEnumerable<TiposMedioContacto>>> GetAllAsync();
    Task<ApiResponse<TiposMedioContacto>> GetByIdAsync(int COD_TIPO_MEDIO_CONTACTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposMedioContacto>>> SearchAsync(TiposMedioContactoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposMedioContactoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposMedioContactoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_TIPO_MEDIO_CONTACTO);
}