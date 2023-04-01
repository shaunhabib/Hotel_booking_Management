using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.RoomType.Queries.Get
{
    public class GetRoomTypeQuery : IRequest<Response<GetRoomTypeQueryVm>>
    {
        public int Id { get; set; }
    }
}
