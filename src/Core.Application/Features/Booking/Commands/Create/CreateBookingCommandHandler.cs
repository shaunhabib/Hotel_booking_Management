using Core.Application.Contracts.Features.Booking.Commands.Create;
using Core.Application.Contracts.Features.Room.Commands.Create;
using Core.Application.Extensions;
using Core.Application.Features.Room.Commands.Create;
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

namespace Core.Application.Features.Booking.Commands.Create
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Response<int>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<CreateBookingCommandHandler> _logger;
        private List<String> _validationError;

        public CreateBookingCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<CreateBookingCommandHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<int>> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            try
            {
                #region hotel checking
                bool hotel = _persistenceUnitOfWork.Hotel.AsNoTracking().Any(x => x.Id == command.HotelId);
                if (!hotel)
                {
                    string msg = "No hotel info found";
                    _logger.LogError(msg);
                    return Response<int>.Fail(msg);
                }
                #endregion

                #region room checking
                bool room = _persistenceUnitOfWork.Room.AsNoTracking().Any(x => x.Id == command.RoomId);
                if (!hotel)
                {
                    string msg = "No room info found";
                    _logger.LogError(msg);
                    return Response<int>.Fail(msg);
                }
                #endregion

                #region price checking
                if (command.TotalPrice <= 0)
                {
                    string msg = "Price should be greater than zero";
                    _logger.LogError(msg);
                    return Response<int>.Fail(msg);
                }
                #endregion

                var newBooking = new Domain.Persistence.Entities.Booking
                {
                    UserId = command.UserId,
                    RoomId = command.RoomId,
                    HotelId = command.HotelId,
                    CheckInDate = command.CheckInDate,
                    CheckOutDate = command.CheckOutDate,
                    TotalPrice = command.TotalPrice
                };
                await _persistenceUnitOfWork.Booking.AddAsync(newBooking);
                await _persistenceUnitOfWork.SaveChangesAsync();

                return Response<int>.Success(newBooking.Id, "Successfully created booking.");

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
