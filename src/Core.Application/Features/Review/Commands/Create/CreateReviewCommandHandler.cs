using Core.Application.Contracts.Features.Booking.Commands.Create;
using Core.Application.Contracts.Features.Review.Commands.Create;
using Core.Application.Extensions;
using Core.Application.Features.Booking.Commands.Create;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Shared.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Core.Application.Features.Review.Commands.Create
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Response<int>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<CreateReviewCommandHandler> _logger;
        private List<String> _validationError;

        public CreateReviewCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<CreateReviewCommandHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<int>> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var hotelRepo = _persistenceUnitOfWork.Hotel.AsQueryable();

                #region hotel checking
                if (!hotelRepo.Any(x => x.Id == command.HotelId))
                {
                    string msg = "No hotel info found";
                    _logger.LogError(msg);
                    return Response<int>.Fail(msg);
                }
                #endregion

                #region rating checking
                if (command.Rating < 0 || command.Rating > 5)
                {
                    string msg = "Rating should be between 1 to 5";
                    _logger.LogError(msg);
                    return Response<int>.Fail(msg);
                }
                #endregion

                var newReview = new Domain.Persistence.Entities.Review
                {
                    UserId = command.UserId,
                    HotelId = command.HotelId,
                    Rating = command.Rating,
                    Comment = command.Comment
                };

                var hotel = hotelRepo.FirstOrDefault(x => x.Id == command.HotelId);
                if (hotel != null)
                {
                    hotel.Rating = command.Rating;
                }

                await _persistenceUnitOfWork.Review.AddAsync(newReview);
                await _persistenceUnitOfWork.SaveChangesAsync();

                return Response<int>.Success(newReview.Id, "Successfully created review.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return Response<int>.Fail(_validationError);
            }
        }
    }
}
