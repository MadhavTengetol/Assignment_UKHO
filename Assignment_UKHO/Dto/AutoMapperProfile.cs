using Assignment_UKHO.Data;
using AutoMapper;

namespace Assignment_UKHO.Dto
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Batch, BatchDto>().ReverseMap();
            CreateMap<Batch, BatchResponseDto>().ReverseMap();
            CreateMap<BatchDto, BatchResponseDto>().ReverseMap();
        }
    }
}
