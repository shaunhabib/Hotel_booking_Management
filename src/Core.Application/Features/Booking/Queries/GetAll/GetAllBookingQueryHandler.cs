using Core.Application.Contracts.Common;
using Core.Application.Contracts.Features.Booking.Queries.GetAll;
using Core.Application.Contracts.Features.Room.Queries.GetAll;
using Core.Application.Contracts.Features.RoomType.Queries.GetAll;
using Core.Application.Extensions;
using Core.Application.Features.RoomType.Queries.GetAll;
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

namespace Core.Application.Features.Booking.Queries.GetAll
{
    public class GetAllBookingQueryHandler : IRequestHandler<GetAllBookingQuery, Response<IReadOnlyList<GetAllBookingQueryVm>>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<GetAllBookingQueryHandler> _logger;
        private List<String> _validationError;

        public GetAllBookingQueryHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<GetAllBookingQueryHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<IReadOnlyList<GetAllBookingQueryVm>>> Handle(GetAllBookingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                #region DB model
                var bookingData = _persistenceUnitOfWork.Booking.AsNoTracking()
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
                    });
                #endregion

                #region filter
                if (request.UserId != null && request.UserId > 0)
                {
                    bookingData = bookingData.Where(x => x.UserId == request.UserId);
                }
                if (request.RoomId != null && request.RoomId > 0)
                {
                    bookingData = bookingData.Where(x => x.RoomId == request.RoomId);
                }
                if (request.HotelId != null && request.HotelId > 0)
                {
                    bookingData = bookingData.Where(x => x.HotelId == request.HotelId);
                }
                #endregion

                #region Response
                var response = bookingData.Select(s => new GetAllBookingQueryVm
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    CheckInDate = s.CheckInDate,
                    CheckOutDate = s.CheckOutDate,
                    TotalPrice = s.TotalPrice,
                    Room = new RoomVm
                    {
                        Id = s.RoomId,
                        Name = s.Room.RoomType.Name,
                        RoomNumber = s.Room.RoomNumber
                    },
                    Hotel = new VmSelectList
                    {
                        Id = s.HotelId,
                        Name = s.Hotel.Name
                    }
                }).OrderByDescending(x => x.Id).ToList();
                #endregion

                return Response<IReadOnlyList<GetAllBookingQueryVm>>.Success(response, "Successfully retrieved booking list");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return Response<IReadOnlyList<GetAllBookingQueryVm>>.Fail(_validationError);
            }
        }
    }
}
