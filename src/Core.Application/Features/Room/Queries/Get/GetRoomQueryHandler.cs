using Core.Application.Contracts.Common;
using Core.Application.Contracts.Features.Hotel.Queries.Get;
using Core.Application.Contracts.Features.Room.Queries.Get;
using Core.Application.Extensions;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Application.Features.Room.Queries.Get
{
    public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, Response<GetRoomQueryVm>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<GetRoomQueryHandler> _logger;
        private List<String> _validationError;

        public GetRoomQueryHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<GetRoomQueryHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<GetRoomQueryVm>> Handle(GetRoomQuery request, CancellationToken cancellationToken)
        {
            try
            {
                #region DB model
                var roomData = _persistenceUnitOfWork.Room.AsNoTracking()
                    .Where(x => x.Id == request.Id)
                    .Include(i => i.Hotel)
                    .Include(i => i.RoomType)
                    .Include(i => i.Features)
                    .Select(s => new
                    {
                        s.Id,
                        s.Description,
                        s.HotelId,
                        s.Hotel,
                        s.RoomType,
                        s.Price,
                        s.Features,
                        s.RoomTypeId,
                        s.Capacity,
                        s.RoomNumber
                    }).FirstOrDefault();
                #endregion

                if (roomData == null)
                    return Response<GetRoomQueryVm>.Fail("No data found");

                #region Response
                var response = new GetRoomQueryVm
                {
                    Id = roomData.Id,
                    Price = roomData.Price,
                    RoomNumber = roomData.RoomNumber,
                    Description = roomData.Description,
                    Capacity = roomData.Capacity,
                    RoomType = new VmSelectList
                    {
                        Id = roomData.RoomTypeId,
                        Name = roomData.RoomType.Name
                    },
                    Hotel = new VmSelectList
                    {
                        Id = roomData.HotelId,
                        Name = roomData.Hotel.Name
                    },
                    Features = roomData.Features.Select(s => new GetRoomFeatureVm
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description
                    }).ToList()
                };
                #endregion

                return Response<GetRoomQueryVm>.Success(response, "Successfully retrieved room list");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return Response<GetRoomQueryVm>.Fail(_validationError);
            }
        }
    }
}
