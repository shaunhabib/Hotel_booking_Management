using Core.Domain.Shared.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Hotel.Commands.Create
{
    public class CreateHotelCommand : IRequest<Response<int>>
    {
        [FromJson]
        public CreateHotelCommandVm Data { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
