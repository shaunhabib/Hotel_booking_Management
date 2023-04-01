using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Review.Queries.Get
{
    public class GetReviewQuery : IRequest<Response<GetReviewQueryVm>>
    {
        public int Id { get; set; }
    }
}
