namespace FightForge.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher<User> _hasher;
        private readonly GymDbContext _context;
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserService(IPasswordHasher<User> hasher, GymDbContext context, IMapper mapper, AuthenticationSettings authenticationSettings)
        {
            _hasher = hasher;
            _context = context;
            _mapper = mapper;
            _authenticationSettings = authenticationSettings;
        }
        public string GenerateJwt(LoginUserDto dto)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
            };

            if (user.DateOfBirth != null)
            {
                claims.Add(
                    new Claim("DateOfBirth", user.DateOfBirth.ToString())
                    );
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public async Task RegisterUser(RegisterUserDto dto)
        {
            var user = _mapper.Map<User>(dto);

            var hashedPassword = _hasher.HashPassword(user, dto.Password);
            user.PasswordHash = hashedPassword;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task RoleChange(int userId)
        {
            var user = _context
                .Users
                .FirstOrDefault(x => x.Id ==  userId);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            if (user.RoleId == 3)
            {
                throw new ForbidException("This user is already an admin");
            }

            user.RoleId = 3;
            await _context.SaveChangesAsync();
        }
    }
}
