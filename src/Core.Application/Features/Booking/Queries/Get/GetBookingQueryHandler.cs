using Core.Application.Contracts.Common;
using Core.Application.Contracts.Features.Booking.Queries.Get;
using Core.Application.Contracts.Features.Booking.Queries.GetAll;
using Core.Application.Contracts.Features.Review.Queries.Get;
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

namespace Core.Application.Features.Booking.Queries.Get
{
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, Response<GetBookingQueryVm>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<GetBookingQueryHandler> _logger;
        private List<String> _validationError;

        public GetBookingQueryHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<GetBookingQueryHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<GetBookingQueryVm>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                #region DB model
                var bookingData = _persistenceUnitOfWork.Booking.AsNoTracking()
                    .Where(x => x.Id == request.Id)
                    .Include(i => i.Hotel)
                    .Include(i => i.Room).ThenInclude(x => x.RoomType)
                    .Select(s => new
                    {
                        s.Id,
                        s.UserId,
                        s.HotelId,
                        s.Hotel,
                        s.Room,
                        s.RoomId,
                        s.CheckInDate,
                        s.CheckOutDate,
                        s.TotalPrice
                    }).FirstOrDefault();
                #endregion


                if (bookingData == null)
                    return Response<GetBookingQueryVm>.Fail("No data found");

                #region Response
                var response = new GetBookingQueryVm
                {
                    Id = bookingData.Id,
                    UserId = bookingData.UserId,
                    CheckInDate = bookingData.CheckInDate,
                    CheckOutDate = bookingData.CheckOutDate,
                    TotalPrice = bookingData.TotalPrice,
                    Room = new RoomVm
                    {
                        Id = bookingData.RoomId,
                        Name = bookingData.Room.RoomType.Name,
                        RoomNumber = bookingData.Room.RoomNumber
                    },
                    Hotel = new VmSelectList
                    {
                        Id = bookingData.HotelId,
                        Name = bookingData.Hotel.Name
                    }
                };
                #endregion

                return Response<GetBookingQueryVm>.Success(response, "Successfully retrieved booking info");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return Response<GetBookingQueryVm>.Fail(_validationError);
            }
        }
    }
}
