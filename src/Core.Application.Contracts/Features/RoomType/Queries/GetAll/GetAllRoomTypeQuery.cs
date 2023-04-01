using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.RoomType.Queries.GetAll
{
    public class GetAllRoomTypeQuery : IRequest<Response<IReadOnlyList<GetAllRoomTypeQueryVm>>>
    {
    }
}
