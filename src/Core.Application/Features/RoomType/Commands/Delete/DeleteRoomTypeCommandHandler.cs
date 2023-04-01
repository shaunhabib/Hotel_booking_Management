using Core.Application.Contracts.Features.Hotel.Commands.Delete;
using Core.Application.Contracts.Features.RoomType.Commands.Delete;
using Core.Application.Extensions;
using Core.Application.Features.Hotel.Commands.Delete;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Shared.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.RoomType.Commands.Delete
{
    public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, Response<bool>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<DeleteRoomTypeCommandHandler> _logger;
        private List<String> _validationError;

        public DeleteRoomTypeCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<DeleteRoomTypeCommandHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<bool>> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                #region checking is this roomtype used or not in a room 
                bool isUsed = _persistenceUnitOfWork.Room.AsNoTracking().Any(x => x.RoomTypeId == request.Id);
                if (isUsed)
                {
                    string msg = "This roomtype is already used on room. Delete the room first";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }
                #endregion

                var exRoomType = _persistenceUnitOfWork.RoomType.AsQueryable().FirstOrDefault(x => x.Id == request.Id);

                if (exRoomType is null)
                {
                    string msg = "No roomType found to delete";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }

                await _persistenceUnitOfWork.RoomType.DeleteAsync(exRoomType);
                await _persistenceUnitOfWork.SaveChangesAsync();

                return Response<bool>.Success(true, "Successfully deleted roomType.");
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
