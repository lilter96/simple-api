using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleApi.Application.Services.Pet;
using SimpleApi.Application.Services.Pet.Dto;
using SimpleApi.Domain.Pet.Dto;
using SimpleApi.Web.Api.Models.Pet;

namespace SimpleApi.Web.Api.Controllers;

[ApiController]
[Route("pet")]
public class PetController : ControllerBase
{
    private readonly IPetService _petService;

    private readonly IMapper _mapper;

    public PetController(
        IPetService petService,
        IMapper mapper)
    {
        _petService = petService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetPet(Guid id)
    {
        var pet = await _petService.GetPetById(id);

        return pet == null ? NotFound($"The pet with the ID {id} was not found.") : Ok(pet);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePet(CreatePetModel createPetModel)
    {
        var createPetRequestDto = _mapper.Map<CreatePetRequestDto>(createPetModel);

        var createdPetSummary = await _petService.CreatePet(createPetRequestDto);

        return createdPetSummary == null
            ? StatusCode(500, "Something went wrong in the server while creating a new pet.")
            : Ok(createdPetSummary);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePet(UpdatePetModel updatePetModel)
    {
        var updatePetRequestDto = _mapper.Map<UpdatePetRequestDto>(updatePetModel);

        var updatedPetSummary = await _petService.UpdatePet(updatePetRequestDto);

        return updatedPetSummary == null 
            ? StatusCode(500, "Something went wrong in the server while updating the pet.")
            : Ok(updatedPetSummary);
    }

    [HttpPut]
    [Route("bind")]
    public async Task<IActionResult> BindPetToUser(BindPetToUserModel bindPetToUserModel)
    {
        var bindPetToUserDto = _mapper.Map<BindPetToUserDto>(bindPetToUserModel);

        var result = await _petService.BindPetToUserAsync(bindPetToUserDto);

        return result
            ? Ok()
            : Problem($"Pet with ID {bindPetToUserModel.PetId} was not bind to the user with {bindPetToUserModel.UserId} ");
    }
}