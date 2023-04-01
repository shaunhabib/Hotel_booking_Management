using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Room.Commands.Update
{
    public class UpdateRoomCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public int HotelId { get; set; }
        public int RoomNumber { get; set; }
        public float Capacity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<UpdateRoomFeatureCommand> Features { get; set; }
    }
}
