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

    [HttpGet(Name = "Process")]
    public Task<ActionResult> Post()
    {
        try
        {
            _processService.CreateProcessAsync("ok");
            var mensagem = "ok";
            return Task.FromResult<ActionResult>(Ok(mensagem));
        }
        catch (Exception)
        {

            throw;
        }

    }
}
