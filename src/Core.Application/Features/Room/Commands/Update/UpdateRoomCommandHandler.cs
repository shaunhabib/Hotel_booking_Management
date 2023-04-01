using Core.Application.Contracts.Features.Room.Commands.Create;
using Core.Application.Contracts.Features.Room.Commands.Update;
using Core.Application.Extensions;
using Core.Application.Features.Room.Commands.Create;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Core.Application.Features.Room.Commands.Update
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Response<bool>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<UpdateRoomCommandHandler> _logger;
        private List<String> _validationError;

        public UpdateRoomCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<UpdateRoomCommandHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<bool>> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
        {
            try
            {
                #region checking feature given or not 
                if (command.Features == null || !command.Features.Any())
                {
                    string msg = "Room features is required";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }
                #endregion

                #region hotel checking
                bool hotel = _persistenceUnitOfWork.Hotel.AsNoTracking().Any(x => x.Id == command.HotelId);
                if (!hotel)
                {
                    string msg = "No hotel info found";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }
                #endregion

                #region roomType checking
                bool roomType = _persistenceUnitOfWork.RoomType.AsNoTracking().Any(x => x.Id == command.RoomTypeId);

                if (!roomType)
                {
                    string msg = "No roomType found";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }
                #endregion

                var exRoom = _persistenceUnitOfWork.Room.AsQueryable()
                    .Include(i => i.Features)
                    .FirstOrDefault(x => x.Id == command.Id);

                if (exRoom is null)
                {
                    string msg = "No room found to update";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }

                #region update Room
                exRoom.HotelId = command.HotelId;
                exRoom.RoomTypeId = command.RoomTypeId;
                exRoom.RoomNumber = command.RoomNumber;
                exRoom.Capacity = command.Capacity;
                exRoom.Price = command.Price;

                #region Features
                var newFeatures = new List<Domain.Persistence.Entities.RoomFeature>();

                foreach (var feature in command.Features)
                {
                    var exFeature = exRoom.Features.FirstOrDefault(x => x.Id == feature.Id);
                    if (exFeature != null)
                    {
                        exFeature.Name = feature.Name;
                        exFeature.Description = feature.Description;
                    }
                    else
                    {
                        newFeatures.Add(new Domain.Persistence.Entities.RoomFeature
                        {
                            RoomId = exRoom.Id,
                            Name = feature.Name,
                            Description = feature.Description
                        });
                    }
                }
                if(newFeatures.Any())
                    await _persistenceUnitOfWork.RoomFeature.AddAsync(newFeatures);
                #endregion

                await _persistenceUnitOfWork.Room.UpdateAsync(exRoom);
                await _persistenceUnitOfWork.SaveChangesAsync();
                #endregion

                return Response<bool>.Success(true, "Successfully updated room.");
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
