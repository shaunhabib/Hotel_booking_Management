using Core.Application.Contracts.Features.Booking.Queries.GetAll;
using Core.Application.Contracts.Features.Comment.Queries.Get;
using Core.Application.Contracts.Features.RoomType.Queries.GetAll;
using Core.Application.Extensions;
using Core.Application.Features.Booking.Commands.Update;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Shared.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Comment.Queries.Get
{
    public class GetCommentsByHotelIdQueryHandler : IRequestHandler<GetCommentsByHotelIdQuery, Response<IReadOnlyList<GetCommentsByHotelIdQueryVm>>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<GetCommentsByHotelIdQueryHandler> _logger;
        private List<String> _validationError;

        public GetCommentsByHotelIdQueryHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<GetCommentsByHotelIdQueryHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<IReadOnlyList<GetCommentsByHotelIdQueryVm>>> Handle(GetCommentsByHotelIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var CommentData = _persistenceUnitOfWork.Comment.AsNoTracking()
                    .Where(x => x.HotelId == request.HotelId)
                    .Select(s => new
                    {
                        s.Id,
                        s.Message,
                    }).ToList();

                var response = CommentData.Select(s => new GetCommentsByHotelIdQueryVm
                {
                    Id = s.Id,
                    Message = s.Message
                }).ToList();

                return Response<IReadOnlyList<GetCommentsByHotelIdQueryVm>>.Success(response, "Successfully retrieved comment list");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return Response<IReadOnlyList<GetCommentsByHotelIdQueryVm>>.Fail(_validationError);
            }
        }
    }
}
