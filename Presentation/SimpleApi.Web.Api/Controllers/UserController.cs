using Microsoft.AspNetCore.Mvc;
using SimpleApi.Data.Persistent.Repositories;
using SimpleApi.Domain.User.Dto;
using IUserDomainService = SimpleApi.Domain.User.IUserService;

namespace SimpleApi.Web.Api.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;

    private readonly IUserDomainService _userDomainService;

    public UserController(
        IUserRepository repository, 
        IUserDomainService userDomainService)
    {
        _repository = repository;
        _userDomainService = userDomainService;
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var user = await _userDomainService.GetUserWithPets(id);

        if (user == null)
        {
            return NotFound($"The user with ID {id} not found!");
        }

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _repository.GetAllAsync();

        if (users == null)
        {
            return NotFound("There are no users!");
        }

        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
    {
        var user = await _repository.CreateAsync(createUserDto);

        return user == null ? StatusCode(500, $"Cannot create the user {createUserDto}") : StatusCode(201, user);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
    {
        var user = await _repository.UpdateAsync(updateUserDto);

        return user == null ? StatusCode(400, $"Cannot update the user {updateUserDto}") : StatusCode(200, user);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var result = await _repository.DeleteByIdAsync(id);

        return result ? Ok() : NotFound($"Cannot delete user with ID {id}");
    }

    [HttpGet]
    [Route("{id:guid}/pets")]
    public async Task<IActionResult> GetUserPets(Guid id)
    {
        var result = await _repository.GetUserPets(id);

        return Ok(result);
    }
}