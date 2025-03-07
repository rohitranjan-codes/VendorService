using AutoMapper;
using VendorService.Application.DTOs;
using VendorService.Application.Features.Commands;
using VendorService.Domain.Entities;

namespace VendorService.Application.Mappings
{
    public class VendorMappingProfile : Profile
    {
        public VendorMappingProfile()
        {

            CreateMap<RequestVendorDto, CreateVendorCommand>()
           .ForMember(dest => dest.RequestVendorDto, opt => opt.MapFrom(src => src));

            CreateMap<RequestVendorDto, UpdateVendorCommand>()
                .ForMember(dest => dest.RequestVendorDto, opt => opt.MapFrom(src => src));
            CreateMap<Vendor, VendorDto>();
        }
    }
}
