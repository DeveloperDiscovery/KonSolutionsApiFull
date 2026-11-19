using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProgramacionEntregaImplementosPersonalDetalleRepository
{
    Task<IEnumerable<ProgramacionEntregaImplementosPersonalDetalle>> ShowallAsync();
    Task<ProgramacionEntregaImplementosPersonalDetalle?> ShowbyidAsync(int NUM_ENTREGA_IMPLEMENTOS, decimal COD_ENTIDAD_PERSONAL, string COD_PRODUCTO_IMPLEMENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProgramacionEntregaImplementosPersonalDetalle>> SearchAsync(ProgramacionEntregaImplementosPersonalDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(ProgramacionEntregaImplementosPersonalDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProgramacionEntregaImplementosPersonalDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_ENTREGA_IMPLEMENTOS, decimal COD_ENTIDAD_PERSONAL, string COD_PRODUCTO_IMPLEMENTO);
}

public interface IProgramacionEntregaImplementosPersonalDetalleService
{
    Task<ApiResponse<IEnumerable<ProgramacionEntregaImplementosPersonalDetalle>>> GetAllAsync();
    Task<ApiResponse<ProgramacionEntregaImplementosPersonalDetalle>> GetByIdAsync(int NUM_ENTREGA_IMPLEMENTOS, decimal COD_ENTIDAD_PERSONAL, string COD_PRODUCTO_IMPLEMENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProgramacionEntregaImplementosPersonalDetalle>>> SearchAsync(ProgramacionEntregaImplementosPersonalDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProgramacionEntregaImplementosPersonalDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProgramacionEntregaImplementosPersonalDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_ENTREGA_IMPLEMENTOS, decimal COD_ENTIDAD_PERSONAL, string COD_PRODUCTO_IMPLEMENTO);
}