using Core.Application.Contracts.Features.Hotel.Commands.Create;
using Core.Application.Contracts.Features.Hotel.Commands.Delete;
using Core.Application.Contracts.Features.Hotel.Commands.Update;
using Core.Application.Contracts.Features.Hotel.Queries.Get;
using Core.Application.Contracts.Features.Hotel.Queries.GetAll;
using Core.Application.Features.Hotel.Queries.Get;
using Core.Domain.Shared.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Web.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class HotelController : BaseApiController
    {
        [HttpPost]
        //[Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromForm] CreateHotelCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(UpdateHotelCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IReadOnlyList<GetAllHotelQueryVm>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(GetAllHotelQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<GetHotelQueryHandler>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Details(int id)
        {
            var query = new GetHotelQuery { Id = id };
            var response = await Mediator.Send(query);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int Id)
        {
            var query = new DeleteHotelCommand { Id = Id };
            var response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}
