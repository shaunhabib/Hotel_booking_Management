using Core.Application.Contracts.Features.RoomType.Queries.Get;
using Core.Application.Contracts.Features.RoomType.Queries.GetAll;
using Core.Application.Extensions;
using Core.Application.Features.RoomType.Queries.GetAll;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Shared.Wrappers;
using LinqToDB;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.RoomType.Queries.Get
{
    public class GetRoomTypeQueryHandler : IRequestHandler<GetRoomTypeQuery, Response<GetRoomTypeQueryVm>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<GetRoomTypeQueryHandler> _logger;
        private List<String> _validationError;

        public GetRoomTypeQueryHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<GetRoomTypeQueryHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<GetRoomTypeQueryVm>> Handle(GetRoomTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var RoomTypeData = _persistenceUnitOfWork.RoomType.AsNoTracking()
                    .Where(x => x.Id == request.Id)
                    .Select(s => new
                    {
                        s.Id,
                        s.Name,
                        s.Description
                    }).FirstOrDefault();

                if (RoomTypeData == null)
                    return Response<GetRoomTypeQueryVm>.Fail("No data found");

                var response = new GetRoomTypeQueryVm
                {
                    Id = RoomTypeData.Id,
                    Name = RoomTypeData.Name,
                    Description = RoomTypeData.Description
                };

                return Response<GetRoomTypeQueryVm>.Success(response, "Successfully retrieved roomType info");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return Response<GetRoomTypeQueryVm>.Fail(_validationError);
            }
        }
    }
}
