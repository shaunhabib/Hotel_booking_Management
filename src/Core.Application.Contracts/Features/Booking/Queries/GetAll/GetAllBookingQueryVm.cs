using Core.Application.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Booking.Queries.GetAll
{
    public class GetAllBookingQueryVm
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public RoomVm Room { get; set; }
        public VmSelectList Hotel { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
