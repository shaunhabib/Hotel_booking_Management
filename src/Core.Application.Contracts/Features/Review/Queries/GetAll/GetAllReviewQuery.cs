using Core.Application.Contracts.Features.RoomType.Queries.GetAll;
using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Review.Queries.GetAll
{
    public class GetAllReviewQuery : IRequest<Response<IReadOnlyList<GetAllReviewQueryVm>>>
    {
        public int? UserId { get; set; }
        public int? HotelId { get; set; }
    }
}
