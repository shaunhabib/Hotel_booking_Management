using Core.Application.Contracts.Common;
using Core.Application.Contracts.Features.Booking.Queries.GetAll;
using Core.Application.Contracts.Features.Review.Queries.GetAll;
using Core.Application.Extensions;
using Core.Application.Features.Booking.Queries.GetAll;
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

namespace Core.Application.Features.Review.Queries.GetAll
{
    public class GetAllReviewQueryHandler : IRequestHandler<GetAllReviewQuery, Response<IReadOnlyList<GetAllReviewQueryVm>>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<GetAllReviewQueryHandler> _logger;
        private List<String> _validationError;

        public GetAllReviewQueryHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<GetAllReviewQueryHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<IReadOnlyList<GetAllReviewQueryVm>>> Handle(GetAllReviewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                #region DB model
                var reviewData = _persistenceUnitOfWork.Review.AsNoTracking()
                    .Include(i => i.Hotel)
                    .Select(s => new
                    {
                        s.Id,
                        s.UserId,
                        s.HotelId,
                        s.Hotel,
                        s.Rating,
                        s.Comment
                    });
                #endregion

                #region filter
                if (request.UserId != null && request.UserId > 0)
                {
                    reviewData = reviewData.Where(x => x.UserId == request.UserId);
                }
                if (request.HotelId != null && request.HotelId > 0)
                {
                    reviewData = reviewData.Where(x => x.HotelId == request.HotelId);
                }
                #endregion

                #region Response
                var response = reviewData.Select(s => new GetAllReviewQueryVm
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    Rating = s.Rating,
                    Comment = s.Comment,
                    Hotel = new VmSelectList
                    {
                        Id = s.HotelId,
                        Name = s.Hotel.Name
                    }
                }).ToList();
                #endregion

                return Response<IReadOnlyList<GetAllReviewQueryVm>>.Success(response, "Successfully retrieved review list");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return Response<IReadOnlyList<GetAllReviewQueryVm>>.Fail(_validationError);
            }
        }
    }
}
