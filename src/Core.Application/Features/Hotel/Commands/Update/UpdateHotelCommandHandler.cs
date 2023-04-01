using Core.Application.Contracts.Features.Hotel.Commands.Update;
using Core.Application.Extensions;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Persistence.Entities;
using Core.Domain.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Core.Application.Features.Hotel.Commands.Update
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, Response<bool>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<UpdateHotelCommandHandler> _logger;
        private List<String> _validationError;

        public UpdateHotelCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<UpdateHotelCommandHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<bool>> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                #region checking feature given or not 
                if (request.Data.Features == null || !request.Data.Features.Any())
                {
                    string msg = "Hotel features is required";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }
                #endregion

                var exHotel = _persistenceUnitOfWork.Hotel.AsQueryable()
                    .Include(i => i.Features)
                    .FirstOrDefault(x => x.Id == request.Data.Id);

                if (exHotel is null)
                {
                    string msg = "No hotel info found to update";
                    _logger.LogError(msg);
                    return Response<bool>.Fail(msg);
                }

                #region update hotel
                exHotel.Name = request.Data.Name;
                exHotel.Description = request.Data.Description;
                exHotel.Address = request.Data.Address;
                exHotel.City = request.Data.City;
                exHotel.State = request.Data.State;
                exHotel.Country = request.Data.Country;
                exHotel.Phone = request.Data.Phone;
                exHotel.Email = request.Data.Email;
                exHotel.Rating = request.Data.Rating;

                #region Features
                var newFeatures = new List<HotelFeature>();

                foreach (var feature in request.Data.Features)
                {
                    var exFeature = exHotel.Features.FirstOrDefault(x => x.Id == feature.Id);
                    if (exFeature != null)
                    {
                        exFeature.Name = feature.Name;
                        exFeature.Description = feature.Description;
                    }
                    else
                    {
                        newFeatures.Add(new HotelFeature
                        {
                            HotelId = exHotel.Id,
                            Name = feature.Name,
                            Description = feature.Description
                        });
                    }
                }
                if (newFeatures.Any())
                    await _persistenceUnitOfWork.HotelFeature.AddAsync(newFeatures);
                #endregion

                await _persistenceUnitOfWork.Hotel.UpdateAsync(exHotel);
                await _persistenceUnitOfWork.SaveChangesAsync();
                #endregion

                return Response<bool>.Success(true, "Successfully updated hotel.");
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
