using Core.Application.Contracts.Features.Room.Commands.Create;
using Core.Application.Contracts.Features.Room.Commands.Delete;
using Core.Application.Contracts.Features.Room.Commands.Update;
using Core.Application.Contracts.Features.Room.Queries.Get;
using Core.Application.Contracts.Features.Room.Queries.GetAll;
using Core.Domain.Shared.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Web.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class RoomController : BaseApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create(CreateRoomCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(UpdateRoomCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IReadOnlyList<GetAllRoomQueryVm>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllRoomQuery();
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<GetRoomQueryVm>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Details(int Id)
        {
            var query = new GetRoomQuery { Id = Id };
            var response = await Mediator.Send(query);
            return Ok(response);
        }



        [HttpDelete]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int Id)
        {
            var query = new DeleteRoomCommand { Id = Id };
            var response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}
