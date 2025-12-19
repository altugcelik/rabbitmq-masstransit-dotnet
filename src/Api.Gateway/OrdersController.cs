using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Commands;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;


    public OrdersController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }


    [HttpPost]
    public async Task<IActionResult> Create(decimal amount)
    {
        var orderId = Guid.NewGuid();


        await _publishEndpoint.Publish(new CreateOrderCommand(orderId, amount));


        return Accepted(new { orderId });
    }
}