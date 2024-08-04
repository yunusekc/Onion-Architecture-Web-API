using MediatR;
using Microsoft.AspNetCore.Mvc;
using Onion.Cqrs.Application.Features.Commands.CreateProduct;
using Onion.Cqrs.Application.Features.Commands.DeleteCustomer;
using Onion.Cqrs.Application.Features.Commands.UpdateCustomer;
using Onion.Cqrs.Application.Features.Queries.GetAllCustomers;
using Onion.Cqrs.Application.Features.Queries.GetCustomerWithId;
using Onion.Cqrs.Application.Wrapper;
using Onion.Cqrs.Domain;

namespace Onion.Cqrs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetlAllCustomersQuery request)
        {
            try
            {
                return Ok(await mediator.Send(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById([FromQuery] GetCustomerWithId request)
        {
            try
            {
                return Ok(await mediator.Send(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("Create")]
        public async Task<IActionResult> Create([FromQuery] CreateCustomerCmd request)
        {
            try
            {
                return Ok(await mediator.Send(request));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteCustomerCmd req)
        {
            try
            {
                return Ok(await mediator.Send(req));
            }
            catch(Exception ex)
            {
                return BadRequest(new ServiceResponse<Guid>(req.Id) { IsSuccess = false, Message = ex.Message, Value = req.Id });
            }

        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromQuery] UpdateCustomerCmd req)
        {
            try
            {
                return Ok(await mediator.Send(req));
            }
            catch (Exception ex)
            {
                return BadRequest(req);
            }
        }
    }
}
