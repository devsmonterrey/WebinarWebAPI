namespace WebApiToken.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class webinarModel : DbContext
    {
        public webinarModel()
            : base("name=webinarModel")
        {
        }

        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Tokens> Tokens { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasMany(e => e.Tokens)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);
        }
    }
}
