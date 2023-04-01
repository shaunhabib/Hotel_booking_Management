using Core.Application.Contracts.Features.Booking.Commands.Update;
using Core.Application.Contracts.Features.Review.Commands.Update;
using Core.Application.Extensions;
using Core.Application.Features.Review.Commands.Create;
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

namespace Core.Application.Features.Review.Commands.Update
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Response<bool>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<UpdateReviewCommandHandler> _logger;
        private List<String> _validationError;

        public UpdateReviewCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<UpdateReviewCommandHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<bool>> Handle(UpdateReviewCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var hotelRepo = _persistenceUnitOfWork.Hotel.AsQueryable();

                #region hotel checking
                if (!hotelRepo.Any(x => x.Id == command.HotelId))
                {
                    string msg = "No hotel info found";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }
                #endregion


                #region rating checking
                if (command.Rating > 0 && command.Rating < 5)
                {
                    string msg = "Rating should be between 1 to 5";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }
                #endregion

                var exReview = _persistenceUnitOfWork.Review.AsQueryable().FirstOrDefault(x => x.Id == command.Id);

                if (exReview is null)
                {
                    string msg = "No review found to update";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }

                #region update review
                exReview.UserId = command.UserId;
                exReview.HotelId = command.HotelId;
                exReview.Comment = command.Comment;
                exReview.Rating = command.Rating;

                var hotel = hotelRepo.FirstOrDefault(x => x.Id == command.HotelId);
                if (hotel != null)
                {
                    hotel.Rating = command.Rating;
                }

                await _persistenceUnitOfWork.Review.UpdateAsync(exReview);
                await _persistenceUnitOfWork.SaveChangesAsync();
                #endregion

                return Response<bool>.Success(true, "Successfully updated review.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return Response<bool>.Fail(_validationError);
            }
        }
    }
}
