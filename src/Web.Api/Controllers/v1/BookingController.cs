using Core.Application.Contracts.Features.Booking.Commands.Create;
using Core.Application.Contracts.Features.Booking.Commands.Delete;
using Core.Application.Contracts.Features.Booking.Commands.Update;
using Core.Application.Contracts.Features.Booking.Queries.Get;
using Core.Application.Contracts.Features.Booking.Queries.GetAll;
using Core.Domain.Shared.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Web.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class BookingController : BaseApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create(CreateBookingCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(UpdateBookingCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IReadOnlyList<GetAllBookingQueryVm>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(GetAllBookingQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<GetBookingQueryVm>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Details(int Id)
        {
            var query = new GetBookingQuery { Id = Id };
            var response = await Mediator.Send(query);
            return Ok(response);
        }



        [HttpDelete]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int Id)
        {
            var query = new DeleteBookingCommand { Id = Id };
            var response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}
