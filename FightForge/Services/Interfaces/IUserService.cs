namespace FightForge.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginUserDto dto);
        Task RoleChange(int userId);
    }
}
