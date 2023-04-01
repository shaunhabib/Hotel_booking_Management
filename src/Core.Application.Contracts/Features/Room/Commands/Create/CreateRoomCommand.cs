using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Room.Commands.Create
{
    public class CreateRoomCommand : IRequest<Response<int>>
    {
        public int RoomTypeId { get; set; }
        public int HotelId { get; set; }
        public int RoomNumber { get; set; }
        public float Capacity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<CreateRoomFeatureCommand> Features { get; set; }
    }
}
