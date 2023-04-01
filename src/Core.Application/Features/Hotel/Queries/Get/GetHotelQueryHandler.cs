using Core.Application.Contracts.Common;
using Core.Application.Contracts.Features.Hotel.Queries.Get;
using Core.Application.Contracts.Features.Hotel.Queries.GetAll;
using Core.Application.Contracts.Features.Room.Queries.Get;
using Core.Application.Contracts.Features.RoomType.Queries.Get;
using Core.Application.Extensions;
using Core.Application.Features.Hotel.Queries.GetAll;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Hotel.Queries.Get
{
    public class GetHotelQueryHandler : IRequestHandler<GetHotelQuery, Response<GetHotelQueryVm>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<GetHotelQueryHandler> _logger;
        private List<String> _validationError;

        public GetHotelQueryHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<GetHotelQueryHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<GetHotelQueryVm>> Handle(GetHotelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                #region DB call
                var hotelData = _persistenceUnitOfWork.Hotel.AsNoTracking()
                    .Where(x => x.Id == request.Id)
                    .Include(i => i.Rooms).ThenInclude(ii => ii.Features)
                    .Include(i => i.Rooms).ThenInclude(ii => ii.RoomType)
                    //.Include(i => i.Features)
                    .Select(s => new
                    {
                        s.Id,
                        s.Name,
                        s.Description,
                        s.Address,
                        s.State,
                        s.City,
                        s.Country,
                        s.Email,
                        s.Phone,
                        s.Rating,
                        s.Rooms,
                        s.Features,
                        s.Review
                    }).FirstOrDefault();
                #endregion

                if (hotelData == null)
                    return Response<GetHotelQueryVm>.Fail("No data found");

                #region image data
                string refName = typeof(Domain.Persistence.Entities.Hotel).Name;
                var imageRepo = _persistenceUnitOfWork.Image.AsNoTracking()
                    .Where(x => x.ReferenceId == hotelData.Id && x.ReferenceName == refName)
                    .Select(s => new
                    {
                        s.Id,
                        s.ImageUrl
                    });
                #endregion

                #region response
                var response = new GetHotelQueryVm
                {
                    Id = hotelData.Id,
                    Name = hotelData.Name,
                    Address = hotelData.Address,
                    City = hotelData.City,
                    State = hotelData.State,
                    Country = hotelData.Country,
                    Description = hotelData.Description,
                    Phone = hotelData.Phone,
                    Email = hotelData.Email,
                    Rating = hotelData.Rating,
                    Images = imageRepo.Select(img => new ImageVm
                    {
                        Id = img.Id,
                        ImageUrl = img.ImageUrl
                    }).ToList(),
                    Features = hotelData.Features.Select(f => new GetHotelFeatureQueryVm
                    {
                        Id = f.Id,
                        Name = f.Name,
                        Description = f.Description,
                    }).ToList(),
                    Rooms = hotelData.Rooms.Select(r => new GetRoomQueryVm
                    {
                        Id = r.Id,
                        Price = r.Price,
                        RoomNumber = r.RoomNumber,
                        Description = r.Description,
                        Capacity = r.Capacity,
                        RoomType = new VmSelectList
                        {
                            Id = r.RoomTypeId,
                            Name = r.RoomType?.Name
                        },
                        Hotel = new VmSelectList
                        {
                            Id = r.HotelId,
                            Name = hotelData.Name
                        },
                        Features = r.Features.Select(s => new GetRoomFeatureVm
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Description = s.Description
                        }).ToList()
                    }).ToList()
                };
                #endregion

                return Response<GetHotelQueryVm>.Success(response, "Successfully retrieved hotel info");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return Response<GetHotelQueryVm>.Fail(_validationError);
            }
        }
    }
}
