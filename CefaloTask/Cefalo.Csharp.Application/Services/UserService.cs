using Cefalo.Csharp.Core.Entities;
using Cefalo.Csharp.Core.Services;
using Cefalo.Csharp.Core.Repositories;

namespace Cefalo.Csharp.Application.Services;

public class UserService(
    IUserRepository userRepository)
    : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public Task<User?> GetUserByIdAsync(int id) => _userRepository.GetByIdAsync(id);

    public Task<User?> GetUserByEmailAsync(string email) => _userRepository.GetByEmailAsync(email);

    public Task<IEnumerable<User>> GetAllUsersAsync() => _userRepository.GetAllAsync();

    public async Task<User> CreateUserAsync(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Name))
        {
            throw new ArgumentException("User name is required");
        }

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            throw new ArgumentException("User email is required");
        }

        var existingUser = await _userRepository.GetByEmailAsync(user.Email);
        if (existingUser != null)
        {
            throw new ArgumentException("A user with this email already exists");
        }

        user.Deleted = false;
        return await _userRepository.AddAsync(user);
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _ = await _userRepository.GetByIdAsync(user.Id) ?? throw new ArgumentException($"User with ID {user.Id} not found");

        if (string.IsNullOrWhiteSpace(user.Name))
        {
            throw new ArgumentException("User name is required");
        }

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            throw new ArgumentException("User email is required");
        }

        var userWithEmail = await _userRepository.GetByEmailAsync(user.Email);
        if (userWithEmail != null && userWithEmail.Id != user.Id)
        {
            throw new ArgumentException("A user with this email already exists");
        }

        user.Deleted = false;
        return await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        _ = await _userRepository.GetByIdAsync(id) ?? throw new ArgumentException($"User with ID {id} not found");
        await _userRepository.DeleteAsync(id);
    }
}