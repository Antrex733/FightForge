﻿namespace FightForge.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; } 
        public string? PostalCode { get; set; }

        public Gym Gym { get; set; } = default!;
    }
}
