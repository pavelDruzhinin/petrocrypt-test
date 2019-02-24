using System.Data.Entity;

namespace VideoApp.Models
{
    public class VideoContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Video> Video { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>()
                .HasKey(x => x.AuthorID);

            modelBuilder.Entity<Genre>()
                .HasKey(x => x.GenreID);

            modelBuilder.Entity<Author>()
                .HasMany(x => x.Video)
                .WithRequired(v => v.Author)
                .HasForeignKey(v => v.AuthorID);

            modelBuilder.Entity<Genre>()
                .HasMany(x => x.Video)
                .WithRequired(v => v.Genre)
                .HasForeignKey(v => v.GenreID);

            modelBuilder.Entity<Video>()
                .HasKey(x => x.VideoID);
        }
    }
}