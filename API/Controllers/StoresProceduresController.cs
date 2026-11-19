using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Developer;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/developer/stores-procedures")]
[ExigePermiso("DEVELOPER.STORES_PROCEDURES")]
public class StoresProceduresController : BaseController
{
    private readonly IStoresProceduresService _service;
    public StoresProceduresController(IStoresProceduresService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] StoresProceduresSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

}