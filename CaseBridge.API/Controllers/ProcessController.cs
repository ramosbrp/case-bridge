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
        try
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(500, new ApiResponse<string>(false, "Dados de entrada inválidos."));
            }

            await _processService.CreateProcessWithClientAsync(dto);
            var mensagem = new ApiResponse<string>(true, "Contrato cadastrado com sucesso!");

            return CreatedAtAction("Post", mensagem);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<string>(false, ex.Message));
        }

    }
}
