using Core.Application.Contracts.Common;
using Core.Application.Contracts.Features.Hotel.Queries.GetAll;
using Core.Application.Contracts.Features.Room.Queries.GetAll;
using Core.Application.Extensions;
using Core.Application.Features.Hotel.Queries.GetAll;
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

namespace Core.Application.Features.Room.Queries.GetAll
{
    public class GetAllRoomQueryHandler : IRequestHandler<GetAllRoomQuery, Response<IReadOnlyList<GetAllRoomQueryVm>>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<GetAllRoomQueryHandler> _logger;
        private List<String> _validationError;

        public GetAllRoomQueryHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<GetAllRoomQueryHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<IReadOnlyList<GetAllRoomQueryVm>>> Handle(GetAllRoomQuery request, CancellationToken cancellationToken)
        {
            try
            {
                #region DB model
                var roomData = _persistenceUnitOfWork.Room.AsNoTracking()
                    .Include(i => i.Hotel)
                    .Include(i => i.RoomType)
                    .Select(s => new
                    {
                        s.Id,
                        s.Description,
                        s.HotelId,
                        s.Hotel,
                        s.RoomType,
                        s.Price,
                        s.RoomTypeId,
                        s.Capacity,
                        s.RoomNumber
                    }).ToList();
                #endregion

                #region Response
                var response = roomData.Select(s => new GetAllRoomQueryVm
                {
                    Id = s.Id,
                    Price = s.Price,
                    RoomNumber = s.RoomNumber,
                    Description = s.Description,
                    Capacity = s.Capacity,
                    RoomType = new VmSelectList
                    {
                        Id = s.RoomTypeId,
                        Name = s.RoomType.Name
                    },
                    Hotel = new VmSelectList
                    {
                        Id = s.HotelId,
                        Name = s.Hotel.Name
                    }
                }).ToList();
                #endregion

                return Response<IReadOnlyList<GetAllRoomQueryVm>>.Success(response, "Successfully retrieved room list");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return Response<IReadOnlyList<GetAllRoomQueryVm>>.Fail(_validationError);
            }
        }
    }
}
