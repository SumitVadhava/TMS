using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Common.Enums;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }

        public DbSet<Tickets> Tickets { get; set; }

        public DbSet<TicketComments> TicketComments { get; set; }

        public DbSet<TicketStatusLogs> TicketStatusLogs { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(u =>
            {
                u.ToTable("Users");

                u.HasKey(x => x.Id);

                u.Property(x => x.Email)
                    .IsRequired();

                u.HasIndex(x => x.Email)
                    .IsUnique();

                u.Property(x => x.Created_At)
                    .IsRequired();

            });

            modelBuilder.Entity<Roles>(u =>
            {
                u.ToTable("Roles");

                u.HasKey(x => x.Id);

                u.Property(x => x.Name)
                    .IsRequired();

                u.HasIndex(x => x.Name)
                    .IsUnique();
            });

        modelBuilder.Entity<Tickets>(u =>
            {
                u.ToTable("Tickets");

                u.HasKey(x => x.Id);

                u.Property(x => x.Title)
                    .IsRequired()
                    .HasColumnType("VARCHAR(MAX)");

                u.Property(x => x.Description)
                    .IsRequired();

                u.Property(x => x.Status)
                    .IsRequired();          
                
                u.Property(x => x.Priority)
                    .IsRequired();

                u.Property(x => x.Created_By)
                 .IsRequired();

                
                u.HasOne(t => t.CreatedByUser)
                 .WithMany(u => u.Tickets)
                 .HasForeignKey(t => t.Created_By)
                 .OnDelete(DeleteBehavior.Cascade);

                
                u.HasOne(t => t.AssignedToUser)
                 .WithMany()
                 .HasForeignKey(t => t.Assigned_To)
                 .OnDelete(DeleteBehavior.NoAction);

        });

        modelBuilder.Entity<TicketComments>(u =>
            {
                u.ToTable("TicketComments");

                u.HasKey(x => x.Id);

                u.Property(x => x.Ticket_id)
                   .IsRequired();

               
                u.HasOne(c => c.Tickets)
                 .WithMany()
                 .HasForeignKey(c => c.Ticket_id)
                 .OnDelete(DeleteBehavior.Cascade);

                u.HasOne(c => c.Users)
                 .WithMany(t => t.TicketComments)
                 .HasForeignKey(c => c.User_id)
                 .OnDelete(DeleteBehavior.NoAction);

                u.Property(x => x.Comment)
                    .IsRequired();

            });


        modelBuilder.Entity<TicketStatusLogs>(u =>
            {
                u.ToTable("TicketStatusLogs");

                u.HasKey(x => x.Id);

                u.Property(x => x.Ticket_id)
                    .IsRequired();

                u.HasOne(l => l.Tickets)
                 .WithMany()
                 .HasForeignKey(l => l.Ticket_id)
                 .OnDelete(DeleteBehavior.Cascade);

                u.HasOne(l => l.Users)
                 .WithMany(t => t.TicketStatusLogs)
                 .HasForeignKey(l => l.Changed_By)
                 .OnDelete(DeleteBehavior.NoAction);

                u.Property(x => x.Old_Status)
                    .IsRequired();

                u.Property(x => x.New_Status)
                    .IsRequired();

                u.Property(x => x.Changed_By)
                    .IsRequired();
           

            });




        }
    }
}
