using Core.Application.Contracts.Features.Hotel.Commands.Create;
using Core.Application.Contracts.Features.Room.Commands.Create;
using Core.Application.Extensions;
using Core.Application.Features.Hotel.Commands.Create;
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

namespace Core.Application.Features.Room.Commands.Create
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Response<int>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<CreateRoomCommandHandler> _logger;
        private List<String> _validationError;

        public CreateRoomCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<CreateRoomCommandHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<int>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
        {
            try
            {
                #region checking feature given or not 
                if (command.Features == null || !command.Features.Any())
                {
                    string msg = "Room features is required";
                    _logger.LogError(msg);
                    return Response<int>.Fail(msg);
                }
                #endregion

                #region hotel checking
                bool hotel = _persistenceUnitOfWork.Hotel.AsNoTracking().Any(x => x.Id == command.HotelId);
                if (!hotel)
                {
                    string msg = "No hotel info found";
                    _logger.LogError(msg);
                    return Response<int>.Fail(msg);
                }
                #endregion

                #region roomType checking
                bool roomType = _persistenceUnitOfWork.RoomType.AsNoTracking().Any(x => x.Id == command.RoomTypeId);

                if (!roomType)
                {
                    string msg = "No roomType found";
                    _logger.LogError(msg);
                    return Response<int>.Fail(msg);
                }
                #endregion

                var newRoom = new Domain.Persistence.Entities.Room
                {
                    HotelId = command.HotelId,
                    Description = command.Description,
                    RoomNumber = command.RoomNumber,
                    RoomTypeId = command.RoomTypeId,
                    Capacity = command.Capacity,
                    Price = command.Price,
                    Features = command.Features.Select(s => new Domain.Persistence.Entities.RoomFeature
                    {
                        Name = s.Name,
                        Description = s.Description
                    }).ToList()
                };
                await _persistenceUnitOfWork.Room.AddAsync(newRoom);
                await _persistenceUnitOfWork.SaveChangesAsync();

                return Response<int>.Success(newRoom.Id, "Successfully created room.");

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
