using Core.Application.Contracts.Features.Comment.Commands.Create;
using Core.Application.Contracts.Features.Comment.Queries.Get;
using Core.Application.Contracts.Features.Review.Commands.Create;
using Core.Application.Contracts.Features.Review.Queries.GetAll;
using Core.Domain.Shared.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Web.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CommentController : BaseApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create(CreateCommentCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IReadOnlyList<GetCommentsByHotelIdQueryVm>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCommentsByHotelId(int hotelId)
        {
            var query = new GetCommentsByHotelIdQuery { HotelId = hotelId };
            var response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}
