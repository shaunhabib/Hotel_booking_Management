using Core.Application.Contracts.Common;
using Core.Application.Contracts.Features.Booking.Queries.Get;
using Core.Application.Contracts.Features.Review.Queries.Get;
using Core.Application.Contracts.Features.Review.Queries.GetAll;
using Core.Application.Contracts.Features.Room.Queries.Get;
using Core.Application.Extensions;
using Core.Application.Features.Review.Queries.GetAll;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Review.Queries.Get
{
    public class GetReviewQueryHandler : IRequestHandler<GetReviewQuery, Response<GetReviewQueryVm>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<GetReviewQueryHandler> _logger;
        private List<String> _validationError;

        public GetReviewQueryHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<GetReviewQueryHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<GetReviewQueryVm>> Handle(GetReviewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                #region DB model
                var reviewData = _persistenceUnitOfWork.Review.AsNoTracking()
                    .Where(x => x.Id == request.Id)
                    .Include(i => i.Hotel)
                    .Select(s => new
                    {
                        s.Id,
                        s.UserId,
                        s.HotelId,
                        s.Hotel,
                        s.Rating,
                        s.Comment
                    }).FirstOrDefault();
                #endregion

                if (reviewData == null)
                    return Response<GetReviewQueryVm>.Fail("No data found");

                #region Response
                var response =  new GetReviewQueryVm
                {
                    Id = reviewData.Id,
                    UserId = reviewData.UserId,
                    Rating = reviewData.Rating,
                    Comment = reviewData.Comment,
                    Hotel = new VmSelectList
                    {
                        Id = reviewData.HotelId,
                        Name = reviewData.Hotel.Name
                    }
                };
                #endregion

                return Response<GetReviewQueryVm>.Success(response, "Successfully retrieved review");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return Response<GetReviewQueryVm>.Fail(_validationError);
            }
        }
    }
}
