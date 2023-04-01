using Core.Application.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Room.Queries.GetAll
{
    public class GetAllRoomQueryVm
    {
        public int Id { get; set; }
        public VmSelectList RoomType { get; set; }
        public VmSelectList Hotel { get; set; }
        public int RoomNumber { get; set; }
        public float Capacity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
