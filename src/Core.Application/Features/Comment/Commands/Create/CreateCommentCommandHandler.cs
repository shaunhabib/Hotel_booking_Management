using Core.Application.Contracts.Features.Booking.Commands.Create;
using Core.Application.Contracts.Features.Comment.Commands.Create;
using Core.Application.Extensions;
using Core.Application.Features.Booking.Commands.Create;
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

namespace Core.Application.Features.Comment.Commands.Create
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Response<int>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<CreateCommentCommandHandler> _logger;
        private List<String> _validationError;

        public CreateCommentCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<CreateCommentCommandHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<int>> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
        {
            try
            {
                #region hotel checking
                if (!_persistenceUnitOfWork.Hotel.AsNoTracking().Any(x => x.Id == command.HotelId))
                {
                    string msg = "No hotel info found";
                    _logger.LogError(msg);
                    return Response<int>.Fail(msg);
                }
                #endregion

                var newComment = new Domain.Persistence.Entities.Comment
                {
                    HotelId = command.HotelId,
                    Message = command.Message,
                };

                await _persistenceUnitOfWork.Comment.AddAsync(newComment);
                await _persistenceUnitOfWork.SaveChangesAsync();

                return Response<int>.Success(newComment.Id, "Successfully created comment.");

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
