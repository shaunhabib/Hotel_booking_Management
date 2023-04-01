using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Booking.Queries.GetAll
{
    public class GetAllBookingQuery : IRequest<Response<IReadOnlyList<GetAllBookingQueryVm>>>
    {
        public int? UserId { get; set; }
        public int? HotelId { get; set; }
        public int? RoomId { get; set; }
    }
}
