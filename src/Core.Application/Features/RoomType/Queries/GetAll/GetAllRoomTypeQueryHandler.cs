using Core.Application.Contracts.Features.Hotel.Queries.GetAll;
using Core.Application.Contracts.Features.RoomType.Queries.GetAll;
using Core.Application.Extensions;
using Core.Application.Features.RoomType.Commands.Create;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Shared.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.RoomType.Queries.GetAll
{
    public class GetAllRoomTypeQueryHandler : IRequestHandler<GetAllRoomTypeQuery, Response<IReadOnlyList<GetAllRoomTypeQueryVm>>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<GetAllRoomTypeQueryHandler> _logger;
        private List<String> _validationError;

        public GetAllRoomTypeQueryHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<GetAllRoomTypeQueryHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<IReadOnlyList<GetAllRoomTypeQueryVm>>> Handle(GetAllRoomTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var RoomTypeData = _persistenceUnitOfWork.RoomType.AsNoTracking()
                    .Select(s => new
                    {
                        s.Id,
                        s.Name,
                        s.Description
                    }).ToList();

                
                var response = RoomTypeData.Select(s => new GetAllRoomTypeQueryVm
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description
                }).ToList();

                return Response<IReadOnlyList<GetAllRoomTypeQueryVm>>.Success(response, "Successfully retrieved roomType list");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return Response<IReadOnlyList<GetAllRoomTypeQueryVm>>.Fail(_validationError);
            }
        }
    }
}
