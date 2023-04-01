using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Hotel.Queries.Get
{
    public class GetHotelQuery : IRequest<Response<GetHotelQueryVm>>
    {
        public int Id { get; set; }
    }
}
