using Core.Application.Contracts.Features.Booking.Commands.Delete;
using Core.Application.Contracts.Features.RoomType.Commands.Delete;
using Core.Application.Extensions;
using Core.Application.Features.RoomType.Commands.Delete;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Shared.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Booking.Commands.Delete
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, Response<bool>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<DeleteBookingCommandHandler> _logger;
        private List<String> _validationError;

        public DeleteBookingCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<DeleteBookingCommandHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<bool>> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                var exBooking = _persistenceUnitOfWork.Booking.AsQueryable().FirstOrDefault(x => x.Id == request.Id);

                if (exBooking is null)
                {
                    string msg = "No booking found to delete";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }

                await _persistenceUnitOfWork.Booking.DeleteAsync(exBooking);
                await _persistenceUnitOfWork.SaveChangesAsync();

                return Response<bool>.Success(true, "Successfully deleted booking.");
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
