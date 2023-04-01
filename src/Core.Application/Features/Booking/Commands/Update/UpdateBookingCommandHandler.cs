using Core.Application.Contracts.Features.Booking.Commands.Update;
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

namespace Core.Application.Features.Booking.Commands.Update
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, Response<bool>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<UpdateBookingCommandHandler> _logger;
        private List<String> _validationError;

        public UpdateBookingCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<UpdateBookingCommandHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<bool>> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                #region hotel checking
                bool hotel = _persistenceUnitOfWork.Hotel.AsNoTracking().Any(x => x.Id == request.HotelId);
                if (!hotel)
                {
                    string msg = "No hotel info found";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }
                #endregion

                #region room checking
                bool room = _persistenceUnitOfWork.Room.AsNoTracking().Any(x => x.Id == request.RoomId);
                if (!hotel)
                {
                    string msg = "No room info found";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }
                #endregion

                #region price checking
                if (request.TotalPrice <= 0)
                {
                    string msg = "Price should be greater than zero";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }
                #endregion

                var exBooking = _persistenceUnitOfWork.Booking.AsQueryable().FirstOrDefault(x => x.Id == request.Id);

                if (exBooking is null)
                {
                    string msg = "No roomType found to update";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }

                #region update hotel
                exBooking.UserId = request.UserId;
                exBooking.RoomId = request.RoomId;
                exBooking.HotelId = request.HotelId;
                exBooking.CheckInDate = request.CheckInDate;
                exBooking.CheckOutDate = request.CheckOutDate;
                exBooking.TotalPrice = request.TotalPrice;

                await _persistenceUnitOfWork.Booking.UpdateAsync(exBooking);
                await _persistenceUnitOfWork.SaveChangesAsync();
                #endregion

                return Response<bool>.Success(true, "Successfully updated booking.");
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
