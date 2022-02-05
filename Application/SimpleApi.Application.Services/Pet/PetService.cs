using AutoMapper;
using SimpleApi.Application.Services.Pet.Dto;
using SimpleApi.Data.Persistent.Repositories;
using SimpleApi.Domain.Pet.Dto;
using IPetDomainService = SimpleApi.Domain.Pet.IPetService;

namespace SimpleApi.Application.Services.Pet;

public class PetService : IPetService
{
    private readonly IPetDomainService _petDomainService;
    private readonly IPetRepository _petRepository;
    private readonly IMapper _mapper;

    public PetService(
        IPetDomainService petDomainService, 
        IPetRepository petRepository, 
        IMapper mapper)
    {
        _petDomainService = petDomainService;
        _petRepository = petRepository;
        _mapper = mapper;
    }

    public async Task<bool> BindPetToUserAsync(BindPetToUserDto bindPetToUserDto)
    {
        var result = await _petDomainService.BindPetToUserAsync(bindPetToUserDto.PetId, bindPetToUserDto.UserId);

        return result.IsSuccess;
    }

    public async Task<PetSummaryResponseDto> CreatePet(CreatePetRequestDto createPetRequestDto)
    {
        return await _petDomainService.CreatePet(createPetRequestDto);
    }

    public async Task<PetSummaryResponseDto> UpdatePet(UpdatePetRequestDto updatePetRequestDto)
    {
        return await _petDomainService.UpdatePet(updatePetRequestDto);
    }

    public async Task<PetSummaryResponseDto> GetPetById(Guid id)
    {
        var pet = await _petRepository.GetByIdAsync(id);

        return _mapper.Map<PetSummaryResponseDto>(pet);
    }
}