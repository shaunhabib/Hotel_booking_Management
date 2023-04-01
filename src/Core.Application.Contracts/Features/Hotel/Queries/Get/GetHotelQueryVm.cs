using Core.Application.Contracts.Common;
using Core.Application.Contracts.Features.Room.Queries.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Hotel.Queries.Get
{
    public class GetHotelQueryVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Rating { get; set; }
        public List<GetHotelFeatureQueryVm> Features { get; set; }
        public List<GetRoomQueryVm> Rooms { get; set; }
        public List<ImageVm> Images { get; set; }
    }
}
