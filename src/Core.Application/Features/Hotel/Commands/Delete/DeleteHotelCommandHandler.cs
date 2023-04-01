using Core.Application.Contracts.Features.Hotel.Commands.Delete;
using Core.Application.Extensions;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Application.Features.Hotel.Commands.Delete
{
    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, Response<bool>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<DeleteHotelCommandHandler> _logger;
        private List<String> _validationError;

        public DeleteHotelCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<DeleteHotelCommandHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<bool>> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exHotel = _persistenceUnitOfWork.Hotel.AsQueryable()
                    .Include(i => i.Features)
                    .FirstOrDefault(x => x.Id == request.Id);

                if (exHotel is null)
                {
                    string msg = "No hotel info found to delete";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }

                await _persistenceUnitOfWork.Hotel.DeleteAsync(exHotel);
                await _persistenceUnitOfWork.SaveChangesAsync();
                return Response<bool>.Success(true, "Successfully deleted hotel info.");
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
