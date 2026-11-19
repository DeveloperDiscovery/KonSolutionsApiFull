using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IUnidadesMedidaEquivalenciasRepository
{
    Task<IEnumerable<UnidadesMedidaEquivalencias>> ShowallAsync();
    Task<UnidadesMedidaEquivalencias?> ShowbyidAsync(string COD_UNIDAD_MEDIDA, string COD_UNIDAD_MEDIDA_EQUIVALENTE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<UnidadesMedidaEquivalencias>> SearchAsync(UnidadesMedidaEquivalenciasSearchDto dto);
    Task<IEnumerable<UnidadesMedidaEquivalencias>> GetByTipoAsync(string COD_TIPO_UNIDAD_MEDIDA);
    Task<WriteResult> UpsertAsync(UnidadesMedidaEquivalenciasInsertDto dto);
    Task<WriteResult> InsertAsync(UnidadesMedidaEquivalenciasInsertDto dto);
    Task<WriteResult> UpdateAsync(UnidadesMedidaEquivalenciasUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_UNIDAD_MEDIDA, string COD_UNIDAD_MEDIDA_EQUIVALENTE);
    Task<WriteResult> CargarEstandarAsync(string COD_TIPO_UNIDAD_MEDIDA);
}

public interface IUnidadesMedidaEquivalenciasService
{
    Task<ApiResponse<IEnumerable<UnidadesMedidaEquivalencias>>> GetAllAsync();
    Task<ApiResponse<UnidadesMedidaEquivalencias>> GetByIdAsync(string COD_UNIDAD_MEDIDA, string COD_UNIDAD_MEDIDA_EQUIVALENTE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<UnidadesMedidaEquivalencias>>> SearchAsync(UnidadesMedidaEquivalenciasSearchDto dto);
    Task<ApiResponse<IEnumerable<UnidadesMedidaEquivalencias>>> GetByTipoAsync(string COD_TIPO_UNIDAD_MEDIDA);
    Task<ApiResponse<WriteResult>> UpsertAsync(UnidadesMedidaEquivalenciasInsertDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(UnidadesMedidaEquivalenciasInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(UnidadesMedidaEquivalenciasUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_UNIDAD_MEDIDA, string COD_UNIDAD_MEDIDA_EQUIVALENTE);
    Task<ApiResponse<WriteResult>> CargarEstandarAsync(string COD_TIPO_UNIDAD_MEDIDA);
}