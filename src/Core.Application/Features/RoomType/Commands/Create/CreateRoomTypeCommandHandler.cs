using Core.Application.Contracts.Features.Hotel.Commands.Create;
using Core.Application.Contracts.Features.RoomType.Commands.Create;
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

namespace Core.Application.Features.RoomType.Commands.Create
{
    public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, Response<int>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<CreateRoomTypeCommandHandler> _logger;
        private List<String> _validationError;

        public CreateRoomTypeCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<CreateRoomTypeCommandHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<int>> Handle(CreateRoomTypeCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var newRoomType = new Domain.Persistence.Entities.RoomType
                {
                    Name = command.Name,
                    Description = command.Description
                };
                await _persistenceUnitOfWork.RoomType.AddAsync(newRoomType);
                await _persistenceUnitOfWork.SaveChangesAsync();

                return Response<int>.Success(newRoomType.Id, "Successfully created roomType.");

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
