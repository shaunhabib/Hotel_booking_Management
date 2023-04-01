using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Booking.Queries.Get
{
    public class GetBookingQuery : IRequest<Response<GetBookingQueryVm>>
    {
        public int Id { get; set; }
    }
}
