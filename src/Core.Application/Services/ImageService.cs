using Core.Application.Features.Hotel.Commands.Create;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Shared.Contacts;
using Core.Domain.Shared.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Core.Application.Services
{
    public class ImageService
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly IFileManagementRepository _fileManagementRepository;
        private readonly ILogger<ImageService> _logger;
        private List<String> _validationError;

        public ImageService(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<ImageService> logger, IFileManagementRepository fileManagementRepository)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
            _fileManagementRepository = fileManagementRepository;
        }
        #endregion

        public async Task<Response<bool>> CreateImagesAsync(List<IFormFile> imageFiles, string directory, int refId, string refName)
        {
            if (imageFiles != null && imageFiles.Any())
            {
                var newImages = new List<Domain.Persistence.Entities.Image>();
                foreach (var imgFile in imageFiles)
                {
                    var imgUrl = await _fileManagementRepository.UploadFile(imgFile, directory);

                    if (imgUrl.Succeeded)
                    {
                        var img = new Domain.Persistence.Entities.Image
                        {
                            ReferenceId = refId,
                            ReferenceName = refName,
                            ImageUrl = imgUrl.Data
                        };
                        newImages.Add(img);
                    }
                    else
                    {
                        _logger.LogError(imgUrl.Message);
                        _validationError.Add(imgUrl.Message);
                    }
                }
                if (newImages.Any())
                {
                    await _persistenceUnitOfWork.Image.AddAsync(newImages);
                    await _persistenceUnitOfWork.SaveChangesAsync();
                    return Response<bool>.Success(true, "Successfully created image.");
                }
            }
            return Response<bool>.Fail(_validationError);
        }
    }
}
