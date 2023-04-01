using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Hotel.Queries.GetAll
{
    public class GetAllHotelQuery : IRequest<Response<IReadOnlyList<GetAllHotelQueryVm>>>
    {
        public int? HotelId { get; set; }
        public int? MinRating { get; set; }
        public int? MaxRating { get; set; }
        public string SearchValue { get; set; }
    }
}
