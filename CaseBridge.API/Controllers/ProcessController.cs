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

    [HttpPost(Name = "Process")]
    public Task<ActionResult> Post([FromBody] CreateProcessDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult<ActionResult>(StatusCode(500, new ApiResponse<string>(false, "Dados de entrada inválidos.")));
            }

            _processService.CreateProcessWithClientAsync(dto);
            var mensagem = new ApiResponse<string>(true, "Contrato cadastrado com sucesso!");

            return Task.FromResult<ActionResult>(CreatedAtAction("Post", mensagem));
        }
        catch (Exception ex)
        {
            return Task.FromResult<ActionResult>(StatusCode(500, new ApiResponse<string>(false, ex.Message)));
        }

    }
}
