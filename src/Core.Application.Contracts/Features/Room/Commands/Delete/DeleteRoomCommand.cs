using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Room.Commands.Delete
{
    public class DeleteRoomCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }
}
