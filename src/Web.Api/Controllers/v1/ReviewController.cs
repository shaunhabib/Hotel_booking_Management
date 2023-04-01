using Core.Application.Contracts.Features.Booking.Commands.Create;
using Core.Application.Contracts.Features.Booking.Commands.Delete;
using Core.Application.Contracts.Features.Booking.Commands.Update;
using Core.Application.Contracts.Features.Booking.Queries.Get;
using Core.Application.Contracts.Features.Booking.Queries.GetAll;
using Core.Application.Contracts.Features.Review.Commands.Create;
using Core.Application.Contracts.Features.Review.Commands.Delete;
using Core.Application.Contracts.Features.Review.Commands.Update;
using Core.Application.Contracts.Features.Review.Queries.Get;
using Core.Application.Contracts.Features.Review.Queries.GetAll;
using Core.Domain.Shared.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Web.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ReviewController : BaseApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create(CreateReviewCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(UpdateReviewCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IReadOnlyList<GetAllReviewQueryVm>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(GetAllReviewQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<GetReviewQueryVm>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Details(int Id)
        {
            var query = new GetReviewQuery { Id = Id };
            var response = await Mediator.Send(query);
            return Ok(response);
        }



        [HttpDelete]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int Id)
        {
            var query = new DeleteReviewCommand { Id = Id };
            var response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}
