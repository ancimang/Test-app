namespace AnaVeljovicTest.Migrations
{
    using AnaVeljovicTest.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AnaVeljovicTest.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AnaVeljovicTest.Models.ApplicationDbContext context)
        {
            context.Agenti.AddOrUpdate(x =>x.Id,

                new Agent() { Id = 1, ImeIprezime = "Pera Peric", Licenca = "Lic1", GodinaRodjenja = 1960, BrojNekretnina = 15 },
                new Agent() { Id = 2, ImeIprezime = "Mika Mikic", Licenca = "Lic2", GodinaRodjenja = 1970, BrojNekretnina = 10 },
                new Agent() { Id = 3, ImeIprezime = "Zika Zikic", Licenca = "Lic3", GodinaRodjenja = 1980, BrojNekretnina = 5 }
                );

            context.Nekretnine.AddOrUpdate(x => x.Id,

                new Nekretnina() { Id = 1, Mesto = "Novi Sad", Oznaka = "Nek01", GodinaIzgradnje = 1974, Kvadratura = 50, Cena = 40000, AgentId = 1 },
                new Nekretnina() { Id = 2, Mesto = "Beograd", Oznaka = "Nek02", GodinaIzgradnje = 1990, Kvadratura = 60, Cena = 50000, AgentId = 2 },
                new Nekretnina() { Id = 3, Mesto = "Subotica", Oznaka = "Nek03", GodinaIzgradnje = 1995, Kvadratura = 55, Cena = 45000, AgentId = 3 },
                new Nekretnina() { Id = 4, Mesto = "Zrenjanin", Oznaka = "Nek04", GodinaIzgradnje = 2010, Kvadratura = 70, Cena = 60000, AgentId = 1 }
                );
        }
    }
}
