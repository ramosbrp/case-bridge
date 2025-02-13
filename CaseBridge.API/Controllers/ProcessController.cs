using CaseBridge.Domain.DTO;
using CaseBridge.Domain.Ports;
using Microsoft.AspNetCore.Mvc;

namespace CaseBridge.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProcessController : ControllerBase
{
    private readonly IProcessService _processService;

    public ProcessController(IProcessService processService)
    {
        _processService = processService;
    }

    [HttpPost]
    [Route("api/Process")]
    public async Task<ActionResult> Process([FromBody] CreateProcessDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.ClientEmail))
            return BadRequest(ApiResponse<string>.Fail("Dados inválidos."));

        var result = await _processService.CreateProcessWithClientAsync(dto);

        if (!result.Success)
            return BadRequest(ApiResponse<string>.Fail(result.ErrorMessage));

        return CreatedAtAction(nameof(Process), new { id = result.Data }, ApiResponse<string>.Ok(result.Data, "Processo criado com sucesso"));

    }
}
