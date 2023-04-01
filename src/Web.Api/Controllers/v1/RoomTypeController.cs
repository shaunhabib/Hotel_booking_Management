using Core.Application.Contracts.Features.Room.Commands.Create;
using Core.Application.Contracts.Features.Room.Commands.Delete;
using Core.Application.Contracts.Features.Room.Commands.Update;
using Core.Application.Contracts.Features.Room.Queries.Get;
using Core.Application.Contracts.Features.Room.Queries.GetAll;
using Core.Application.Contracts.Features.RoomType.Commands.Create;
using Core.Application.Contracts.Features.RoomType.Commands.Delete;
using Core.Application.Contracts.Features.RoomType.Commands.Update;
using Core.Application.Contracts.Features.RoomType.Queries.Get;
using Core.Application.Contracts.Features.RoomType.Queries.GetAll;
using Core.Domain.Shared.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Web.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class RoomTypeController : BaseApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create(CreateRoomTypeCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(UpdateRoomTypeCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IReadOnlyList<GetAllRoomTypeQueryVm>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllRoomTypeQuery();
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<GetRoomTypeQueryVm>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Details(int Id)
        {
            var query = new GetRoomTypeQuery { Id = Id };
            var response = await Mediator.Send(query);
            return Ok(response);
        }



        [HttpDelete]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int Id)
        {
            var query = new DeleteRoomTypeCommand { Id = Id };
            var response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}
