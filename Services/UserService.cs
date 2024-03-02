using WebApiCar.Infrastructure.Entities;
using WebApiCar.Infrastructure.Repositories;
using WebApiCar.Models.Dtos;

namespace WebApiCar.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterUser(RegisterUserDto dto)
        {
            var user = _userRepository.GetUserAsync(dto.Mail);

            if (user != null)
                throw new Exception("User already exists");

            var userToAdd = new User 
            { 
                Mail = dto.Mail ,
                HashedPassword = dto.Password
            };
        }
    }
}
