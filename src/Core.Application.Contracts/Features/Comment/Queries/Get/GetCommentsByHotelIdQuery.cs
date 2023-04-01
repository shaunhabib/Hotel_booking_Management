using Core.Application.Contracts.Features.Booking.Queries.GetAll;
using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Comment.Queries.Get
{
    public class GetCommentsByHotelIdQuery : IRequest<Response<IReadOnlyList<GetCommentsByHotelIdQueryVm>>>
    {
        public int HotelId { get; set; }
    }
}
