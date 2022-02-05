using AutoMapper;
using SimpleApi.Application.Services.Pet.Dto;
using SimpleApi.Domain.Pet.Dto;

namespace SimpleApi.Web.Api.Models.Pet.MapperProfile;

public class PetProfile : Profile
{
    public PetProfile()
    {
        CreateMap<CreatePetModel, CreatePetRequestDto>();
        CreateMap<UpdatePetModel, UpdatePetRequestDto>();
        CreateMap<BindPetToUserModel, BindPetToUserDto>();
    }
}