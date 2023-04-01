using Core.Application.Contracts.Features.Hotel.Commands.Delete;
using Core.Application.Contracts.Features.Room.Commands.Delete;
using Core.Application.Extensions;
using Core.Application.Features.Hotel.Commands.Delete;
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

namespace Core.Application.Features.Room.Commands.Delete
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, Response<bool>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<DeleteRoomCommandHandler> _logger;
        private List<String> _validationError;

        public DeleteRoomCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<DeleteRoomCommandHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<bool>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exRoom = _persistenceUnitOfWork.Room.AsQueryable()
                    .Include(i => i.Features)
                    .FirstOrDefault(x => x.Id == request.Id);

                if (exRoom is null)
                {
                    string msg = "No room found to delete";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }

                await _persistenceUnitOfWork.Room.DeleteAsync(exRoom);
                await _persistenceUnitOfWork.SaveChangesAsync();

                return Response<bool>.Success(true, "Successfully deleted room.");
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
