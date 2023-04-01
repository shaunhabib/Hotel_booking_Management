using Core.Application.Contracts.Features.Review.Commands.Delete;
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

namespace Core.Application.Features.Review.Commands.Delete
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Response<bool>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<DeleteReviewCommandHandler> _logger;
        private List<String> _validationError;

        public DeleteReviewCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<DeleteReviewCommandHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<bool>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exReview = _persistenceUnitOfWork.Review.AsQueryable().FirstOrDefault(x => x.Id == request.Id);

                if (exReview is null)
                {
                    string msg = "No review found to delete";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }

                await _persistenceUnitOfWork.Review.DeleteAsync(exReview);
                await _persistenceUnitOfWork.SaveChangesAsync();

                return Response<bool>.Success(true, "Successfully deleted review.");
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
