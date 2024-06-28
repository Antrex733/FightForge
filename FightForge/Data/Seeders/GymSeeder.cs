﻿namespace FightForge.Data.Seeders
{
    public class GymSeeder : IGymSeeder
    {
        private readonly GymDbContext _dbContext;

        public GymSeeder(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.Gyms.Any())
                {
                    var gyms = GetGyms();
                    _dbContext.Gyms.AddRange(gyms);
                    await _dbContext.SaveChangesAsync();
                }

            }
        }

        private IEnumerable<Gym> GetGyms()
        {
            var gyms = new List<Gym>()
            {
                new Gym()
                {
                    Name = "Warszawskie Centrum Atletyki",
                    Description =
                        "Klub sztuk walki i siłownia w jednym. Tutaj każdy znajdzie coś dla siebie, zarówno osoba rozpoczynająca swoją przygodę z treningami jak i profesjonalny zawodnik, szukający kompleksowej bazy umożliwiającej mu przygotowanie do startów.",
                    ContactEmail = "contact@wca.com",
                    ContactNumber = "111222333",
                    Sports = new List<Sport>()
                    {
                        new Sport()
                        {
                            Name = "MMA",
                            Difficulty = "intermediate",
                        },

                        new Sport()
                        {
                            Name = "Grappling",
                            Difficulty = "beginner",
                        },
                    },
                    Address = new Address()
                    {
                        City = "Warszawa",
                        Street = "Zofii Roesler 2",
                        PostalCode = "01-991"
                    }
                },
                new Gym()
                {
                    Name = "Aligatores Fight Club",
                    Description =
                        "Walka czyni nas bardziej ludźmi. Krótkotrwały stres o wysokiej amplitudzie jest dobry dla naszego ciała. W odróżnieniu od długotrwałego “przejmowania się” pracą i niespłaconym kredytem, czy tym, co kto sobie o nas kiedyś pomyśli.",
                    ContactEmail = "contact@aligator.com",
                    ContactNumber = "222333444",
                    Sports = new List<Sport>()
                    {
                        new Sport()
                        {
                            Name = "Kickboxing",
                            Difficulty = "intro",
                        },

                        new Sport()
                        {
                            Name = "Muay thai",
                            Difficulty = "beginner",
                        },
                    },
                    Address = new Address()
                    {
                        City = "Warszawa",
                        Street = "Nowogrodzka 50/54",
                        PostalCode = "00-695"
                    }
                },
                new Gym()
                {
                    Name = "Academia Gorila",
                    Description =
                        "Gorila po portugalsku znaczy goryl. Silny i olbrzymi, ale jednocześnie zwinny, uśmiechnięty i pokojowo nastawiony do świata – to nas idealnie podsumowuje i określa.",
                    ContactEmail = "contact@gorila.com",
                    ContactNumber = "333444555",
                    Sports = new List<Sport>()
                    {
                        new Sport()
                        {
                            Name = "Boks",
                            Difficulty = "fighters",
                        },

                        new Sport()
                        {
                            Name = "BJJ",
                            Difficulty = "beginner",
                        },
                    },
                    Address = new Address()
                    {
                        City = "Bielsko-Biała",
                        Street = "Komorowicka 43",
                        PostalCode = "43-300"
                    }
                },
            };
            return gyms;
        }
    }
}
