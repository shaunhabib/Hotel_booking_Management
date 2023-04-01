using Core.Application.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Review.Queries.Get
{
    public class GetReviewQueryVm
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public VmSelectList Hotel { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
