using Microsoft.AspNetCore.Mvc;
using SimpleApi.Application.Services.Pet;
using SimpleApi.Application.Services.Pet.Dto;
using SimpleApi.Web.Api.Models.Pet;

namespace SimpleApi.Web.Api.Controllers;

[ApiController]
[Route("pet")]
public class PetController : ControllerBase
{
    private readonly IPetService _petService;

    public PetController(IPetService petService)
    {
        _petService = petService;
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetPet(Guid id)
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetPets([FromQuery] int skip, [FromQuery] int take)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePet(CreatePetModel createPetModel)
    {
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePet(UpdatePetModel updatePetModel)
    {
        return Ok();
    }

    [HttpPut]
    [Route("bind")]
    public async Task<IActionResult> BindPetToUser(BindPetToUserModel bindPetToUserModel)
    {
        var bindPetToUserDto = new BindPetToUserDto
        {
            PetId = bindPetToUserModel.PetId,
            UserId = bindPetToUserModel.UserId
        };

        var result = await _petService.BindPetToUserAsync(bindPetToUserDto);


        return result
            ? Ok()
            : Problem($"Pet with ID {bindPetToUserModel.PetId} was not bind to the user with {bindPetToUserModel.UserId} ");
    }
}