using Core.Application.Contracts.Features.Hotel.Commands.Update;
using Core.Application.Contracts.Features.Hotel.Queries.GetAll;
using Core.Application.Extensions;
using Core.Application.Features.Hotel.Commands.Update;
using Core.Domain.Persistence.Contracts;
using Core.Domain.Shared.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Hotel.Queries.GetAll
{
    public class GetAllHotelQueryHandler : IRequestHandler<GetAllHotelQuery, Response<IReadOnlyList<GetAllHotelQueryVm>>>
    {
        #region ctor
        private readonly IPersistenceUnitOfWork _persistenceUnitOfWork;
        private readonly ILogger<GetAllHotelQueryHandler> _logger;
        private List<String> _validationError;

        public GetAllHotelQueryHandler(IPersistenceUnitOfWork persistenceUnitOfWork, ILogger<GetAllHotelQueryHandler> logger)
        {
            _persistenceUnitOfWork = persistenceUnitOfWork;
            _logger = logger;
            _validationError = new List<string>();
        }
        #endregion
        public async Task<Response<IReadOnlyList<GetAllHotelQueryVm>>> Handle(GetAllHotelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var hotelData = _persistenceUnitOfWork.Hotel.AsNoTracking()
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
                        s.Rating
                    }).ToList();

                #region Searching implementation
                if (!string.IsNullOrWhiteSpace(request.SearchValue))
                {
                    hotelData = hotelData.Where(x => 
                       x.Name.ToLower().Contains(request.SearchValue.ToLower())
                    || x.Address.ToLower().Contains(request.SearchValue.ToLower())
                    || x.City.ToLower().Contains(request.SearchValue.ToLower())
                    || x.State.ToLower().Contains(request.SearchValue.ToLower())
                    || x.Country.ToLower().Contains(request.SearchValue.ToLower())
                    ).ToList();
                }
                #endregion

                #region Filter implementation
                if (request.HotelId != null && request.HotelId > 0)
                {
                    hotelData = hotelData.Where(x => x.Id == request.HotelId).ToList();
                }
                if (request.MinRating != null)
                {
                    hotelData = hotelData.Where(x => x.Rating >= request.MinRating).ToList();
                }
                if (request.MaxRating != null)
                {
                    hotelData = hotelData.Where(x => x.Rating <= request.MaxRating).ToList();
                }
                #endregion

                var response = hotelData.Select(s => new GetAllHotelQueryVm
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    City = s.City,
                    State = s.State,
                    Country = s.Country,
                    Description = s.Description,
                    Phone = s.Phone,
                    Email = s.Email,
                    Rating = s.Rating,
                }).ToList();

                return Response<IReadOnlyList<GetAllHotelQueryVm>>.Success(response, "Successfully retrieved hotel list");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return Response<IReadOnlyList<GetAllHotelQueryVm>>.Fail(_validationError);
            }
        }
    }
}
