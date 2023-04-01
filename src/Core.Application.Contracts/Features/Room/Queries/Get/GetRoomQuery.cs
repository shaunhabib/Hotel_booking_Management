using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Room.Queries.Get
{
    public class GetRoomQuery : IRequest<Response<GetRoomQueryVm>>
    {
        public int Id { get; set; }
    }
}
