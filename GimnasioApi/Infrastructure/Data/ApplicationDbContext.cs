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
        public DbSet<ClientGymSession> ClientGymSessions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.UserType)
                .HasConversion<int>();

            modelBuilder.Entity<Trainer>()
                .Property(u => u.TrainerSpeciality)
                .HasConversion<int>();

            modelBuilder.Entity<GymSession>()
                .Property(u => u.SessionType)
                .HasConversion<int>();

            modelBuilder.Entity<User>()
                .HasDiscriminator<UserType>("UserType")
                .HasValue<Client>(UserType.Client)
                .HasValue<Trainer>(UserType.Trainer)
                .HasValue<Admin>(UserType.Admin)
                .HasValue<SuperAdmin>(UserType.SuperAdmin); // Se ha eliminado el punto y coma extra

            // Configuración de la relación 1 a muchos entre Routine y GymSession
            modelBuilder.Entity<GymSession>()
                .HasOne(gs => gs.Routine)
                .WithMany(r => r.GymSessions)
                .HasForeignKey(gs => gs.RoutineId)
                .OnDelete(DeleteBehavior.Restrict); // Evita borrado en cascada

            modelBuilder.Entity<ClientGymSession>()
                .HasOne(cgs => cgs.Client)
                .WithMany(c => c.ClientGymSessions)
                .HasForeignKey(cgs => cgs.ClientId);

            modelBuilder.Entity<ClientGymSession>()
                .HasOne(cgs => cgs.GymSession)
                .WithMany(gs => gs.ClientGymSessions)
                .HasForeignKey(cgs => cgs.GymSessionId);
        }
    }

}
