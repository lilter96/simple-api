using AutoMapper;
using SimpleApi.Domain.Pet.Dto;

namespace SimpleApi.Application.Services.Pet.MapperProfile;

public class PetProfile : Profile
{
    public PetProfile()
    {
        CreateMap<Domain.Pet.Pet, PetSummaryResponseDto>();
    }
}