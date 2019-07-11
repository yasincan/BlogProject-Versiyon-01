namespace BlogProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class blogContext : DbContext
    {
        public blogContext()
            : base("name=blogConnection")
        {
        }

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Picture> Picture { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Article)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);
        }
    }
}
