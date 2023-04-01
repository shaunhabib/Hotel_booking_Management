using Core.Application.Contracts.Features.Hotel.Commands.Create;
using Core.Application.Extensions;
using Core.Application.Services;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Shared.Contacts;
using Core.Domain.Shared.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Features.Hotel.Commands.Create
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Response<int>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ImageService _imageService;
        private readonly ILogger<CreateHotelCommandHandler> _logger;
        private List<String> _validationError;

        public CreateHotelCommandHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<CreateHotelCommandHandler> logger, ImageService imageService)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
            _imageService = imageService;
        }
        #endregion
        public async Task<Response<int>> Handle(CreateHotelCommand command, CancellationToken cancellationToken)
        {
            try
            {
                #region checking features are given or not 
                if (command.Data.Features == null || !command.Data.Features.Any())
                {
                    string msg = "Hotel features is required";
                    _logger.LogError(msg);
                    return Response<int>.Fail(msg);
                }
                #endregion

                #region checking images are give or not
                if (command.Images == null || !command.Images.Any())
                {
                    string msg = "Images is required";
                    _logger.LogError(msg);
                    return Response<int>.Fail(msg);
                }
                #endregion

                await _persistenceUnitOfWork.BeginTranscationAsync();

                #region Hotel 
                var newHotel = new Domain.Persistence.Entities.Hotel
                {
                    Name = command.Data.Name,
                    Description = command.Data.Description,
                    Address = command.Data.Address,
                    City = command.Data.City,
                    State = command.Data.State,
                    Country = command.Data.Country,
                    Phone = command.Data.Phone,
                    Email = command.Data.Email,
                    Rating = command.Data.Rating,
                    Features = command.Data.Features == null ? null : command.Data.Features.Select(s => new Domain.Persistence.Entities.HotelFeature
                    {
                        Name = s.Name,
                        Description = s.Description
                    }).ToList()
                };
                await _persistenceUnitOfWork.Hotel.AddAsync(newHotel);
                await _persistenceUnitOfWork.SaveChangesAsync();
                #endregion

                #region saving images
                if (command.Images != null && command.Images.Any())
                {
                    string directory = "/Images/Hotel";
                    string referenceName = typeof(Domain.Persistence.Entities.Hotel).Name;

                    var imgResponse = await _imageService.CreateImagesAsync(command.Images, directory, newHotel.Id, referenceName);

                    if (imgResponse.Succeeded)
                    {
                        await _persistenceUnitOfWork.CommitTransactionAsync();
                        return Response<int>.Success(newHotel.Id, "Successfully created hotel.");
                    }
                    _validationError.Add(imgResponse.Message);
                }
                #endregion

                await _persistenceUnitOfWork.RollbackTransactionAsync();
                return Response<int>.Fail("Failed to create hotel\n " + _validationError);

            }
            catch (Exception ex)
            {
                await _persistenceUnitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return Response<int>.Fail(_validationError);
            }
        }
    }
}
