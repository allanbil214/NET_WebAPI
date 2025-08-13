using AutoMapper;
using Microsoft.AspNetCore.Identity;
using YoutubeAPI.Models.DTOs;
using YoutubeAPI.Models.Entities;
using YoutubeAPI.Services.Interfaces;

namespace YoutubeAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<LoginResponseDTO> RegisterAsync(UserRegisterDTO registerDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var existingUsername = await _userManager.FindByNameAsync(registerDto.Username);
            if (existingUsername != null)
            {
                throw new InvalidOperationException("Username is already taken.");
            }

            var user = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"User creation failed: {errors}");
            }

            await _userManager.AddToRoleAsync(user, "User");

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtService.GenerateToken(user, roles);

            return new LoginResponseDTO
            {
                Token = token,
                User = new UserResponseDTO
                {
                    Id = user.Id,
                    Username = user.UserName!,
                    Email = user.Email!,
                    Roles = roles
                }
            };
        }

        public async Task<LoginResponseDTO> LoginAsync(UserLoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || user.IsDeleted)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtService.GenerateToken(user, roles);

            return new LoginResponseDTO
            {
                Token = token,
                User = new UserResponseDTO
                {
                    Id = user.Id,
                    Username = user.UserName!,
                    Email = user.Email!,
                    Roles = roles
                }
            };
        }
    }
}