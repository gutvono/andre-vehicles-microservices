using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AndreVehicles.Data
{
    public class AndreVehiclesContext : DbContext
    {
        public AndreVehiclesContext (DbContextOptions<AndreVehiclesContext> options)
            : base(options)
        {
        }

        public DbSet<AcceptTerms> AcceptTerms { get; set; } = default!;
        public DbSet<Address> Address { get; set; } = default!;
        public DbSet<Bank> Bank { get; set; } = default!;
        public DbSet<Car> Car { get; set; } = default!;
        public DbSet<Card> Card { get; set; } = default!;
        public DbSet<CarJob> CarJob { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<CNH> CNH { get; set; } = default!;
        public DbSet<Conductor> Conductor { get; set; } = default!;
        public DbSet<Customer> Customer { get; set; } = default!;
        public DbSet<Dependent> Dependent { get; set; } = default!;
        public DbSet<Employee> Employee { get; set; } = default!;
        public DbSet<FinancialPending> FinancialPending { get; set; } = default!;
        public DbSet<Financing> Financing { get; set; } = default!;
        public DbSet<Insurance> Insurance { get; set; } = default!;
        public DbSet<Job> Job { get; set; } = default!;
        public DbSet<Payment> Payment { get; set; } = default!;
        public DbSet<Pix> Pix { get; set; } = default!;
        public DbSet<PixType> PixType { get; set; } = default!;
        public DbSet<Purchase> Purchase { get; set; } = default!;
        public DbSet<Role> Role { get; set; } = default!;
        public DbSet<Sale> Sale { get; set; } = default!;
        public DbSet<Term> Term { get; set; } = default!;
        public DbSet<Ticket> Ticket { get; set; } = default!;
    }
}
