using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<GymSession> GymSessions { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.UserType)
                .HasConversion<int>();

            modelBuilder.Entity<Trainer>()
                .Property(u => u.TrainerSpeciality)
                .HasConversion<int>();

            modelBuilder.Entity<GymSession>()
                .Property(u => u.SessionType)
                .HasConversion<int>();

            // Configuración del discriminador de tipo User
            modelBuilder.Entity<User>()
                .HasDiscriminator<UserType>("UserType")
                .HasValue<Client>(UserType.Client)
                .HasValue<Trainer>(UserType.Trainer)
                .HasValue<Admin>(UserType.Admin)
                .HasValue<SuperAdmin>(UserType.SuperAdmin);

            // Datos de semilla para usuarios
            modelBuilder.Entity<SuperAdmin>().HasData(new SuperAdmin
            {
                Id = 1,
                Name = "SuperAdmin",
                Surname = "Gym",
                Email = "superadmin@gmail.com",
                Password = "superadmin",
                Phone = 123456789
            });

            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                Id = 2,
                Name = "Admin",
                Surname = "Gym",
                Email = "admin@gmail.com",
                Password = "admin",
                Phone = 123456789
            });

            modelBuilder.Entity<Client>().HasData(new Client
            {
                Id = 3,
                Name = "Client",
                Surname = "Gym",
                Email = "client@gmail.com",
                Password = "client",
                Phone = 123456789
            });

            modelBuilder.Entity<Trainer>().HasData(new Trainer
            {
                Id = 4,
                Name = "Trainer",
                Surname = "Gym",
                Email = "trainer@gmail.com",
                Password = "trainer",
                Phone = 123456789,
                TrainerSpeciality = Speciality.Hiit
            });

            // Datos de semilla para Routines y GymSessions
            modelBuilder.Entity<Routine>().HasData(CreateRoutineSeed());
            modelBuilder.Entity<GymSession>().HasData(CreateGymSessionSeed());

            base.OnModelCreating(modelBuilder);
        }

        private GymSession[] CreateGymSessionSeed()
        {
            return new GymSession[]
            {
            new GymSession
            {
                Id = 2,
                SessionType = SessionType.BoxingSession,
                TrainerId = 4,
                RoutineId = 2,  // Cambiar para evitar conflicto de IDs
                SessionDate = new DateTime(2025, 08, 12, 10, 0, 0)
            },
            };
        }

        private Routine[] CreateRoutineSeed()
        {
            return new Routine[]
            {
            new Routine
            {
                Id = 2, // Cambiar para evitar conflicto de IDs
                Name = "Full Body Beginner",
                TrainerId = 4
                // El campo GymSessionId se elimina, ya que la relación es 1:N
            },
            };
        }
    }
}
