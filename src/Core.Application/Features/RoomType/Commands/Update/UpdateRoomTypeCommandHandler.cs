using Core.Application.Contracts.Features.Hotel.Commands.Update;
using Core.Application.Contracts.Features.RoomType.Commands.Update;
using Core.Application.Extensions;
using Core.Application.Features.Hotel.Commands.Update;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Shared.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.RoomType.Commands.Update
{
    public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, Response<bool>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<UpdateRoomTypeCommandHandler> _logger;
        private List<String> _validationError;

        public UpdateRoomTypeCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<UpdateRoomTypeCommandHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<bool>> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exRoomType = _persistenceUnitOfWork.RoomType.AsQueryable().FirstOrDefault(x => x.Id == request.Id);

                if (exRoomType is null)
                {
                    string msg = "No roomType found to update";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }

                #region update hotel
                exRoomType.Name = request.Name;
                exRoomType.Description = request.Description;

                await _persistenceUnitOfWork.RoomType.UpdateAsync(exRoomType);
                await _persistenceUnitOfWork.SaveChangesAsync();
                #endregion

                return Response<bool>.Success(true, "Successfully updated roomType.");
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
