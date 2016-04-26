using Kino.App_Start;
using Kino.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Kino.DAL
{
    public class CinemaContext : IdentityDbContext<ApplicationUser>
    {
        public CinemaContext() : base("LocalMySqlServer")
        {

        }

        public static CinemaContext Create()
        {
            return new CinemaContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Seans> Seans { get; set; }
        public DbSet<Film> Film { get; set; }
        public DbSet<Kino.Models.Kino> Kino { get; set; }
        public DbSet<RezerwacjaPrzyjeta> RezerwacjaPrzyjeta { get; set; }
        public DbSet<RezerwacjaZlozona> RezerwacjaZlozona { get; set; }
        public DbSet<Miejsce> Miejsce { get; set; }
        public DbSet<Sala> Sala { get; set; }
        // public DbSet<User> Users { get; set; }
    }
}