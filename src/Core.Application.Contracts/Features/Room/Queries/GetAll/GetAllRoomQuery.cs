using Core.Application.Contracts.Features.Hotel.Queries.GetAll;
using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Room.Queries.GetAll
{
    public class GetAllRoomQuery : IRequest<Response<IReadOnlyList<GetAllRoomQueryVm>>>
    {
    }
}
