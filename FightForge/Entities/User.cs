﻿namespace FightForge.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public string PasswordHash { get; set; } = default!;

        public virtual Role? Role { get; set; }
        public int RoleId { get; set; }
    }
}
