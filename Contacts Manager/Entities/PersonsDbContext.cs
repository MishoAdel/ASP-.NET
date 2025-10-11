using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PersonsDbContext :DbContext
    {
        public PersonsDbContext(DbContextOptions options) : base(options)
        {
        }

        

        public DbSet<Person> Persons { get; set; }

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().ToTable("Countries");

            modelBuilder.Entity<Person>().ToTable("Persons");

            string countries = System.IO.File.ReadAllText("countries.json");
            List<Country> countriesList = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countries);
            foreach(Country country in countriesList)
                modelBuilder.Entity<Country>().HasData(country);

            string persons = System.IO.File.ReadAllText("persons.json");
            List<Person> personsList = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(persons);
            foreach (Person person in personsList)
                modelBuilder.Entity<Person>().HasData(person);


        }
    }
}
