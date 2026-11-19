using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesTiposMedioContactoRepository
{
    Task<IEnumerable<EntidadesTiposMedioContacto>> ShowallAsync(decimal COD_ENTIDAD);
    Task<EntidadesTiposMedioContacto?> ShowbyidAsync(decimal COD_ENTIDAD, short NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EntidadesTiposMedioContacto>> SearchAsync(EntidadesTiposMedioContactoSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesTiposMedioContactoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, short NUM_SECUENCIA);
}

public interface IEntidadesTiposMedioContactoService
{
    Task<ApiResponse<IEnumerable<EntidadesTiposMedioContacto>>> GetAllAsync(decimal COD_ENTIDAD);
    Task<ApiResponse<EntidadesTiposMedioContacto>> GetByIdAsync(decimal COD_ENTIDAD, short NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EntidadesTiposMedioContacto>>> SearchAsync(EntidadesTiposMedioContactoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesTiposMedioContactoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, short NUM_SECUENCIA);
}
