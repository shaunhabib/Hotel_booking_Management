﻿using Core.Domain.Shared.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Features.Hotel.Commands.Delete
{
    public class DeleteHotelCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }
}
