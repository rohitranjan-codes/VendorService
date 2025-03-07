using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorService.Application.DTOs;

namespace VendorService.Application.Features.Queries
{
    public class GetVendorByIdQuery(Guid id) : IRequest<VendorDto?>
    {
        public Guid Id { get; } = id;
    }
}
