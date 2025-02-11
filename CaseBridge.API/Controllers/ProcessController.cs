using Microsoft.AspNetCore.Mvc;

namespace CaseBridge.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProcessController : ControllerBase
{


    public ProcessController()
    {
    }

    [HttpGet(Name = "Process")]
    public Task<ActionResult> Get()
    {
        var mensagem = "ok";
        return Task.FromResult<ActionResult>(Ok(mensagem));

    }
}
