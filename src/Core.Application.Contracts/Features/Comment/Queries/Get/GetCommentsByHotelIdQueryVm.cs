using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Comment.Queries.Get
{
    public class GetCommentsByHotelIdQueryVm
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
