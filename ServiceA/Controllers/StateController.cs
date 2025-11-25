using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace ServiceA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StateController(
    DaprClient daprClient,
    ILogger<StateController> logger) : ControllerBase
{
    [HttpPost("write")]
    public async Task<IActionResult> Write([FromBody] Product product)
    {
        logger.LogInformation("Writing {Product} to state store", product);
        await daprClient.SaveStateAsync("statestore", product.Id.ToString(), product);
        return Ok(product);
    }

    [HttpGet("read")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Read(int id)
    {
        var state = await daprClient.GetStateEntryAsync<Product>("statestore", id.ToString());
        var product = state?.Value;

        if (product is null)
        {
            return NotFound();
        }

        logger.LogInformation("Reading {Product}, corresponding to {Id} from state store", product, id);
        return Ok(product);
    }
}
